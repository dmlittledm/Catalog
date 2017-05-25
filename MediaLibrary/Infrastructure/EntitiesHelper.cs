using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MediaLibrary.Interfaces;
using ToolBox.Expressions;

namespace MediaLibrary.Infrastructure
{
    public static class EntitiesHelper
    {
        public static Func<IField, bool> NameIs(string name)
        {
            return Expression<IField>(x => x.FieldType.Role == FieldRoles.Name
                                   & x.Value == null ? x.Value.ToString() == name : name == null);
        }

        public static Func<IField, bool> FieldRoleIs(FieldRoles role)
        {
            return Expression<IField>(x => x.FieldType.Role == role);
        }

        public static Func<IField, bool> FieldValueIs(string fieldName, object value)
        {
            return Expression<IField>(x => x.Name == fieldName && x.Value == value);
        }

        private static Func<T, bool> Expression<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return new FilterExpression<T>(predicate).AsExpression().Compile();
        }

    }
}
