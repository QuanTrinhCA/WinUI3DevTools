using System;
using System.ComponentModel;

namespace WinUI3DevTools.Classes
{
    /// <summary>
    /// Custom observable boolean class.
    /// </summary>
    public class ObservableBoolean : INotifyPropertyChanged
    {
        private bool _value;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Value
        {
            get => _value;
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

        /// <summary>
        /// Raises the property changed event with the name of the property that changed.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// Custom observable control size class.
    /// </summary>
    public class ObservableControlSize : INotifyPropertyChanged
    {
        private double _height;

        private double _width;

        public event PropertyChangedEventHandler PropertyChanged;

        public double Height
        {
            get => _height;
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
            get => _width;
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

        /// <summary>
        /// Raises the property changed event with the name of the property that changed.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}