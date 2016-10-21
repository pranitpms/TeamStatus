using System;

namespace EntityObject
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TableAttribute : Attribute
    {
        public TableAttribute() { }
        public TableAttribute(string tableName,string alias, string databaseName):this(tableName,alias,databaseName,false)
        {
           
        }

        public TableAttribute(string tableName, string alias, string databaseName,bool isHasModified)
        {
            Alias = alias;
            DataBaseName = databaseName;
            _tableName = tableName;
            IsHasModified = isHasModified;
        }

        private readonly string _tableName;

        public bool IsHasModified;
        public string Alias;
        public string DataBaseName;
        public string TableName
        {
            get
            {
                if (string.IsNullOrEmpty(Alias))
                    return _tableName;
                return string.Format("{0} {1}", _tableName, Alias);
            }
        }

    }
}
