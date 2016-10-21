using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace EntityObject.Utility
{
    public class ConvertValue
    {
        #region ToType

        public static T ToType<T>(object source)
        {
            return (T) ToType(typeof (T), source, true);
        }

        public static T TryToType<T>(object source)
        {
            return (T) ToType(typeof (T), source, false);
        }

        public static object ToType(Type type, object source)
        {
            return ToType(type, source, false);
        }

        public static object ToType(Type type, object source, bool throwOnError)
        {
            if (source != null && type.IsInstanceOfType(source))
                return source;

            Type underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)
            {
                if (source == null || source is DBNull || string.IsNullOrWhiteSpace(source.ToString()))
                    return null;
                type = underlyingType;
            }

            if (type == typeof (long))
                return ToLongValue(source, throwOnError);
            if (type == typeof (int))
                return ToIntegerValue(source, throwOnError);
            if (type == typeof (decimal))
                return ToDecimalValue(source, throwOnError);
            if (type == typeof (string))
                return ToStringValue(source);
            if (type == typeof (DateTime))
                return ToDateTimeValue(source, throwOnError);
            if (type == typeof (double))
                return ToDoubleValue(source, throwOnError);
            if (type == typeof (char))
                return ToCharValue(source);
            if (type == typeof (Byte[]))
                return ToByteArrayValue(source, throwOnError);
            if (type == typeof (bool))
                return ToBooleanValue(source);
            if (type.BaseType == typeof (Enum))
                return Enum.Parse(type, ToStringValue(source), true);
            if (type == typeof (XmlDocument))
                return ToXmlValue(source, throwOnError);
            if (type.IsGenericType && type.GetInterface(typeof (IList<>).Name) != null)
            {
                MethodInfo methodInfo = typeof (ConvertValue).GetMethod("ToDBListValue", new[] {typeof (object)});
                return methodInfo.MakeGenericMethod(type.GetGenericArguments()[0]).Invoke(null, new[] {source});
            }
            if (throwOnError)
            {
                throw new ArgumentException(string.Format("Don't know how to convert '{0}' to type '{1}'.",
                    ToStringValue(source), type));
            }
            return source;
        }

        #endregion // ToType

        #region double

        public static double? ToNullableDouble(object objValue)
        {
            return ToNullableDouble(objValue, false);
        }

        public static double? ToNullableDouble(object objValue, bool throwOnError)
        {
            if (objValue == null || objValue is DBNull || string.IsNullOrWhiteSpace(objValue.ToString()))
                return null;

            return ToDoubleValue(objValue, throwOnError);
        }

        public static double ToDoubleValue(object objValue)
        {
            return ToDoubleValue(objValue, false);
        }

        public static double ToDoubleValue(object objValue, bool throwOnError)
        {
            double dbValue = 0;

            if (null == objValue || objValue is DBNull)
                return dbValue;

            if (objValue is Byte)
            {
                dbValue = Convert.ToDouble((Byte) objValue);
            }
            else if (objValue is Int16)
            {
                dbValue = Convert.ToDouble((Int16) objValue);
            }
            else if (objValue is Int32)
            {
                dbValue = Convert.ToDouble((Int32) objValue);
            }
            else if (objValue is Int64)
            {
                dbValue = Convert.ToDouble((Int64) objValue);
            }
            else if (objValue is SByte)
            {
                dbValue = Convert.ToDouble((SByte) objValue);
            }
            else if (objValue is UInt16)
            {
                dbValue = Convert.ToDouble((UInt16) objValue);
            }
            else if (objValue is UInt32)
            {
                dbValue = Convert.ToDouble((UInt32) objValue);
            }
            else if (objValue is UInt64)
            {
                dbValue = Convert.ToDouble((UInt64) objValue);
            }
            else if (objValue is Decimal)
            {
                dbValue = Convert.ToDouble((Decimal) objValue);
            }
            else if (objValue is Double)
            {
                dbValue = Convert.ToDouble((Double) objValue);
            }
            else
            {
                var s = objValue as string;
                if (s != null)
                {
                    if (!string.IsNullOrWhiteSpace(s)) double.TryParse(s, out dbValue);
                }
                else
                {
                    if (throwOnError)
                    {
                        throw new ArgumentException(
                            string.Format("Don't know how to convert '{0}' to type 'System.Double'.",
                                ToStringValue(objValue)));
                    }
                }
            }

            return dbValue;
        }

        #endregion // double

        #region decimal

        public static decimal? ToNullableDecimal(object objValue)
        {
            return ToNullableDecimal(objValue, false);
        }

        public static decimal? ToNullableDecimal(object objValue, bool throwOnError)
        {
            if (objValue == null || objValue is DBNull || string.IsNullOrWhiteSpace(objValue.ToString()))
                return null;

            return ToDecimalValue(objValue, throwOnError);
        }

        public static decimal ToDecimalValue(object objValue)
        {
            return ToDecimalValue(objValue, false);
        }

        public static decimal ToDecimalValue(object objValue, bool throwOnError)
        {
            decimal decValue = 0;

            if (null == objValue || objValue is DBNull)
                return decValue;

            if (objValue is Byte)
            {
                decValue = Convert.ToDecimal((Byte) objValue);
            }
            else if (objValue is Int16)
            {
                decValue = Convert.ToDecimal((Int16) objValue);
            }
            else if (objValue is Int32)
            {
                decValue = Convert.ToDecimal((Int32) objValue);
            }
            else if (objValue is Int64)
            {
                decValue = Convert.ToDecimal((Int64) objValue);
            }
            else if (objValue is SByte)
            {
                decValue = Convert.ToDecimal((SByte) objValue);
            }
            else if (objValue is UInt16)
            {
                decValue = Convert.ToDecimal((UInt16) objValue);
            }
            else if (objValue is UInt32)
            {
                decValue = Convert.ToDecimal((UInt32) objValue);
            }
            else if (objValue is UInt64)
            {
                decValue = Convert.ToDecimal((UInt64) objValue);
            }
            else if (objValue is Decimal)
            {
                decValue = Convert.ToDecimal((Decimal) objValue);
            }
            else if (objValue is Single)
            {
                decValue = Convert.ToDecimal((Single) objValue);
            }
            else if (objValue is Double)
            {
                decValue = Convert.ToDecimal(objValue);
            }
            else if (objValue is String)
            {
                if (!string.IsNullOrWhiteSpace((string) objValue)) decimal.TryParse((string) objValue, out decValue);
            }
            else
            {
                if (throwOnError)
                {
                    throw new ArgumentException(
                        string.Format("Don't know how to convert '{0}' to type 'System.Decimal'.",
                            ToStringValue(objValue)));
                }
            }

            return decValue;
        }

        #endregion // decimal

        #region long

        public static long? ToNullableLong(object objValue)
        {
            return ToNullableLong(objValue, false);
        }

        public static long? ToNullableLong(object objValue, bool throwOnError)
        {
            if (objValue == null || objValue is DBNull || string.IsNullOrWhiteSpace(objValue.ToString()))
                return null;

            return ToLongValue(objValue, throwOnError);
        }

        public static long ToLongValue(object objValue)
        {
            return ToLongValue(objValue, false);
        }

        public static long ToLongValue(object objValue, bool throwOnError)
        {
            long lValue = 0;

            if (null == objValue || objValue is DBNull)
                return lValue;

            if (objValue is Byte)
            {
                lValue = Convert.ToInt64((Byte) objValue);
            }
            else if (objValue is Int16)
            {
                lValue = Convert.ToInt64((Int16) objValue);
            }
            else if (objValue is Int32)
            {
                lValue = Convert.ToInt64((Int32) objValue);
            }
            else if (objValue is Int64)
            {
                lValue = Convert.ToInt64((Int64) objValue);
            }
            else if (objValue is SByte)
            {
                lValue = Convert.ToInt64((SByte) objValue);
            }
            else if (objValue is UInt16)
            {
                lValue = Convert.ToInt64((UInt16) objValue);
            }
            else if (objValue is UInt32)
            {
                lValue = Convert.ToInt64((UInt32) objValue);
            }
            else if (objValue is UInt64)
            {
                lValue = Convert.ToInt64((UInt64) objValue);
            }
            else if (objValue is Decimal)
            {
                lValue = Convert.ToInt64((Decimal) objValue);
            }
            else if (objValue is Double)
            {
                lValue = Convert.ToInt64((Double) objValue);
            }
            else if (objValue is String)
            {
                if (!string.IsNullOrWhiteSpace((string) objValue)) long.TryParse((string) objValue, out lValue);
            }
            else
            {
                if (throwOnError)
                {
                    throw new ArgumentException(string.Format("Don't know how to convert '{0}' to type 'System.Long'.",
                        ToStringValue(objValue)));
                }
            }

            return lValue;
        }

        #endregion // long

        #region int

        public static int? ToNullableInteger(object objValue)
        {
            return ToNullableInteger(objValue, false);
        }

        public static int? ToNullableInteger(object objValue, bool throwOnError)
        {
            if (objValue == null || objValue is DBNull || string.IsNullOrWhiteSpace(objValue.ToString()))
                return null;

            return ToIntegerValue(objValue, throwOnError);
        }

        public static int ToIntegerValue(object objValue)
        {
            return ToIntegerValue(objValue, false);
        }

        public static int ToIntegerValue(object objValue, bool throwOnError)
        {
            int nValue = 0;

            if (null == objValue || objValue is DBNull)
                return nValue;

            if (objValue is Byte)
            {
                nValue = Convert.ToInt32((Byte) objValue);
            }
            else if (objValue is Int16)
            {
                nValue = Convert.ToInt32((Int16) objValue);
            }
            else if (objValue is Int32)
            {
                nValue = Convert.ToInt32((Int32) objValue);
            }
            else if (objValue is Int64)
            {
                nValue = Convert.ToInt32((Int64) objValue);
            }
            else if (objValue is SByte)
            {
                nValue = Convert.ToInt32((SByte) objValue);
            }
            else if (objValue is UInt16)
            {
                nValue = Convert.ToInt32((UInt16) objValue);
            }
            else if (objValue is UInt32)
            {
                nValue = Convert.ToInt32((UInt32) objValue);
            }
            else if (objValue is UInt64)
            {
                nValue = Convert.ToInt32((UInt64) objValue);
            }
            else if (objValue is Decimal)
            {
                nValue = Convert.ToInt32((Decimal) objValue);
            }
            else if (objValue is Double)
            {
                nValue = Convert.ToInt32((Double) objValue);
            }
            else if (objValue is String)
            {
                if (!string.IsNullOrWhiteSpace((string) objValue)) int.TryParse((string) objValue, out nValue);
            }
            else
            {
                if (throwOnError)
                {
                    throw new ArgumentException(
                        string.Format("Don't know how to convert '{0}' to type 'System.Integer'.",
                            ToStringValue(objValue)));
                }
            }

            return nValue;
        }

        #endregion // int

        #region bool

        public static bool? ToNullableBoolean(object value)
        {
            if (value == null || value is DBNull || string.IsNullOrWhiteSpace(value.ToString()))
                return null;

            return ToBooleanValue(value);
        }

        public static bool ToBooleanValue(object objValue)
        {
            if (null == objValue || objValue is DBNull)
                return false;

            if (objValue is bool)
                return (bool) objValue;

            string strValue = objValue as string;

            if (strValue != null)
                return strValue != string.Empty &&
                       (Char.ToUpperInvariant(strValue[0]) == 'Y' || Char.ToUpperInvariant(strValue[0]) == 'T');

            return (ToLongValue(objValue) != 0);
        }

        #endregion // bool

        #region string

        public static string ToNullableString(object value)
        {
            if (value == null || value is DBNull)
                return null;

            return value.ToString();
        }

        public static string ToStringValue(object objValue)
        {
            if ((null == objValue) || (objValue is DBNull))
                return (string.Empty);

            string strValue;

            if (objValue is String)
            {
                strValue = (string) objValue;
                strValue = strValue.TrimEnd(null);
            }
            else
            {
                strValue = objValue.ToString();
            }

            return strValue;
        }

        #endregion // string

        #region char

        public static char? ToNullableChar(object value)
        {
            if (value == null || value is DBNull || string.IsNullOrWhiteSpace(value.ToString()))
                return null;

            return ToCharValue(value);
        }

        public static char ToCharValue(object objValue)
        {
            string strValue = ToStringValue(objValue);

            if (strValue.Length > 0)
                return (strValue[0]);

            return ('0');
        }

        #endregion // char

        #region Byte[]

        public static Byte[] ToByteArrayValue(object objValue)
        {
            return ToByteArrayValue(objValue, false);
        }

        public static Byte[] ToByteArrayValue(object objValue, bool throwOnError)
        {
            if (objValue == null || objValue is DBNull)
                return null;

            if (!(objValue is Byte[]))
            {
                if (throwOnError)
                {
                    throw new ArgumentException(string.Format(
                        "Don't know how to convert '{0}' to type 'System.Byte[]'.", ToStringValue(objValue)));
                }
            }

            return (Byte[]) objValue;
        }

        #endregion // Byte[]

        #region DateTime

        public static DateTime? ToNullableDateTime(object value)
        {
            if (value == null || value is DBNull || string.IsNullOrWhiteSpace(value.ToString()))
                return null;

            return ToDateTimeValue(value);
        }

        public static DateTime ToDateTimeValue(object objValue)
        {
            return ToDateTimeValue(objValue, false);
        }

        public static DateTime ToDateTimeValue(object objValue, bool throwOnError)
        {
            return ToDateTimeValue(objValue, throwOnError, null);
        }

        public static DateTime ToDateTimeValue(object objValue, bool throwOnError, string timeZoneSetting)
        {
            var datValue = NullValue.DateTimeValue();

            if (null == objValue || objValue is DBNull)
                return datValue;

            if (objValue is DateTime)
            {
                datValue = (DateTime) objValue;
            }
            else if (objValue is String)
            {
                try
                {
                    //	A zero-length string will cause an 
                    //	unnecessary exception, so just use default
                    if (((String) objValue).Length > 0)
                        datValue = Convert.ToDateTime(objValue);
                }
                catch
                {
                    if (throwOnError)
                    {
                        throw new ArgumentException(
                            string.Format("Don't know how to convert '{0}' to type 'System.DateTime'.",
                                ToStringValue(objValue)));
                    }
                    return datValue;
                }
            }

            if (!String.IsNullOrEmpty(timeZoneSetting))
            {
                //Nearly all dates in enterprise have been trimmed when 
                //updated so a time of 12:00:00 am was stored by default 
                //and we do not want to convert these times
                if (!(datValue.Hour == 0 && datValue.Minute == 0 &&
                      datValue.Second == 0 && datValue.Millisecond == 0))
                {
                    //Convert date to from DB stored UTC/GMT time to users TimeZone
                    //SessionInfo info = SessionInfo.GetSessionInfo(this.AppContext);                             
                    //Convert date to time zone specified in the SETTINGS table
                    if ((datValue).Kind.Equals(DateTimeKind.Local))
                    {
                        datValue = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(datValue, TimeZoneInfo.Local.Id,
                            timeZoneSetting);
                    }
                }
            }

            return datValue;
        }

        #endregion // DateTime

        #region Xml / XmlDocument

        public static XmlDocument ToXmlValue(object objValue)
        {
            return ToXmlValue(objValue, false);
        }

        public static XmlDocument ToXmlValue(object objValue, bool throwOnError)
        {
            if (objValue == null || objValue is DBNull || string.IsNullOrWhiteSpace(objValue.ToString()))
                return null;

            if (!(objValue is XmlDocument))
            {
                XmlDocument xml = new XmlDocument();
                try
                {
                    xml.LoadXml(objValue.ToString());
                }
                catch
                {
                    if (throwOnError)
                    {
                        throw new ArgumentException(
                            string.Format("Don't know how to convert '{0}' to type 'System.Xml.XmlDocument'.",
                                ToStringValue(objValue)));
                    }
                }

                return xml;
            }

            return (XmlDocument) objValue;
        }

        #endregion // Xml / XmlDocument



        #region Utility Functions

        // a very simple utility function that converts any kind of values to string to see if they are different
        public static bool ValuesToStringAreDifferent(object objValue1, object objValue2)
        {
            string strValue1 = ((objValue1 == null) || (objValue1 == DBNull.Value))
                ? string.Empty
                : objValue1.ToString();
            string strValue2 = ((objValue2 == null) || (objValue1 == DBNull.Value))
                ? string.Empty
                : objValue2.ToString();

            return (strValue1 != strValue2);
        }

        public static decimal GetNullEqualsZeroDecimal(object objVal)
        {
            return ToDecimalValue(objVal);
        }

        public static long GetNullEqualsZeroLong(object objVal)
        {
            return ToLongValue(objVal);
        }

        #endregion // Utility Functions
    }
}
