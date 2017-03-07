using System.Collections.ObjectModel;
using MvvmCross.Core.ViewModels;

namespace KeketteTravel.Presentation.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        public override void Start()
        {
            base.Start();

            Countries = new ObservableCollection<CountryItemViewModel>()
            {
                new CountryItemViewModel() { Name = "Costa Rica", ImageUrl="http://jayana.ca/wp-content/uploads/2015/12/o-COSTA-RICA-facebook.jpg"},
                new CountryItemViewModel() { Name = "Chili", ImageUrl="http://www.guidevoyages.org/wordpress/wp-content/uploads/2013/10/Parque-Nacional-Lauca-y-Volc%C3%A1n-Parinacota-Chile.-Autor-mtchm.-Licensed-under-the-Creative-Commons-Attribution-Share-Alike.jpg" },
            };
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
    }
}
