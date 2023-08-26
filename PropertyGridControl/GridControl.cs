using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GridControl : Grid
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

        public static readonly DependencyProperty FocusedLabelFontSizeProperty =
            DependencyProperty.RegisterAttached("FocusedLabelFontSize", typeof(double), typeof(GridControl), new PropertyMetadata(12d));
        public static readonly DependencyProperty FocusedLabelFontWeightProperty =
            DependencyProperty.RegisterAttached("FocusedLabelFontWeight", typeof(FontWeight), typeof(GridControl), new PropertyMetadata(FontWeights.Normal));
        public static readonly DependencyProperty FocusedLabelFontFamilyProperty =
            DependencyProperty.RegisterAttached("FocusedLabelFontFamily", typeof(FontFamily), typeof(GridControl), new PropertyMetadata(new FontFamily("Segoe UI")));
        public static readonly DependencyProperty FocusedLabelFontStretchProperty =
            DependencyProperty.RegisterAttached("FocusedLabelFontStretch", typeof(FontStretch), typeof(GridControl), new PropertyMetadata(FontStretches.Normal));
        public static readonly DependencyProperty FocusedLabelFontStyleProperty =
            DependencyProperty.RegisterAttached("FocusedLabelFontStyle", typeof(FontStyle), typeof(GridControl), new PropertyMetadata(FontStyles.Normal));

        public static readonly DependencyProperty ValueFontSizeProperty =
            DependencyProperty.RegisterAttached("ValueFontSize", typeof(double), typeof(GridControl), new PropertyMetadata(12d));
        public static readonly DependencyProperty ValueFontWeightProperty =
            DependencyProperty.RegisterAttached("ValueFontWeight", typeof(FontWeight), typeof(GridControl), new PropertyMetadata(FontWeights.Normal));
        public static readonly DependencyProperty ValueFontFamilyProperty =
            DependencyProperty.RegisterAttached("ValueFontFamily", typeof(FontFamily), typeof(GridControl), new PropertyMetadata(new FontFamily("Segoe UI")));
        public static readonly DependencyProperty ValueFontStretchProperty =
            DependencyProperty.RegisterAttached("ValueFontStretch", typeof(FontStretch), typeof(GridControl), new PropertyMetadata(FontStretches.Normal));
        public static readonly DependencyProperty ValueFontStyleProperty =
            DependencyProperty.RegisterAttached("ValueFontStyle", typeof(FontStyle), typeof(GridControl), new PropertyMetadata(FontStyles.Normal));

        public static readonly DependencyProperty FocusedValueFontSizeProperty =
            DependencyProperty.RegisterAttached("FocusedValueFontSize", typeof(double), typeof(GridControl), new PropertyMetadata(12d));
        public static readonly DependencyProperty FocusedValueFontWeightProperty =
            DependencyProperty.RegisterAttached("FocusedValueFontWeight", typeof(FontWeight), typeof(GridControl), new PropertyMetadata(FontWeights.Normal));
        public static readonly DependencyProperty FocusedValueFontFamilyProperty =
            DependencyProperty.RegisterAttached("FocusedValueFontFamily", typeof(FontFamily), typeof(GridControl), new PropertyMetadata(new FontFamily("Segoe UI")));
        public static readonly DependencyProperty FocusedValueFontStretchProperty =
            DependencyProperty.RegisterAttached("FocusedValueFontStretch", typeof(FontStretch), typeof(GridControl), new PropertyMetadata(FontStretches.Normal));
        public static readonly DependencyProperty FocusedValueFontStyleProperty =
            DependencyProperty.RegisterAttached("ValueFontStyle", typeof(FontStyle), typeof(GridControl), new PropertyMetadata(FontStyles.Normal));

        static GridControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridControl), new FrameworkPropertyMetadata(typeof(GridControl)));
        }
    }
}
