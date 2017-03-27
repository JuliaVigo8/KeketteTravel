using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using KeketteTravel.Presentation.ViewModels;
using MvvmCross.Binding.BindingContext;

namespace KeketteTravel.Droid.Views
{
    public class CountryDetailsMap : BaseFragment
    {
        protected override int FragmentId => Resource.Layout.CountryDetails_Map;
        private Dictionary<string, Activity> _activityByMarker;
        private LinearLayout _mapLayout;
        private GraphicsOverlay _overlay;
        private MapView _mapView = new MapView();

        public override void OnViewCreated(View view, Android.OS.Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            //_activityMarkers = new List<Marker>();
            _activityByMarker = new Dictionary<string, Activity>();

            _mapLayout = view.FindViewById<LinearLayout>(Resource.Id.mapLayout);

            HasOptionsMenu = true;

            var set = this.CreateBindingSet<CountryDetailsMap, CountryDetailsViewModel>();

            set.Bind()
               .For(v => v.Country)
               .To(vm => vm.Country);

            set.Apply();
        }

        async void OnMapViewTapped(object sender, GeoViewInputEventArgs e)
        {
            foreach (var marker in _overlay.Graphics)
            {
                var mapPoint = new MapPoint(marker.Geometry.Extent.XMax, marker.Geometry.Extent.YMax, SpatialReferences.WebMercator);
                var point = _mapView.LocationToScreen(mapPoint);

                if (point.X <= e.Position.X + 60 && point.X >= e.Position.X - 60 && point.Y <= e.Position.Y + 60 && point.Y >= e.Position.Y - 60)
                {
                    var activity = _country.Activities
                                           .FirstOrDefault(a => a.Position.X == marker.Geometry.Extent.XMax
                                                           && a.Position.Y == marker.Geometry.Extent.YMax);
                    (ViewModel as CountryDetailsViewModel).NavigateToDetails.Execute(activity);
                    return;
                }
            }

            (ViewModel as CountryDetailsViewModel).NavigateToAddActivity.Execute(new Position(e.Location.X, e.Location.Y));
        }

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

                // Create a new Map instance with the basemap               
                var map = new Map(Basemap.CreateStreetsVector());

                var initialLocation = new Envelope(
                    _country.TopLeftPosition.X, 
                    _country.TopLeftPosition.Y,
                    _country.BottomRightPosition.X,
                    _country.BottomRightPosition.Y,
                    SpatialReferences.WebMercator);
                
                map.InitialViewpoint = new Viewpoint(initialLocation);

                // Provide used Map to the MapView
                _mapView.Map = map;

                _mapView.GeoViewTapped += OnMapViewTapped;

                // Create overlay to where graphics are shown
                _overlay = new GraphicsOverlay();

                SetMarkers();

                // Add created overlay to the MapView
                _mapView.GraphicsOverlays.Add(_overlay);

                _mapLayout.AddView(_mapView);
            }
        }

        private void SetMarkers()
        {
            foreach (var activity in _country.Activities)
            {
                if (activity.Position != null)
                {
                    switch (activity.Type)
                    {
                        case ActivityType.Accomodation:
                            CreatePictureMarkerSymbolFromResources(
                                Resources.OpenRawResource(Resource.Drawable.accomodationMarker),
                                activity.Position);
                            break;
                        case ActivityType.Activity:
                            CreatePictureMarkerSymbolFromResources(
                                Resources.OpenRawResource(Resource.Drawable.activityMarker),
                                activity.Position);
                            break;
                        case ActivityType.Bar:
                            CreatePictureMarkerSymbolFromResources(
                                Resources.OpenRawResource(Resource.Drawable.barMarker),
                                activity.Position);
                            break;
                        case ActivityType.Restaurant:
                            CreatePictureMarkerSymbolFromResources(
                                Resources.OpenRawResource(Resource.Drawable.restaurantMarker),
                                activity.Position);
                            break;
                        case ActivityType.SexSpot:
                            CreatePictureMarkerSymbolFromResources(
                                Resources.OpenRawResource(Resource.Drawable.sexMarker),
                                activity.Position);
                            break;
                        case ActivityType.SightSeeing:
                            CreatePictureMarkerSymbolFromResources(
                                Resources.OpenRawResource(Resource.Drawable.sightseeingMarker),
                                activity.Position);
                            break;
                    }
                }
            }
        }

        private async void CreatePictureMarkerSymbolFromResources(Stream resourceStream, Position location)
        {
            // Create new symbol using asynchronous factory method from stream
            var pinSymbol = await PictureMarkerSymbol.CreateAsync(resourceStream);

            //var pinSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, System.Drawing.Color.AliceBlue, 12);

            // Create location for the pint
            var pinPoint = new MapPoint(location.X, location.Y, SpatialReferences.WebMercator);

            // Create graphic with the location and symbol
            var pinGraphic = new Graphic(pinPoint, pinSymbol);

            // Add graphic to the graphics overlay
            _overlay.Graphics.Add(pinGraphic);
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
                case Resource.Id.action_showList:
                    (ViewModel as CountryDetailsViewModel).ShowList.Execute();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        //public View GetInfoContents(Marker marker)
        //{
        //    _showingInfoWindowMarker = marker;

        //    var view = Activity.LayoutInflater.Inflate(Resource.Layout.Marker_Activity, null);

        //    var activity = GetActivityForMarker(marker);

        //    if (activity == null)
        //    {
        //        return null;
        //    }

        //    var activityNameTextView = view.FindViewById<TextView>(Resource.Id.activityNameTextView);
        //    activityNameTextView.Text = activity.Name;

        //    var activityAdressTextView = view.FindViewById<TextView>(Resource.Id.activityAdressTextView);
        //    activityAdressTextView.Text = activity.Address.ToString();

        //    return view;
        //}

        //public Activity GetActivityForMarker(Marker marker)
        //{
        //    if (_activityByMarker.ContainsKey(marker.Title))
        //    {
        //        return _activityByMarker[marker.Title];
        //    }
        //    return null;
        //}

        //public View GetInfoWindow(Marker marker)
        //{
        //    return null;
        //}

        //public void OnInfoWindowClick(Marker marker)
        //{
        //    var activity = GetActivityForMarker(marker);

        //    if (activity != null)
        //    {
        //        (ViewModel as CountryDetailsViewModel).NavigateToDetails.Execute(activity);
        //    }
        //}
    }
}
