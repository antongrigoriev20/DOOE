﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CounterValue
{
    /// <summary>
    /// Класс отвечает за изменения в строке поиска
    /// </summary>
    class TextChangedBehavior : Behavior<Xamarin.Forms.SearchBar>
    {
        protected override void OnAttachedTo(Xamarin.Forms.SearchBar bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += Bindable_TextChanged;
        }

        protected override void OnDetachingFrom(Xamarin.Forms.SearchBar bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((Xamarin.Forms.SearchBar)sender).SearchCommand?.Execute(e.NewTextValue);
        }

    }
}
