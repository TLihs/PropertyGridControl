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
    /// <summary>
    /// Führen Sie die Schritte 1a oder 1b und anschließend Schritt 2 aus, um dieses benutzerdefinierte Steuerelement in einer XAML-Datei zu verwenden.
    ///
    /// Schritt 1a) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die im aktuellen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:PropertyGridControl"
    ///
    ///
    /// Schritt 1b) Verwenden des benutzerdefinierten Steuerelements in einer XAML-Datei, die in einem anderen Projekt vorhanden ist.
    /// Fügen Sie dieses XmlNamespace-Attribut dem Stammelement der Markupdatei 
    /// an der Stelle hinzu, an der es verwendet werden soll:
    ///
    ///     xmlns:MyNamespace="clr-namespace:PropertyGridControl;assembly=PropertyGridControl"
    ///
    /// Darüber hinaus müssen Sie von dem Projekt, das die XAML-Datei enthält, einen Projektverweis
    /// zu diesem Projekt hinzufügen und das Projekt neu erstellen, um Kompilierungsfehler zu vermeiden:
    ///
    ///     Klicken Sie im Projektmappen-Explorer mit der rechten Maustaste auf das Zielprojekt und anschließend auf
    ///     "Verweis hinzufügen"->"Projekte"->[Dieses Projekt auswählen]
    ///
    ///
    /// Schritt 2)
    /// Fahren Sie fort, und verwenden Sie das Steuerelement in der XAML-Datei.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
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
            DependencyProperty.RegisterAttached("PropertyBackground", typeof(Brush), typeof (GridControl), new PropertyMetadata(Brushes.Black));
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
            DependencyProperty.RegisterAttached("LabelWidth", typeof(double), typeof(GridControl), new PropertyMetadata(50d));
        public double LabelWidth
        {
            get => (double)GetValue(LabelWidthProperty);
            set => SetValue(LabelWidthProperty, value);
        }

        static GridControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridControl), new FrameworkPropertyMetadata(typeof(GridControl)));
        }

        public GridControl()
        {

        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == LabelFontSizeProperty)
                foreach (BaseGridItem item in Children)
                    item.LabelFontSize = (double)e.NewValue;
            else if (e.Property == LabelFontFamilyProperty)
                foreach (BaseGridItem item in Children)
                    item.LabelFontFamily = (FontFamily)e.NewValue;
            else if (e.Property == LabelFontStretchProperty)
                foreach (BaseGridItem item in Children)
                    item.LabelFontStretch = (FontStretch)e.NewValue;
            else if (e.Property == LabelFontStyleProperty)
                foreach (BaseGridItem item in Children)
                    item.LabelFontStyle = (FontStyle)e.NewValue;
            else if (e.Property == LabelFontWeightProperty)
                foreach (BaseGridItem item in Children)
                    item.LabelFontWeight = (FontWeight)e.NewValue;
            else if (e.Property == PropertyForegroundProperty)
                foreach (BaseGridItem item in Children)
                    item.Foreground = (Brush)e.NewValue;
            else if (e.Property == PropertyBackgroundProperty)
                foreach (BaseGridItem item in Children)
                    item.Background = (Brush)e.NewValue;

            base.OnPropertyChanged(e);
        }
    }
}
