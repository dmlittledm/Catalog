using ToolBox.Functional;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace ToolBox.Utils
{
    public static class ValidationUtils
    {
        #region Проверки значений

        /// <summary>
        /// Проверка условия.
        /// </summary>
        /// <param name="condition">Проверяемое условие</param>
        /// <param name="msg">Сообщение</param>
        public static Result Assert(bool condition, string msg = "")
        {
            if (!condition)
                return Result.Fail(msg.OnEmpty("Условие не выполнено."));

            return Result.Ok();
        }

        /// <summary>
        /// Проверка значения на null. 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="msg"></param>
        public static Result IsNull(object obj, string msg = "")
        {
            return Assert(obj == null, msg.OnEmpty("Значение объекта не является пустым."));
        }

        /// <summary>
        /// Проверка значения на null.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="msg"></param>
        public static Result NotNull(this object obj, string msg = "")
        {
            return Assert(obj != null, msg.OnEmpty("Значение объекта не определено."));
        }

        /// <summary>
        /// Проверка значения на null или 0.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="msg"></param>
        public static Result NotNullOrDefault(this int? value, string msg = "")
        {
            return NotNull(value, msg)
                .OnSuccess(() => Assert(value != default(int), msg.OnEmpty("Значение переменной не может быть равно 0.")));
        }

        /// <summary>
        /// Проверить соотвествие значения шаблону
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pattern"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Result Match(this string value, string pattern, string msg = "")
        {
            if(!Regex.IsMatch(value, pattern))
                return Result.Fail(msg.OnEmpty("Значение не соответствует шаблону."));

            return Result.Ok();
        }

        /// <summary>
        /// Проверка значения на null или 0.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="msg"></param>
        public static Result NotNullOrDefault(this short? value, string msg = "")
        {
            return NotNull(value, msg)
                .OnSuccess(() => Assert(value != default(short), msg.OnEmpty("Значение переменной не может быть равно 0.")));
        }

        /// <summary>
        /// Проверка значения на null или 0.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="msg"></param>
        public static Result NotNullOrDefault(this decimal? value, string msg = "")
        {
            return NotNull(value, msg)
                .OnSuccess(() => Assert(value != default(decimal), msg.OnEmpty("Значение переменной не может быть равно 0.")));
        }

        /// <summary>
        /// Проверка значения на null или 0.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="msg"></param>
        public static Result NotNullOrDefault(this double? value, string msg = "")
        {
            return NotNull(value, msg)
                .OnSuccess(() => Assert(value != default(double), msg.OnEmpty("Значение переменной не может быть равно 0.")));
        }

        /// <summary>
        /// Проверка значения на null или Guid.Empty.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="msg"></param>
        public static Result NotNullOrDefault(this Guid? value, string msg = "")
        {
            return NotNull(value, msg)
                .OnSuccess(() => Assert(value != default(Guid), msg.OnEmpty("Значение переменной не может быть пустым.")));
        }

        /// <summary>
        /// Проверка значения на null или отсутствие элементов.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="msg"></param>
        public static Result NotNullOrDefault<T>(this IEnumerable<T> value, string msg = "")
        {
            return NotNull(value, msg)
                .OnSuccess(() => Assert(value.Any(), msg.OnEmpty("Значение переменной не может быть пустым.")));
        }

        /// <summary>
        /// Проверка значения на null или String.Empty.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="msg"></param>
        public static Result NotNullOrDefault(this string value, string msg = "")
        {
            return Assert(!String.IsNullOrEmpty(value), msg.OnEmpty("Значение переменной не может быть пустым."));
        }

        /// <summary>
        /// Проверка значения на null или String.Empty или пустую строку. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="msg"></param>
        public static Result NotNullOrWhiteSpace(this string value, string msg = "")
        {
            return Assert(!String.IsNullOrWhiteSpace(value), msg.OnEmpty("Значение переменной не может быть пустым."));
        }

        /// <summary>
        /// Проверка на null или String.Empty
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <param name="msg"></param>
        /// <param name="moremsg"></param>
        public static Result NotNullOrNotMore(this string value, int maxLength, string msg = "", string moremsg = "")
        {
            return value.NotNullOrDefault(msg)
                .OnSuccess(() => Assert(value.Length <= maxLength, string.Format(moremsg.OnEmpty("Значение переменной не может превышать {0} символов."), maxLength)));
        }

        /// <summary>
        /// Проверка на null или String.Empty или пустую строку
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <param name="msg"></param>
        /// <param name="moremsg"></param>
        public static Result NotWhiteSpaceOrNotMore(this string value, int maxLength, string msg = "", string moremsg = "")
        {
            return value.NotNullOrWhiteSpace(msg)
                .OnSuccess(() => Assert(value.Length <= maxLength, string.Format(moremsg.OnEmpty("Значение переменной не может превышать {0} символов."), maxLength)));
        }

        /// <summary>
        /// Проверка на не превышение длины строки
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <param name="moremsg"></param>
        public static Result NotMore(this string value, int maxLength, string moremsg = "")
        {
            return Assert(value == null || value.Length <= maxLength, string.Format(moremsg.OnEmpty("Значение переменной не может превышать {0} символов."), maxLength));
        }

        #region Bool

        public static Result IsTrue(this bool val, string msg = "")
        {
            return Assert(val, msg);
        }

        public static Result IsFalse(this bool val, string msg = "")
        {
            return Assert(!val, msg);
        }

        #endregion

        #region Guid

        public static Result NotEmpty(this Guid val, string msg = "")
        {
            return Assert(val != Guid.Empty, msg);
        }

        #endregion

        #region Int

        public static Result IsGreaterThan(this int val, int thanValue, string msg = "")
        {
            return Assert(val > thanValue, string.Format(msg.OnEmpty("Значение переменной не может быть меньше или равно {0}."), thanValue));
        }

        public static Result IsLessThan(this int val, int thanValue, string msg = "")
        {
            return Assert(val < thanValue, string.Format(msg.OnEmpty("Значение переменной не может быть больше или равно {0}."), thanValue));
        }

        public static Result IsGreaterThanOrEqual(this int val, int thanValue, string msg = "")
        {
            return Assert(val >= thanValue, string.Format(msg.OnEmpty("Значение переменной не может быть меньше {0}."), thanValue));
        }

        public static Result IsLessThanOrEqual(this int val, int thanValue, string msg = "")
        {
            return Assert(val <= thanValue, string.Format(msg.OnEmpty("Значение переменной не может быть больше {0}."), thanValue));
        }

        public static Result IsEqualsTo(this int val, int toValue, string msg = "")
        {
            return Assert(val == toValue, string.Format(msg.OnEmpty("Значение переменной должно быть равно {0}."), toValue));
        }

        #endregion

        #region Decimal

        public static Result IsGreaterThan(this decimal val, decimal thanValue, string msg = "")
        {
            return Assert(val > thanValue, string.Format(msg.OnEmpty("Значение переменной не может быть меньше или равно {0}."), thanValue));
        }

        public static Result IsLessThan(this decimal val, decimal thanValue, string msg = "")
        {
            return Assert(val < thanValue, string.Format(msg.OnEmpty("Значение переменной не может быть больше или равно {0}."), thanValue));
        }

        public static Result IsGreaterThanOrEqual(this decimal val, decimal thanValue, string msg = "")
        {
            return Assert(val >= thanValue, string.Format(msg.OnEmpty("Значение переменной не может быть меньше {0}."), thanValue));
        }

        public static Result IsLessThanOrEqual(this decimal val, decimal thanValue, string msg = "")
        {
            return Assert(val <= thanValue, string.Format(msg.OnEmpty("Значение переменной не может быть больше {0}."), thanValue));
        }

        public static Result IsEqualsTo(this decimal val, decimal toValue, string msg = "")
        {
            return Assert(val == toValue, string.Format(msg.OnEmpty("Значение переменной должно быть равно {0}."), toValue));
        }

        #endregion

        #region DateTime

        public static Result IsGreaterThan(this DateTime val, DateTime thanValue, string msg = "")
        {
            return Assert(val > thanValue, string.Format(msg.OnEmpty("Значение переменной не может быть меньше или равно {0}."), thanValue));
        }

        public static Result IsLessThan(this DateTime val, DateTime thanValue, string msg = "")
        {
            return Assert(val < thanValue, string.Format(msg.OnEmpty("Значение переменной не может быть больше или равно {0}."), thanValue));
        }

        public static Result IsGreaterThanOrEqual(this DateTime val, DateTime thanValue, string msg = "")
        {
            return Assert(val >= thanValue, string.Format(msg.OnEmpty("Значение переменной не может быть меньше {0}."), thanValue));
        }

        public static Result IsLessThanOrEqual(this DateTime val, DateTime thanValue, string msg = "")
        {
            return Assert(val <= thanValue, string.Format(msg.OnEmpty("Значение переменной не может быть больше {0}."), thanValue));
        }

        public static Result IsEqualsTo(this DateTime val, DateTime toValue, string msg = "")
        {
            return Assert(val == toValue, string.Format(msg.OnEmpty("Значение переменной должно быть равно {0}."), toValue));
        }

        #endregion

        #endregion

        #region Парсинг из строки

        /// <summary>
        /// Получение значения Guid из строки
        /// </summary>
        /// <param name="value">Строка</param>
        /// <returns></returns>
        public static Result<Guid> TryGetGuid(string value, string msg = "")
        {
            Guid guid;
            if (!Guid.TryParse(value, out guid))
            {
                return Result.Fail<Guid>(msg.OnEmpty("Неверное значение уникального идентификатора."));
            }

            return Result.Ok(guid);
        }

        /// <summary>
        /// Получение значения Guid из строки. Значение должно быть не равно Guid.Empty
        /// </summary>
        /// <param name="value">Строка</param>
        /// <returns></returns>
        public static Result<Guid> TryGetNotEmptyGuid(string value, string msg = "")
        {
            Guid guid;
            if (!Guid.TryParse(value, out guid) || guid == Guid.Empty)
            {
                return Result.Fail<Guid>(msg.OnEmpty("Неверное значение уникального идентификатора."));
            }

            return Result.Ok(guid);
        }

        /// <summary>
        /// Получение целого значения из строки
        /// </summary>
        /// <param name="value">Строка</param>
        /// <param name="msg">Сообщение при ошибке</param>
        /// <param name="treatEmptyAsZero">Признак нужно ли раценивать пустую строку как нулевое значение</param>
        /// <returns>Целое число</returns>
        public static Result<int> TryGetInt(string value, string msg = "", bool treatEmptyAsZero = false)
        {
            int result = 0;
            if (!(treatEmptyAsZero && String.IsNullOrWhiteSpace(value)))
            {
                if (!int.TryParse(value, out result))
                {
                    return Result.Fail<int>(msg.OnEmpty("Значение не является целым числом."));
                }
            }

            return Result.Ok(result);
        }

        public static Result<short> TryGetShort(string value, string msg = "", bool treatEmptyAsZero = false)
        {
            short result = 0;
            if (!(treatEmptyAsZero && String.IsNullOrWhiteSpace(value)))
            {
                if (!short.TryParse(value, out result))
                {
                    return Result.Fail<short>(msg.OnEmpty("Значение не является целым числом."));
                }
            }

            return Result.Ok(result);
        }

        public static Result<byte> TryGetByte(string value, string msg = "", bool treatEmptyAsZero = false)
        {
            byte result = 0;
            if (!(treatEmptyAsZero && String.IsNullOrWhiteSpace(value)))
            {
                if (!byte.TryParse(value, out result))
                {
                    return Result.Fail<byte>(msg.OnEmpty("Значение не является целым числом."));
                }
            }

            return Result.Ok(result);
        }

        /// <summary>
        /// Метод возвращает значение перечисления. 
        /// </summary>
        /// <typeparam name="T">Тип перечисления</typeparam>
        /// <param name="value">Строка со значением перечисления в виде </param>
        /// <returns>Значение перечисления</returns>
        public static Result<T> TryGetEnum<T>(string value, string msg = "") where T : struct
        {
            T result;
            if (Enum.TryParse(value, true, out result) && 
                EnumUtils.GetValues<T>().Contains(result))
            {
                return Result.Ok(result);
            }

            return Result.Fail<T>(msg.OnEmpty("Значение не является членом перечисления."));
        }


        /// <summary>
        /// Получение Nullable decimal из строки
        /// </summary>
        /// <param name="value">Строка</param>
        /// <param name="msg">Сообщение при ошибке</param>
        /// <param name="treatEmptyAsZero">Признак нужно ли раценивать не-числовое значение как null</param>
        /// <returns>Nullable decimal</returns>
        public static Result<decimal?> TryGetNullableDecimal(string value, string msg = "", bool treatNonDecimalAsNull = false)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Ok<decimal?>(null); 

            decimal result = 0;
            if (!decimal.TryParse(value.Replace('.', ','), out result))
            {
                if (treatNonDecimalAsNull)
                    return Result.Ok<decimal?>(null);

                return Result.Fail<decimal?>(msg.OnEmpty("Значение не является числом с плавающей точкой."));
            }

            return Result.Ok((decimal?)result);
        }



        /// <summary>
        /// Получение decimal из строки
        /// </summary>
        /// <param name="value">Строка</param>
        /// <param name="msg">Сообщение при ошибке</param>
        /// <param name="treatEmptyAsZero">Признак нужно ли раценивать пустую строку как нулевое значение</param>
        /// <returns>decimal</returns>
        public static Result<decimal> TryGetDecimal(string value, string msg = "", bool treatEmptyAsZero = false)
        {
            decimal result = 0;
            if (!(treatEmptyAsZero && String.IsNullOrWhiteSpace(value)))
            {
                if (!decimal.TryParse(value.Replace('.', ','), out result))
                {
                    return Result.Fail<decimal>(msg.OnEmpty("Значение не является числом с плавающей точкой."));
                }
            }

            return Result.Ok(result);
        }

        /// <summary>
        /// Попытка преобразовать строку в число, ограничив число знаков до и после запятой (точки)
        /// </summary>
        /// <param name="value">Строка</param>
        /// <param name="precision">Максимально допустимое количество знаков до запятой (точки)</param>
        /// <param name="scale">Максимально допустимое количество знаков после запятой (точки)</param>
        /// <param name="parseErrorMsg">Сообщение при ошибке преобразования</param>
        /// <param name="scaleErrorMsg">Сообщение при превышении количества знаков после запятой</param>
        /// <returns>Строка, преобразованная в число</returns>
        public static Result<decimal> TryGetDecimal(string value, int precision, int scale, string parseErrorMsg = "", string precisionErrorMsg = "",
            string scaleErrorMsg = "")
        {
            precision.MustBeGreaterThanOrEqual(1, "Неверно задано количество знаков до запятой.");
            scale.MustBeGreaterThanOrEqual(0, "Неверно задано количество знаков после запятой.");
            value.Required();

            Regex parseRegex = new Regex(StringUtils.Patterns.Number, RegexOptions.Compiled);

            return Assert(parseRegex.IsMatch(value), parseErrorMsg.OnEmpty("Введите число."))
                .OnSuccess(() =>
                {
                    Regex scaleRegex = new Regex(StringUtils.Patterns.NumberWithScale(scale), RegexOptions.Compiled);

                    return Assert(scaleRegex.IsMatch(value), GetScaleErrorMessage(scale, scaleErrorMsg));
                })
                .OnSuccess(() =>
                {
                    Regex precisionRegex = new Regex(StringUtils.Patterns.NumberWithPrecision(precision), RegexOptions.Compiled);

                    return Assert(precisionRegex.IsMatch(value), string.Format(precisionErrorMsg.OnEmpty("Введите не более {0} знаков до запятой."), precision));
                })
                .OnBoth(r => 
                {
                    if (r.Failure)
                        return Result.Fail<decimal>(r.Error);

                    return TryGetDecimal(value, parseErrorMsg);
                });
        }

        /// <summary>
        /// Попытка преобразовать строку в число, ограничив число знаков после запятой (точки)
        /// </summary>
        /// <param name="value">Строка</param>
        /// <param name="scale">Максимально допустимое количество знаков после запятой (точки)</param>
        /// <param name="parseErrorMsg">Сообщение при ошибке преобразования</param>
        /// <param name="scaleErrorMsg">Сообщение при превышении количества знаков после запятой</param>
        /// <returns>Строка, преобразованная в число</returns>
        public static Result<decimal> TryGetDecimal(string value, int scale, string parseErrorMsg = "", string scaleErrorMsg = "")
        {
            scale.MustBeGreaterThanOrEqual(0, "Неверно задано количество знаков после запятой.");
            value.Required();

            Regex parseRegex = new Regex(StringUtils.Patterns.Number, RegexOptions.Compiled);

            return Assert(parseRegex.IsMatch(value), parseErrorMsg.OnEmpty("Введите число."))
                .OnSuccess(() =>
                {
                    Regex scaleRegex = new Regex(StringUtils.Patterns.NumberWithScale(scale), RegexOptions.Compiled);

                    return Assert(scaleRegex.IsMatch(value), GetScaleErrorMessage(scale, scaleErrorMsg));
                })
                .OnBoth(r =>
                {
                    if (r.Failure)
                        return Result.Fail<decimal>(r.Error);

                    return TryGetDecimal(value, parseErrorMsg);
                });
        }

        /// <summary>
        /// Проверка числа на количество знаков после запятой (точки)
        /// </summary>
        /// <param name="value">Число</param>
        /// <param name="scale">Максимально допустимое количество знаков после запятой (точки)</param>
        /// <param name="scaleErrorMsg">Сообщение при превышении количества знаков после запятой</param>
        public static Result CheckDecimalScale(decimal value, int scale, string scaleErrorMsg = "")
        {
            value.Required();
            scale.MustBeGreaterThanOrEqual(0, "Неверно задано количество знаков после запятой.");

            var stringValue = value.ToString();
            
            var pointPosition = stringValue.IndexOfAny(new char[] { '.', ',' });
            if (pointPosition > -1)
            {
                var fractionSize = stringValue.Length - pointPosition - 1;
                return Assert(fractionSize <= scale, GetScaleErrorMessage(scale, scaleErrorMsg));
            }

            return Result.Ok();
        }

        /// <summary>
        /// Проверка числа на количество знаков после запятой (точки)
        /// </summary>
        /// <param name="value">Число</param>
        /// <param name="precision">Максимально допустимое количество знаков до запятой (точки)</param>
        /// <param name="scale">Максимально допустимое количество знаков после запятой (точки)</param>
        /// <param name="precisionErrorMsg">Сообщение при превышении количества знаков до запятой</param>
        /// <param name="scaleErrorMsg">Сообщение при превышении количества знаков после запятой</param>
        public static Result CheckDecimalScaleAndPrecision(decimal value, int precision, int scale, string precisionErrorMsg = "", string scaleErrorMsg = "")
        {
            precision.MustBeGreaterThanOrEqual(1, "Неверно задано количество знаков до запятой.");
            scale.MustBeGreaterThanOrEqual(0, "Неверно задано количество знаков после запятой.");
            value.Required();

            var stringValue = value.ToString();
            int integerSize;

            var pointPosition = stringValue.IndexOfAny(new char[] { '.', ',' });
            if (pointPosition > -1)
            {
                var fractionSize = stringValue.Length - pointPosition - 1;
                integerSize = pointPosition;

                if(Assert(fractionSize <= scale).Failure)
                    return Result.Fail(GetScaleErrorMessage(scale, scaleErrorMsg));
            }
            else
            {
                integerSize = stringValue.Length;
            }

            return Assert(integerSize <= precision, 
                string.Format(scale > 0 
                    ? precisionErrorMsg.OnEmpty("Введите не более {0} знаков до запятой.") 
                    : precisionErrorMsg.OnEmpty("Введите целое число, не более {0} знаков.")
                , precision));
        }

        /// <summary> Формирует сообщение об ошибке о избытке знаков после запятой 
        /// </summary>
        /// <param name="scale">кол-во знаков после запятой</param>
        /// <param name="scaleErrorMsg">сообщение об ошибке</param>
        /// <returns></returns>
        private static string GetScaleErrorMessage(int scale, string scaleErrorMsg)
        {
            return string.Format(scale > 0
                    ? scaleErrorMsg.OnEmpty("Введите не более {0} знаков после запятой.")
                    : scaleErrorMsg.OnEmpty("Введите целое число.")
                , scale);
        }


        /// <summary>
        /// Парсинг булева значения
        /// </summary>
        public static Result<bool> TryGetBool(string value, string msg = "")
        {
            value.Required();

            switch (value.ToLower())
            {
                case "0":
                case "false":
                    return Result.Ok(false);

                case "1":
                case "true":
                    return Result.Ok(true);

                default:
                    return Result.Fail<bool>(msg.OnEmpty("Значение не является булевым типом."));
            }
        }

        public static bool IsTrue(string value)
        {
            value.Required();

            var result = TryGetBool(value);

            return result.Success && result.Value;
        }

        public static bool IsFalse(string value)
        {
            value.Required();

            var result = TryGetBool(value);

            return result.Success && !result.Value;
        }

        /// <summary>
        /// Парсинг Nullable даты
        /// </summary>
        /// <param name="value"></param>
        /// <param name="msg"></param>
        /// <param name="treatNonDateAsNull">Признак нужно ли раценивать не-дату как null</param>
        /// <returns></returns>
        public static Result<DateTime?> TryGetNullableDate(string value, string msg = "", bool treatNonDateAsNull = false)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Ok<DateTime?>(null);

            DateTime result;
            if (!DateTime.TryParse(value, out result))
            {
                if(treatNonDateAsNull)
                    return Result.Ok<DateTime?>(null);

                return Result.Fail<DateTime?>(msg.OnEmpty("Значение не является датой."));
            }

            return Result.Ok((DateTime?)result);
        }

        /// <summary>
        /// Парсинг даты
        /// </summary>
        /// <param name="value"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Result<DateTime> TryGetDate(string value, string msg = "")
        {
            DateTime result;
            if (!DateTime.TryParse(value, out result))
            {
                return Result.Fail<DateTime>(msg.OnEmpty("Значение не является датой."));
            }

            return Result.Ok(result);
        }


        public static Result<Uri> TryGetUri(string value, string msg = "")
        {
            Uri uriResult;
            bool result = Uri.TryCreate(value, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!result)
                return Result.Fail<Uri>(msg.OnEmpty("Значение не является корректным Url адресом."));

            return Result.Ok(uriResult);
        }

        #endregion
    }
}
