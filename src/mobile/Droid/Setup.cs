using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using KeketteTravel.Presentation;
using System.Collections.Generic;
using KeketteTravel.Droid.Converters;
using System.Linq;
using System;

namespace KeketteTravel.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            AndroidModule.Initialize(ApplicationContext);
            return new App();
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var mvxFragmentsPresenter = new ViewPresenter(AndroidViewAssemblies);
            Mvx.RegisterSingleton<IMvxAndroidViewPresenter>(mvxFragmentsPresenter);
            return mvxFragmentsPresenter;
        }

        protected override IEnumerable<Type> ValueConverterHolders
        {
            get
            {
                return base.ValueConverterHolders
                    .Concat(new[]
                    {
                        typeof(AppConverters)
                    });
            }
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}
