using System;
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

        }
    }
}
