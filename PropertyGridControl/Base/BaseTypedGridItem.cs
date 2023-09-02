using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PropertyGridControl.Base
{
    public abstract class BaseTypedGridItem<T1, T2, T3> : BaseGridItem
        where T2 : Control
        where T3 : BaseTypedGridItem<T1, T2, T3>
    {
        public static List<T3> Items = new List<T3>();

        public event EventHandler<T1> ValueChanged;
        public void InvokeValueChanged(T1 newValue)
        {
            ValueChanged.Invoke(this, newValue);
        }

        private T1 _value;

        protected T2 _valueControl;

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached("Value", typeof(T1), typeof(T2), new PropertyMetadata(default(T1)));
        public T1 Value
        {
            get => _value;
            set
            {
                if (!value.Equals(_value) && value != null)
                {
                    _value = value;
                    InvokeValueChanged(_value);
                }
            }
        }

        public T2 ValueControl
        {
            get => _valueControl;
            set
            {
                if (value != null)
                {
                    if (_valueControl != null)
                        this.Children.Remove(_valueControl);

                    _valueControl = value;
                    this.Children.Add(_valueControl);
                    Grid.SetColumn(_valueControl, 1);
                    Grid.SetColumnSpan(_valueControl, 1);
                }
            }
        }

        public BaseTypedGridItem(string name, T1 defaultValue) : base(name)
        {
            Items.Add((T3)this);

            Value = defaultValue;
            ValueChanged += OnValueChanged;
        }

        public abstract void OnValueChanged(object sender, T1 e);
    }
}
