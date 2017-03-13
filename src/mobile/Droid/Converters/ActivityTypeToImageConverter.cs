using System;
using System.Globalization;
using KeketteTravel.Presentation.ViewModels;
using MvvmCross.Platform.Converters;

namespace KeketteTravel.Droid.Converters
{
    public class ActivityTypeToImageConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (ActivityType)value;
            var typeParam = int.Parse(parameter.ToString());

            switch (typeParam)
            {
                case 0:
                    if (type == ActivityType.Accomodation)
                    {
                        return "accomodationMarker";
                    }
                    else
                    {
                        return "accomodationPinUnselected";
                    }
                case 1:
                    if (type == ActivityType.Activity)
                    {
                        return "activityMarker";
                    }
                    else
                    {
                        return "activityPinUnselected";
                    }
                case 2:
                    if (type == ActivityType.SightSeeing)
                    {
                        return "sightSeeingMarker";
                    }
                    else
                    {
                        return "sightSeeingPinUnselected";
                    }
                case 3:
                    if (type == ActivityType.Restaurant)
                    {
                        return "restaurantMarker";
                    }
                    else
                    {
                        return "restaurantPinUnselected";
                    }
                case 4:
                    if (type == ActivityType.Bar)
                    {
                        return "barMarker";
                    }
                    else
                    {
                        return "barPinUnselected";
                    }
                case 5:
                    if (type == ActivityType.SexSpot)
                    {
                        return "sexMarker";
                    }
                    else
                    {
                        return "sexPinUnselected";
                    }
                default:
                    return string.Empty;
            }
        }
    }
}
