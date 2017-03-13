using System;
using Android.Content;
using KeketteTravel.Presentation.PlatformIntegration;

namespace KeketteTravel.Droid.PlatformIntegration
{
    public class ShareService : IShareService
    {
        private readonly Context _context;

        public ShareService(IAndroidContext androidContext)
        {
            _context = androidContext.Context;
        }

        public void OpenBrowser(string url)
        {
            var uri = Android.Net.Uri.Parse(url);
            var intent = new Intent(Intent.ActionView, uri);
            _context.StartActivity(intent);
        }
    }
}
