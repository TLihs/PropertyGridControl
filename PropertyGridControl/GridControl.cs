using PropertyGridControl.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PropertyGridControl
{
    public class GridControl : StackPanel
    {
        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.RegisterAttached("BorderThickness", typeof(Thickness), typeof(GridControl), new PropertyMetadata(new Thickness(1)));
        public Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.RegisterAttached("BorderBrush", typeof(Brush), typeof(GridControl), new PropertyMetadata(Brushes.Black));
        public Brush BorderBrush
        {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }

        public static readonly DependencyProperty PropertyBorderThicknessProperty =
            DependencyProperty.RegisterAttached("PropertyBorderThickness", typeof(Thickness), typeof(GridControl), new PropertyMetadata(new Thickness(1)));
        public Thickness PropertyBorderThickness
        {
            get => (Thickness)GetValue(PropertyBorderThicknessProperty);
            set => SetValue(PropertyBorderThicknessProperty, value);
        }

        public static readonly DependencyProperty PropertyBorderBrushProperty =
            DependencyProperty.RegisterAttached("PropertyBorderBrush", typeof(Brush), typeof(GridControl), new PropertyMetadata(Brushes.Black));
        public Brush PropertyBorderBrush
        {
            get => (Brush)GetValue(PropertyBorderBrushProperty);
            set => SetValue(PropertyBorderBrushProperty, value);
        }

        public static readonly DependencyProperty PropertyBackgroundProperty =
            DependencyProperty.RegisterAttached("PropertyBackground", typeof(Brush), typeof(GridControl), new PropertyMetadata(Brushes.Black));
        public Brush PropertyBackground
        {
            get => (Brush)GetValue(PropertyBackgroundProperty);
            set => SetValue(PropertyBackgroundProperty, value);
        }

        public static readonly DependencyProperty PropertyForegroundProperty =
            DependencyProperty.RegisterAttached("PropertyForeground", typeof(Brush), typeof(GridControl), new PropertyMetadata(Brushes.White));
        public Brush PropertyForeground
        {
            get => (Brush)GetValue(PropertyForegroundProperty);
            set => SetValue(PropertyForegroundProperty, value);
        }

        public static readonly DependencyProperty HoveredPropertyBackgroundProperty =
            DependencyProperty.RegisterAttached("HoveredPropertyBackground", typeof(Brush), typeof(GridControl), new PropertyMetadata(Brushes.Black));
        public Brush HoveredPropertyBackground
        {
            get => (Brush)GetValue(HoveredPropertyBackgroundProperty);
            set => SetValue(HoveredPropertyBackgroundProperty, value);
        }

        public static readonly DependencyProperty HoveredPropertyForegroundProperty =
            DependencyProperty.RegisterAttached("HoveredPropertyForeground", typeof(Brush), typeof(GridControl), new PropertyMetadata(Brushes.White));
        public Brush HoveredPropertyForeground
        {
            get => (Brush)GetValue(HoveredPropertyForegroundProperty);
            set => SetValue(HoveredPropertyForegroundProperty, value);
        }

        public static readonly DependencyProperty SelectedPropertyBackgroundProperty =
            DependencyProperty.RegisterAttached("SelectedPropertyBackground", typeof(Brush), typeof(GridControl), new PropertyMetadata(Brushes.Black));
        public Brush SelectedPropertyBackground
        {
            get => (Brush)GetValue(SelectedPropertyBackgroundProperty);
            set => SetValue(SelectedPropertyBackgroundProperty, value);
        }

        public static readonly DependencyProperty SelectedPropertyForegroundProperty =
            DependencyProperty.RegisterAttached("SelectedPropertyForeground", typeof(Brush), typeof(GridControl), new PropertyMetadata(Brushes.White));
        public Brush SelectedPropertyForeground
        {
            get => (Brush)GetValue(SelectedPropertyForegroundProperty);
            set => SetValue(SelectedPropertyForegroundProperty, value);
        }

        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.RegisterAttached("LabelFontSize", typeof(double), typeof(GridControl), new PropertyMetadata(12d));
        public double LabelFontSize
        {
            get => (double)GetValue(LabelFontSizeProperty);
            set => SetValue(LabelFontSizeProperty, value);
        }

        public static readonly DependencyProperty LabelFontWeightProperty =
            DependencyProperty.RegisterAttached("LabelFontWeight", typeof(FontWeight), typeof(GridControl), new PropertyMetadata(FontWeights.Normal));
        public FontWeight LabelFontWeight
        {
            get => (FontWeight)GetValue(LabelFontWeightProperty);
            set => SetValue(LabelFontWeightProperty, value);
        }

        public static readonly DependencyProperty LabelFontFamilyProperty =
            DependencyProperty.RegisterAttached("LabelFontFamily", typeof(FontFamily), typeof(GridControl), new PropertyMetadata(new FontFamily("Segoe UI")));
        public FontFamily LabelFontFamily
        {
            get => (FontFamily)GetValue(LabelFontFamilyProperty);
            set => SetValue(LabelFontFamilyProperty, value);
        }

        public static readonly DependencyProperty LabelFontStretchProperty =
            DependencyProperty.RegisterAttached("LabelFontStretch", typeof(FontStretch), typeof(GridControl), new PropertyMetadata(FontStretches.Normal));
        public FontStretch LabelFontStretch
        {
            get => (FontStretch)GetValue(LabelFontStretchProperty);
            set => SetValue(LabelFontStretchProperty, value);
        }

        public static readonly DependencyProperty LabelFontStyleProperty =
            DependencyProperty.RegisterAttached("LabelFontStyle", typeof(FontStyle), typeof(GridControl), new PropertyMetadata(FontStyles.Normal));
        public FontStyle LabelFontStyle
        {
            get => (FontStyle)GetValue(LabelFontStyleProperty);
            set => SetValue(LabelFontStyleProperty, value);
        }

        public static readonly DependencyProperty HoveredLabelFontSizeProperty =
            DependencyProperty.RegisterAttached("HoveredLabelFontSize", typeof(double), typeof(GridControl), new PropertyMetadata(12d));
        public double HoveredLabelFontSize
        {
            get => (double)GetValue(HoveredLabelFontSizeProperty);
            set => SetValue(HoveredLabelFontSizeProperty, value);
        }

        public static readonly DependencyProperty HoveredLabelFontWeightProperty =
            DependencyProperty.RegisterAttached("HoveredLabelFontWeight", typeof(FontWeight), typeof(GridControl), new PropertyMetadata(FontWeights.Normal));
        public FontWeight HoveredLabelFontWeight
        {
            get => (FontWeight)GetValue(HoveredLabelFontWeightProperty);
            set => SetValue(HoveredLabelFontWeightProperty, value);
        }

        public static readonly DependencyProperty HoveredLabelFontFamilyProperty =
            DependencyProperty.RegisterAttached("HoveredLabelFontFamily", typeof(FontFamily), typeof(GridControl), new PropertyMetadata(new FontFamily("Segoe UI")));
        public FontFamily HoveredLabelFontFamily
        {
            get => (FontFamily)GetValue(HoveredLabelFontFamilyProperty);
            set => SetValue(HoveredLabelFontFamilyProperty, value);
        }

        public static readonly DependencyProperty HoveredLabelFontStretchProperty =
            DependencyProperty.RegisterAttached("HoveredLabelFontStretch", typeof(FontStretch), typeof(GridControl), new PropertyMetadata(FontStretches.Normal));
        public FontStretch HoveredLabelFontStretch
        {
            get => (FontStretch)GetValue(HoveredLabelFontStretchProperty);
            set => SetValue(HoveredLabelFontStretchProperty, value);
        }

        public static readonly DependencyProperty HoveredLabelFontStyleProperty =
            DependencyProperty.RegisterAttached("HoveredLabelFontStyle", typeof(FontStyle), typeof(GridControl), new PropertyMetadata(FontStyles.Normal));
        public FontStyle HoveredLabelFontStyle
        {
            get => (FontStyle)GetValue(HoveredLabelFontStyleProperty);
            set => SetValue(HoveredLabelFontStyleProperty, value);
        }

        public static readonly DependencyProperty SelectedLabelFontSizeProperty =
            DependencyProperty.RegisterAttached("SelectedLabelFontSize", typeof(double), typeof(GridControl), new PropertyMetadata(12d));
        public double SelectedLabelFontSize
        {
            get => (double)GetValue(SelectedLabelFontSizeProperty);
            set => SetValue(SelectedLabelFontSizeProperty, value);
        }

        public static readonly DependencyProperty SelectedLabelFontWeightProperty =
            DependencyProperty.RegisterAttached("SelectedLabelFontWeight", typeof(FontWeight), typeof(GridControl), new PropertyMetadata(FontWeights.Normal));
        public FontWeight SelectedLabelFontWeight
        {
            get => (FontWeight)GetValue(SelectedLabelFontWeightProperty);
            set => SetValue(SelectedLabelFontWeightProperty, value);
        }

        public static readonly DependencyProperty SelectedLabelFontFamilyProperty =
            DependencyProperty.RegisterAttached("SelectedLabelFontFamily", typeof(FontFamily), typeof(GridControl), new PropertyMetadata(new FontFamily("Segoe UI")));
        public FontFamily SelectedLabelFontFamily
        {
            get => (FontFamily)GetValue(SelectedLabelFontFamilyProperty);
            set => SetValue(SelectedLabelFontFamilyProperty, value);
        }

        public static readonly DependencyProperty SelectedLabelFontStretchProperty =
            DependencyProperty.RegisterAttached("SelectedLabelFontStretch", typeof(FontStretch), typeof(GridControl), new PropertyMetadata(FontStretches.Normal));
        public FontStretch SelectedLabelFontStretch
        {
            get => (FontStretch)GetValue(SelectedLabelFontStretchProperty);
            set => SetValue(SelectedLabelFontStretchProperty, value);
        }

        public static readonly DependencyProperty SelectedLabelFontStyleProperty =
            DependencyProperty.RegisterAttached("SelectedLabelFontStyle", typeof(FontStyle), typeof(GridControl), new PropertyMetadata(FontStyles.Normal));
        public FontStyle SelectedLabelFontStyle
        {
            get => (FontStyle)GetValue(SelectedLabelFontStyleProperty);
            set => SetValue(SelectedLabelFontStyleProperty, value);
        }

        public static readonly DependencyProperty ValueFontSizeProperty =
            DependencyProperty.RegisterAttached("ValueFontSize", typeof(double), typeof(GridControl), new PropertyMetadata(12d));
        public double ValueFontSize
        {
            get => (double)GetValue(ValueFontSizeProperty);
            set => SetValue(ValueFontSizeProperty, value);
        }

        public static readonly DependencyProperty ValueFontWeightProperty =
            DependencyProperty.RegisterAttached("ValueFontWeight", typeof(FontWeight), typeof(GridControl), new PropertyMetadata(FontWeights.Normal));
        public FontWeight ValueFontWeight
        {
            get => (FontWeight)GetValue(ValueFontWeightProperty);
            set => SetValue(ValueFontWeightProperty, value);
        }

        public static readonly DependencyProperty ValueFontFamilyProperty =
            DependencyProperty.RegisterAttached("ValueFontFamily", typeof(FontFamily), typeof(GridControl), new PropertyMetadata(new FontFamily("Segoe UI")));
        public FontFamily ValueFontFamily
        {
            get => (FontFamily)GetValue(ValueFontFamilyProperty);
            set => SetValue(ValueFontFamilyProperty, value);
        }

        public static readonly DependencyProperty ValueFontStretchProperty =
            DependencyProperty.RegisterAttached("ValueFontStretch", typeof(FontStretch), typeof(GridControl), new PropertyMetadata(FontStretches.Normal));
        public FontStretch ValueFontStretch
        {
            get => (FontStretch)GetValue(ValueFontStretchProperty);
            set => SetValue(ValueFontStretchProperty, value);
        }

        public static readonly DependencyProperty ValueFontStyleProperty =
            DependencyProperty.RegisterAttached("ValueFontStyle", typeof(FontStyle), typeof(GridControl), new PropertyMetadata(FontStyles.Normal));
        public FontStyle ValueFontStyle
        {
            get => (FontStyle)GetValue(ValueFontStyleProperty);
            set => SetValue(ValueFontStyleProperty, value);
        }

        public static readonly DependencyProperty HoveredValueFontSizeProperty =
            DependencyProperty.RegisterAttached("HoveredValueFontSize", typeof(double), typeof(GridControl), new PropertyMetadata(12d));
        public double HoveredValueFontSize
        {
            get => (double)GetValue(HoveredValueFontSizeProperty);
            set => SetValue(HoveredValueFontSizeProperty, value);
        }

        public static readonly DependencyProperty HoveredValueFontWeightProperty =
            DependencyProperty.RegisterAttached("HoveredValueFontWeight", typeof(FontWeight), typeof(GridControl), new PropertyMetadata(FontWeights.Normal));
        public FontWeight HoveredValueFontWeight
        {
            get => (FontWeight)GetValue(HoveredValueFontWeightProperty);
            set => SetValue(HoveredValueFontWeightProperty, value);
        }

        public static readonly DependencyProperty HoveredValueFontFamilyProperty =
            DependencyProperty.RegisterAttached("HoveredValueFontFamily", typeof(FontFamily), typeof(GridControl), new PropertyMetadata(new FontFamily("Segoe UI")));
        public FontFamily HoveredValueFontFamily
        {
            get => (FontFamily)GetValue(HoveredValueFontFamilyProperty);
            set => SetValue(HoveredValueFontFamilyProperty, value);
        }

        public static readonly DependencyProperty HoveredValueFontStretchProperty =
            DependencyProperty.RegisterAttached("HoveredValueFontStretch", typeof(FontStretch), typeof(GridControl), new PropertyMetadata(FontStretches.Normal));
        public FontStretch HoveredValueFontStretch
        {
            get => (FontStretch)GetValue(HoveredValueFontStretchProperty);
            set => SetValue(HoveredValueFontStretchProperty, value);
        }

        public static readonly DependencyProperty HoveredValueFontStyleProperty =
            DependencyProperty.RegisterAttached("HoveredValueFontStyle", typeof(FontStyle), typeof(GridControl), new PropertyMetadata(FontStyles.Normal));
        public FontStyle HoveredValueFontStyle
        {
            get => (FontStyle)GetValue(HoveredValueFontStyleProperty);
            set => SetValue(HoveredValueFontStyleProperty, value);
        }

        public static readonly DependencyProperty SelectedValueFontSizeProperty =
            DependencyProperty.RegisterAttached("SelectedValueFontSize", typeof(double), typeof(GridControl), new PropertyMetadata(12d));
        public double SelectedValueFontSize
        {
            get => (double)GetValue(SelectedValueFontSizeProperty);
            set => SetValue(SelectedValueFontSizeProperty, value);
        }

        public static readonly DependencyProperty SelectedValueFontWeightProperty =
            DependencyProperty.RegisterAttached("SelectedValueFontWeight", typeof(FontWeight), typeof(GridControl), new PropertyMetadata(FontWeights.Normal));
        public FontWeight SelectedValueFontWeight
        {
            get => (FontWeight)GetValue(SelectedValueFontWeightProperty);
            set => SetValue(SelectedValueFontWeightProperty, value);
        }

        public static readonly DependencyProperty SelectedValueFontFamilyProperty =
            DependencyProperty.RegisterAttached("SelectedValueFontFamily", typeof(FontFamily), typeof(GridControl), new PropertyMetadata(new FontFamily("Segoe UI")));
        public FontFamily SelectedValueFontFamily
        {
            get => (FontFamily)GetValue(SelectedValueFontFamilyProperty);
            set => SetValue(SelectedValueFontFamilyProperty, value);
        }

        public static readonly DependencyProperty SelectedValueFontStretchProperty =
            DependencyProperty.RegisterAttached("SelectedValueFontStretch", typeof(FontStretch), typeof(GridControl), new PropertyMetadata(FontStretches.Normal));
        public FontStretch SelectedValueFontStretch
        {
            get => (FontStretch)GetValue(SelectedValueFontStretchProperty);
            set => SetValue(SelectedValueFontStretchProperty, value);
        }

        public static readonly DependencyProperty SelectedValueFontStyleProperty =
            DependencyProperty.RegisterAttached("SelectedValueFontStyle", typeof(FontStyle), typeof(GridControl), new PropertyMetadata(FontStyles.Normal));
        public FontStyle SelectedValueFontStyle
        {
            get => (FontStyle)GetValue(SelectedValueFontStyleProperty);
            set => SetValue(SelectedValueFontStyleProperty, value);
        }

        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.RegisterAttached("LabelWidth", typeof(GridLength), typeof(GridControl), new PropertyMetadata(new GridLength(0d, GridUnitType.Star)));
        public GridLength LabelWidth
        {
            get => (GridLength)GetValue(LabelWidthProperty);
            set => SetValue(LabelWidthProperty, value);
        }

        public static readonly DependencyProperty ContentWidthProperty =
            DependencyProperty.RegisterAttached("ContentWidth", typeof(GridLength), typeof(GridControl), new PropertyMetadata(new GridLength(0d, GridUnitType.Star)));
        public GridLength ContentWidth
        {
            get => (GridLength)GetValue(ContentWidthProperty);
            set => SetValue(ContentWidthProperty, value);
        }

        public static readonly DependencyProperty PropertyMarginProperty =
            DependencyProperty.RegisterAttached("PropertyMargin", typeof(Thickness), typeof(GridControl), new PropertyMetadata(new Thickness(0d)));
        public Thickness PropertyMargin
        {
            get => (Thickness)GetValue(PropertyMarginProperty);
            set => SetValue(PropertyMarginProperty, value);
        }

        public static readonly DependencyProperty PropertyHeightProperty =
            DependencyProperty.RegisterAttached("PropertyHeight", typeof(double), typeof(GridControl), new PropertyMetadata(30d));
        public double PropertyHeight
        {
            get => (double)GetValue(PropertyHeightProperty);
            set => SetValue(PropertyHeightProperty, value);
        }

        public static readonly DependencyProperty ContentBackgroundProperty =
            DependencyProperty.RegisterAttached("ContentBackground", typeof(Brush), typeof(GridControl), new PropertyMetadata(Brushes.White));
        public Brush ContentBackground
        {
            get => (Brush)GetValue(ContentBackgroundProperty);
            set => SetValue(ContentBackgroundProperty, value);
        }

        public static readonly DependencyProperty ContentForegroundProperty =
            DependencyProperty.RegisterAttached("ContentForeground", typeof(Brush), typeof(GridControl), new PropertyMetadata(Brushes.Black));
        public Brush ContentForeground
        {
            get => (Brush)GetValue(ContentForegroundProperty);
            set => SetValue(ContentForegroundProperty, value);
        }

        public static readonly DependencyProperty ContentBorderBrushProperty =
            DependencyProperty.RegisterAttached("ContentBorderBrush", typeof(Brush), typeof(GridControl), new PropertyMetadata(Brushes.Gray));
        public Brush ContentBorderBrush
        {
            get => (Brush)GetValue(ContentBorderBrushProperty);
            set => SetValue(ContentBorderBrushProperty, value);
        }

        public static readonly DependencyProperty ContentBorderThicknessProperty =
            DependencyProperty.RegisterAttached("ContentBorderThickness", typeof(Thickness), typeof(GridControl), new PropertyMetadata(new Thickness(0d)));
        public Thickness ContentBorderThickness
        {
            get => (Thickness)GetValue(ContentBorderThicknessProperty);
            set => SetValue(ContentBorderThicknessProperty, value);
        }

        public static readonly DependencyProperty ContentFontSizeProperty =
            DependencyProperty.RegisterAttached("ContentFontSize", typeof(double), typeof(GridControl), new PropertyMetadata(12d));
        public double ContentFontSize
        {
            get => (double)GetValue(ContentFontSizeProperty);
            set => SetValue(ContentFontSizeProperty, value);
        }

        public static readonly DependencyProperty ContentFontWeightProperty =
            DependencyProperty.RegisterAttached("ContentFontWeight", typeof(FontWeight), typeof(GridControl), new PropertyMetadata(FontWeights.Normal));
        public FontWeight ContentFontWeight
        {
            get => (FontWeight)GetValue(ContentFontWeightProperty);
            set => SetValue(ContentFontWeightProperty, value);
        }

        public static readonly DependencyProperty ContentFontFamilyProperty =
            DependencyProperty.RegisterAttached("ContentFontFamily", typeof(FontFamily), typeof(GridControl), new PropertyMetadata(new FontFamily("Segoe UI")));
        public FontFamily ContentFontFamily
        {
            get => (FontFamily)GetValue(ContentFontFamilyProperty);
            set => SetValue(ContentFontFamilyProperty, value);
        }

        public static readonly DependencyProperty ContentFontStretchProperty =
            DependencyProperty.RegisterAttached("ContentFontStretch", typeof(FontStretch), typeof(GridControl), new PropertyMetadata(FontStretches.Normal));
        public FontStretch ContentFontStretch
        {
            get => (FontStretch)GetValue(ContentFontStretchProperty);
            set => SetValue(ContentFontStretchProperty, value);
        }

        public static readonly DependencyProperty ContentFontStyleProperty =
            DependencyProperty.RegisterAttached("ContentFontStyle", typeof(FontStyle), typeof(GridControl), new PropertyMetadata(FontStyles.Normal));
        public FontStyle ContentFontStyle
        {
            get => (FontStyle)GetValue(ContentFontStyleProperty);
            set => SetValue(ContentFontStyleProperty, value);
        }

        public static readonly DependencyProperty ContentTextProperty =
            DependencyProperty.RegisterAttached("ContentText", typeof(string), typeof(GridControl), new PropertyMetadata(default(string)));
        public string ContentText
        {
            get => (string)GetValue(ContentTextProperty);
            set => SetValue(ContentTextProperty, value);
        }

        static GridControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridControl), new FrameworkPropertyMetadata(typeof(GridControl)));
        }

        public GridControl()
        {

        }
    }
}
