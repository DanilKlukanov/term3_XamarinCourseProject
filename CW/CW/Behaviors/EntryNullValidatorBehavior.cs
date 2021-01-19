using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CW.Behaviors
{
    class EntryNullValidatorBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            var text = e.NewTextValue;
            if (entry.Text != null)
            {
                if (text.Length > 1 && text[0] == '0' && text[1] == '0')
                {
                    entry.Text = e.OldTextValue;
                }
            }
        }
    }
}
