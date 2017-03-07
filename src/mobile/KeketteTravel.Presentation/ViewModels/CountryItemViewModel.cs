using System;
using MvvmCross.Core.ViewModels;

namespace KeketteTravel.Presentation.ViewModels
{
    public class CountryItemViewModel : MvxNotifyPropertyChanged
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
