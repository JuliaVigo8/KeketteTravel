using System;
using KeketteTravel.Presentation.Messages;
using KeketteTravel.Presentation.PlatformIntegration;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using static Amp.MvvmCross.CommandFactory;

namespace KeketteTravel.Presentation.ViewModels
{
    public class CountryDetailsViewModel : MvxViewModel
    {
        private readonly IDataService _dataService;
        private readonly MvxSubscriptionToken _dataUpdatedMessageToken;

        public CountryDetailsViewModel(
            IDataService dataService,
            IMvxMessenger messenger)
        {
            _dataService = dataService;

            _dataUpdatedMessageToken = messenger.Subscribe<DataUpdatedMessage>(OnDataUpdated);
        }

        public void Init(string countryId)
        {
            Country = _dataService.GetCountry(countryId);
        }

        private void OnDataUpdated(DataUpdatedMessage msg)
        {
            Country = _dataService.GetCountry(Country.Id);
        }

        private CountryItemViewModel _country;
        public CountryItemViewModel Country
        {
            get
            {
                return _country;
            }
            set
            {
                SetProperty(ref _country, value);
            }
        }

        private MvxCommand<Activity> _navigateToDetails;
        public IMvxCommand NavigateToDetails => CreateCommand(ref _navigateToDetails, activity =>
        {
            ShowViewModel<ActivityDetailsViewModel>(new { countryId = Country.Id, activityId = activity.Id });
        });

        private MvxCommand<Activity> _navigateToAddActivity;
        public IMvxCommand NavigateToAddActivity => CreateCommand(ref _navigateToAddActivity, activity =>
        {
            ShowViewModel<AddActivityViewModel>(new { countryId = Country.Id });
        });
    }
}
