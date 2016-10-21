using System;

namespace EntityObject
{
    public class TableKeyGeneratorAttribute : Attribute
    {
        public TableKeyGeneratorAttribute() : this(string.Empty)
        {
            
        }

        public TableKeyGeneratorAttribute(string fieldName) : this(fieldName,string.Empty)
        {
            _fieldName = fieldName;
        }

        public TableKeyGeneratorAttribute(string fieldName, string aliasName)
        {
            _fieldName = fieldName;
            _aliasName = aliasName;
        }

        private readonly string _fieldName;
        private readonly string _aliasName;

        public string TableName
        {
            get { return _fieldName; }
        }

        public string FieldName
        {
            get { return _fieldName;}
        }

        public string AliasName
        {
            get { return _aliasName;}
        }
    }
}
