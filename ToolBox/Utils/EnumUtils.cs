using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace ToolBox.Utils
{
    public static class EnumUtils
    {
        /// <summary>
        /// Отображает назание элемента перечисления
        /// Ищет по очереди аттрибуты EnumDisplayName, DisplayName и Display. И выводит название из первого попавшегося, иначе ToString.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Type type = value.GetType();

            FieldInfo fieldInfo = type.GetField(Enum.GetName(type, value));
            var enumDisplayNameAttribute =
                (EnumDisplayNameAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(EnumDisplayNameAttribute));

            if (enumDisplayNameAttribute != null)
                return enumDisplayNameAttribute.DisplayName;

            var displayNameAttribute =
                (DisplayNameAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DisplayNameAttribute));

            if (displayNameAttribute != null)
                return displayNameAttribute.DisplayName;

            var displayAttribute =
                (DisplayAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DisplayAttribute));

            if (displayAttribute != null)
                return displayAttribute.GetName();

            return value.ToString();
        }

        /// <summary>
        /// Отображает описание элемента перечисления
        /// Ищет по очереди аттрибуты EnumDescription и Description. И выводит значение первого иначе ToString.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Type type = value.GetType();

            FieldInfo fieldInfo = type.GetField(Enum.GetName(type, value));
            var enumDescriptionAttribute =
                (EnumDescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(EnumDescriptionAttribute));

            if (enumDescriptionAttribute != null)
                return enumDescriptionAttribute.DisplayName;

            var descriptionAttribute =
                (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));

            if (descriptionAttribute != null)
                return descriptionAttribute.Description;

            return value.ToString();
        }
        
        /// <summary>
        ///  Преобразует значение перечислимого типа в число, а затем в строку
        /// </summary>
        /// <param name="value">Исходное значение перечислимого типа</param>
        /// <returns>Строку с текстовым представлением числового значения элемента enum</returns>
        public static string ValueToString(this Enum value)
        {
            value.Required();

            Type type = value.GetType();
            Type underlyingType = Enum.GetUnderlyingType(type);

            return Convert.ChangeType(value, underlyingType).ToString();
        }

        /// <summary>
        /// Определяет: содержится ли указанное значение перечисления в указанном списке значений перечислений
        /// </summary>
        /// <param name="value">Искомое значние</param>
        /// <param name="values">Список значений для поиска</param>
        /// <returns>Результат проверки</returns>
        public static bool ContainsIn(this Enum value, params Enum[] values)
        {
            values.MustBeNotEmpty("Список выбора значений перечисления пуст.");
            value.Required();

            var type = value.GetType();

            foreach (var item in values)
            {
                if (item.GetType() != type)
                    throw new ArgumentException($"Значение «{item}» не принадлежит перечислению «{type.Name}».");

                if (item.ValueToString() == value.ValueToString()) return true;
            }

            return false;
        }

        /// <summary>
        /// Определяет, содержится ли указанное значение в перечислении заданного типа
        /// </summary>
        /// <param name="enumType">тип перечисления</param>
        /// <param name="value">значение</param>
        /// <returns>Результат проверки</returns>
        public static bool IsDefined(Type enumType, object value)
        {
            value.Required();
            enumType.Required();

            if(!enumType.IsEnum)
                throw new ArgumentException($"Указанный тип «{enumType.Name}» не является перечислением.");

            if (!Enum.IsDefined(enumType, value))
                throw new ArgumentException($"Значение «{value}» не принадлежит перечислению «{enumType.Name}».");

            return true;
        }

        /// <summary>
        /// Получить список значений перечисления
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] GetValues<T>()
            where T : struct
        {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new ArgumentException($"{type} is not enum type!");

            return (T[])Enum.GetValues(type);
        }

        /// <summary> Получить словарь пар значение-название перечисления
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<EnumValueName<T>> GetValueNameList<T>()
            where T : struct
        {
            return GetValues<T>()
                .Select(s => new EnumValueName<T>() { Value = s, Name = GetDisplayName(s as Enum) });
                //.ToDictionary(key => key, value => GetDisplayName(value as Enum));
        }
    }

    /// <summary> Структура для передачи пары значение-название перечисления
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumValueName<T>
    {
        public T Value;
        public string Name;
    }

    /// <summary>
    /// Аттрибут с именем элемента перечисления
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class EnumDisplayNameAttribute : DisplayNameAttribute
    {
        public EnumDisplayNameAttribute(string displayName)
            : base(displayName)
        { }
    }

    /// <summary>
    /// Аттрибут с описанием элемента перечисления
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class EnumDescriptionAttribute : DisplayNameAttribute
    {
        public EnumDescriptionAttribute(string displayName)
            : base(displayName)
        { }
    }
}
