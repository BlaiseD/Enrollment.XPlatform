using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public abstract class ReadOnlyObjectBase<T> : IReadOnly
    {
        protected ReadOnlyObjectBase(string name, string templateName)
        {
            Name = name;
            TemplateName = templateName;
        }

        #region Fields
        private T _value;
        private string _name;
        private string _templateName;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion Fields

        #region Properties
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value)
                    return;

                _name = value;
                OnPropertyChanged();
            }
        }

        public string TemplateName
        {
            get => _templateName;
            set
            {
                if (_templateName == value)
                    return;

                _templateName = value;
                OnPropertyChanged();
            }
        }

        public virtual T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value))
                    return;

                _value = value;
                OnPropertyChanged();
            }
        }

        object IReadOnly.Value { get => Value; set => Value = (T)value; }
        #endregion Properties

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
