using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PropertyGridControl.Base;

namespace PropertyGridControl.Controls
{
    public class ComboBoxGridItem : BaseTextTypedGridItem<string, ComboBox, ComboBoxGridItem>
    {
        public ComboBoxGridItem() : base($"ComboBoxGridItem{(Items.Count > 0 ? (Items.Count + 1).ToString() : string.Empty)}")
        {

        }
    }
}
