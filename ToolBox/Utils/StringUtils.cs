using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ToolBox.Utils
{
    public static class StringUtils
    {
        /// <summary>
        /// Вернуть значение или значение по умолчанию
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string OnEmpty(this string str, string defaultValue)
        {
            return !String.IsNullOrWhiteSpace(str) ? str : defaultValue;
        }

        /// <summary>
        /// Дополнить строку нулями слева до заданной длины
        /// </summary>
        /// <param name="str">Входная строка</param>
        /// <param name="newLength">Новая длина строки (если строка была длиннее, она не меняется)</param>
        /// <returns></returns>
        public static string PadLeftZeroes(this string str, int newLength)
        {
            str.Required();
            newLength.MustBeGreaterThanOrEqual(0);

            return str.PadLeft(newLength, '0');
        }

        /// <summary>
        /// Разбить на части согласно нотации Camel case
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] SplitCamelCase(this string str)
        {
            str.Required();

            return Regex.Split(str, @"(?=\p{Lu}\p{Ll})|(?<=\p{Ll})(?=\p{Lu})")
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .ToArray();
        }

        public static string Join(this string[] strs, string separator)
        {
            return string.Join(separator, strs);
        }


        public static bool EqualsIgnoreCase(this string str, string otherString)
        {
            return ReferenceEquals(str, null) ? ReferenceEquals(otherString, null) :
                str.Equals(otherString, StringComparison.InvariantCultureIgnoreCase);
        }


        /// <summary>
        /// Закодировать в Base64
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncodeToBase64(this string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        public static string DecodeFromBase64(this string str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }

        /// <summary>
        /// Строка закодирована в Base64
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsBase64String(this string s)
        {
            var str = s.Trim();
            return (str.Length % 4 == 0) && Regex.IsMatch(str, Patterns.Base64Chars , RegexOptions.None);

        }

        /// <summary>
        /// Получить строку описывающую действие которое происходило в течении определенного количества времени
        /// Напр: Вы отсутствовали: 1 день 2 часа 5 минут 6 секунд
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string PeriodString(TimeSpan time)
        {
            var result = "";
            if(time.Days > 0)
                result += string.Format("{0} {1} ", time.Days, EntityCount(time.Days, "день", "дня", "дней"));

            if (time.Hours > 0)
                result += string.Format("{0} {1} ", time.Hours, EntityCount(time.Hours, "час", "час", "часов"));

            if (time.Minutes > 0)
                result += string.Format("{0} {1} ", time.Minutes, EntityCount(time.Minutes, "минуту", "минуты", "минут"));

            if (time.Seconds > 0)
                result += string.Format("{0} {1} ", time.Seconds, EntityCount(time.Seconds, "секунду", "секунды", "секунд"));

            return result.Trim();
        }

        /// <summary>
        /// Получение правильной формы слова в зависимости от количества
        /// </summary>
        /// <param name="value">Кол-во сущностей</param>
        /// <param name="oneEntityString">Значение для одной сущности</param>
        /// <param name="fromX2ToX4EntitiesString">Значение для остатка от деления на 10 от 2 до 4 сущностей</param>
        /// <param name="otherEntityCountString">Значение в других случаях</param>
        public static string EntityCount(int value, string oneEntityString, string fromX2ToX4EntitiesString, string otherEntityCountString)
        {
            value = Math.Abs(value);

            if (value % 100 > 10 && value % 100 < 20) return otherEntityCountString;

            switch (value % 10)
            {
                case 1: return oneEntityString;
                case 2:
                case 3:
                case 4: return fromX2ToX4EntitiesString;

                default: return otherEntityCountString;
            }
        }

        #region Форматированный вывод чисел 

        /// <summary>
        /// Приведение к формату для отображения
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ForDisplay(this int value)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberGroupSeparator = " ";

            return value.ToString("n0", nfi);
        }

        public static string ForDisplay(this int? value)
        {
            if (!value.HasValue)
                return "---";

            return value.Value.ForDisplay();
        }

        public static string ForDisplay(this short? value)
        {
            if (!value.HasValue)
                return "---";

            return value.Value.ForDisplay();
        }

        public static string ForDisplay(this short value)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberGroupSeparator = " ";

            return value.ToString("n0", nfi);
        }

        public static string ForDisplay(this byte value)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberGroupSeparator = " ";

            return value.ToString("n0", nfi);
        }

        /// <summary>
        /// Форматированный вывод значения с заданной точностью
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="scale">Точность</param>
        /// <returns>Форматированное представление значения с заданной точностью</returns>
        public static string ForDisplay(this decimal value, byte scale)
        {
            var nfi = new NumberFormatInfo();
            var scaleFormat = "";
            for (int i = 0; i < scale; i++)
            { scaleFormat += '0'; }
            // отображать ли нулевые копейки
            var format = String.Format("### ### ### ### ### ##0.{0}####", scaleFormat);

            return Math.Round(value, scale).ToString(format, nfi).TrimStart(' ')
                .Replace("-     ", "-").Replace("-    ", "-").Replace("-   ", "-").Replace("-  ", "-").Replace("- ", "-");
        }

        #endregion

        /// <summary>
        /// Очистка строки от спецсимволов, кавычек, лишних пробелов и т.д.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Clean(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return str;

            return Regex.Replace(Regex.Replace(str, Patterns.NonLiteral, " "), Patterns.WhiteSpaces, " ").Trim();
        }

        /// <summary>
        /// Паттерны регулярных выражений
        /// </summary>
        public static class Patterns
        {
            public const string Url = @"^(https?|s?ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$";

            public const string Email = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            public const string WhiteSpaces = @"\s+";

            public const string NonLiteral = @"[^a-zA-Z0-9а-яА-ЯёЁ№`!«»@#$%^&*()_+|\-=\\{}\[\]:"";'<>?,./]";

            public const string Number = @"^(-)?[0-9]+([,.][0-9]+)?$";

            public const string IntegerNumber = @"^(-)?[0-9]+$";

            public const string Base64Chars = @"^[a-zA-Z0-9\+/]*={0,3}$";

            public static string NumberWithScale(int scale)
            {
                if(scale > 0)
                    return @"^(-)?[0-9]+([,.][0-9]{1," + scale.ToString() + "})?$";

                return IntegerNumber;
            }

            public static string NumberWithPrecision(int precision)
            {
                return @"^(-)?[0-9]{1," + precision.ToString() + "}([,.][0-9]+)?$";
            }

            public static string HexNumber = @"^[a-fA-F0-9]+$";
        }
    }
}
