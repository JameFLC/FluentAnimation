using CommunityToolkit.WinUI.Controls;
using FluentAnimation.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAnimation.Controls
{
    public partial class TriangleSlider : Control
    {
        /// <summary>
        /// Identifies the <see cref="First"/> property.
        /// </summary>
        public static readonly DependencyProperty FirstProperty =
        DependencyProperty.Register(
            nameof(First),
            typeof(double),
            typeof(TriangleSlider),
            new PropertyMetadata(DefaultFirst, FirstChangedCallback));

        /// <summary>
        /// Identifies the <see cref="Second"/> property.
        /// </summary>
        public static readonly DependencyProperty SecondProperty =
        DependencyProperty.Register(
            nameof(Second),
            typeof(double),
            typeof(TriangleSlider),
            new PropertyMetadata(DefaultSecond, SecondChangedCallback));

        /// <summary>
        /// Identifies the <see cref="Third"/> property.
        /// </summary>
        public static readonly DependencyProperty ThirdProperty =
        DependencyProperty.Register(
            nameof(Third),
            typeof(double),
            typeof(TriangleSlider),
            new PropertyMetadata(DefaultThird, ThirdChangedCallback));

        /// <summary>
        /// Gets or sets the absolute minimum value of the range.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        public double First
        {
            get => (double)GetValue(FirstProperty);
            set
            {
                if (!Double.IsNaN(value)) SetValue(FirstProperty, value.Clamped(0, 1));
            }
        }

        /// <summary>
        /// Gets or sets the absolute minimum value of the range.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        public double Second
        {
            get => (double)GetValue(SecondProperty);
            set
            {
                if (!Double.IsNaN(value)) SetValue(SecondProperty, value.Clamped(0, 1));
            }
        }

        /// <summary>
        /// Gets or sets the absolute minimum value of the range.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        public double Third
        {
            get => (double)GetValue(ThirdProperty);
            set
            {
                if (!Double.IsNaN(value)) SetValue(ThirdProperty, value.Clamped(0, 1));
            }
        }

        private enum ChangedValue
        {
            First,
            Second,
            Third
        }

        private static void FirstChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var triangleSlider = d as TriangleSlider;

            if (triangleSlider is null || !triangleSlider._valuesAssigned )
            {
                return;
            }
            if (!triangleSlider._valueUpdating)
            {
                triangleSlider.UpdateValues(ChangedValue.First);
            }
        }

        private static void SecondChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var triangleSlider = d as TriangleSlider;

            if (triangleSlider is null || !triangleSlider._valuesAssigned)
            {
                return;
            }

            double newValue = ((double)e.NewValue).Clamped(0, 1);
            if (!triangleSlider._valueUpdating)
            {
                triangleSlider.UpdateValues(ChangedValue.Second);
            }
        }

        private static void ThirdChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var triangleSlider = d as TriangleSlider;

            if (triangleSlider is null || !triangleSlider._valuesAssigned)
            {
                return;
            }

            if (!triangleSlider._valueUpdating)
            {
                triangleSlider.UpdateValues(ChangedValue.Third);
            }
        }

        private void UpdateValues(ChangedValue changedValue)
        {
            _valueUpdating = true;
            switch (changedValue)
            {
                case ChangedValue.First:
                    if (First == 1)
                    {
                        Second = 0;
                        Third = 0;
                        break;
                    }
                    if (Second == 0 && Third == 0)
                    {
                        Second = 1 - First;
                        break;
                    }
                    if (Second < Third)
                    {
                        Second = 0;
                        Third = 1 - First;
                        break;
                    }
                    else
                    {
                        Second = 1 - First;
                        Third = 0;
                        break;
                    }
                case ChangedValue.Second:
                    if (Second == 1)
                    {
                        Third = 0;
                        First = 0;
                        break;
                    }
                    if (Third == 0 && First == 0)
                    {
                        Third = 1 - Second;
                        break;
                    }
                    if (Third < First)
                    {
                        Third = 0;
                        First = 1 - Second;
                        break;
                    }
                    else
                    {
                        Third = 1 - Second;
                        First = 0;
                        break;
                    }
                case ChangedValue.Third:
                    if (Third == 1)
                    {
                        First = 0;
                        Second = 0;
                        break;
                    }
                    if (First == 0 && Second == 0)
                    {
                        First = 1 - Third;
                        break;
                    }
                    if (First < Second)
                    {
                        First = 0;
                        Second = 1 - Third;
                        break;
                    }
                    else
                    {
                        First = 1 - Third;
                        Second = 0;
                        break;
                    }
            }
            _valueUpdating = false;
            SyncThumb();
        }
    }
}