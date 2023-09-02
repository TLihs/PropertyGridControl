using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PropertyGridControl.Base
{
    public class BaseCheckBoxGridItem : BaseTypedGridItem<bool, CheckBox, BaseCheckBoxGridItem>
    {
        public BaseCheckBoxGridItem() : base($"CheckBoxGridItem{(Items.Count > 0 ? (Items.Count + 1).ToString() : string.Empty)}", false)
        {

        }

        public override void OnValueChanged(object sender, bool e)
        {
            // throw new NotImplementedException();
        }
    }
}
