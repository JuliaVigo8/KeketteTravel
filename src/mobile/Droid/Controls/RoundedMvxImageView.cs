using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using MvvmCross.Droid.Support.V7.AppCompat.Widget;

namespace KeketteTravel.Droid.Controls
{
    [Register("keketteTravel.droid.controls.RoundedMvxImageView")]
    public class RoundedMvxImageView : MvxAppCompatImageView
    {
        [Register(".ctor", "(Landroid/content/Context;)V", "")]
        public RoundedMvxImageView(Context context)
            : base(context)
        {
        }

        [Register(".ctor", "(Landroid/content/Context;Landroid/util/AttributeSet;)V", "")]
        public RoundedMvxImageView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public override void SetImageBitmap(Bitmap bm)
        {
            if (bm != null)
            {
                Bitmap destBm = null;

                if (bm.Width >= bm.Height)
                {
                    destBm = Bitmap.CreateBitmap(
                       bm,
                       bm.Width / 2 - bm.Height / 2,
                       0,
                       bm.Height,
                       bm.Height);
                }
                else
                {
                    destBm = Bitmap.CreateBitmap(
                       bm,
                       0,
                       bm.Height / 2 - bm.Width / 2,
                       bm.Width,
                       bm.Width
                       );
                }

                var output = Bitmap.CreateBitmap(destBm.Width, destBm.Height, Bitmap.Config.Argb8888);
                var canvas = new Canvas(output);

                var paint = new Paint();
                paint.AntiAlias = true;
                paint.Color = Color.Black;

                canvas.DrawARGB(0, 0, 0, 0);
                canvas.DrawCircle(destBm.Width / 2, destBm.Height / 2, destBm.Width / 2, paint);
                paint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcIn));

                var rect = new Rect(0, 0, destBm.Width, destBm.Height);

                canvas.DrawBitmap(bm, rect, rect, paint);

                base.SetImageBitmap(output);
                return;
            }

            base.SetImageBitmap(bm);
        }
    }
}
