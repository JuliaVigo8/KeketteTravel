using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Views;
using Android.Widget;
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

            SetContentView(Resource.Layout.CountryDetails);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.top_bar);
            SetSupportActionBar(toolbar);

            var set = this.CreateBindingSet<CountryDetailsView, CountryDetailsViewModel>();

            set.Bind()
                .For(v => v.Title)
                .To(vm => vm.Country.Name);

            set.Bind()
               .For(v => v.IsShowingList)
               .To(vm => vm.IsShowingList);

            set.Apply();
        }

        private bool _isShowingList;
        public bool IsShowingList
        {
            get
            {
                return _isShowingList;
            }
            set
            {
                _isShowingList = value;

                BaseFragment fragment;
                string tag;
                if (_isShowingList)
                {
                    fragment = new CountryDetailsList
                    {
                        ViewModel = ViewModel
                    };
                    tag = typeof(CountryDetailsList).FullName;
                }
                else
                {
                    fragment = new CountryDetailsMap
                    {
                        ViewModel = ViewModel
                    };
                    tag = typeof(CountryDetailsMap).FullName;
                }

                SupportFragmentManager
                .BeginTransaction()
                    .Replace(Resource.Id.content_frame_country, fragment, tag)
                    .Commit();
            }
        }
    }
}
