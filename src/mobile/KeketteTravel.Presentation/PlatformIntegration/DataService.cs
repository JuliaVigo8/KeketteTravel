using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amp.Caching;
using Amp.Core;
using KeketteTravel.Presentation.ViewModels;

namespace KeketteTravel.Presentation.PlatformIntegration
{
    public class DataService : IDataService
    {
        private readonly ICacheService _cacheService;
        private const string DataCacheKey = "Data_Cache_Key";
        private List<CountryItemViewModel> _data;

        public DataService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        private async Task Initialize()
        {
            _data = await _cacheService.Get<List<CountryItemViewModel>>(DataCacheKey);

            if (_data == null)
            {
                _data = new List<CountryItemViewModel>()
                {
                    new CountryItemViewModel()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Costa Rica",
                        ImageUrl = "http://jayana.ca/wp-content/uploads/2015/12/o-COSTA-RICA-facebook.jpg",
                        TopLeftPosition = new Position(){Latitude = 11.362092, Longitude = -86.367949},
                        BottomRightPosition = new Position(){Latitude = 7.798608, Longitude = -82.182158},
                        Activities = new System.Collections.Generic.List<Activity>()
                        {
                            new Activity()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = "Skydive Costa Rica",
                                ImageUrl = "https://skydivingcostarica.com/wp-content/uploads/2015/11/12030477_1186355414711179_3122376341308444018_o-400x250.jpg",
                                Type = ActivityType.Activity,
                                WebsiteUrl = "https://skydivingcostarica.com/",
                                Address = new Address(){Street = "Carretera Pacífica Fernandez", City="Quepos"},
                                PhoneNumber = "(506) 8406-8544 / 8350-2746",
                                Position = new Position(){Latitude = 9.425491, Longitude = -84.112870},
                                Description = "We want everyone to experience the human-flight, something you will never forget. Our goal is to give you the best experience in your life with awesome weather and the best views of the ocean, mountains, rivers and wildlife. Our instructors are highly qualified, certified in the USA and with plenty of experience. We offer Tandem skydiving and also special courses to get your skydiving license. We hold a 100% safety record!"
                            },
                            new Activity()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = "Trogon Lodge",
                                ImageUrl = "https://s-ec.bstatic.com/images/hotel/max1024x768/411/41159729.jpg",
                                Type = ActivityType.Accomodation,
                                WebsiteUrl = "https://www.booking.com/hotel/cr/trogon-lodge.fr.html?aid=808749;label=pri811jc-hotel-fr-cr-trogonNlodge-unspec-ca-com-L%3Afr-O%3AosSx-B%3Achrome-N%3Ayes-S%3Abo-U%3Asao-H%3As;sid=904d535f8d4ebe068e1de9cdc25385c4;all_sr_blocks=103021801_96199711_2_41_0;checkin=2017-09-21;checkout=2017-09-22;dest_id=-1108681;dest_type=city;dist=0;group_adults=2;highlighted_blocks=103021801_96199711_2_41_0;hpos=1;room1=A%2CA;sb_price_type=total;srfid=81b326847a9a8611a4bb82bce71f10e222ab8100X1;type=total;ucfs=1&",
                                Address = new Address(){Street = "San Gerardo de Dota", PostalCode="10980", City="Providencia"},
                                Position = new Position(){Latitude = 9.576315, Longitude = -83.799891},
                                Description = "Le Trogon Lodge se situe à 1,5 km du centre-ville de San Gerardo de Dota et à 9 km du parc national Los Quetzales. Il possède un vaste jardin, une terrasse et un barbecue.Les chambres disposent d'une terrasse et d'une salle de bains privative avec douche. Elles offrent une vue sur les montagnes et le jardin. Le linge de lit est fourni. Ce lodge se situe à côté de la rivière Savegre, à 1h00 de route des cascades de San Gerardo. L'aéroport international Juan Santamaría est accessible en 2 heures de route."
                            }
                        }
                    },
                    new CountryItemViewModel() 
                    { 
                        Id = Guid.NewGuid().ToString(), 
                        Name = "Chili", 
                        ImageUrl="http://www.guidevoyages.org/wordpress/wp-content/uploads/2013/10/Parque-Nacional-Lauca-y-Volc%C3%A1n-Parinacota-Chile.-Autor-mtchm.-Licensed-under-the-Creative-Commons-Attribution-Share-Alike.jpg",
                        TopLeftPosition = new Position(){Latitude = -16.764936, Longitude = -77.911225},
                        BottomRightPosition = new Position(){Latitude = -56.293587, Longitude = -65.167086}
                    },
                };

                await _cacheService.Set(DataCacheKey, _data);
            }
        }

        public Activity GetActivity(string countryId, string id)
        {
            var country = _data.FirstOrDefault(c => c.Id == countryId);

            if (country != null)
            {
                return country.Activities.FirstOrDefault(activity => activity.Id == id);
            }

            return null;
        }

        public async Task<List<CountryItemViewModel>> GetCountries()
        {
            if (_data == null)
            {
                await Initialize();
            }

            return _data;
        }

        public CountryItemViewModel GetCountry(string id)
        {
            return _data.FirstOrDefault(country => country.Id == id);
        }

        public async Task AddActivity(string countryId, Activity activity)
        {
            _data.FirstOrDefault(c => c.Id == countryId)?.Activities.Add(activity);
            await UpdateData(_data);
        }

        private async Task UpdateData(List<CountryItemViewModel> data)
        {
            await _cacheService.Set(DataCacheKey, _data);
        }
    }
}
