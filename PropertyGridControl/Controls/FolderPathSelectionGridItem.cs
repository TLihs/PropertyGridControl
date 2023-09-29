using System.Windows;
using PropertyGridControl.Base;

namespace PropertyGridControl.Controls
{
    public class FolderPathSelectionGridItem : BasePathSelectionGridItem
    {
        public FolderPathSelectionGridItem() : base($"FolderPathSelectionGridItem{(Items.Count > 0 ? (Items.Count + 1).ToString() : string.Empty)}")
        {

        }

        protected override void OnButtonPathSelection_Click(object sender, RoutedEventArgs e)
        {
            PathSelectionDialog.EnsureFileExists = false;
            PathSelectionDialog.EnsurePathExists = EnsurePathExists;
            PathSelectionDialog.EnsureValidNames = EnsureValidNames;
            PathSelectionDialog.Multiselect = Multiselect;
            PathSelectionDialog.NavigateToShortcut = NavigateToShortcut;
            PathSelectionDialog.RestoreDirectory = RestoreDirectory;
            PathSelectionDialog.ShowPlacesList = ShowPlacesList;
            PathSelectionDialog.IsFolderPicker = true;
            PathSelectionDialog.Title = DialogTitle;
        }
    }
}
