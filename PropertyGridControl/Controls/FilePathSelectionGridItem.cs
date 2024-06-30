using Microsoft.WindowsAPICodePack.Dialogs;
using PropertyGridControl.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace PropertyGridControl.Controls
{
    public partial class FilePathSelectionGridItem : BasePathSelectionGridItem
    {
        // This regex is not sufficient, since filters are added via CommonFileDialogFilter()
        // Input should be like: "All Files|*|Images|jpg;bmp|Text Files|txt;doc;docx;docm||"
        // ^([a-zA-Z0-9 ]+(\(\*\.[a-zA-Z0-9*]*\))\;)*$ <- not finished (https://regex101.com/r/yBd6Rj/1)
        [GeneratedRegex("^((?'Name'([^\\|]+))\\|((?'Filter'([^\\|]+))\\|))+\\|$")]
        public static partial Regex ExtensionFilterString();

        public event EventHandler<string>? ExtensionFilterChanged;
        public static readonly DependencyProperty ExtensionFilterProperty =
            DependencyProperty.RegisterAttached("ExtensionFilter", typeof(string),
                typeof(GridControl), new PropertyMetadata(string.Empty));
        public string ExtensionFilter
        {
            get => (string)GetValue(ExtensionFilterProperty);
            set
            {
                if (!string.IsNullOrEmpty(value) && !ExtensionFilterString().IsMatch(value))
                    throw new ArgumentException("Filter string does not match the " +
                        "predefined format (i.e. 'Text Files|txt;doc||')");
                SetExtensionFilter(value);
                SetValue(ExtensionFilterProperty, value);
                ExtensionFilterChanged?.Invoke(this, value);
            }
        }
        public event EventHandler<bool>? EnsureFileExistsChanged;
        public static readonly DependencyProperty EnsureFileExistsProperty =
            DependencyProperty.RegisterAttached("EnsureFileExists", typeof(bool),
                typeof(GridControl), new PropertyMetadata(true));
        public bool EnsureFileExists
        {
            get => (bool)GetValue(EnsureFileExistsProperty);
            set
            {
                SetValue(EnsureFileExistsProperty, value);
                EnsureFileExistsChanged?.Invoke(this, value);
            }
        }

        public FilePathSelectionGridItem() :
            base($"FilePathSelectionGridItem{(Items.Count > 0 ? (Items.Count + 1).ToString() : string.Empty)}")
        {

        }

        private bool SetExtensionFilter(string filterstring)
        {
            Debug.Print($"SetExtensionFilter('{filterstring}')");
            PathSelectionDialog.Filters.Clear();

            if (string.IsNullOrEmpty(filterstring))
                return true;

            if (!ExtensionFilterString().IsMatch(filterstring))
                return false;

            CommonFileDialogFilter filter;
            List<string> filternames = [];
            List<string> extensions = [];
            foreach (Match match in ExtensionFilterString().Matches(filterstring))
                foreach (Group group in match.Groups.Cast<Group>())
                    foreach (Capture capture in group.Captures)
                        if (group.Name == "Name")
                            filternames.Add(capture.Value);
                        else if (group.Name == "Filter")
                            extensions.Add(capture.Value);

            for (int filterindex = 0; filterindex < filternames.Count; filterindex++)
            {
                filter = new CommonFileDialogFilter
                {
                    DisplayName = filternames[filterindex]
                };

                string[] filterextensions = extensions[filterindex].Split(';');
                foreach (string extension in filterextensions)
                    filter.Extensions.Add(extension);

                PathSelectionDialog.Filters.Add(filter);
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
