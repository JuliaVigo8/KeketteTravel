using System;
using Acr.UserDialogs;
using Amp.Caching;
using KeketteTravel.Presentation.PlatformIntegration;
using MvvmCross.Platform;

namespace KeketteTravel.Presentation
{
    public static class PresentationModule
    {
        static bool _isInitialized;

        public static void Init()
        {
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;

            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            Mvx.LazyConstructAndRegisterSingleton<IDataService>(() => new DataService(Mvx.Resolve<ICacheService>()));
        }
    }
}
