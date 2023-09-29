using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PropertyGridControl.Base;
using System.Collections.Generic;

namespace PropertyGridControl.Controls
{
    public class TextBoxGridItem : BaseTextTypedGridItem<string, TextBox, TextBoxGridItem>
    {
        private readonly HashSet<char> _allowedChars = new HashSet<char>();
        private bool _isFiltering = true;

        public event EventHandler<string> CharFilterChanged;
        public static readonly DependencyProperty CharFilterProperty =
            DependencyProperty.RegisterAttached("CharFilter", typeof(string), typeof(TextBoxGridItem), new PropertyMetadata(string.Empty));
        public string CharFilter
        {
            get => (string)GetValue(CharFilterProperty);
            set
            {
                SetValue(CharFilterProperty, value);
                CharFilterChanged?.Invoke(this, value);
                InitializeAllowedChars(value);
            }
        }

        public TextBoxGridItem() : base($"TextBoxGridItem{(Items.Count > 0 ? (Items.Count + 1).ToString() : string.Empty)}")
        {
            Debug.Print($"TextBoxGridItem::TextBoxGridItem()");

            ValueControl.PreviewTextInput += OnTextBoxContent_PreviewTextInput;
        }

        private void InitializeAllowedChars(string charFilter)
        {
            _allowedChars.Clear();
            foreach (char c in charFilter)
                _allowedChars.Add(c);
        }

        public virtual void OnTextBoxContent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (_isFiltering)
            {
                e.Handled = true;
                return;
            }

            string text = e.Text;
            string charfilter = CharFilter;

            foreach (char c in charfilter)
                text = text.Replace(c.ToString(), string.Empty);

            if (!string.IsNullOrEmpty(text))
            {
                _isFiltering = true;
                ValueControl.Text = ValueControl.Text.Insert(ValueControl.CaretIndex, e.Text);
                ValueControl.CaretIndex += text.Length;
                _isFiltering = false;
                e.Handled = true;
            }
        }
    }
}
