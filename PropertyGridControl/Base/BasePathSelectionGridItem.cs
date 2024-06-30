using Microsoft.WindowsAPICodePack.Dialogs;
using PropertyGridControl.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PropertyGridControl.Base
{
    public abstract class BasePathSelectionGridItem : BaseTextTypedGridItem<string, TextBox, BasePathSelectionGridItem>
    {
        public readonly Button Button_PathSelection = new Button() { Content = "..." };

        protected CommonOpenFileDialog PathSelectionDialog;

        public event EventHandler<bool>? EnsurePathExistsChanged;
        public static readonly DependencyProperty EnsurePathExistsProperty =
            DependencyProperty.RegisterAttached("EnsurePathExists", typeof(bool),
                typeof(BasePathSelectionGridItem), new PropertyMetadata(true));
        public bool EnsurePathExists
        {
            get => (bool)GetValue(EnsurePathExistsProperty);
            set
            {
                SetValue(EnsurePathExistsProperty, value);
                EnsurePathExistsChanged?.Invoke(this, value);
            }
        }
        public event EventHandler<bool>? EnsureValidNamesChanged;
        public static readonly DependencyProperty EnsureValidNamesProperty =
            DependencyProperty.RegisterAttached("EnsureValidNames", typeof(bool),
                typeof(BasePathSelectionGridItem), new PropertyMetadata(true));
        public bool EnsureValidNames
        {
            get => (bool)GetValue(EnsureValidNamesProperty);
            set
            {
                SetValue(EnsureValidNamesProperty, value);
                EnsureValidNamesChanged?.Invoke(this, value);
            }
        }
        public event EventHandler<bool>? NavigateToShortcutChanged;
        public static readonly DependencyProperty NavigateToShortcutProperty =
            DependencyProperty.RegisterAttached("NavigateToShortcut", typeof(bool),
                typeof(BasePathSelectionGridItem), new PropertyMetadata(true));
        public bool NavigateToShortcut
        {
            get => (bool)GetValue(NavigateToShortcutProperty);
            set
            {
                SetValue(NavigateToShortcutProperty, value);
                NavigateToShortcutChanged?.Invoke(this, value);
            }
        }
        public event EventHandler<bool>? RestoreDirectoryChanged;
        public static readonly DependencyProperty RestoreDirectoryProperty =
            DependencyProperty.RegisterAttached("RestoreDirectory", typeof(bool),
                typeof(BasePathSelectionGridItem), new PropertyMetadata(true));
        public bool RestoreDirectory
        {
            get => (bool)GetValue(RestoreDirectoryProperty);
            set
            {
                SetValue(RestoreDirectoryProperty, value);
                RestoreDirectoryChanged?.Invoke(this, value);
            }
        }
        public event EventHandler<bool>? ShowPlacesListChanged;
        public static readonly DependencyProperty ShowPlacesListProperty =
            DependencyProperty.RegisterAttached("ShowPlacesList", typeof(bool),
                typeof(BasePathSelectionGridItem), new PropertyMetadata(true));
        public bool ShowPlacesList
        {
            get => (bool)GetValue(ShowPlacesListProperty);
            set
            {
                SetValue(ShowPlacesListProperty, value);
                ShowPlacesListChanged?.Invoke(this, value);
            }
        }
        public event EventHandler<bool>? MultiselectChanged;
        public static readonly DependencyProperty MultiselectProperty =
            DependencyProperty.RegisterAttached("Multiselect", typeof(bool),
                typeof(BasePathSelectionGridItem), new PropertyMetadata(true));
        public bool Multiselect
        {
            get => (bool)GetValue(MultiselectProperty);
            set
            {
                SetValue(MultiselectProperty, value);
                MultiselectChanged?.Invoke(this, value);
            }
        }
        public event EventHandler<string>? DialogTitleChanged;
        public static readonly DependencyProperty DialogTitleProperty =
            DependencyProperty.RegisterAttached("DialogTitle", typeof(string),
                typeof(BasePathSelectionGridItem), new PropertyMetadata("Title"));
        public string DialogTitle
        {
            get => (string)GetValue(DialogTitleProperty);
            set
            {
                SetValue(DialogTitleProperty, value);
                DialogTitleChanged?.Invoke(this, value);
            }
        }

        public BasePathSelectionGridItem(string name) : base(name)
        {
            ValueControlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1d, GridUnitType.Star) });
            ValueControlGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30d, GridUnitType.Pixel) });

            ValueControlGrid.Children.Add(Button_PathSelection);
            Grid.SetColumn(Button_PathSelection, 1);
            Grid.SetColumnSpan(Button_PathSelection, 1);

            Button_PathSelection.Click += OnButtonPathSelection_Click;
            PathSelectionDialog = new CommonOpenFileDialog();
        }

        protected virtual void OnButtonPathSelection_Click(object sender, RoutedEventArgs e)
        {
            PathSelectionDialog.EnsurePathExists = EnsurePathExists;
            PathSelectionDialog.EnsureValidNames = EnsureValidNames;
            PathSelectionDialog.Multiselect = Multiselect;
            PathSelectionDialog.NavigateToShortcut = NavigateToShortcut;
            PathSelectionDialog.RestoreDirectory = RestoreDirectory;
            PathSelectionDialog.ShowPlacesList = ShowPlacesList;
            PathSelectionDialog.Title = DialogTitle;

            PathSelectionDialog.ShowDialog();
        }
    }
}
