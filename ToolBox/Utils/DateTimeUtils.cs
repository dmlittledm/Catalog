using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBox.Utils
{
    public static class DateTimeUtils
    {
        /// <summary>
        /// Проверяет, находится ли дата в заданном диапазоне
        /// </summary>
        /// <param name="value">проверяемая дата</param>
        /// <param name="start">начало диапазона</param>
        /// <param name="end">конец диапазона</param>
        /// <returns>true, если дата попадает в диапазон</returns>
        public static bool IsInRange(this DateTime value, DateTime start, DateTime end, bool inclusive = true)
        {
            return value.IsInRange(start, (DateTime?)end, inclusive);
        }

        /// <summary>
        /// Проверяет, находится ли дата в заданном диапазоне
        /// </summary>
        /// <param name="value">проверяемая дата</param>
        /// <param name="start">начало диапазона</param>
        /// <param name="end">конец диапазона, <value>null</value> - диапазон не ограничен в будущем</param>
        /// <returns>true, если дата попадает в диапазон</returns>
        public static bool IsInRange(this DateTime value, DateTime start, DateTime? end, bool inclusive = true)
        {
            if (end.HasValue && start > end.Value) // inclusive не влияет на эту проверку
            {
                throw new Exception("Начальная дата диапазона больше конечной даты.");
            }

            if (end.HasValue)
            {
                return start <= value && (inclusive ? (value <= end) : (value < end));
            }
            else
            {
                return start <= value;
            }
        }

        /// <summary>
        /// Установить для даты произвольные часы, минуты и секунды
        /// </summary>
        /// <param name="value">изменяемая дата</param>
        /// <param name="_hour">новое значение часов</param>
        /// <param name="_minute">новое значение минут</param>
        /// <param name="_second">новое значение секунд</param>
        /// <returns></returns>
        public static DateTime SetHoursMinutesAndSeconds(this DateTime value, int _hour, int _minute, int _second)
        {
            return new DateTime(value.Year, value.Month, value.Day, _hour, _minute, _second);
        }

        /// <summary>
        /// Округлить до секунд
        /// </summary>
        /// <param name="value">Исходная дата. Остается неизменной.</param>
        /// <returns></returns>
        public static DateTime RoundToSeconds(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
        }

        /// <summary>
        /// Привести дату к строке вида "гггг-мм-дд чч:мм:сс"
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ConvertToSqlDate(DateTime? date)
        {
            return date == null ? "null" : String.Format("{0}-{1}-{2} {3}:{4}:{5}",
                date.Value.Year,
                (date.Value.Month < 10 ? "0" : "") + date.Value.Month,
                (date.Value.Day < 10 ? "0" : "") + date.Value.Day,
                (date.Value.Hour < 10 ? "0" : "") + date.Value.Hour,
                (date.Value.Minute < 10 ? "0" : "") + date.Value.Minute,
                (date.Value.Second < 10 ? "0" : "") + date.Value.Second);
        }

        /// <summary>
        /// Возвращает представление времени в формате HH:mm:ss
        /// </summary>
        public static string ToFullTimeString(this DateTime value)
        {
            return value.ToString("HH:mm:ss");
        }

        /// <summary>
        /// Возвращает представление даты в формате dd.MM.yyyy HH:mm:ss
        /// </summary>
        public static string ToFullDateTimeString(this DateTime value)
        {
            return value.ToString("dd.MM.yyyy HH:mm:ss");
        }

        /// <summary>
        /// Возвращает представление даты в формате dd.MM.yyyy HH:mm
        /// </summary>
        public static string ToShortDateTimeString(this DateTime value)
        {
            return value.ToString("dd.MM.yyyy HH:mm");
        }

        /// <summary>
        /// Получение текущих даты и времени без милисекунд
        /// </summary>
        public static DateTime GetCurrentDateTime()
        {
            var now = DateTime.Now;

            return new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
        }

        /// <summary>
        /// Прибавить указанное количество рабочих дней к дате.
        /// Если дата - выходной день, делаем так же, как если бы нам передали ближайший следующий рабочий день
        /// </summary>
        public static DateTime AddWorkDays(this DateTime startDate, int daysCount)
        {
            var date = startDate;
            var sign = daysCount < 0 ? -1 : 1;
            var totalDays = daysCount * sign;

            while (totalDays > 0)
            {
                date = date.AddDays(sign);

                if(date.IsWorkDay())
                    totalDays--;
            }

            return date;
        }

        /// <summary>
        /// Проверка, является ли данный день рабочим днем
        /// </summary>
        public static bool IsWorkDay(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return true;
                case DayOfWeek.Tuesday:
                    return true;
                case DayOfWeek.Wednesday:
                    return true;
                case DayOfWeek.Thursday:
                    return true;
                case DayOfWeek.Friday:
                    return true;
                case DayOfWeek.Saturday:
                    return false;
                case DayOfWeek.Sunday:
                    return false;
                default:
                    throw new Exception("Неизвестный день недели.");
            }
        }

        /// <summary>
        /// Получение названия месяца в родительном падеже
        /// </summary>
        /// <param name="monthNumber">Номер месяца (1 - январь)</param>
        /// <returns></returns>
        public static string GetGenitiveMonthName(int monthNumber)
        {
            string result;
            switch (monthNumber)
            {
                case 1:
                    result = "января";
                    break;
                case 2:
                    result = "февраля";
                    break;
                case 3:
                    result = "марта";
                    break;
                case 4:
                    result = "апреля";
                    break;
                case 5:
                    result = "мая";
                    break;
                case 6:
                    result = "июня";
                    break;
                case 7:
                    result = "июля";
                    break;
                case 8:
                    result = "августа";
                    break;
                case 9:
                    result = "сентября";
                    break;
                case 10:
                    result = "октября";
                    break;
                case 11:
                    result = "ноября";
                    break;
                case 12:
                    result = "декабря";
                    break;
                default:
                    throw new Exception(String.Format("Месяца под номером «{0}» не существует.", monthNumber));
            }

            return result;
        }


        /// <summary>
        /// Получение названия месяца
        /// </summary>
        /// <param name="monthNumber">Номер месяца (1 - январь)</param>
        /// <param name="year">Год (если нужно добавить к месяцу год)</param>
        /// <returns></returns>
        public static string GetMonthName(int monthNumber, int? year = null)
        {
            string result;
            string yearString = year.HasValue ? " " + year.Value.ToString() : "";
            switch (monthNumber)
            {
                case 1:
                    result = "январь" + yearString;
                    break;
                case 2:
                    result = "февраль" + yearString;
                    break;
                case 3:
                    result = "март" + yearString;
                    break;
                case 4:
                    result = "апрель" + yearString;
                    break;
                case 5:
                    result = "май" + yearString;
                    break;
                case 6:
                    result = "июнь" + yearString;
                    break;
                case 7:
                    result = "июль" + yearString;
                    break;
                case 8:
                    result = "август" + yearString;
                    break;
                case 9:
                    result = "сентябрь" + yearString;
                    break;
                case 10:
                    result = "октябрь" + yearString;
                    break;
                case 11:
                    result = "ноябрь" + yearString;
                    break;
                case 12:
                    result = "декабрь" + yearString;
                    break;
                default:
                    throw new Exception(String.Format("Месяца под номером «{0}» не существует.", monthNumber));
            }

            return result;
        }

        /// <summary>
        /// Получение названия квартала
        /// </summary>
        /// <param name="quarterNumber">Номер квартала (1 - 4)</param>
        /// <param name="year">Год (если нужно добавить к кварталу год)</param>
        /// <returns></returns>
        public static string GetQuarterName(int quarterNumber, int? year = null)
        {
            string result;
            string yearString = year.HasValue ? " " + year.Value.ToString() : "";
            switch (quarterNumber)
            {
                case 1:
                    result = "I квартал" + yearString;
                    break;
                case 2:
                    result = "II квартал" + yearString;
                    break;
                case 3:
                    result = "III квартал" + yearString;
                    break;
                case 4:
                    result = "IV квартал" + yearString;
                    break;
                default:
                    throw new Exception(String.Format("Квартала под номером «{0}» не существует.", quarterNumber));
            }

            return result;
        }

        /// <summary>
        /// Получение минимальной даты из коллекции
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DateTime? GetMinDate(IEnumerable<DateTime> list)
        {
            DateTime? value = null;
            foreach (var data in list)
            {
                if ((value == null && data != null) || value > data)
                {
                    value = data;
                }
            }

            return value;
        }

        /// <summary>
        /// Получение максимальной даты из коллекции
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DateTime? GetMaxDate(IEnumerable<DateTime> list)
        {
            DateTime? value = null;
            foreach (var date in list)
            {
                if ((value == null && date != null) || value < date)
                {
                    value = date;
                }
            }

            return value;
        }

        /// <summary>
        /// Получение максимальной из двух дат
        /// </summary>
        public static DateTime GetMaxDate(DateTime firstDate, DateTime secondDate)
        {
            return firstDate > secondDate ? firstDate : secondDate;
        }

        /// <summary>
        /// Получить дату начала первой недели квартала
        /// </summary>
        /// <param name="year">Год</param>
        /// <param name="quarter">Квартал</param>
        public static DateTime GetQuarterFirstWeekStartDate(int year, int quarter)
        {
            var quarterStartDate = new DateTime(year, quarter * 3 - 2, 1);

            switch (quarterStartDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return quarterStartDate;
                case DayOfWeek.Tuesday:
                    return quarterStartDate.AddDays(-1);
                case DayOfWeek.Wednesday:
                    return quarterStartDate.AddDays(-2);
                case DayOfWeek.Thursday:
                    return quarterStartDate.AddDays(-3);
                case DayOfWeek.Friday:
                    return quarterStartDate.AddDays(3);
                case DayOfWeek.Saturday:
                    return quarterStartDate.AddDays(2);
                case DayOfWeek.Sunday:
                    return quarterStartDate.AddDays(1);
                default:
                    throw new Exception("Ошибка при вычислении даты начала первой недели квартала.");
            }
        }

        /// <summary>
        /// Получить дату конца последней недели квартала
        /// </summary>
        /// <param name="year">Год</param>
        /// <param name="quarter">Квартал</param>
        public static DateTime GetQuarterLastWeekEndDate(int year, int quarter)
        {
            var quarterLastMonth = quarter * 3;

            var quarterEndDate = new DateTime(year, quarterLastMonth, DateTime.DaysInMonth(year, quarterLastMonth));

            switch (quarterEndDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return quarterEndDate.AddDays(-1);
                case DayOfWeek.Tuesday:
                    return quarterEndDate.AddDays(-2);
                case DayOfWeek.Wednesday:
                    return quarterEndDate.AddDays(-3);
                case DayOfWeek.Thursday:
                    return quarterEndDate.AddDays(3);
                case DayOfWeek.Friday:
                    return quarterEndDate.AddDays(2);
                case DayOfWeek.Saturday:
                    return quarterEndDate.AddDays(1);
                case DayOfWeek.Sunday:
                    return quarterEndDate;
                default:
                    throw new Exception("Ошибка при вычислении даты конца последней недели квартала.");
            }
        }

        /// <summary>
        /// Подсчет номера недели в году согласно ISO
        /// </summary>
        /// <param name="date">Дата номер недели которой надо вычислить</param>
        /// <returns></returns>
        public static int WeekNumber(this DateTime date)
        {
            //Находим четверг этой недели
            DateTime thursday = new DateTime();

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    thursday = date.AddDays(3);
                    break;
                case DayOfWeek.Tuesday:
                    thursday = date.AddDays(2);
                    break;
                case DayOfWeek.Wednesday:
                    thursday = date.AddDays(1);
                    break;
                case DayOfWeek.Thursday:
                    thursday = date;
                    break;
                case DayOfWeek.Friday:
                    thursday = date.AddDays(-1);
                    break;
                case DayOfWeek.Saturday:
                    thursday = date.AddDays(-2);
                    break;
                case DayOfWeek.Sunday:
                    thursday = date.AddDays(-3);
                    break;
                default:
                    throw new Exception("Ошибка при вычислении номера недели.");
            }

            //По четвергу определяем номер недели
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(thursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// Получить дату начала недели
        /// </summary>
        /// <param name="year">Год</param>
        /// <param name="week">Номер недели</param>
        public static DateTime GetWeekFirstDate(int year, int week)
        {
            var addingDays = (week - 1) * 7;
            var firstDayOfTheYear = new DateTime(year, 1, 1);

            switch (firstDayOfTheYear.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return firstDayOfTheYear.AddDays(addingDays);
                case DayOfWeek.Tuesday:
                    return firstDayOfTheYear.AddDays(addingDays - 1);
                case DayOfWeek.Wednesday:
                    return firstDayOfTheYear.AddDays(addingDays - 2);
                case DayOfWeek.Thursday:
                    return firstDayOfTheYear.AddDays(addingDays - 3);
                case DayOfWeek.Friday:
                    return firstDayOfTheYear.AddDays(addingDays + 3);
                case DayOfWeek.Saturday:
                    return firstDayOfTheYear.AddDays(addingDays + 2);
                case DayOfWeek.Sunday:
                    return firstDayOfTheYear.AddDays(addingDays + 1);
                default:
                    throw new Exception("Ошибка при вычислении даты начала первой недели квартала.");
            }
        }

        /// <summary>
        /// Проверка: является ли дата определенным числом определеного месяца
        /// </summary>
        public static bool DateIs(this DateTime date, int day, int month)
        {
            return date.Day == day && date.Month == month;
        }
    }
}
