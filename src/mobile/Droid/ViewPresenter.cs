using System;
using System.Collections.Generic;
using System.Reflection;
using KeketteTravel.Presentation.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Shared.Presenter;

namespace KeketteTravel.Droid
{
    public class ViewPresenter : MvxFragmentsPresenter
    {
        private readonly Type[] _rootViewModelTypes = new[] {
            typeof(HomeViewModel),
        };

        public ViewPresenter(IEnumerable<Assembly> androidViewAssemblies)
            : base(androidViewAssemblies)
        {
        }

        protected override Android.Content.Intent CreateIntentForRequest(MvxViewModelRequest request)
        {
            var intent = base.CreateIntentForRequest(request);

            intent.SetFlags(intent.Flags ^ Android.Content.ActivityFlags.NewTask);

            if (Array.IndexOf(_rootViewModelTypes, request.ViewModelType) >= 0)
            {
                intent.AddFlags(Android.Content.ActivityFlags.ClearTop);
            }

            return intent;
        }
    }
}
