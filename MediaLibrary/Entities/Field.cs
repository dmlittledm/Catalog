using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using MediaLibrary.Annotations;
using MediaLibrary.Infrastructure;
using MediaLibrary.Interfaces;
using ToolBox.Expressions;

namespace MediaLibrary.Entities
{
    public class Field<T> : IField<T> 
    {
        private T _value;

        public T Value
        {
            get { return _value; }
            private set
            {
                if (_value.Equals(value))
                    return;

                _value = value;
                IsDirty = true;
                OnPropertyChanged(FieldType.Name);
            }
        }

        public IFieldType FieldType { get; } // TODO: need to handle changes in this field to correlate with the Value

        public string Name => FieldType.Name;
        object IField.Value => Value;

        public bool IsDirty { get; private set; }
        public void Update(object value)
        {
            Update((T)value);
        }


        public Field([NotNull] IFieldType fieldType, T value)
        {
            if(fieldType == null)
                throw new ArgumentNullException(nameof(fieldType));

            if(typeof(T) != fieldType.GetDataType())
                throw new ArgumentException(Messages.Field.FieldTypeMismatch, nameof(value));

            FieldType = fieldType;
            CheckValue(value);
            _value = value;
        }


        public void Update(T value)
        {
            CheckValue(value);

            Value = value;
        }


        /// <summary> Проверить, что значение соответствует требованиям
        /// </summary>
        /// <param name="value"></param>
        private void CheckValue(T value)
        {
            if (!FieldType.IsMandatory)
                return;

            if (value == null)
                throw new ArgumentNullException(Messages.Field.MandatoryFieldValueCantBeNullOrEmpty);

            var type = FieldType.GetDataType();
            if (string.IsNullOrEmpty(value.ToString().Trim()) && !type.IsValueType)
                throw new ArgumentNullException(Messages.Field.MandatoryFieldValueCantBeNullOrEmpty);
        }


        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
