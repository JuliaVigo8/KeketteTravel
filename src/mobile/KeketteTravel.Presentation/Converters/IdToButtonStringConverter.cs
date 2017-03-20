using System;
using System.Globalization;
using KeketteTravel.Presentation.Localization;
using MvvmCross.Platform.Converters;

namespace KeketteTravel.Presentation.Converters
{
    public class IdToButtonStringConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null && parameter.ToString() == "Title")
            {
                if (string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return Master.addactivity_title;
                }
                return Master.addactivity_title_edit;
            }

            if (string.IsNullOrWhiteSpace(value.ToString()))
            {
                return Master.addactivity_add;
            }
            return Master.addactivity_edit;
        }
    }
}
