using System;
using System.Reflection;

namespace EntityObject
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    [Serializable]
    public class DataFieldAttribute : Attribute
    {
        #region Constructor

        public DataFieldAttribute() { }

        public DataFieldAttribute(string fieldName)
            : this(string.Empty, fieldName)
        { }

        public DataFieldAttribute(string tableAlias, string fieldName)
        {
            _fieldName = fieldName;
            _aliasName = tableAlias;
        }
        #endregion

        #region Private Member

        private readonly string _fieldName;
        private readonly string _aliasName;

        #endregion

        public string GetDbField()
        {
            if (string.IsNullOrEmpty(_aliasName))
            {
                return _fieldName;
            }
            return string.Format("{0}.{1}", _aliasName, _fieldName);
        }

        public string FieldName
        {
            get { return _fieldName; }
        }

        public string AliasName
        {
            get { return _aliasName; }
        }

        public string DbFieldName
        {
            get { return GetDbField(); }
        }

        public virtual FieldMap CreateFieldMap(FieldInfo fieldinfo)
        {
            if(fieldinfo == null)
                throw new ArgumentException("DataFieldAttribute.CreateFieldMap requires an non-null fieldInfo.");

            var alias = GetAlias(fieldinfo);

            return new FieldMap(alias, DbFieldName, fieldinfo.FieldType);
        }

        public string GetAlias(FieldInfo fieldinfo)
        {
            string propertyAlias = fieldinfo.Name;
            if(propertyAlias.StartsWith("_"))
            {
                propertyAlias = propertyAlias.Substring(1);
            }
            return propertyAlias;
        }

    }
}
