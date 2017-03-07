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

            SetContentView(Resource.Layout.Home_Container);

            var toolbar = FindViewById<Toolbar>(Resource.Id.top_bar);
            SetSupportActionBar(toolbar);

            var fragment = new HomeFragment
            {
                ViewModel = ViewModel
            };

            SupportFragmentManager
            .BeginTransaction()
                .Replace(Resource.Id.content_frame_home, fragment, typeof(HomeFragment).FullName)
            .Commit();

            Title = "Kekette Travel";
        }
    }
}
