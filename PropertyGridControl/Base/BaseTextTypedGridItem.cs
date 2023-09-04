using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PropertyGridControl.Base
{
    public abstract class BaseTextTypedGridItem<T1, T2, T3> : BaseTypedGridItem<T1, T2, T3>
        where T2 : Control, new()
        where T3 : BaseTextTypedGridItem<T1, T2, T3>
    {
        public static readonly DependencyProperty ContentBackgroundProperty =
            DependencyProperty.RegisterAttached("ContentBackground", typeof(Brush), typeof(T3), new PropertyMetadata(Brushes.White));
        public Brush ContentBackground
        {
            get => (Brush)GetValue(ContentBackgroundProperty);
            set => SetValue(ContentBackgroundProperty, value);
        }

        public static readonly DependencyProperty ContentForegroundProperty =
            DependencyProperty.RegisterAttached("ContentForeground", typeof(Brush), typeof(T3), new PropertyMetadata(Brushes.Black));
        public Brush ContentForeground
        {
            get => (Brush)GetValue(ContentForegroundProperty);
            set => SetValue(ContentForegroundProperty, value);
        }

        public static readonly DependencyProperty ContentBorderBrushProperty =
            DependencyProperty.RegisterAttached("ContentBorderBrush", typeof(Brush), typeof(T3), new PropertyMetadata(Brushes.Gray));
        public Brush ContentBorderBrush
        {
            get => (Brush)GetValue(ContentBorderBrushProperty);
            set => SetValue(ContentBorderBrushProperty, value);
        }

        public static readonly DependencyProperty ContentBorderThicknessProperty =
            DependencyProperty.RegisterAttached("ContentBorderThickness", typeof(Thickness), typeof(T3), new PropertyMetadata(new Thickness(0d)));
        public Thickness ContentBorderThickness
        {
            get => (Thickness)GetValue(ContentBorderThicknessProperty);
            set => SetValue(ContentBorderThicknessProperty, value);
        }

        public static readonly DependencyProperty ContentFontSizeProperty =
            DependencyProperty.RegisterAttached("ContentFontSize", typeof(double), typeof(T3), new PropertyMetadata(12d));
        public double ContentFontSize
        {
            get => (double)GetValue(ContentFontSizeProperty);
            set => SetValue(ContentFontSizeProperty, value);
        }

        public static readonly DependencyProperty ContentFontWeightProperty =
            DependencyProperty.RegisterAttached("ContentFontWeight", typeof(FontWeight), typeof(T3), new PropertyMetadata(FontWeights.Normal));
        public FontWeight ContentFontWeight
        {
            get => (FontWeight)GetValue(ContentFontWeightProperty);
            set => SetValue(ContentFontWeightProperty, value);
        }

        public static readonly DependencyProperty ContentFontFamilyProperty =
            DependencyProperty.RegisterAttached("ContentFontFamily", typeof(FontFamily), typeof(T3), new PropertyMetadata(new FontFamily("Segoe UI")));
        public FontFamily ContentFontFamily
        {
            get => (FontFamily)GetValue(ContentFontFamilyProperty);
            set => SetValue(ContentFontFamilyProperty, value);
        }

        public static readonly DependencyProperty ContentFontStretchProperty =
            DependencyProperty.RegisterAttached("ContentFontStretch", typeof(FontStretch), typeof(T3), new PropertyMetadata(FontStretches.Normal));
        public FontStretch ContentFontStretch
        {
            get => (FontStretch)GetValue(ContentFontStretchProperty);
            set => SetValue(ContentFontStretchProperty, value);
        }

        public static readonly DependencyProperty ContentFontStyleProperty =
            DependencyProperty.RegisterAttached("ContentFontStyle", typeof(FontStyle), typeof(T3), new PropertyMetadata(FontStyles.Normal));
        public FontStyle ContentFontStyle
        {
            get => (FontStyle)GetValue(ContentFontStyleProperty);
            set => SetValue(ContentFontStyleProperty, value);
        }

        public static readonly DependencyProperty ContentTextProperty =
            DependencyProperty.RegisterAttached("ContentText", typeof(string), typeof(T3), new PropertyMetadata(default(string)));
        public string ContentText
        {
            get => (string)GetValue(ContentTextProperty);
            set => SetValue(ContentTextProperty, value);
        }

        public BaseTextTypedGridItem(string name) : base(name)
        {
            SetGridParentBindings();
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
            SetBinding("ContentBackground", GridParent, this, ContentBackgroundProperty);
            SetBinding("ContentForeground", GridParent, this, ContentForegroundProperty);
            SetBinding("ContentBorderBrush", GridParent, this, ContentBorderBrushProperty);
            SetBinding("ContentBorderThickness", GridParent, this, ContentBorderThicknessProperty);
        }
    }
}
