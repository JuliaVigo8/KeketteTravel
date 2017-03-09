using System;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using KeketteTravel.Presentation.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace KeketteTravel.Droid.Views
{
    [Activity(Theme = "@style/AppTheme")]
    public class CountryDetailsView : MvxCachingFragmentCompatActivity<CountryDetailsViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.RequestFeature(WindowFeatures.NoTitle);

            SetContentView(Resource.Layout.CountryDetails_Container);

            var toolbar = FindViewById<Toolbar>(Resource.Id.top_bar);
            SetSupportActionBar(toolbar);

            var fragment = new CountryDetailsFragment
            {
                ViewModel = ViewModel
            };

            SupportFragmentManager
            .BeginTransaction()
                .Replace(Resource.Id.content_frame_countrydetails, fragment, typeof(HomeFragment).FullName)
            .Commit();

            var set = this.CreateBindingSet<CountryDetailsView, CountryDetailsViewModel>();

            set.Bind()
                .For(v => v.Title)
                .To(vm => vm.Country.Name);

            set.Apply();
        }
    }
}
