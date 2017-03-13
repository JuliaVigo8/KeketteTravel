using System;
using System.Collections.ObjectModel;
using Amp.Logging;
using KeketteTravel.Presentation.Messages;
using KeketteTravel.Presentation.PlatformIntegration;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using static Amp.MvvmCross.CommandFactory;

namespace KeketteTravel.Presentation.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private readonly ILogger _logger = LoggerFactory.GetLogger<HomeViewModel>();
        private readonly IDataService _dataService;
        private readonly MvxSubscriptionToken _dataUpdatedMessageToken;

        public HomeViewModel(
            IDataService dataService,
            IMvxMessenger messenger)
        {
            _dataService = dataService;

            _dataUpdatedMessageToken = messenger.Subscribe<DataUpdatedMessage>(OnDataUpdated);
        }

        public override async void Start()
        {
            base.Start();

            try
            {
                var countries = await _dataService.GetCountries();

                if (countries != null)
                {
                    Countries = new ObservableCollection<CountryItemViewModel>(countries);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Problem while starting HomeViewModel");
            }
        }

        private async void OnDataUpdated(DataUpdatedMessage msg)
        {
            try
            {
                var countries = await _dataService.GetCountries();

                if (countries != null)
                {
                    Countries = new ObservableCollection<CountryItemViewModel>(countries);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to update data on HomeViewModel");
            }
        }

        private ObservableCollection<CountryItemViewModel> _countries;
        public ObservableCollection<CountryItemViewModel> Countries
        {
            get
            {
                return _countries;
            }
            set
            {
                SetProperty(ref _countries, value);
            }
        }

        private MvxCommand<CountryItemViewModel> _navigateToDetails;
        public IMvxCommand NavigateToDetails => CreateCommand(ref _navigateToDetails, country =>
        {
            ShowViewModel<CountryDetailsViewModel>(new { countryId = country.Id });
        });
    }
}
