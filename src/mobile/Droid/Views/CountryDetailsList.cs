using System;
using Android.Views;
using KeketteTravel.Presentation.ViewModels;

namespace KeketteTravel.Droid.Views
{
    public class CountryDetailsList : BaseFragment
    {
        protected override int FragmentId => Resource.Layout.CountryDetails_List;

        public override void OnViewCreated(Android.Views.View view, Android.OS.Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            HasOptionsMenu = true;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.country_details_list_menu, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_showMap:
                    (ViewModel as CountryDetailsViewModel).ShowMap.Execute();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}
