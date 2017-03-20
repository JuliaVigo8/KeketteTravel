using Android.App;
using Android.OS;
using Android.Views;
using KeketteTravel.Presentation.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace KeketteTravel.Droid.Views
{
    [Activity(Theme = "@style/AppTheme")]
    public class ActivityDetailsView : MvxCachingFragmentCompatActivity<ActivityDetailsViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.RequestFeature(WindowFeatures.NoTitle);

            SetContentView(Resource.Layout.ActivityDetails);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.top_bar);
            SetSupportActionBar(toolbar);

            var set = this.CreateBindingSet<ActivityDetailsView, ActivityDetailsViewModel>();

            set.Bind()
                .For(v => v.Title)
               .To(vm => vm.Activity.Type);

            set.Apply();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.activity_details_menu, menu);

            base.OnCreateOptionsMenu(menu);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_edit:
                    (ViewModel as ActivityDetailsViewModel).NavigateToEdit.Execute();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}
