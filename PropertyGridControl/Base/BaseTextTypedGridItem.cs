using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PropertyGridControl.Base
{
    public abstract class BaseTextTypedGridItem<T1, T2, T3> : BaseTypedGridItem<T1, T2, T3>
        where T1 : notnull
        where T2 : Control, new()
        where T3 : BaseTextTypedGridItem<T1, T2, T3>
    {
        protected bool InputHandled { get; set; } = false;

        public event EventHandler<Brush>? ContentBackgroundChanged;
        public static readonly DependencyProperty ContentBackgroundProperty =
            DependencyProperty.RegisterAttached("ContentBackground", typeof(Brush), typeof(T3),
                new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));
        public Brush ContentBackground
        {
            get => (Brush)GetValue(ContentBackgroundProperty);
            set
            {
                SetValue(ContentBackgroundProperty, value);
                ContentBackgroundChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<Brush>? ContentForegroundChanged;
        public static readonly DependencyProperty ContentForegroundProperty =
            DependencyProperty.RegisterAttached("ContentForeground", typeof(Brush), typeof(T3),
                new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));
        public Brush ContentForeground
        {
            get => (Brush)GetValue(ContentForegroundProperty);
            set
            {
                SetValue(ContentForegroundProperty, value);
                ContentForegroundChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<Brush>? ContentBorderBrushChanged;
        public static readonly DependencyProperty ContentBorderBrushProperty =
            DependencyProperty.RegisterAttached("ContentBorderBrush", typeof(Brush), typeof(T3),
                new FrameworkPropertyMetadata(Brushes.Gray, FrameworkPropertyMetadataOptions.AffectsRender));
        public Brush ContentBorderBrush
        {
            get => (Brush)GetValue(ContentBorderBrushProperty);
            set
            {
                SetValue(ContentBorderBrushProperty, value);
                ContentBorderBrushChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<Thickness>? ContentBorderThicknessChanged;
        public static readonly DependencyProperty ContentBorderThicknessProperty =
            DependencyProperty.RegisterAttached("ContentBorderThickness", typeof(Thickness), typeof(T3),
                new FrameworkPropertyMetadata(new Thickness(0d), FrameworkPropertyMetadataOptions.AffectsRender));
        public Thickness ContentBorderThickness
        {
            get => (Thickness)GetValue(ContentBorderThicknessProperty);
            set
            {
                SetValue(ContentBorderThicknessProperty, value);
                ContentBorderThicknessChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<double>? ContentFontSizeChanged;
        public static readonly DependencyProperty ContentFontSizeProperty =
            DependencyProperty.RegisterAttached("ContentFontSize", typeof(double), typeof(T3),
                new FrameworkPropertyMetadata(12d, FrameworkPropertyMetadataOptions.AffectsRender));
        public double ContentFontSize
        {
            get => (double)GetValue(ContentFontSizeProperty);
            set
            {
                SetValue(ContentFontSizeProperty, value);
                ContentFontSizeChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<FontWeight>? ContentFontWeightChanged;
        public static readonly DependencyProperty ContentFontWeightProperty =
            DependencyProperty.RegisterAttached("ContentFontWeight", typeof(FontWeight), typeof(T3),
                new FrameworkPropertyMetadata(FontWeights.Normal, FrameworkPropertyMetadataOptions.AffectsRender));
        public FontWeight ContentFontWeight
        {
            get => (FontWeight)GetValue(ContentFontWeightProperty);
            set
            {
                SetValue(ContentFontWeightProperty, value);
                ContentFontWeightChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<FontFamily>? ContentFontFamilyChanged;
        public static readonly DependencyProperty ContentFontFamilyProperty =
            DependencyProperty.RegisterAttached("ContentFontFamily", typeof(FontFamily), typeof(T3),
                new FrameworkPropertyMetadata(new FontFamily("Segoe UI"), FrameworkPropertyMetadataOptions.AffectsRender));
        public FontFamily ContentFontFamily
        {
            get => (FontFamily)GetValue(ContentFontFamilyProperty);
            set
            {
                SetValue(ContentFontFamilyProperty, value);
                ContentFontFamilyChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<FontStretch>? ContentFontStretchChanged;
        public static readonly DependencyProperty ContentFontStretchProperty =
            DependencyProperty.RegisterAttached("ContentFontStretch", typeof(FontStretch), typeof(T3),
                new FrameworkPropertyMetadata(FontStretches.Normal, FrameworkPropertyMetadataOptions.AffectsRender));
        public FontStretch ContentFontStretch
        {
            get => (FontStretch)GetValue(ContentFontStretchProperty);
            set
            {
                SetValue(ContentFontStretchProperty, value);
                ContentFontStretchChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<FontStyle>? ContentFontStyleChanged;
        public static readonly DependencyProperty ContentFontStyleProperty =
            DependencyProperty.RegisterAttached("ContentFontStyle", typeof(FontStyle), typeof(T3),
                new FrameworkPropertyMetadata(FontStyles.Normal, FrameworkPropertyMetadataOptions.AffectsRender));
        public FontStyle ContentFontStyle
        {
            get => (FontStyle)GetValue(ContentFontStyleProperty);
            set
            {
                SetValue(ContentFontStyleProperty, value);
                ContentFontStyleChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<string>? ContentTextChanged;
        public static readonly DependencyProperty ContentTextProperty =
            DependencyProperty.RegisterAttached("ContentText", typeof(string), typeof(T3),
                new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.AffectsRender));
        public string ContentText
        {
            get => (string)GetValue(ContentTextProperty);
            set
            {
                SetValue(ContentTextProperty, value);
                ContentTextChanged?.Invoke(this, value);
            }
        }

        public override T1 Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                if (!InputHandled)
                {
                    if (typeof(T2) == typeof(TextBox))
                    {
                        TextBox control = (TextBox)(Control)ValueControl;
                        int caretposition = control.CaretIndex;
                        control.Text = value.ToString();
                        control.CaretIndex = caretposition;
                    }
                    else if (typeof(T2) == typeof(ComboBox))
                        ((ComboBox)(Control)ValueControl).Text = base.Value.ToString();

                    InputHandled = true;
                }
            }
        }

        public BaseTextTypedGridItem(string name) : base(name)
        {
            base.GridParentChanged += OnGridParentChanged;
        }

        private void OnGridParentChanged(object? sender, GridControl? e)
        {
            SetGridParentBindings();
        }

        protected override void SetBaseGridItemBindings()
        {
            base.SetBaseGridItemBindings();

            if (ValueControl.GetType() == typeof(TextBox))
            {
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
            else if (ValueControl.GetType() == typeof(ComboBox))
            {
                SetBinding("ContentFontSize", this, ValueControl, ComboBox.FontSizeProperty);
                SetBinding("ContentFontWeight", this, ValueControl, ComboBox.FontWeightProperty);
                SetBinding("ContentFontFamily", this, ValueControl, ComboBox.FontFamilyProperty);
                SetBinding("ContentFontStretch", this, ValueControl, ComboBox.FontStretchProperty);
                SetBinding("ContentFontStyle", this, ValueControl, ComboBox.FontStyleProperty);
                SetBinding("ContentBackground", this, ValueControl, ComboBox.BackgroundProperty);
                SetBinding("ContentForeground", this, ValueControl, ComboBox.ForegroundProperty);
                SetBinding("ContentBorderBrush", this, ValueControl, ComboBox.BorderBrushProperty);
                SetBinding("ContentBorderThickness", this, ValueControl, ComboBox.BorderThicknessProperty);
            }
        }

        protected override void SetGridParentBindings()
        {
            base.SetGridParentBindings();

            SetBinding("ContentFontSize", GridParent, this, ContentFontSizeProperty);
            SetBinding("ContentFontWeight", GridParent, this, ContentFontWeightProperty);
            SetBinding("ContentFontFamily", GridParent, this, ContentFontFamilyProperty);
            SetBinding("ContentFontStretch", GridParent, this, ContentFontStretchProperty);
            SetBinding("ContentFontStyle", GridParent, this, ContentFontStyleProperty);
            SetBinding("ContentWidth", GridParent, this, ContentWidthProperty);
            SetBinding("ContentHeight", GridParent, this, ContentHeightProperty);
            SetBinding("ContentBackground", GridParent, this, ContentBackgroundProperty);
            SetBinding("ContentForeground", GridParent, this, ContentForegroundProperty);
            SetBinding("ContentBorderBrush", GridParent, this, ContentBorderBrushProperty);
            SetBinding("ContentBorderThickness", GridParent, this, ContentBorderThicknessProperty);
        }
    }
}
