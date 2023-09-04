using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PropertyGridControl.Base;

namespace PropertyGridControl.Controls
{
    public class NumericUpDownGridItem : BaseTextTypedGridItem<double, TextBox, NumericUpDownGridItem>
    {
        public readonly Regex NumericString = new Regex("^-*\\d+.*\\d+$");
        public readonly Regex NumericSymbols = new Regex("[-\\d.]");

        public readonly Button Button_IncreaseValue = new Button();
        public readonly Button Button_DecreaseValue = new Button();

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.RegisterAttached("MinValue", typeof(double), typeof(NumericUpDownGridItem), new PropertyMetadata(double.NegativeInfinity));
        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.RegisterAttached("MaxValue", typeof(double), typeof(NumericUpDownGridItem), new PropertyMetadata(double.NegativeInfinity));
        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public static readonly DependencyProperty IncrementProperty =
            DependencyProperty.RegisterAttached("Increment", typeof(double), typeof(NumericUpDownGridItem), new PropertyMetadata(1d));
        public double Increment
        {
            get => (double)GetValue(IncrementProperty);
            // I thought about restricting increment to MaxValue or to only allow positive values. I think, that the programmer should
            // take care of what they wants to do.
            set => SetValue(IncrementProperty, value);
        }

        public static readonly DependencyProperty PrecisionProperty =
            DependencyProperty.RegisterAttached("Precision", typeof(uint), typeof(NumericUpDownGridItem), new PropertyMetadata((uint)0));
        public int Precision
        {
            get => (int)GetValue(PrecisionProperty);
            set
            {
                // Since floating point with double precision only support an exact accuracy up to 15 places, we reduce it to 15.
                if (value > 15)
                    value = 15;

                SetValue(PrecisionProperty, value);
            }
        }
        public new double Value
        {
            get => base.Value;
            set
            {
                // NaN and Infinity shouldn't be allowed.
                if (double.IsNaN(value) || double.IsInfinity(value) || double.IsNegativeInfinity(value))
                    return;
                if (value < MinValue)
                    value = MinValue;
                if (value > MaxValue)
                    value = MaxValue;
                base.Value = value;
            }
        }

        public NumericUpDownGridItem() : base($"NumericUpDownGridItem{(Items.Count > 0 ? (Items.Count + 1).ToString() : string.Empty)}")
        {
            ValueControlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1d, GridUnitType.Star) });
            ValueControlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30d, GridUnitType.Pixel) });

            Grid buttongrid = new Grid();
            ValueControlGrid.Children.Add(buttongrid);
            Grid.SetColumn(buttongrid, 1);
            Grid.SetColumnSpan(buttongrid, 1);
            buttongrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1d, GridUnitType.Star) });
            buttongrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1d, GridUnitType.Star) });

            buttongrid.Children.Add(Button_IncreaseValue);
            Grid.SetRow(Button_IncreaseValue, 0);
            Grid.SetRowSpan(Button_IncreaseValue, 1);
            buttongrid.Children.Add(Button_DecreaseValue);
            Grid.SetRow(Button_DecreaseValue, 1);
            Grid.SetRowSpan(Button_DecreaseValue, 1);

            Button_IncreaseValue.Click += OnButtonIncreaseValue_Click;
            Button_DecreaseValue.Click += OnButtonDecreaseValue_Click;

            ValueControl.PreviewTextInput += OnTextBoxContent_PreviewInput;
            ValueControl.PreviewKeyDown += OnTextBoxContent_KeyDown;
            ValueControl.TextChanged += OnTextBoxContent_TextChanged;
        }

        protected override void SetBaseGridItemBindings()
        {
            base.SetBaseGridItemBindings();

            SetBinding("ContentFontSize", this, ValueControl, TextBox.FontSizeProperty);
            SetBinding("ContentFontWeight", this, ValueControl, TextBox.FontWeightProperty);
            SetBinding("ContentFontFamily", this, ValueControl, TextBox.FontFamilyProperty);
            SetBinding("ContentFontStretch", this, ValueControl, TextBox.FontStretchProperty);
            SetBinding("ContentFontStyle", this, ValueControl, TextBox.FontStyleProperty);
            SetBinding("ContentBackground", this, ValueControl, TextBox.BackgroundProperty);
            SetBinding("ContentForeground", this, ValueControl, TextBox.ForegroundProperty);
            SetBinding("ContentBorderBrush", this, ValueControl, TextBox.BorderBrushProperty);
            SetBinding("ContentBorderThickness", this, ValueControl, TextBox.BorderThicknessProperty);
        }

        public override void OnValueChanged(object sender, double e)
        {
            ValueControl.Text = e.ToString();
        }

        protected virtual void OnButtonIncreaseValue_Click(object sender, RoutedEventArgs e)
        {
            Value += Increment;
        }

        protected virtual void OnButtonDecreaseValue_Click(object sender, RoutedEventArgs e)
        {
            Value -= Increment;
        }

        protected virtual void OnTextBoxContent_PreviewInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
            string text = e.Text;

            if (text.EndsWith("."))
                text = text.Substring(text.Length - 1);

            if (double.TryParse(text, out double parsedvalue))
                Value = parsedvalue;
        }

        protected virtual void OnTextBoxContent_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            string symbol = e.Key.ToString();
            if (!NumericSymbols.IsMatch(symbol))
                return;

            string currentvalue = Value.ToString();
            currentvalue.Insert(ValueControl.CaretIndex, symbol);
            if (double.TryParse(currentvalue, out double parsedvalue))
                Value = parsedvalue;
        }

        protected virtual void OnTextBoxContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.Handled= true;
            string text = ValueControl.Text;

            if (text.EndsWith("."))
                text = text.Substring(0, text.Length - 1);

            int precision = text.Contains('.') ? Precision + 1 : Precision;

            if (text.Length > precision)
                text.Substring(0, precision);

            if (double.TryParse(text, out double parsedvalue))
                Value = parsedvalue;
        }
    }
}
