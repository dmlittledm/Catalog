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

        public IFieldType FieldType { get; } // TODO: need to handle changes in this field to correlate with the Value

        public string Name => FieldType.Name;

        public bool IsDirty { get; private set; }


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


        public void Update(object value)
        {
            CheckValue(value);

            Value = value;
        }


        // TODO: remove these fielters from interface and move methods to some helper-class with static methods
        public Expression<Func<IField, bool>> NameIs(string name)
        {
            // TODO: find how to easily combine expressions to use FieldRoleIs() here
            return Expression(x => x.FieldType.Role == FieldRoles.Name 
                & x.Value == null ? x.Value.ToString() == name : name == null);
            //return Expression(x => x.FieldRoleIs(FieldRoles.Name) & x.Value?.ToString() == name));
        }

        public Expression<Func<IField, bool>> FieldRoleIs(FieldRoles role)
        {
            return Expression(x => x.FieldType.Role == role);
        }

        private static Expression<Func<IField, bool>> Expression(Expression<Func<IField, bool>> predicate)
        {
            return new FilterExpression<IField>(predicate).AsExpression();
        }

        /// <summary> Проверить, что значение соответствует требованиям
        /// </summary>
        /// <param name="value"></param>
        private void CheckValue(object value)
        {
            if (!FieldType.IsMandatory)
                return;

            if (value == null)
                throw new ArgumentNullException(Messages.Field.MandatoryFieldValueCantBeNullOrEmpty);

            var type = FieldType.GetDataType();
            if (string.IsNullOrEmpty(value.ToString()) && !type.IsValueType)
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
