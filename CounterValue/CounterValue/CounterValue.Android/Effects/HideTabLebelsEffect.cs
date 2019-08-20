﻿using System.Linq;
using Android.Support.Design.BottomNavigation;
using Android.Support.Design.Widget;
using CounterValue.Droid.Effects;
using CounterValue.Droid.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportEffect(typeof(HideTabLabelsEffect), nameof(HideTabLabelsEffect))]

namespace CounterValue.Droid.Effects
{
    public class HideTabLabelsEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var renderer = (Control ?? Container) as TabbedPageRenderer;

            var children = renderer?.ViewGroup?.RetrieveAllChildViews();
            if (children?.FirstOrDefault(x => x is BottomNavigationView) is BottomNavigationView bottomNav)
            {
                bottomNav.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilityUnlabeled;
            }
        }

        protected override void OnDetached()
        {
        }
    }
}