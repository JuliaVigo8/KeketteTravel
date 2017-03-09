using Android.App;
using KeketteTravel.Presentation.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace KeketteTravel.Droid.Views
{
    [Activity(Theme = "@style/AppTheme")]
    public class ActivityDetailsView : MvxCachingFragmentCompatActivity<ActivityDetailsViewModel>
    {
    }
}
