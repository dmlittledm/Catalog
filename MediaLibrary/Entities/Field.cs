using System.ComponentModel;
using System.Runtime.CompilerServices;
using MediaLibrary.Annotations;
using MediaLibrary.Interfaces;

namespace MediaLibrary.Entities
{
    public class Field<T> : IField 
    {
        private T _value;

        public object Value
        {
            get { return _value; }
            private set
            {
                if (_value.Equals(value))
                    return;

                _value = (T)value;
                IsDirty = true;
                OnPropertyChanged(FieldType.Name);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IFieldType FieldType { get; }

        public string Name => FieldType.Name;

        public bool IsDirty { get; private set; }


        public Field([NotNull] IFieldType fieldType, T value)
        {
            FieldType = fieldType;
            _value = value;
        }


        public void Update(object value)
        {
            Value = value;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
