using System;
using Acr.UserDialogs;
using KeketteTravel.Presentation.Localization;
using KeketteTravel.Presentation.Messages;
using KeketteTravel.Presentation.PlatformIntegration;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using static Amp.MvvmCross.CommandFactory;

namespace KeketteTravel.Presentation.ViewModels
{
    public class AddActivityViewModel : MvxViewModel
    {
        private readonly IDataService _dataService;
        private readonly IUserDialogs _dialogs;
        private readonly IMvxMessenger _messenger;
        private string _countryId;
        private Position _position;

        public AddActivityViewModel(
            IDataService dataService, 
            IUserDialogs dialogs, 
            IMvxMessenger messenger)
        {
            _dataService = dataService;
            _dialogs = dialogs;
            _messenger = messenger;
        }

        public void Init(string countryId, string activityId, double x, double y)
        {
            _countryId = countryId;
            _position = new Position(x, y);

            if (!string.IsNullOrWhiteSpace(activityId))
            {
                var activity = _dataService.GetActivity(countryId, activityId);
                Id = activity.Id;
                Name = activity.Name;
                Description = activity.Description;
                Street = activity.Address.Street;
                PostalCode = activity.Address.PostalCode;
                City = activity.Address.City;
                _position = activity.Position;
                PhotoUrl = activity.ImageUrl;
                WebsiteUrl = activity.WebsiteUrl;
                PhoneNumber = activity.PhoneNumber;
                Type = activity.Type;
            }
        }

        private string _id;
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                SetProperty(ref _description, value);
            }
        }

        private string _street;
        public string Street
        {
            get
            {
                return _street;
            }
            set
            {
                SetProperty(ref _street, value);
            }
        }

        private string _postalCode;
        public string PostalCode
        {
            get
            {
                return _postalCode;
            }
            set
            {
                SetProperty(ref _postalCode, value);
            }
        }

        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                SetProperty(ref _city, value);
            }
        }

        private string _photoUrl;
        public string PhotoUrl
        {
            get
            {
                return _photoUrl;
            }
            set
            {
                SetProperty(ref _photoUrl, value);
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                SetProperty(ref phoneNumber, value);
            }
        }

        private string _websiteUrl;
        public string WebsiteUrl
        {
            get
            {
                return _websiteUrl;
            }
            set
            {
                SetProperty(ref _websiteUrl, value);
            }
        }

        private ActivityType _type;
        public ActivityType Type
        {
            get
            {
                return _type;
            }
            set
            {
                SetProperty(ref _type, value);
            }
        }

        private MvxAsyncCommand _add;
        public IMvxCommand Add => CreateCommand(ref _add, async () =>
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                _dialogs.Alert(Master.addactivity_name_error);
                return;
            }

            var activity = new Activity()
            {
                Id = string.IsNullOrWhiteSpace(Id) ? Guid.NewGuid().ToString() : Id,
                Name = Name,
                Description = Description,
                Type = Type,
                Address = new Address()
                {
                    Street = Street,
                    PostalCode = PostalCode,
                    City = City
                },
                Position = _position,
                ImageUrl = PhotoUrl,
                WebsiteUrl = WebsiteUrl,
                PhoneNumber = PhoneNumber
            };

            if (string.IsNullOrWhiteSpace(Id))
            {
                await _dataService.AddActivity(_countryId, activity);
            }
            else
            {
                await _dataService.EditActivity(_countryId, activity);
            }
            _messenger.Publish(new DataUpdatedMessage(this));
            Close(this);
        });

        private MvxCommand _clickOnAccomodationType;
        public IMvxCommand ClickOnAccomodationType => CreateCommand(ref _clickOnAccomodationType, () =>
        {
            Type = ActivityType.Accomodation;
        });

        private MvxCommand _clickOnActivityType;
        public IMvxCommand ClickOnActivityType => CreateCommand(ref _clickOnActivityType, () =>
        {
            Type = ActivityType.Activity;
        });

        private MvxCommand _clickOnRestaurantType;
        public IMvxCommand ClickOnRestaurantType => CreateCommand(ref _clickOnRestaurantType, () =>
        {
            Type = ActivityType.Restaurant;
        });

        private MvxCommand _clickOnBarType;
        public IMvxCommand ClickOnBarType => CreateCommand(ref _clickOnBarType, () =>
        {
            Type = ActivityType.Bar;
        });

        private MvxCommand _clickOnSightseeingType;
        public IMvxCommand ClickOnSightseeingType => CreateCommand(ref _clickOnSightseeingType, () =>
        {
            Type = ActivityType.SightSeeing;
        });

        private MvxCommand _clickOnSexSpotType;
        public IMvxCommand ClickOnSexSpotType => CreateCommand(ref _clickOnSexSpotType, () =>
        {
            Type = ActivityType.SexSpot;
        });
    }
}
