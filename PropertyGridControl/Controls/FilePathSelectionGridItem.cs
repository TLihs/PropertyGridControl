using System;
using System.Text.RegularExpressions;
using System.Windows;
using PropertyGridControl.Base;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;
using System.Linq;

namespace PropertyGridControl.Controls
{
    public class FilePathSelectionGridItem : BasePathSelectionGridItem
    {
        // This regex is not sufficient, since filters are added via CommonFileDialogFilter()
        // Input should be like: "All Files|*|Images|jpg;bmp|Text Files|txt;doc;docx;docm||"
        // ^([a-zA-Z0-9 ]+(\(\*\.[a-zA-Z0-9*]*\))\;)*$ <- not finished (https://regex101.com/r/yBd6Rj/1)
        public readonly Regex ExtensionFilterString = new Regex("^(?'Filter'(?:[^\\|]+)\\|((?:[a-zA-Z0-9]+)?|(?:||$))(?:(?:;((?:[a-zA-Z0-9]+)))+|(\\*)?)\\|)+\\|$");

        public event EventHandler<string> ExtensionFilterChanged;
        public static readonly DependencyProperty ExtensionFilterProperty =
            DependencyProperty.RegisterAttached("ExtensionFilter", typeof(string), typeof(FilePathSelectionGridItem), new PropertyMetadata(string.Empty));
        public string ExtensionFilter
        {
            get => (string)GetValue(ExtensionFilterProperty);
            set
            {
                if (!string.IsNullOrEmpty(value) && !ExtensionFilterString.IsMatch(value))
                    throw new ArgumentException("Filter string does not match the predefined format (format is based on Windows FileDialog)");
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

        private bool SetExtensionFilter(string filterstring)
        {
            PathSelectionDialog.Filters.Clear();

            if (string.IsNullOrEmpty(filterstring))
                return true;

            if (!ExtensionFilterString.IsMatch(filterstring))
                return false;

            CommonFileDialogFilter filter;
            string[] splittedentry;
            string filtername;
            string extensionstext;
            string[] extensions;
            foreach (Group group in ExtensionFilterString.Matches(filterstring)[0].Groups)
            {
                if (group.Name == "Filter")
                {
                    foreach (Capture capture in group.Captures)
                    {
                        splittedentry = capture.Value.Split('|');
                        filtername = splittedentry[0];
                        extensionstext = splittedentry[1];
                        extensions = extensionstext.Split(';');

                        filter = new CommonFileDialogFilter();
                        filter.DisplayName = filtername;
                        foreach (string ext in extensions)
                            filter.Extensions.Add(ext);

                        PathSelectionDialog.Filters.Add(filter);
                    }
                }
            }

            return true;
        }

        protected override void OnButtonPathSelection_Click(object sender, RoutedEventArgs e)
        {
            PathSelectionDialog.EnsureFileExists = EnsureFileExists;
            PathSelectionDialog.IsFolderPicker = false;

            base.OnButtonPathSelection_Click(sender, e);
        }
    }
}
