using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection;

namespace ToolBox.Reflection
{
    public class CompositePropertyInfo
    {
        private PropertyInfo _property;
        private CompositePropertyInfo _parent;

        public Type ReflectedType
        {
            get { return _parent == null ? _property.ReflectedType : _parent.ReflectedType; }
        }

        public Type DeclaringType
        {
            get { return _parent == null ? _property.DeclaringType : _parent.DeclaringType; }
        }

        public string Name
        {
            get { return _parent == null ? _property.Name : _parent.Name + "." + _property.Name; }
        }

        public Type PropertyType
        {
            get { return _property.PropertyType; }
        }

        public CompositePropertyInfo(MemberExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            PropertyInfo propInfo = expression.Expression.Type.GetProperty(expression.Member.Name);
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    expression.ToString()));

            _property = propInfo;

            var parentExpression = expression.Expression as MemberExpression;
            if (parentExpression != null)
                _parent = new CompositePropertyInfo(parentExpression);
        }

        public LambdaExpression GetLambda()
        {
            var parameter = Expression.Parameter(ReflectedType, "x");
            
            return Expression.Lambda(GetMember(parameter), parameter);
        }

        public MemberExpression GetMember(ParameterExpression parameter)
        {
            return _parent == null ? Expression.MakeMemberAccess(parameter, _property) : Expression.MakeMemberAccess(_parent.GetMember(parameter), _property);
        }

        public object GetValue(object obj)
        {
            Contract.Requires(obj != null);
            
            return _parent == null ? _property.GetValue(obj) : _property.GetValue(_parent.GetValue(obj));
        }

        public CompositePropertyInfo Clone()
        {
            return new CompositePropertyInfo(this.GetMember(Expression.Parameter(ReflectedType, "x")));
        }

        public void AddParent(CompositePropertyInfo parentProperty)
        {
            Contract.Requires(parentProperty != null);
            
            if (_parent == null)
            {
                if (parentProperty.PropertyType != ReflectedType)
                    throw new ArgumentException(string.Format("Property with parameter type '{0}' cannot be child of property with type '{1}'.", ReflectedType, parentProperty.PropertyType));

                _parent = parentProperty;
            }
            else
                _parent.AddParent(parentProperty);
        }

        public static CompositePropertyInfo Combine(CompositePropertyInfo parent, CompositePropertyInfo child)
        {
            Contract.Requires(parent != null);
            Contract.Requires(child != null);

            var result = child.Clone();
            result.AddParent(parent);
            return result;
        }
    }
}
