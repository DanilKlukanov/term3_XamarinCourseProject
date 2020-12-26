using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CW.Models
{
    public class BindableMap : Map
    {
        public static readonly BindableProperty FocusPositionProperty =
                     BindableProperty.Create(nameof(FocusPosition), typeof(Position), typeof(BindableMap), default(Position), propertyChanged: OnPropertyChanged);

        public Position FocusPosition
        {
            get => (Position)GetValue(FocusPositionProperty);
            set => SetValue(FocusPositionProperty, value);
        }

        private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisInstance = bindable as BindableMap;
            var newPosition = (Position)newValue;
            thisInstance?.MoveToRegion(MapSpan.FromCenterAndRadius(newPosition, Distance.FromKilometers(5)));
        }
    }
}
