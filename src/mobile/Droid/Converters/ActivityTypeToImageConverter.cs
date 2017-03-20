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

            if (typeParam == -1)
            {
                switch (type)
                {
                    case ActivityType.Accomodation:
                        return "accomodationmarker";
                    case ActivityType.Activity:
                        return "activitymarker";
                    case ActivityType.SightSeeing:
                        return "sightseeingmarker";
                    case ActivityType.Restaurant:
                        return "restaurantmarker";
                    case ActivityType.Bar:
                        return "barmarker";
                    case ActivityType.SexSpot:
                        return "sexmarker";
                    default:
                        return string.Empty;
                }
            }

            switch (typeParam)
            {
                case 0:
                    if (type == ActivityType.Accomodation)
                    {
                        return "accomodationmarker";
                    }
                    else
                    {
                        return "accomodationpinunselected";
                    }
                case 1:
                    if (type == ActivityType.Activity)
                    {
                        return "activitymarker";
                    }
                    else
                    {
                        return "activitypinunselected";
                    }
                case 2:
                    if (type == ActivityType.SightSeeing)
                    {
                        return "sightseeingmarker";
                    }
                    else
                    {
                        return "sightseeingpinunselected";
                    }
                case 3:
                    if (type == ActivityType.Restaurant)
                    {
                        return "restaurantmarker";
                    }
                    else
                    {
                        return "restaurantpinunselected";
                    }
                case 4:
                    if (type == ActivityType.Bar)
                    {
                        return "barmarker";
                    }
                    else
                    {
                        return "barpinunselected";
                    }
                case 5:
                    if (type == ActivityType.SexSpot)
                    {
                        return "sexmarker";
                    }
                    else
                    {
                        return "sexpinunselected";
                    }
                default:
                    return string.Empty;
            }
        }
    }
}
