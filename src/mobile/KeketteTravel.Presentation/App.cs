using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace KeketteTravel.Presentation
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.LazyConstructAndRegisterSingleton<IMvxAppStart, StartApplication>();
            PresentationModule.Init();
        }
    }
}
