using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ToolBox.Reflection;

namespace ToolBox.Utils
{
    public static class ReflectionUtils
    {
        public static string GetPropertyName<TObject, TResult>(Expression<Func<TObject, TResult>> expression)
        {
            expression.Required();

            var member = expression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException("Expression is not a member access", "expression");
            
            return member.Member.Name;
        }

        public static string GetHumanReadClassName(Type type)
        {
            if (type.IsArray)
                return type.GetElementType().Name + "s";
            else if (type.GetInterface("IEnumerable") != null)
                return type.GetGenericArguments()[0].Name + "s";
            else
                return type.Name;
        }

        public static void SetPrivateProperty<TObject, TProperty>(TObject obj, Expression<Func<TObject, TProperty>> propertyLambda, TProperty val)
        {
            var type = typeof(TObject);
            var propertyName = GetPropertyName(propertyLambda);

            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo == null)
                throw new InvalidOperationException(string.Format("Can't find property {0} in {1}.", propertyName, type.Name));

            if(propertyInfo.DeclaringType != obj.GetType())
                propertyInfo = propertyInfo.DeclaringType.GetProperty(propertyName);

            propertyInfo.SetValue(obj, val, null);
        }

        public static CompositePropertyInfo GetCompositePropertyInfo<TObject, TProperty>(Expression<Func<TObject, TProperty>> propertyLambda)
        {
            MemberExpression member = propertyLambda.Body as MemberExpression;
            
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda.ToString()));

            return new CompositePropertyInfo(member);
        }

        public static string GetPropertyDisplayName<TObject, TResult>(Expression<Func<TObject, TResult>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            var propertyName = GetPropertyName(expression);

            Type type = typeof(TObject);

            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo == null)
                throw new InvalidOperationException(string.Format("Can't find property {0} in {1}.", propertyName, type.Name));

            var displayNameAttribute =
                (DisplayNameAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(DisplayNameAttribute));

            if (displayNameAttribute != null)
                return displayNameAttribute.DisplayName;

            var displayAttribute =
                (DisplayAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(DisplayAttribute));

            if (displayAttribute != null)
                return displayAttribute.GetName();

            return propertyName;
        }

        public static object GetPropertyValue(object obj, string propertyName)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
                return propertyInfo.GetValue(obj);

            return null;
        }

        public static bool HasAttribute<T>(object obj) where T : Attribute
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            return obj.GetType().GetCustomAttribute(typeof(T)) != null;
        }

        public static dynamic CreateDynamicObject(Dictionary<string, string> properties)
        {
            ExpandoObject obj = new ExpandoObject();

            var propertyKeys = properties.Keys.Select(s => s.Split('.'));

            foreach (var propertyKey in propertyKeys.GroupBy(x => x[0]))
            {
                object propertyValue = null;
                if (propertyKey.Count() > 1)
                {
                    var complexObjectProperties = properties.Where(w => w.Key.StartsWith(propertyKey.Key))
                        .ToDictionary(x => x.Key.Substring(propertyKey.Key.Length + 1), x => x.Value);

                    propertyValue = CreateDynamicObject(complexObjectProperties);
                }
                else
                    propertyValue = properties[propertyKey.Key];

                string propertyName = propertyKey.Key;
                if (propertyName.Contains("["))
                {
                    propertyName = propertyName.Substring(0, propertyName.IndexOf("["));

                    if (((IDictionary<string, object>)obj).ContainsKey(propertyName))
                    {
                        ((List<object>)((IDictionary<string, object>)obj)[propertyName]).Add(propertyValue);
                    }
                    else
                        ((IDictionary<string, object>)obj).Add(propertyName, new List<object> { propertyValue });
                }
                else
                    ((IDictionary<string, object>)obj).Add(propertyName, propertyValue);
            }

            return (dynamic)obj;
        }

        public static Dictionary<string, string> ObjectToDictionary(object obj)
        {
            var result = new Dictionary<string, string>();

            var objType = obj.GetType();
            foreach (var property in objType.GetProperties())
            {
                if (property.PropertyType.IsPrimitive)
                    result.Add(property.Name, property.GetValue(obj).ToString());
                else if (property.PropertyType == typeof(string))
                    result.Add(property.Name, (string)property.GetValue(obj));
                else if (typeof(IEnumerable<object>).IsAssignableFrom(property.PropertyType))
                {
                    var array = property.GetValue(obj) as IEnumerable<object>;

                    int i = 0;
                    foreach (var item in array)
                    {
                        foreach (var value in ArrayItemToDictionary(property.Name, i, item))
                            result.Add(value.Key, value.Value);

                        i++;
                    }
                }
                else
                {
                    foreach (var value in ObjectToDictionary(property.GetValue(obj)))
                        result.Add(property.Name + "." + value.Key, value.Value);
                }
            }

            return result;
        }

        private static Dictionary<string, string> ArrayItemToDictionary(string propertyName, int index, object item)
        {
            var result = new Dictionary<string, string>();

            var itemType = item.GetType();

            if (itemType.IsPrimitive)
                result.Add(propertyName + "[" + index + "]", item.ToString());
            else if (itemType == typeof(string))
                result.Add(propertyName + "[" + index + "]", (string)item);
            else
            {
                foreach (var value in ObjectToDictionary(item))
                    result.Add(propertyName + "[" + index + "]." + value.Key, value.Value);
            }

            return result;
        }



        private static string CreateSubPropertyName(string prefix, string propertyName)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                return propertyName;
            }
            else if (string.IsNullOrEmpty(propertyName))
            {
                return prefix;
            }
            else
            {
                return prefix + "." + propertyName;
            }
        }

        private static IEnumerable<string> GetZeroBasedIndexes()
        {
            int i = 0;
            while (true)
            {
                yield return i.ToString(CultureInfo.InvariantCulture);
                i++;
            }
        }

        private static string CreateSubIndexName(string prefix, string index)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}[{1}]", prefix, index);
        }

        public static bool IsNullableValueType(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        public static bool TypeAllowsNullValue(Type type)
        {
            return (!type.IsValueType || IsNullableValueType(type));
        }

        public static object GetDefaultValue(Type type)
        {
            return (TypeAllowsNullValue(type)) ? null : Activator.CreateInstance(type);
        }

        private static bool MatchesGenericType(Type type, Type matchType)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == matchType;
        }

        public static Type ExtractGenericInterface(Type queryType, Type interfaceType)
        {
            if (MatchesGenericType(queryType, interfaceType))
                return queryType;

            return queryType.GetInterfaces().FirstOrDefault(i => MatchesGenericType(i, interfaceType));
        }

        public static T CreateObject<T>(Dictionary<string, object> propertyValues)
            where T: class, new()
        {
            return (T)CreateObject(typeof(T), propertyValues);
        }

        /// <summary>
        /// Создать объект из набора значений его свойств
        /// </summary>
        /// <param name="type">Тип объекта - класс с дефолтным конструктором </param>
        /// <param name="propertyValues">Значения свойств</param>
        /// <returns></returns>
        public static object CreateObject(Type type, Dictionary<string, object> propertyValues)
        {
            if (!type.IsClass)
                throw new ArgumentException("Type must be a class.");

            var instance = Activator.CreateInstance(type);
            if (propertyValues == null || !propertyValues.Any())
                return instance;

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                object value;
                if(TryGetPropertyValue(property.PropertyType, propertyValues, property.Name, out value))
                    property.SetValue(instance, value);
            }

            return instance;
        }
        private static IList CollectListOf(Type elementType, Dictionary<string, object> propertyValues, string prefix)
        {
            var listType = typeof(List<>).MakeGenericType(elementType);
            var list = (IList)Activator.CreateInstance(listType);
            foreach (var index in GetZeroBasedIndexes())
            {
                var subIndexName = CreateSubIndexName(prefix, index);

                object listElement;

                if (!TryGetPropertyValue(elementType, propertyValues, subIndexName, out listElement))
                    break;

                list.Add(listElement);
            }
            return list;
        }

        private static IList<KeyValuePair<object, object>> CollectDictionaryListOf(Type keyType, Type valueType,
            Dictionary<string, object> propertyValues, string prefix)
        {
            List<KeyValuePair<object, object>> dictionaryList = new List<KeyValuePair<object, object>>();
            foreach (var index in GetZeroBasedIndexes())
            {
                string subIndexKey = CreateSubIndexName(prefix, index);

                object keyValue;
                string keyFieldKey = CreateSubPropertyName(subIndexKey, "key");
                if (!TryGetPropertyValue(keyType, propertyValues, keyFieldKey, out keyValue))
                    break;

                object valueValue;
                string valueFieldKey = CreateSubPropertyName(subIndexKey, "value");
                if (!TryGetPropertyValue(valueType, propertyValues, valueFieldKey, out valueValue))
                    valueValue = GetDefaultValue(valueType);

                dictionaryList.Add(new KeyValuePair<object, object>(keyValue, valueValue));
            }
            return dictionaryList;
        }

        private static object CreateInstanceOfPropertyType(Type type)
        {
            Type typeToCreate = type;

            //Для интерфейсов используем их реализации
            if (type.IsInterface && type.IsGenericType)
            {
                Type genericTypeDefinition = type.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(IDictionary<,>) || genericTypeDefinition == typeof(IReadOnlyDictionary<,>))
                {
                    typeToCreate = typeof(Dictionary<,>).MakeGenericType(type.GetGenericArguments());
                }
                else if (genericTypeDefinition == typeof(IEnumerable<>) || genericTypeDefinition == typeof(ICollection<>) || genericTypeDefinition == typeof(IList<>) ||
                    genericTypeDefinition == typeof(IReadOnlyCollection<>) || genericTypeDefinition == typeof(IReadOnlyList<>))
                {
                    typeToCreate = typeof(List<>).MakeGenericType(type.GetGenericArguments());
                }
            }

            return Activator.CreateInstance(typeToCreate);
        }

        private static bool TryGetPropertyValue(Type type, Dictionary<string, object> propertyValues, string name, out object value) 
        {
            value = GetDefaultValue(type);

            if (propertyValues.ContainsKey(name))
            {
                value = ConvertPropertyValue(propertyValues[name], type);
                return true;
            }

            if (type.IsPrimitive || type == typeof(string) || type == typeof(DateTime) || IsNullableValueType(type) || type.IsEnum)
                return false;


            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                var list = CollectListOf(elementType, propertyValues, name);

                if (list.Count == 0) return false;

                Array array = Array.CreateInstance(elementType, list.Count);
                list.CopyTo(array, 0);

                value = array;
                return true; 
            }

            //Свойство это класс или интерфейс. Нужно создать экземпляр чтобы получить конкретный тип объекта
            var propertyInstance = CreateInstanceOfPropertyType(type);
            var propertyInstanceType = propertyInstance.GetType();

            // Если это словарь
            Type dictionaryType = ExtractGenericInterface(propertyInstanceType, typeof(IDictionary<,>));
            if (dictionaryType != null)
            {
                Type[] genericArguments = dictionaryType.GetGenericArguments();
                Type keyType = genericArguments[0];
                Type valueType = genericArguments[1];

                var dictionaryList = CollectDictionaryListOf(keyType, valueType, propertyValues, name);

                if (dictionaryList.Count == 0) return false;

                var addMethod = dictionaryType.GetMethod("Add", new Type[] { keyType, valueType });

                if (addMethod != null)
                    foreach (var item in dictionaryList)
                        addMethod.Invoke(propertyInstance, new object[] { item.Key, item.Value });

                value = propertyInstance;
                return true;
            }

            //Если это коллекция
            Type enumerableType = ExtractGenericInterface(propertyInstanceType, typeof(IEnumerable<>));
            if (enumerableType != null)
            {
                Type elementType = enumerableType.GetGenericArguments()[0];

                Type collectionType = typeof(ICollection<>).MakeGenericType(elementType);
                if (collectionType.IsInstanceOfType(propertyInstance))
                {
                    var list = CollectListOf(elementType, propertyValues, name);

                    if (list.Count == 0) return false;

                    var addMethod = collectionType.GetMethod("Add", new Type[] { elementType });

                    if (addMethod != null)
                        foreach (var item in list)
                            addMethod.Invoke(propertyInstance, new object[] { item });

                    value = propertyInstance;
                    return true;
                }
            }

            // Это какой-то класс
            var properties = propertyInstanceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var setAtLeastOneProperty = false;

            foreach (var property in properties)
            {
                object propertyValue;
                var propertyName = CreateSubPropertyName(name, property.Name);
                if (TryGetPropertyValue(property.PropertyType, propertyValues, propertyName, out propertyValue))
                {
                    property.SetValue(propertyInstance, propertyValue);
                    setAtLeastOneProperty = true;
                }
            }

            if (setAtLeastOneProperty)
            {
                value = propertyInstance;
                return true;
            }

            return false;
        }

        private static object ConvertPropertyValue(object value, Type propertyType)
        {
            var defaultValue = GetDefaultValue(propertyType);
            if (value == null) return defaultValue;

            try
            {
                if (propertyType == value.GetType())
                    return value;

                if(propertyType.IsPrimitive || IsNullableValueType(propertyType) || propertyType.IsEnum || propertyType == typeof(DateTime))
                    return ConvertValue(value, propertyType);

                if (propertyType == typeof(string))
                    return value.ToString();

                if (propertyType.IsArray)
                {
                    var propertyElementType = propertyType.GetElementType();

                    var list = ConvertToList(value, propertyElementType);

                    if (list.Count == 0) return defaultValue;

                    Array array = Array.CreateInstance(propertyElementType, list.Count);
                    list.CopyTo(array, 0);

                    return array;
                }

                Type dictionaryType = ExtractGenericInterface(propertyType, typeof(IDictionary<,>)) ??
                    ExtractGenericInterface(propertyType, typeof(IReadOnlyDictionary<,>));
                if (dictionaryType != null)
                {
                    Type[] genericArguments = dictionaryType.GetGenericArguments();
                    Type keyType = genericArguments[0];
                    Type valueType = genericArguments[1];

                    var dictionaryList = ConvertToDictionaryList(value, keyType, valueType);

                    if (dictionaryList.Count == 0) return defaultValue;

                    var propertyInstance = CreateInstanceOfPropertyType(propertyType);
                    var addMethod = dictionaryType.GetMethod("Add", new Type[] { keyType, valueType });

                    if (addMethod != null)
                        foreach (var item in dictionaryList)
                            addMethod.Invoke(propertyInstance, new object[] { item.Key, item.Value });

                    return propertyInstance;
                }

                Type enumerableType = ExtractGenericInterface(propertyType, typeof(IEnumerable<>));
                if (enumerableType != null)
                {
                    var enumerableElementType = enumerableType.GetGenericArguments()[0];

                    var list = ConvertToList(value, enumerableElementType);

                    if (list.Count == 0) return defaultValue;

                    var propertyInstance = CreateInstanceOfPropertyType(propertyType);
                    Type collectionType = typeof(ICollection<>).MakeGenericType(enumerableElementType);

                    if (!collectionType.IsInstanceOfType(propertyInstance)) return defaultValue;

                    var addMethod = collectionType.GetMethod("Add", new Type[] { enumerableElementType });

                    if (addMethod != null)
                        foreach (var item in list)
                            addMethod.Invoke(propertyInstance, new object[] { item });

                    return propertyInstance;
                }

                return ProjectTo(value, propertyType) ?? defaultValue;
            }
            catch (Exception e) { return defaultValue; }
        }

        private static object ProjectTo(object value, Type type)
        {
            var valueType = value.GetType();
            if (!valueType.IsClass || type.IsClass) return null;

            var propertyInstance = Activator.CreateInstance(type);

            var destProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var sourceProperties = valueType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var setAtLeastOneProperty = false;

            foreach (var property in destProperties)
            {
                var sourceProp = sourceProperties.FirstOrDefault(x => x.Name == property.Name);
                if (sourceProp != null)
                {
                    property.SetValue(propertyInstance, ConvertPropertyValue(sourceProp.GetValue(value), property.PropertyType));
                    setAtLeastOneProperty = true;
                }
            }

            return setAtLeastOneProperty ? propertyInstance : null;
        }

        private static IList<KeyValuePair<object, object>> ConvertToDictionaryList(object value, Type keyType, Type valueType)
        {
            List<KeyValuePair<object, object>> dictionaryList = new List<KeyValuePair<object, object>>();

            if (value.GetType().GetInterface(nameof(IDictionary)) != null)
            {
                foreach (var key in ((IDictionary)value).Keys)
                    dictionaryList.Add(new KeyValuePair<object, object>(
                            ConvertPropertyValue(key, keyType),
                            ConvertPropertyValue(((IDictionary)value)[key], valueType)));
            }

            return dictionaryList;
        }

        private static IList ConvertToList(object value, Type destinationElementType)
        {
            var listType = typeof(List<>).MakeGenericType(destinationElementType);
            var list = (IList)Activator.CreateInstance(listType);

            var valueType = value.GetType();
            if (valueType.IsArray)
            {
                foreach (var item in ((object[])value))
                    list.Add(ConvertPropertyValue(item, destinationElementType));
            }
            else if (valueType.GetInterface(nameof(IEnumerable)) != null)
            {
                foreach (var item in (IEnumerable)value)
                    list.Add(ConvertPropertyValue(item, destinationElementType));
            }

            return list;
        }

        private static object ConvertValue(object value, Type destinationType)
        {
            var tc = TypeDescriptor.GetConverter(destinationType);
            var valueType = value.GetType();

            if (valueType == typeof(string))
                return tc.ConvertFromString(null, CultureInfo.InvariantCulture, (string)value);

            if (tc.CanConvertFrom(valueType))
                return tc.ConvertFrom(value);

            return Convert.ChangeType(value, destinationType);
        }
    }
}
