using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PropertyGridControl.Base;

namespace PropertyGridControl.Controls
{
    public class TextBoxGridItem : BaseTextTypedGridItem<string, TextBox, TextBoxGridItem>
    {
        public event EventHandler<string> CharFilterChanged;
        public static readonly DependencyProperty CharFilterProperty =
            DependencyProperty.RegisterAttached("CharFilter", typeof(string), typeof(TextBoxGridItem), new PropertyMetadata(string.Empty));
        public string CharFilter
        {
            get => (string)GetValue(CharFilterProperty);
            set
            {
                // Since floating point with double precision only support an exact accuracy up to 15 digits, we reduce it to 12.
                SetValue(CharFilterProperty, value);
                CharFilterChanged?.Invoke(this, value);
            }
        }

        public TextBoxGridItem() : base($"TextBoxGridItem{(Items.Count > 0 ? (Items.Count + 1).ToString() : string.Empty)}")
        {
            Debug.Print($"TextBoxGridItem::TextBoxGridItem()");

            ValueControl.PreviewTextInput += OnTextBoxContent_PreviewTextInput;
        }

        public virtual void OnTextBoxContent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string text = e.Text;
            string charfilter = CharFilter;
            bool inputchanged = false;

            foreach (char c in charfilter)
            {
                for (int index = text.Length - 1; index >= 0; index--)
                {
                    if (text[index] == c)
                    {
                        text = text.Remove(index, 1);
                        if (!inputchanged)
                            inputchanged = true;
                    }
                }
            }

            if (inputchanged)
            {
                ValueControl.PreviewTextInput -= OnTextBoxContent_PreviewTextInput;
                ValueControl.Text.Insert(ValueControl.CaretIndex, text);
                ValueControl.CaretIndex += text.Length;
                ValueControl.PreviewTextInput += OnTextBoxContent_PreviewTextInput;
                e.Handled = true;
            }
        }
    }
}
