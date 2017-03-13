using KeketteTravel.Presentation.PlatformIntegration;
using MvvmCross.Core.ViewModels;
using Plugin.Messaging;
using static Amp.MvvmCross.CommandFactory;

namespace KeketteTravel.Presentation.ViewModels
{
    public class ActivityDetailsViewModel : MvxViewModel
    {
        private readonly IShareService _shareService;
        private readonly IDataService _dataService;

        public ActivityDetailsViewModel(
            IShareService shareService,
            IDataService dataService)
        {
            _dataService = dataService;
            _shareService = shareService;
        }

        public void Init(string countryId, string activityId)
        {
            Activity = _dataService.GetActivity(countryId, activityId);
        }

        private Activity _activity;
        public Activity Activity
        {
            get
            {
                return _activity;
            }
            set
            {
                SetProperty(ref _activity, value);
            }
        }

        private MvxCommand _call;
        public IMvxCommand Call => CreateCommand(ref _call, () =>
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
            {
                phoneDialer.MakePhoneCall(_activity.PhoneNumber);
            }
        });

        private MvxCommand _navigateToWebsite;
        public IMvxCommand NavigateToWebsite => CreateCommand(ref _navigateToWebsite, () =>
        {
            _shareService.OpenBrowser(_activity.WebsiteUrl);
        });
    }
}
