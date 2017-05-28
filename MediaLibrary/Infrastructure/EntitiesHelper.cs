using System;
using System.Linq.Expressions;
using MediaLibrary.Interfaces;
using ToolBox.Expressions;

namespace MediaLibrary.Infrastructure
{
    public static class EntitiesHelper
    {
        public static Func<IField, bool> NameIs(string name)
        {
            return Expression<IField>(x => x.FieldType.Role == FieldRoles.Name
                                   && (x.Value == null ? name == null : x.Value.ToString() == name));
        }

        public static Func<IField, bool> RoleIs(FieldRoles role)
        {
            return Expression<IField>(x => x.FieldType.Role == role);
        }

        public static Func<IField, bool> ValueIs(string fieldName, object value)
        {
            return Expression<IField>(x => x.Name == fieldName && x.Value.Equals(value));
        }

        private static Func<T, bool> Expression<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return new FilterExpression<T>(predicate).AsExpression().Compile();
        }

    }
}
