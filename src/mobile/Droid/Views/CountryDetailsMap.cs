using System;
using System.Collections.Generic;
using System.Linq;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Views;
using Android.Widget;
using KeketteTravel.Presentation.ViewModels;
using MvvmCross.Binding.BindingContext;

namespace KeketteTravel.Droid.Views
{
    public class CountryDetailsMap : BaseFragment, IOnMapReadyCallback, GoogleMap.IOnInfoWindowClickListener, GoogleMap.IInfoWindowAdapter
    {
        protected override int FragmentId => Resource.Layout.CountryDetails_Map;
        private GoogleMap _map;
        private List<Marker> _activityMarkers;
        private Dictionary<string, Activity> _activityByMarker;
        private Marker _showingInfoWindowMarker;

        public override void OnViewCreated(View view, Android.OS.Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _activityMarkers = new List<Marker>();
            _activityByMarker = new Dictionary<string, Activity>();

            HasOptionsMenu = true;

            var mapFragment = (SupportMapFragment)ChildFragmentManager.FindFragmentById(Resource.Id.mapFragment);
            mapFragment.GetMapAsync(this);
        }

        // TODO find another way to refresh map
        //public override void OnResume()
        //{
        //    base.OnResume();

        //    if (_showingInfoWindowMarker != null && _showingInfoWindowMarker.IsInfoWindowShown)
        //    {
        //        _showingInfoWindowMarker.ShowInfoWindow();
        //    }
        //}

        private CountryItemViewModel _country;
        public CountryItemViewModel Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;

                if (_map == null)
                {
                    return;
                }

                foreach (var marker in _activityMarkers)
                {
                    if (_country.Activities.FirstOrDefault(a => a.Id == marker.Id) == null)
                    {
                        marker.Remove();
                    }
                }

                foreach (var activity in _country.Activities)
                {
                    if (activity.Position != null && _activityMarkers.FirstOrDefault(m => m.Title == activity.Id) == null)
                    {
                        var markerOptions = new MarkerOptions();
                        markerOptions.SetPosition(new LatLng(activity.Position.Latitude, activity.Position.Longitude));

                        switch (activity.Type)
                        {
                            case ActivityType.Accomodation:
                                markerOptions.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.accomodationMarker));
                                break;
                            case ActivityType.Activity:
                                markerOptions.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.activityMarker));
                                break;
                            case ActivityType.Bar:
                                markerOptions.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.barMarker));
                                break;
                            case ActivityType.Restaurant:
                                markerOptions.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.restaurantMarker));
                                break;
                            case ActivityType.SexSpot:
                                markerOptions.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.sexMarker));
                                break;
                            case ActivityType.SightSeeing:
                                markerOptions.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.sightseeingMarker));
                                break;
                        }

                        var marker = _map?.AddMarker(markerOptions);
                        if (marker != null)
                        {
                            _activityMarkers.Add(marker);
                            marker.Title = activity.Id;
                            _activityByMarker.Add(marker.Title, activity);
                        }
                    }
                }

                var builder = new LatLngBounds.Builder();
                builder.Include(new LatLng(_country.TopLeftPosition.Latitude, _country.TopLeftPosition.Longitude));
                builder.Include(new LatLng(_country.BottomRightPosition.Latitude, _country.BottomRightPosition.Longitude));
                var bounds = builder.Build();

                var width = Resources.DisplayMetrics.WidthPixels;
                var height = Resources.DisplayMetrics.HeightPixels;

                var cameraUpdate = CameraUpdateFactory.NewLatLngBounds(bounds, width, height, 0);
                _map?.AnimateCamera(cameraUpdate);
            }
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.country_details_menu, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_map_add:
                    (ViewModel as CountryDetailsViewModel).NavigateToAddActivity.Execute();
                    break;
                case Resource.Id.action_showList:
                    (ViewModel as CountryDetailsViewModel).ShowList.Execute();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;
            _map.SetInfoWindowAdapter(this);
            _map.SetOnInfoWindowClickListener(this);

            var set = this.CreateBindingSet<CountryDetailsMap, CountryDetailsViewModel>();

            set.Bind()
               .For(v => v.Country)
               .To(vm => vm.Country);

            set.Apply();
        }

        public View GetInfoContents(Marker marker)
        {
            _showingInfoWindowMarker = marker;

            var view = Activity.LayoutInflater.Inflate(Resource.Layout.Marker_Activity, null);

            var activity = GetActivityForMarker(marker);

            if (activity == null)
            {
                return null;
            }

            var activityNameTextView = view.FindViewById<TextView>(Resource.Id.activityNameTextView);
            activityNameTextView.Text = activity.Name;

            var activityAdressTextView = view.FindViewById<TextView>(Resource.Id.activityAdressTextView);
            activityAdressTextView.Text = activity.Address.ToString();

            return view;
        }

        public Activity GetActivityForMarker(Marker marker)
        {
            if (_activityByMarker.ContainsKey(marker.Title))
            {
                return _activityByMarker[marker.Title];
            }
            return null;
        }

        public View GetInfoWindow(Marker marker)
        {
            return null;
        }

        public void OnInfoWindowClick(Marker marker)
        {
            var activity = GetActivityForMarker(marker);

            if (activity != null)
            {
                (ViewModel as CountryDetailsViewModel).NavigateToDetails.Execute(activity);
            }
        }
    }
}
