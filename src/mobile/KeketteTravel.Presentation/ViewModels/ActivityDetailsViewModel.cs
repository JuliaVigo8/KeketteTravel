using KeketteTravel.Presentation.Messages;
using KeketteTravel.Presentation.PlatformIntegration;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Plugin.Messaging;
using static Amp.MvvmCross.CommandFactory;

namespace KeketteTravel.Presentation.ViewModels
{
    public class ActivityDetailsViewModel : MvxViewModel
    {
        private readonly IShareService _shareService;
        private readonly IDataService _dataService;
        private string _countryId;
        private readonly MvxSubscriptionToken _dataUpdatedMessageToken;

        public ActivityDetailsViewModel(
            IShareService shareService,
            IDataService dataService,
            IMvxMessenger messenger)
        {
            _dataService = dataService;
            _shareService = shareService;

            _dataUpdatedMessageToken = messenger.SubscribeOnMainThread<DataUpdatedMessage>(OnDataUpdated);
        }

        public void Init(string countryId, string activityId)
        {
            _countryId = countryId;
            Activity = _dataService.GetActivity(countryId, activityId);
        }

        private void OnDataUpdated(DataUpdatedMessage msg)
        {
            Activity = _dataService.GetActivity(_countryId, Activity.Id);
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

        private MvxCommand _navigateToEdit;
        public IMvxCommand NavigateToEdit => CreateCommand(ref _navigateToEdit, () =>
        {
            ShowViewModel<AddActivityViewModel>(new { countryId = _countryId, activityId = Activity.Id });
        });
    }
}
