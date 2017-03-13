using System;
using Android.Content;

namespace KeketteTravel.Droid.PlatformIntegration
{
    public class AndroidContext : IAndroidContext
    {
        public AndroidContext(Context c)
        {
            Context = c;
        }

        public Context Context
        {
            get;
            private set;
        }
    }
}
