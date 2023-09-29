using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PropertyGridControl.Base;

namespace PropertyGridControl.Controls
{
    public class NumericUpDownGridItem : BaseTextTypedGridItem<double, TextBox, NumericUpDownGridItem>
    {
        private readonly Key[] _validKeys = new Key[] {
            Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3, Key.NumPad4, Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9,
            Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9,
            Key.OemPeriod, Key.OemComma, Key.Separator, Key.Decimal,
            Key.OemMinus, Key.Subtract, Key.OemPlus, Key.Add,
            Key.Delete, Key.Back, Key.Tab, Key.Enter,
            Key.Left, Key.Right, Key.Up, Key.Down, Key.Home, Key.End,
            Key.A, Key.C, Key.V
        };

        private readonly char _decimalSeparationChar = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        
        public readonly Regex NumericString = new Regex("^-*\\d+[\\.,]*\\d+$");
        public readonly Regex NumericSymbols = new Regex("[-\\d\\.,]");

        public readonly Button Button_IncreaseValue = new Button();
        public readonly Button Button_DecreaseValue = new Button();

        public event EventHandler<double> MinValueChanged;
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.RegisterAttached("MinValue", typeof(double), typeof(NumericUpDownGridItem), new PropertyMetadata(double.MinValue));
        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set
            {
                SetValue(MinValueProperty, value);
                MinValueChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<double> MaxValueChanged;
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.RegisterAttached("MaxValue", typeof(double), typeof(NumericUpDownGridItem), new PropertyMetadata(double.MaxValue));
        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set
            {
                SetValue(MaxValueProperty, value);
                MaxValueChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<double> IncrementChanged;
        public static readonly DependencyProperty IncrementProperty =
            DependencyProperty.RegisterAttached("Increment", typeof(double), typeof(NumericUpDownGridItem), new PropertyMetadata(1d));
        public double Increment
        {
            get => (double)GetValue(IncrementProperty);
            // I thought about restricting increment to MaxValue or to only allow positive values. I think, that the programmer should
            // take care of what they wants to do.
            set
            {
                SetValue(IncrementProperty, value);
                IncrementChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<int> PrecisionChanged;
        public static readonly DependencyProperty PrecisionProperty =
            DependencyProperty.RegisterAttached("Precision", typeof(int), typeof(NumericUpDownGridItem), new PropertyMetadata(0));
        public int Precision
        {
            get => (int)GetValue(PrecisionProperty);
            set
            {
                // Since floating point with double precision only support an exact accuracy up to 15 digits, we reduce it to 12.
                if (value > 12)
                    value = 12;
                else if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                SetValue(PrecisionProperty, value);
                PrecisionChanged?.Invoke(this, value);
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

            ValueControl.PreviewKeyDown += OnTextBoxContent_PreviewKeyDown;
            // ValueControl.TextChanged += OnTextBoxContent_TextChanged;

            ValueChanged += OnValueChanged;
        }

        protected virtual void OnValueChanged(object sender, double newValue)
        {
            if (double.IsNaN(newValue))
                throw new ArgumentOutOfRangeException(nameof(newValue));
            if (newValue < MinValue || double.IsNegativeInfinity(newValue))
            {
                InputHandled = false;
                Value = MinValue;
            }
            else if (newValue > MaxValue || double.IsInfinity(newValue))
            {
                InputHandled = false;
                Value = MaxValue;
            }
        }

        protected virtual void OnButtonIncreaseValue_Click(object sender, RoutedEventArgs e)
        {
            Debug.Print($"OnButtonIncreaseValue_Click(sender, e) -> base.Value: '{base.Value}', Value: '{Value}', Increment '{Increment}'");
            InputHandled = false;
            Value += Increment;
        }

        protected virtual void OnButtonDecreaseValue_Click(object sender, RoutedEventArgs e)
        {
            Debug.Print($"OnButtonDecreaseValue_Click(sender, e) -> base.Value: '{base.Value}', Value: '{Value}', Increment '{Increment}'");
            InputHandled = false;
            Value -= Increment;
        }

        protected virtual void OnTextBoxContent_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Debug.Print($"OnTextBoxContent_PreviewKeyDown(sender, {e.Key})");

            string currenttext = ValueControl.Text;
            int currentcaretpos = ValueControl.CaretIndex;

            if (!_validKeys.Contains(e.Key))
            {
                e.Handled = true;
                return;
            }
            else if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                Button_IncreaseValue.Focus();
                ValueControl.Text = Value.ToString();
                e.Handled = e.Key == Key.Enter;
                return;
            }
            else if (e.Key == Key.OemMinus || e.Key == Key.Subtract)
            {
                if (!currenttext.StartsWith("-"))
                {
                    currentcaretpos++;
                    currenttext = "-" + currenttext;
                    e.Handled = true;
                }
            }
            else if (e.Key == Key.OemPlus || e.Key == Key.Add)
            {
                if (currenttext.StartsWith("-"))
                {
                    currentcaretpos--;
                    currenttext = currenttext.Substring(1);
                    e.Handled = true;
                }
            }
            else
                return;

            InputHandled = false;

            ValueControl.Text = currenttext;
            ValueControl.CaretIndex = currentcaretpos;
        }

        protected virtual void OnTextBoxContent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string currentText = ValueControl.Text;
            int currentCaretPos = ValueControl.CaretIndex;

            string inputText = e.Text;

            // Check if the input is a valid character
            if (!IsValidInputCharacter(inputText))
            {
                e.Handled = true;
                return;
            }

            // Determine if the input contains a decimal separator
            bool containsDecimalSeparator = currentText.Contains(_decimalSeparationChar.ToString());

            // Construct the new text by inserting the input at the caret position
            string newText = currentText.Substring(0, currentCaretPos) + inputText +
                             currentText.Substring(currentCaretPos + ValueControl.SelectionLength);

            // Normalize the text to use the current culture's decimal separator
            newText = newText.Replace('.', _decimalSeparationChar).Replace(',', _decimalSeparationChar);

            if (double.TryParse(newText, out double parsedValue))
            {
                // Ensure the parsed value is within bounds
                parsedValue = Math.Max(MinValue, Math.Min(MaxValue, parsedValue));

                // If the input contained a decimal separator, ensure it's retained
                if (containsDecimalSeparator)
                {
                    string[] parts = newText.Split(_decimalSeparationChar);
                    if (parts.Length == 2)
                    {
                        int precision = Math.Min(parts[1].Length, Precision);
                        newText = parts[0] + _decimalSeparationChar + parts[1].Substring(0, precision);
                    }
                }

                InputHandled = true;
                Value = parsedValue;
            }

            ValueControl.Text = newText;
            ValueControl.CaretIndex = currentCaretPos + inputText.Length;

            e.Handled = true;
        }

        private bool IsValidInputCharacter(string input)
        {
            // Only allow valid numeric and formatting characters
            return NumericSymbols.IsMatch(input);
        }

        //protected virtual void OnTextBoxContent_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    Debug.Print($"OnTextBoxContent_TextChanged(sender, {ValueControl.Text})");

        //    if (e.Handled || InputHandled)
        //    {
        //        InputHandled = false;
        //        return;
        //    }

        //    string text = ValueControl.Text;
        //    if (text == Value.ToString())
        //        return;

        //    int currentcaretpos = ValueControl.CaretIndex;
        //    bool focused = ValueControl.IsFocused;
        //    text = text.Replace('.', _decimalSeparationChar).Replace(',', _decimalSeparationChar);

        //    if (text.Contains(_decimalSeparationChar))
        //    {
        //        if (text.Count(character => character == _decimalSeparationChar) > 1)
        //        {
        //            bool firstdecimalfound = false;
        //            string temptext = "";
        //            foreach (char c in text)
        //            {
        //                if (!firstdecimalfound && c == _decimalSeparationChar)
        //                {
        //                    firstdecimalfound = true;
        //                    temptext += c;
        //                }
        //                else if (c != _decimalSeparationChar)
        //                {
        //                    temptext += c;
        //                }
        //            }
        //            text = temptext;
        //        }

        //        string[] splittedvalue = text.Split(_decimalSeparationChar);
        //        splittedvalue[1] = splittedvalue[1].Substring(0, Math.Min(Precision, splittedvalue[1].Length));
        //        if (splittedvalue[1].Length > 0)
        //            text = string.Join(_decimalSeparationChar.ToString(), splittedvalue);
        //        else if (focused)
        //            text = splittedvalue[0] + _decimalSeparationChar;
        //    }

        //    if (double.TryParse(text, out double parsedvalue))
        //    {
        //        if (parsedvalue < MinValue)
        //        {
        //            parsedvalue = MinValue;
        //            text = parsedvalue.ToString();
        //        }
        //        else if (parsedvalue > MaxValue)
        //        {
        //            parsedvalue = MaxValue;
        //            text = parsedvalue.ToString();
        //        }

        //        if (Value != parsedvalue)
        //        {
        //            InputHandled = true;
        //            Value = parsedvalue;
        //        }
        //    }

        //    if (ValueControl.Text != text)
        //    {
        //        ValueControl.Text = text;
        //        ValueControl.CaretIndex = currentcaretpos;
        //    }

        //    e.Handled = true;
        //}
    }
}
