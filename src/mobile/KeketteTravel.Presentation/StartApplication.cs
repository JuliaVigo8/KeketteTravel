using System;
using KeketteTravel.Presentation.ViewModels;
using MvvmCross.Core.ViewModels;

namespace KeketteTravel.Presentation
{
    public class StartApplication : MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {
            ShowViewModel<HomeViewModel>();
        }
    }
}
