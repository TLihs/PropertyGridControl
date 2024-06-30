using PropertyGridControl.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PropertyGridControl.Controls
{
    public class CheckBoxGridItem : BaseTypedGridItem<bool, CheckBox, CheckBoxGridItem>
    {
        public CheckBoxGridItem() : base($"CheckBoxGridItem{(Items.Count > 0 ? (Items.Count + 1).ToString() : string.Empty)}")
        {

        }
    }
}
