using System;
using System.Text.RegularExpressions;
using System.Windows;
using PropertyGridControl.Base;

namespace PropertyGridControl.Controls
{
    public class FilePathSelectionGridItem : BasePathSelectionGridItem
    {
        // This regex is not sufficient, since filters are added via CommonFileDialogFilter()
        // Input should be like: "All Files (*.*);Bitmap Images (*.bmp);JPG Images (*.jpg, *.jpeg)"
        // ^([a-zA-Z0-9 ]+(\(\*\.[a-zA-Z0-9*]*\))\;)*$ <- not finished (https://regex101.com/r/yBd6Rj/1)
        public readonly Regex ExtensionFilterString = new Regex("^([a-zA-Z0-9.()\\*\\-\\+ ]+\\|\\*\\.[a-zA-Z0-9*]+\\|)*$");

        public event EventHandler<string> ExtensionFilterChanged;
        public static readonly DependencyProperty ExtensionFilterProperty =
            DependencyProperty.RegisterAttached("ExtensionFilter", typeof(string), typeof(FilePathSelectionGridItem), new PropertyMetadata(string.Empty));
        public string ExtensionFilter
        {
            get => (string)GetValue(ExtensionFilterProperty);
            set
            {
                SetValue(ExtensionFilterProperty, value);
                ExtensionFilterChanged?.Invoke(this, value);
            }
        }
        public event EventHandler<bool> EnsureFileExistsChanged;
        public static readonly DependencyProperty EnsureFileExistsProperty =
            DependencyProperty.RegisterAttached("EnsureFileExists", typeof(bool), typeof(FilePathSelectionGridItem), new PropertyMetadata(true));
        public bool EnsureFileExists
        {
            get => (bool)GetValue(EnsureFileExistsProperty);
            set
            {
                SetValue(EnsureFileExistsProperty, value);
                EnsureFileExistsChanged?.Invoke(this, value);
            }
        }

        public FilePathSelectionGridItem() : base($"FilePathSelectionGridItem{(Items.Count > 0 ? (Items.Count + 1).ToString() : string.Empty)}")
        {

        }

        protected override void OnButtonPathSelection_Click(object sender, RoutedEventArgs e)
        {
            PathSelectionDialog.EnsureFileExists = EnsureFileExists;
            PathSelectionDialog.EnsurePathExists = EnsurePathExists;
            PathSelectionDialog.EnsureValidNames = EnsureValidNames;
            PathSelectionDialog.Multiselect = Multiselect;
            PathSelectionDialog.NavigateToShortcut = NavigateToShortcut;
            PathSelectionDialog.RestoreDirectory = RestoreDirectory;
            PathSelectionDialog.ShowPlacesList = ShowPlacesList;
            PathSelectionDialog.IsFolderPicker = false;
            PathSelectionDialog.Title = DialogTitle;
        }
    }
}
