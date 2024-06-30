using PropertyGridControl.Base;
using System.Windows;

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
            PathSelectionDialog.IsFolderPicker = true;

            base.OnButtonPathSelection_Click(sender, e);
        }
    }
}
