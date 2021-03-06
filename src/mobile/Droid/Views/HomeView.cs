using Acr.UserDialogs;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using KeketteTravel.Presentation.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace KeketteTravel.Droid.Views
{
    [Activity(Theme = "@style/AppTheme")]
    public class HomeView : MvxCachingFragmentCompatActivity<HomeViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.RequestFeature(WindowFeatures.NoTitle);

            UserDialogs.Init(this);

            SetContentView(Resource.Layout.Home);

            var toolbar = FindViewById<Toolbar>(Resource.Id.top_bar);
            SetSupportActionBar(toolbar);

            Title = Resources.GetString(Resource.String.app_name);
        }
    }
}
