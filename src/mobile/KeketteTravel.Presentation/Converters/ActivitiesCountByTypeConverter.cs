using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using KeketteTravel.Presentation.ViewModels;
using MvvmCross.Platform.Converters;

namespace KeketteTravel.Presentation.Converters
{
    public class ActivitiesCountByTypeConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var activities = (List<Activity>)value;
            var type = int.Parse(parameter.ToString());

            if (activities == null)
            {
                return 0;
            }

            switch (type)
            {
                case 0:
                    return activities.Where(a => a.Type == ActivityType.Accomodation).ToArray().Length;
                case 1:
                    return activities.Where(a => a.Type == ActivityType.Activity).ToArray().Length;
                case 2:
                    return activities.Where(a => a.Type == ActivityType.SightSeeing).ToArray().Length;
                case 3:
                    return activities.Where(a => a.Type == ActivityType.Restaurant).ToArray().Length;
                case 4:
                    return activities.Where(a => a.Type == ActivityType.Bar).ToArray().Length;
                case 5:
                    return activities.Where(a => a.Type == ActivityType.SexSpot).ToArray().Length;
                default:
                    return 0;
            }
        }
    }
}
