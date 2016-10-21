using System;
using System.Xml;

namespace EntityObject.DataAccess
{
    public enum DataType
    {
        String = 0,			//more than one char
        Integer = 1,		//whole number
        Decimal,			//float or real
        DateTime,			//datetime or date
        //--dd-- I am not sure why this is necessary
        Char,				//a char
        Binary,
        RecordSet,//Image
        Xml,
        Cursor,//Cursor
        Unknown
    }

    /// <summary>
    /// A Database Column Type
    /// </summary>
    public class DataTypeUtility
    {
        public static DataType SystemTypeToDataType(Type type)
        {
            if (type == typeof(Byte) ||
                type == typeof(Int16) ||
                type == typeof(Int32) ||
                type == typeof(Int64) ||
                type == typeof(SByte) ||
                type == typeof(UInt16) ||
                type == typeof(UInt32) ||
                type == typeof(UInt64) ||
                type == typeof(Boolean))
            {
                return DataType.Integer;
            }

            if (type == typeof(Char))
            {
                return DataType.Char;
            }

            if (type == typeof(DateTime))
            {
                return DataType.DateTime;
            }

            if (type == typeof(Double) || type == typeof(Decimal))
            {
                return DataType.Decimal;
            }

            if (type == typeof(String) || type == typeof(Byte[]))
            {
                return DataType.String;
            }

            //if (type == typeof(System.Xml.XmlDocument) || type.IsArray
            //    || (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Orion.Utility.DBList<>))))
            //{
            //    return DataType.Xml;
            //}

            if (type == typeof(XmlDocument) || type.IsArray)
            {
                return DataType.Xml;
            }

            if (type == typeof(Single))
            {
                return (DataType.Decimal);
            }

            //TO DO: --dd-- it's wrong to return a binary by default. 
            //Instead we should throw an exception and insert the type among the previos branching ifs
            //return (DataType.Binary);
            throw new Exception("Unknown data type: '" + type.FullName + "'.");
        }
    }
}
