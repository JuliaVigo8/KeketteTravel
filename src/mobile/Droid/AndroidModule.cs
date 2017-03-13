using System;
using Amp.Caching;
using Android.Content;
using KeketteTravel.Droid.PlatformIntegration;
using KeketteTravel.Presentation.PlatformIntegration;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;

namespace KeketteTravel.Droid
{
    public static class AndroidModule
    {
        static bool _isInitialized;

        public static void Initialize(Context context)
        {
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;

            MvxSimpleIoCContainer.Initialize();
            AmpCachingModule.Init(
                (interfaceType, serviceType) => Mvx.RegisterSingleton(interfaceType, Mvx.IocConstruct(serviceType)),
                type => Mvx.Resolve(type)
            );

            Mvx.RegisterSingleton<IAndroidContext>(() => new AndroidContext(context));
            Mvx.RegisterType<IShareService, ShareService>(); ;
        }
    }
}