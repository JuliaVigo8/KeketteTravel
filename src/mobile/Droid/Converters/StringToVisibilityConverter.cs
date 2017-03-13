using System;
using System.Globalization;
using Android.Views;
using MvvmCross.Platform.Converters;

namespace KeketteTravel.Droid.Converters
{
    public class StringToVisibilityConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value.ToString();

            return string.IsNullOrWhiteSpace(stringValue) ? ViewStates.Gone : ViewStates.Visible;
        }
    }
}
