using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ToolBox.Utils
{
    public static class ContractUtils
    {
        [ContractArgumentValidator]
        public static void Required(this object val, string msg = "")
        {
            if (val == null)
                throw new ArgumentException(msg.OnEmpty("Значение переменной не может быть пустым."));

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void MustBeNotEmpty<T>(this IEnumerable<T> val, string msg = "")
        {
            if (val == null)
                throw new ArgumentException(msg.OnEmpty("Значение переменной не может быть пустым."));
            if (!val.Any())
                throw new ArgumentException(msg.OnEmpty("Значение переменной не может быть пустым."));

            Contract.EndContractBlock();
        }

        #region String

        [ContractArgumentValidator]
        public static void Required(this string val, string msg = "")
        {
            if (string.IsNullOrWhiteSpace(val))
                throw new ArgumentException(msg.OnEmpty("Значение переменной не может быть пустым."));

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void MaxLength(this string val, int maxLength, string msg = "")
        {
            if (val != null && val.Length > maxLength)
                throw new ArgumentException(string.Format(msg.OnEmpty("Значение переменной не может превышать {0} символов."), maxLength));

            Contract.EndContractBlock();
        }

        #endregion

        #region Bool

        [ContractArgumentValidator]
        public static void MustBeTrue(this bool val, string msg = "")
        {
            if (!val)
                throw new ArgumentException(msg.OnEmpty("Значение переменной - Ложь."));

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void MustBeFalse(this bool val, string msg = "")
        {
            if (val)
                throw new ArgumentException(msg.OnEmpty("Значение переменной - Истина."));

            Contract.EndContractBlock();
        } 

        #endregion

        #region Int

        [ContractArgumentValidator]
        public static void MustBeGreaterThan(this int val, int thanValue, string msg = "")
        {
            if (val <= thanValue)
                throw new ArgumentException(string.Format(msg.OnEmpty("Значение переменной меньше или равно {0}."), thanValue));

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void MustBeLessThan(this int val, int thanValue, string msg = "")
        {
            if (val >= thanValue)
                throw new ArgumentException(string.Format(msg.OnEmpty("Значение переменной больше или равно {0}."), thanValue));

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void MustBeGreaterThanOrEqual(this int val, int thanValue, string msg = "")
        {
            if (val < thanValue)
                throw new ArgumentException(string.Format(msg.OnEmpty("Значение переменной меньше {0}."), thanValue));

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void MustBeLessThanOrEqual(this int val, int thanValue, string msg = "")
        {
            if (val > thanValue)
                throw new ArgumentException(string.Format(msg.OnEmpty("Значение переменной больше {0}."), thanValue));

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void MustEqualsTo(this int val, int toValue, string msg = "")
        {
            if (val != toValue)
                throw new ArgumentException(string.Format(msg.OnEmpty("Значение переменной не равно {0}."), toValue));

            Contract.EndContractBlock();
        } 

        #endregion

        #region Decimal

        [ContractArgumentValidator]
        public static void MustBeGreaterThan(this decimal val, decimal thanValue, string msg = "")
        {
            if (val <= thanValue)
                throw new ArgumentException(string.Format(msg.OnEmpty("Значение переменной меньше или равно {0}."), thanValue));

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void MustBeLessThan(this decimal val, decimal thanValue, string msg = "")
        {
            if (val >= thanValue)
                throw new ArgumentException(string.Format(msg.OnEmpty("Значение переменной больше или равно {0}."), thanValue));

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void MustBeGreaterThanOrEqual(this decimal val, decimal thanValue, string msg = "")
        {
            if (val < thanValue)
                throw new ArgumentException(string.Format(msg.OnEmpty("Значение переменной меньше {0}."), thanValue));

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void MustBeLessThanOrEqual(this decimal val, decimal thanValue, string msg = "")
        {
            if (val > thanValue)
                throw new ArgumentException(string.Format(msg.OnEmpty("Значение переменной больше {0}."), thanValue));

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void MustEqualsTo(this decimal val, decimal toValue, string msg = "")
        {
            if (val != toValue)
                throw new ArgumentException(string.Format(msg.OnEmpty("Значение переменной не равно {0}."), toValue));

            Contract.EndContractBlock();
        }

        #endregion

        #region DateTime

        [ContractArgumentValidator]
        public static void MustBeGreaterThan(this DateTime val, DateTime thanValue, string msg = "")
        {
            if (val <= thanValue)
                throw new ArgumentException(string.Format(msg.OnEmpty("Значение переменной меньше или равно {0}."), thanValue));

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void MustBeLessThan(this DateTime val, DateTime thanValue, string msg = "")
        {
            if (val >= thanValue)
                throw new ArgumentException(string.Format(msg.OnEmpty("Значение переменной больше или равно {0}."), thanValue));

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void MustBeGreaterThanOrEqual(this DateTime val, DateTime thanValue, string msg = "")
        {
            if (val < thanValue)
                throw new ArgumentException(string.Format(msg.OnEmpty("Значение переменной меньше {0}."), thanValue));

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void MustBeLessThanOrEqual(this DateTime val, DateTime thanValue, string msg = "")
        {
            if (val > thanValue)
                throw new ArgumentException(string.Format(msg.OnEmpty("Значение переменной больше {0}."), thanValue));

            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void MustEqualsTo(this DateTime val, DateTime toValue, string msg = "")
        {
            if (val != toValue)
                throw new ArgumentException(string.Format(msg.OnEmpty("Значение переменной не равно {0}."), toValue));

            Contract.EndContractBlock();
        }

        #endregion

        [Pure]
        public static void AssumeInvariant<T>(T o) { }

    }
}
