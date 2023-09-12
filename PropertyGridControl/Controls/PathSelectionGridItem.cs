using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PropertyGridControl.Base;

namespace PropertyGridControl.Controls
{
    public class PathSelectionGridItem : BaseTextTypedGridItem<string, TextBox, PathSelectionGridItem>
    {
        public readonly Button Button_PathSelection = new Button() { Content = "..." };
        
        public PathSelectionGridItem() : base($"PathSelectionGridItem{(Items.Count > 0 ? (Items.Count + 1).ToString() : string.Empty)}")
        {
            ValueControlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1d, GridUnitType.Star) });
            ValueControlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30d, GridUnitType.Pixel) });

            ValueControlGrid.Children.Add(Button_PathSelection);
            Grid.SetColumn(Button_PathSelection, 1);
            Grid.SetColumnSpan(Button_PathSelection, 1);

            Button_PathSelection.Click += OnButtonPathSelection_Click;
        }

        protected virtual void OnButtonPathSelection_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
