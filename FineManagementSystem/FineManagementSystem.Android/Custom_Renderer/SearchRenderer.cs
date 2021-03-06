using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FineManagementSystem.Custom_Renderer;
using FineManagementSystem.Droid.Custom_Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SearchEntry), typeof(SeachRenderer))]
namespace FineManagementSystem.Droid.Custom_Renderer
{
    public class SeachRenderer : EntryRenderer
    {
        public SeachRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(60f);
                gradientDrawable.SetStroke(5, Android.Graphics.Color.Black);
                gradientDrawable.SetStroke(5, Android.Graphics.Color.Transparent);
                Control.SetBackground(gradientDrawable);
                Control.SetPadding(20, 2, 2, 2);
                Control.SetWidth(350);


            }
        }
    }
}