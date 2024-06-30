using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace PropertyGridControl.Base
{
    // Event for changed properties:
    // Though a PropertyChanged event is already implemented in Control, it seems more intuitive to define a Changed event
    // for every property itself, so the user can directly access each properties event without having to filter the specific
    // property from within the OnPropertyChanged function (or any custom appended function to the PropertyChanged event).

    public abstract class BaseTypedGridItem<T1, T2, T3> : Grid
        where T1 : notnull
        where T2 : Control, new()
        where T3 : BaseTypedGridItem<T1, T2, T3>
    {
        public static List<T3> Items = [];

        private string _name = string.Empty;
        private Label _nameLabel = new();
        private ColumnDefinition _columnDefinitionLeftMargin = new() { Width = new GridLength(0d, GridUnitType.Pixel) };
        private ColumnDefinition _columnDefinitionRightMargin = new() { Width = new GridLength(0d, GridUnitType.Pixel) };
        private RowDefinition _rowDefinitionTopMargin = new() { Height = new GridLength(0d, GridUnitType.Pixel) };
        private RowDefinition _rowDefinitionBottomMargin = new() { Height = new GridLength(0d, GridUnitType.Pixel) };
        private ColumnDefinition _columnDefinitionLabel = new() { Width = new GridLength(50d, GridUnitType.Pixel) };
        private ColumnDefinition _columnDefinitionValue = new() { Width = new GridLength(50d, GridUnitType.Pixel) };
        private RowDefinition _rowDefinitionContent = new() { Height = new GridLength(1d, GridUnitType.Star) };
        private GridControl? _gridParent = null;
        private T1 _originalValue = default;
        private bool _originalValueSet = false;
        private bool _valueSet = false;

        protected T2? _valueControl;

        public string PropertyName
        {
            get => _name;
            set
            {
                if (value != _name && !string.IsNullOrWhiteSpace(value))
                    _name = value;
            }
        }

        public event EventHandler<GridControl>? GridParentChanged;
        public GridControl? GridParent
        {
            get => _gridParent;
            private set
            {
                if (_gridParent != value)
                {
                    _gridParent = value;
                    SetGridParentBindings();
                    if (value != null)
                        GridParentChanged?.Invoke(this, value);
                }
            }
        }

        public event EventHandler<double>? LabelFontSizeChanged;
        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.RegisterAttached("LabelFontSize", typeof(double), typeof(T3), new PropertyMetadata(12d));
        public double LabelFontSize
        {
            get => (double)GetValue(LabelFontSizeProperty);
            set
            {
                SetValue(LabelFontSizeProperty, value);
                LabelFontSizeChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<FontWeight>? LabelFontWeightChanged;
        public static readonly DependencyProperty LabelFontWeightProperty =
            DependencyProperty.RegisterAttached("LabelFontWeight", typeof(FontWeight), typeof(T3), new PropertyMetadata(FontWeights.Normal));
        public FontWeight LabelFontWeight
        {
            get => (FontWeight)GetValue(LabelFontWeightProperty);
            set
            {
                SetValue(LabelFontWeightProperty, value);
                LabelFontWeightChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<FontFamily>? LabelFontFamilyChanged;
        public static readonly DependencyProperty LabelFontFamilyProperty =
            DependencyProperty.RegisterAttached("LabelFontFamily", typeof(FontFamily), typeof(T3), new PropertyMetadata(new FontFamily("Segoe UI")));
        public FontFamily LabelFontFamily
        {
            get => (FontFamily)GetValue(LabelFontFamilyProperty);
            set
            {
                SetValue(LabelFontFamilyProperty, value);
                LabelFontFamilyChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<FontStretch>? LabelFontStretchChanged;
        public static readonly DependencyProperty LabelFontStretchProperty =
            DependencyProperty.RegisterAttached("LabelFontStretch", typeof(FontStretch), typeof(T3), new PropertyMetadata(FontStretches.Normal));
        public FontStretch LabelFontStretch
        {
            get => (FontStretch)GetValue(LabelFontStretchProperty);
            set
            {
                SetValue(LabelFontStretchProperty, value);
                LabelFontStretchChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<FontStyle>? LabelFontStyleChanged;
        public static readonly DependencyProperty LabelFontStyleProperty =
            DependencyProperty.RegisterAttached("LabelFontStyle", typeof(FontStyle), typeof(T3), new PropertyMetadata(FontStyles.Normal));
        public FontStyle LabelFontStyle
        {
            get => (FontStyle)GetValue(LabelFontStyleProperty);
            set
            {
                SetValue(LabelFontStyleProperty, value);
                LabelFontStyleChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<string>? LabelTextChanged;
        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.RegisterAttached("LabelText", typeof(string), typeof(T3), new PropertyMetadata(default(string)));
        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set
            {
                SetValue(LabelTextProperty, value);
                LabelTextChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<GridLength>? LabelWidthChanged;
        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.RegisterAttached("LabelWidth", typeof(GridLength), typeof(T3), new PropertyMetadata(new GridLength(0d, GridUnitType.Star)));
        public GridLength LabelWidth
        {
            get => (GridLength)GetValue(LabelWidthProperty);
            set
            {
                SetValue(LabelWidthProperty, value);
                LabelWidthChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<GridLength>? ContentWidthChanged;
        public static readonly DependencyProperty ContentWidthProperty =
            DependencyProperty.RegisterAttached("ContentWidth", typeof(GridLength), typeof(T3), new PropertyMetadata(new GridLength(0d, GridUnitType.Star)));
        public GridLength ContentWidth
        {
            get => (GridLength)GetValue(ContentWidthProperty);
            set
            {
                SetValue(ContentWidthProperty, value);
                ContentWidthChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<Brush>? ForegroundChanged;
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.RegisterAttached("Foreground", typeof(Brush), typeof(T3), new PropertyMetadata(Brushes.Black));
        public Brush Foreground
        {
            get => (Brush)GetValue(ForegroundProperty);
            set
            {
                SetValue(ForegroundProperty, value);
                ForegroundChanged?.Invoke(this, value);
            }
        }

        // BackgroundProperty already exists on the base control (Grid), so we don't need to register the DependencyProperty, but
        // need to create a new Getter and Setter, to be able to use the BackgroundChanged event.
        public event EventHandler<Brush>? BackgroundChanged;
        public new Brush Background
        {
            get => base.Background;
            set
            {
                base.Background = value;
                BackgroundChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<bool>? IsSelectedChanged;
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(T3), new PropertyMetadata(false));
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set
            {
                SetValue(IsSelectedProperty, value);
                IsSelectedChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<Thickness>? PropertyMarginChanged;
        public static readonly DependencyProperty PropertyMarginProperty =
            DependencyProperty.RegisterAttached("PropertyMargin", typeof(Thickness), typeof(T3), new PropertyMetadata(new Thickness(0d)));
        public Thickness PropertyMargin
        {
            get => (Thickness)GetValue(PropertyMarginProperty);
            set
            {
                SetValue(PropertyMarginProperty, value);
                PropertyMarginChanged?.Invoke(this, value);
            }
        }

        // The implementation of value is only basic and in most cases needs an override to handle the Control
        // dependant limitations, which is why it's declared virtual.
        public event EventHandler<T1>? ValueChanged;
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached("Value", typeof(T1), typeof(T3), new PropertyMetadata(default(T1)));
        public virtual T1 Value
        {
            get => (T1)GetValue(ValueProperty);
            set
            {
                T1 currentvalue = (T1)GetValue(ValueProperty);
                if (!value.Equals(currentvalue) && value != null)
                {
                    SetValue(ValueProperty, value);
                    _valueSet = true;
                    if (!_originalValueSet)
                    {
                        _originalValue = value;
                        _originalValueSet = true;
                    }
                    ValueChanged?.Invoke(this, value);
                }
            }
        }

        // We don't define an DefaultPropertyChanged event, since it doesn't seem reasonable to change a default
        // value during runtime, so there shouldn't be any preimplemented event triggered for changes to this.
        public static readonly DependencyProperty DefaultValueProperty =
            DependencyProperty.RegisterAttached("DefaultValue", typeof(T1), typeof(T3), new PropertyMetadata(default(T1)));
        public T1 DefaultValue
        {
            get => (T1)GetValue(DefaultValueProperty);
            set
            {
                SetValue(DefaultValueProperty, value);
                // We don't refer to 'this' for value, since an override might use additional functions or
                // limitations, which is why we want them to be triggered again.
                if (!_valueSet)
                    Value = value;
            }
        }

        // We don't want the programmer implementing the PropertyGridControl to change the ValueControl or it's
        // parent Grid during runtime, that's why it is set to readonly and protected.
        protected readonly T2 ValueControl = new T2();
        protected readonly Grid ValueControlGrid = new Grid();

        public BaseTypedGridItem(string name)
        {
            Debug.Print($"BaseGridItem({name})");

            ColumnDefinitions.Add(_columnDefinitionLeftMargin);
            ColumnDefinitions.Add(_columnDefinitionLabel);
            ColumnDefinitions.Add(_columnDefinitionValue);
            ColumnDefinitions.Add(_columnDefinitionRightMargin);
            RowDefinitions.Add(_rowDefinitionTopMargin);
            RowDefinitions.Add(_rowDefinitionContent);
            RowDefinitions.Add(_rowDefinitionBottomMargin);

            this.Children.Add(_nameLabel);
            Grid.SetColumn(_nameLabel, 1);
            Grid.SetColumnSpan(_nameLabel, 1);
            Grid.SetRow(_nameLabel, 1);
            Grid.SetRowSpan(_nameLabel, 1);
            _nameLabel.Name = "NameLabel";
            _nameLabel.HorizontalAlignment = HorizontalAlignment.Stretch;
            _nameLabel.VerticalAlignment = VerticalAlignment.Stretch;
            _nameLabel.HorizontalContentAlignment = HorizontalAlignment.Left;
            _nameLabel.VerticalContentAlignment = VerticalAlignment.Center;

            this.Children.Add(ValueControlGrid);
            Grid.SetColumn(ValueControlGrid, 2);
            Grid.SetColumnSpan(ValueControlGrid, 1);
            Grid.SetRow(ValueControlGrid, 1);
            Grid.SetRowSpan(ValueControlGrid, 1);
            ValueControlGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            ValueControlGrid.VerticalAlignment = VerticalAlignment.Stretch;

            ValueControlGrid.Children.Add(ValueControl);
            ValueControl.HorizontalAlignment = HorizontalAlignment.Stretch;
            ValueControl.VerticalAlignment = VerticalAlignment.Stretch;

            PropertyName = name;
            this.Height = 30d;

            Items.Add((T3)this);

            SetBaseGridItemBindings();
        }

        protected virtual void SetGridParentBindings()
        {
            Debug.Print("SetGridParentBindings()");

            SetBinding("LabelFontSize", GridParent, this, LabelFontSizeProperty);
            SetBinding("LabelFontWeight", GridParent, this, LabelFontWeightProperty);
            SetBinding("LabelFontFamily", GridParent, this, LabelFontFamilyProperty);
            SetBinding("LabelFontStretch", GridParent, this, LabelFontStretchProperty);
            SetBinding("LabelFontStyle", GridParent, this, LabelFontStyleProperty);
            SetBinding("LabelWidth", GridParent, this, LabelWidthProperty);
            SetBinding("ContentWidth", GridParent, this, ContentWidthProperty);

            SetBinding("PropertyMargin", GridParent, this, PropertyMarginProperty);
            SetBinding("PropertyHeight", GridParent, this, HeightProperty);
            SetBinding("PropertyForeground", GridParent, this, ForegroundProperty);
            SetBinding("PropertyBackground", GridParent, this, BackgroundProperty);
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
            SetBinding("ContentWidth", this, _columnDefinitionValue, ColumnDefinition.WidthProperty);

            SetBinding("Foreground", this, _nameLabel, Label.ForegroundProperty);
        }

        public static void SetBinding(string sourcePropertyName, object? sourceControl, FrameworkElement targetControl, DependencyProperty targetProperty)
        {
            Binding binding = new(sourcePropertyName)
            {
                Source = sourceControl
            };
            targetControl.SetBinding(targetProperty, binding);
        }

        public static void SetBinding(string sourcePropertyName, object? sourceControl, FrameworkContentElement targetControl, DependencyProperty targetProperty)
        {
            Binding binding = new(sourcePropertyName)
            {
                Source = sourceControl
            };
            targetControl.SetBinding(targetProperty, binding);
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);

            if (Parent.GetType() == typeof(GridControl))
                GridParent = (GridControl)Parent;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            if (!double.IsNaN(this.Width) && !double.IsNaN(_columnDefinitionLabel.Width.Value))
                _columnDefinitionValue.Width = new GridLength(this.Width - _columnDefinitionLabel.Width.Value, GridUnitType.Pixel);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == PropertyMarginProperty)
            {
                Thickness newvalue = (Thickness)e.NewValue;
                _columnDefinitionLeftMargin.Width = new GridLength(newvalue.Left, GridUnitType.Pixel);
                _columnDefinitionRightMargin.Width = new GridLength(newvalue.Right, GridUnitType.Pixel);
                _rowDefinitionTopMargin.Height = new GridLength(newvalue.Top, GridUnitType.Pixel);
                _rowDefinitionBottomMargin.Height = new GridLength(newvalue.Bottom, GridUnitType.Pixel);
            }

            base.OnPropertyChanged(e);
        }

        public void ResetToDefault()
        {
            Value = DefaultValue;
        }

        public void ResetChanges()
        {
            Value = _originalValue;
        }

        public void SetChanges()
        {
            _originalValue = Value;
        }
    }
}
