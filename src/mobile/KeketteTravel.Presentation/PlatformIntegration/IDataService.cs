using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KeketteTravel.Presentation.ViewModels;

namespace KeketteTravel.Presentation.PlatformIntegration
{
    public interface IDataService
    {
        Task<List<CountryItemViewModel>> GetCountries();
        CountryItemViewModel GetCountry(string id);
        Activity GetActivity(string countryId, string id);
        Task AddActivity(string countryId, Activity activity);
        Task EditActivity(string countryId, Activity activity);
    }
}
