using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Xml.Linq;

namespace PropertyGridControl.Base
{
    public abstract class BaseGridItem : Grid
    {
        private string _name;
        private Label _nameLabel = new Label();
        private ColumnDefinition _columnDefinitionLabel;
        private ColumnDefinition _columnDefinitionValue;
        private GridControl _gridParent;

        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.RegisterAttached("LabelFontSize", typeof(double), typeof(BaseGridItem), new PropertyMetadata(12d));
        public double LabelFontSize
        {
            get => (double)GetValue(LabelFontSizeProperty);
            set => SetValue(LabelFontSizeProperty, value);
        }

        public static readonly DependencyProperty LabelFontWeightProperty =
            DependencyProperty.RegisterAttached("LabelFontWeight", typeof(FontWeight), typeof(BaseGridItem), new PropertyMetadata(FontWeights.Normal));
        public FontWeight LabelFontWeight
        {
            get => (FontWeight)GetValue(LabelFontWeightProperty);
            set => SetValue(LabelFontWeightProperty, value);
        }

        public static readonly DependencyProperty LabelFontFamilyProperty =
            DependencyProperty.RegisterAttached("LabelFontFamily", typeof(FontFamily), typeof(BaseGridItem), new PropertyMetadata(new FontFamily("Segoe UI")));
        public FontFamily LabelFontFamily
        {
            get => (FontFamily)GetValue(LabelFontFamilyProperty);
            set => SetValue(LabelFontFamilyProperty, value);
        }

        public static readonly DependencyProperty LabelFontStretchProperty =
            DependencyProperty.RegisterAttached("LabelFontStretch", typeof(FontStretch), typeof(BaseGridItem), new PropertyMetadata(FontStretches.Normal));
        public FontStretch LabelFontStretch
        {
            get => (FontStretch)GetValue(LabelFontStretchProperty);
            set => SetValue(LabelFontStretchProperty, value);
        }

        public static readonly DependencyProperty LabelFontStyleProperty =
            DependencyProperty.RegisterAttached("LabelFontStyle", typeof(FontStyle), typeof(BaseGridItem), new PropertyMetadata(FontStyles.Normal));
        public FontStyle LabelFontStyle
        {
            get => (FontStyle)GetValue(LabelFontStyleProperty);
            set => SetValue(LabelFontStyleProperty, value);
        }

        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.RegisterAttached("LabelText", typeof(string), typeof(BaseGridItem), new PropertyMetadata(default(string)));
        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.RegisterAttached("LabelWidth", typeof(double), typeof(BaseGridItem), new PropertyMetadata(double.NaN));
        public double LabelWidth
        {
            get => (double)GetValue(LabelWidthProperty);
            set => SetValue(LabelWidthProperty, value);
        }

        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.RegisterAttached("Foreground", typeof(Brush), typeof(BaseGridItem), new PropertyMetadata(Brushes.Black));
        public Brush Foreground
        {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(BaseGridItem), new PropertyMetadata(false));
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public string PropertyName
        {
            get => _name;
            set
            {
                if (value != _name && !string.IsNullOrWhiteSpace(value))
                    _name = value;
            }
        }
        public GridControl GridParent
        {
            get => _gridParent;
            set
            {
                if (_gridParent != value)
                {
                    Debug.Print($"BaseGridItem({value})");
                    _gridParent = value;
                    SetGridParentBindings();
                }
            }
        }

        protected BaseGridItem(string name)
        {
            Debug.Print($"BaseGridItem({name})");

            _columnDefinitionLabel = new ColumnDefinition() { Width = new GridLength(50d, GridUnitType.Pixel) };
            _columnDefinitionValue = new ColumnDefinition() { Width = new GridLength(0d, GridUnitType.Auto) };

            ColumnDefinitions.Add(_columnDefinitionLabel);
            ColumnDefinitions.Add(_columnDefinitionValue);

            _nameLabel.Name = "Label_Name";
            this.Children.Add(_nameLabel);
            Grid.SetColumn(_nameLabel, 0);
            Grid.SetColumnSpan(_nameLabel, 1);
            Debug.Print($"Set Name Label: {_nameLabel.Content}");

            PropertyName = name;
            this.Height = 30d;

            SetBaseGridItemBindings();
        }

        protected virtual void SetBaseGridItemBindings()
        {
            Debug.Print("SetBaseGridItemBindings()");

            SetBinding("LabelFontSize", this, _nameLabel, Label.FontSizeProperty);
            SetBinding("LabelFontWeight", this, _nameLabel, Label.FontWeightProperty);
            SetBinding("LabelFontFamily", this, _nameLabel, Label.FontFamilyProperty);
            SetBinding("LabelFontStretch", this, _nameLabel, Label.FontStretchProperty);
            SetBinding("LabelFontStyle", this, _nameLabel, Label.FontStyleProperty);
            SetBinding("LabelText", this, _nameLabel, Label.ContentProperty);
            SetBinding("LabelWidth", this, _columnDefinitionLabel, ColumnDefinition.WidthProperty);

            SetBinding("Foreground", this, _nameLabel, Label.ForegroundProperty);
        }

        protected virtual void SetGridParentBindings()
        {
            Debug.Print("SetGridParentBindings()");

            SetBinding("LabelFontSize", _gridParent, this, LabelFontSizeProperty);
            SetBinding("LabelFontWeight", _gridParent, this, LabelFontWeightProperty);
            SetBinding("LabelFontFamily", _gridParent, this, LabelFontFamilyProperty);
            SetBinding("LabelFontStretch", _gridParent, this, LabelFontStretchProperty);
            SetBinding("LabelFontStyle", _gridParent, this, LabelFontStyleProperty);
            SetBinding("LabelWidth", _gridParent, this, LabelWidthProperty);

            SetBinding("PropertyForeground", _gridParent, this, ForegroundProperty);
            SetBinding("PropertyBackground", _gridParent, this, BackgroundProperty);
        }

        public static void SetBinding(string name, object source, FrameworkElement targetControl, DependencyProperty targetProperty)
        {
            Binding binding = new Binding(name);
            binding.Source = source;
            targetControl.SetBinding(targetProperty, binding);
        }

        public static void SetBinding(string name, object source, FrameworkContentElement targetControl, DependencyProperty targetProperty)
        {
            Binding binding = new Binding(name);
            binding.Source = source;
            targetControl.SetBinding(targetProperty, binding);
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);

            if (Parent.GetType() == typeof(GridControl))
                GridParent = (GridControl)Parent;
        }
    }
}
