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

        protected override void SetBaseGridItemBindings()
        {
            base.SetBaseGridItemBindings();

            SetBinding("ContentFontSize", this, ValueControl, ComboBox.FontSizeProperty);
            SetBinding("ContentFontWeight", this, ValueControl, ComboBox.FontWeightProperty);
            SetBinding("ContentFontFamily", this, ValueControl, ComboBox.FontFamilyProperty);
            SetBinding("ContentFontStretch", this, ValueControl, ComboBox.FontStretchProperty);
            SetBinding("ContentFontStyle", this, ValueControl, ComboBox.FontStyleProperty);
            SetBinding("ContentBackground", this, ValueControl, ComboBox.BackgroundProperty);
            SetBinding("ContentForeground", this, ValueControl, ComboBox.ForegroundProperty);
        }

        public override void OnValueChanged(object sender, string e)
        {
            // throw new NotImplementedException();
        }
    }
}
