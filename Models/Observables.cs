using System;
using System.ComponentModel;

namespace WinUI3DevTools.Models
{
    public class ObservableBoolean : INotifyPropertyChanged
    {
        private bool _value;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value.GetType() != typeof(bool))
                {
                    throw new Exception("Value is not Boolean");
                }
                if (value == _value)
                {
                    return;
                }
                _value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ObservableControlSize : INotifyPropertyChanged
    {
        private double _height;

        private double _width;

        public event PropertyChangedEventHandler PropertyChanged;

        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (value == 0.0)
                {
                    throw new Exception("Height must be larger than 0.0");
                }
                if (value == _height)
                {
                    return;
                }
                _height = value;
                RaisePropertyChanged(nameof(Height));
            }
        }

        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value == 0.0)
                {
                    throw new Exception("Width must be larger than 0.0");
                }
                if (value == _width)
                {
                    return;
                }
                _width = value;
                RaisePropertyChanged(nameof(Width));
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}