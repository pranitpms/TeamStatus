using System;
using EntityObject.Utility;

namespace EntityObject
{
    public class FieldMap
    {
        public FieldMap(string aliasName, string fieldName, Type classType)
        {
            _aliasName = aliasName;
            _fieldName = fieldName;
            _classtype = classType;
            _isFieldTypeNullable = NullValue.IsNullableType(_classtype);
        }

        #region Private Memeber

        private readonly string _aliasName;
        private readonly string _fieldName;
        private readonly Type _classtype;
        private readonly bool _isFieldTypeNullable;

        #endregion

        #region Public Member

        public string AliasName
        {
            get { return _aliasName; }
        }

        public string FieldName
        {
            get { return _fieldName; }
        }

        public Type ClassType
        {
            get { return _classtype; }
        }

        public bool IsFieldTypeNullable
        {
            get { return _isFieldTypeNullable; }
        }

        public bool IsPrimaryKey { get; set; }

        #endregion

        public object ConvertToClassType(object dbValue)
        {
            if (ClassType == typeof (string) && (dbValue == null || dbValue is DBNull))
                return null;

            return ConvertValue.ToType(ClassType, dbValue);
        }
    }
}
