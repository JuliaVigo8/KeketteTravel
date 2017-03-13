using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;

namespace KeketteTravel.Presentation.ViewModels
{
    public class CountryItemViewModel : MvxNotifyPropertyChanged
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public Position TopLeftPosition { get; set; }
        public Position BottomRightPosition { get; set; }
        public List<Activity> Activities { get; set; }

        public CountryItemViewModel()
        {
            Activities = new List<Activity>();
        }
    }

    public class Activity
    {
        public Activity()
        {
            PhoneNumber = string.Empty;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public ActivityType Type { get; set; }
        public Address Address { get; set; }
        public Position Position { get; set; }
        public string Description { get; set; }
        public string WebsiteUrl { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public string Address2
        {
            get
            {
                if (string.IsNullOrWhiteSpace(PostalCode))
                {
                    return $"{PostalCode} {City}";
                }
                return $"{City}";
            }
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(PostalCode))
            {
                return $"{Street}, {PostalCode} {City}";
            }
            return $"{Street}, {City}";
        }
    }

    public class Position
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return $"{Latitude}, {Longitude}";
        }
    }

    public enum ActivityType
    {
        Accomodation,
        Activity,
        SightSeeing,
        Bar,
        Restaurant,
        SexSpot
    }
}
