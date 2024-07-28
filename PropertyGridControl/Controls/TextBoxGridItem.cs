using PropertyGridControl.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PropertyGridControl.Controls
{
    public class TextBoxGridItem : BaseTextTypedGridItem<string, TextBox, TextBoxGridItem>
    {
        private readonly HashSet<char> _allowedChars = [];
        private bool _isFiltering = true;

        public event EventHandler<string>? CharFilterChanged;
        public static readonly DependencyProperty CharFilterProperty =
            DependencyProperty.RegisterAttached("CharFilter", typeof(string), typeof(TextBoxGridItem),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));
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

        public event EventHandler<bool>? IsReadonlyChanged;
        public static readonly DependencyProperty IsReadonlyProperty =
            DependencyProperty.RegisterAttached("IsReadonly", typeof(bool), typeof(TextBoxGridItem),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
        public bool IsReadonly
        {
            get => (bool)GetValue(IsReadonlyProperty);
            set
            {
                SetValue(IsReadonlyProperty, value);
                IsReadonlyChanged?.Invoke(this, value);
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
