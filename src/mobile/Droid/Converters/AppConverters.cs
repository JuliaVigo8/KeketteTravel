using System;
using KeketteTravel.Presentation.Converters;

namespace KeketteTravel.Droid.Converters
{
    public class AppConverters
    {
        static AppConverters _instance;
        public static AppConverters Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppConverters();
                }
                return _instance;
            }
        }

        public readonly ActivitiesCountByTypeConverter ActivitiesCountByType = new ActivitiesCountByTypeConverter();
    }
}
