using System;
using Android.App;
using Android.OS;
using Android.Views;
using KeketteTravel.Presentation.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace KeketteTravel.Droid.Views
{
    [Activity(Theme = "@style/AppTheme")]
    public class AddActivityView : MvxCachingFragmentCompatActivity<AddActivityViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.RequestFeature(WindowFeatures.NoTitle);

            SetContentView(Resource.Layout.AddActivity);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.top_bar);
            SetSupportActionBar(toolbar);

            Title = Resources.GetString(Resource.String.addactivity_title);

            var set = this.CreateBindingSet<AddActivityView, AddActivityViewModel>();

            set.Bind()
                .For(v => v.Title)
                .To(vm => vm.Id)
                .WithConversion("IdToButtonString", "Title");

            set.Apply();
        }
    }
}
