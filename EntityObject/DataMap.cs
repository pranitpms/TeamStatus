using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EntityObject
{
    public class DataMap
    {
        public DataMap(object instance)
        {
            _instance = instance;
            _fieldCollection = new List<FieldMap>();
            _primaryKey = new Dictionary<int, string>();
            TableKeyGeneratorList = new List<FieldMap>();
            CreateDataMap();
        }

        private readonly object _instance;
        private readonly Dictionary<int,string> _primaryKey;
        private readonly List<FieldMap> _fieldCollection;
        private string _tableName;
        private string _databaseName;

        private const BindingFlags FieldBindingFlag = BindingFlags.NonPublic | BindingFlags.Instance;

        public string DatabaseName
        {
            get { return _databaseName; }
        }

        public string TableName
        {
            get { return _tableName; }
        }
        public Dictionary<int, string> PrimaryKeyFields
        {
            get { return _primaryKey ; }
        }
        public List<FieldMap> FieldCollection
        {
            get { return _fieldCollection; }
        }

        public bool IsHasModified { get; set; }

        public List<FieldMap> TableKeyGeneratorList { get; set; } 

        private void CreateDataMap()
        {
             var type = _instance.GetType();

            CheckPrimaryKey(type);
            CreateTableMap(type);
            CheckTableKeyGeneratorField(type);
            var fields = type.GetFields(FieldBindingFlag);

            foreach (var field in fields)
            {
                var attributes = field.GetCustomAttributes(false);
                var fieldMap = (DataFieldAttribute)attributes.FirstOrDefault(att => att.GetType() == typeof(DataFieldAttribute));
                if (fieldMap != null)
                {
                    _fieldCollection.Add(fieldMap.CreateFieldMap(field)); 
                }
                
            }
        }

        private void CreateTableMap(Type type)
        {
            var attribute = type.GetCustomAttributes(false);
            var tableMap = (TableAttribute) attribute.FirstOrDefault(att => att.GetType() == typeof(TableAttribute));

            if (tableMap == null) return;
            _tableName = tableMap.TableName;
            _databaseName = tableMap.DataBaseName;
            IsHasModified = tableMap.IsHasModified;
        }

        private void CheckPrimaryKey(IReflect type)
        {
            var fields = type.GetFields(FieldBindingFlag);

            foreach (var field in fields)
            {
                var fieldAtt = (PrimaryKeyFieldAttribute)field.GetCustomAttribute(typeof(PrimaryKeyFieldAttribute));
                if (fieldAtt != null)
                {
                    _primaryKey.Add(fieldAtt.PrimaryKeyIndex, fieldAtt.FieldName) ;

                    var fieldMap = fieldAtt.CreateFieldMap(field);
                    fieldMap.IsPrimaryKey = true;
                    _fieldCollection.Add(fieldMap);
                }
            }
        }

        private void CheckTableKeyGeneratorField(IReflect type)
        {
            var fields = type.GetFields(FieldBindingFlag);

            foreach (var field in fields)
            {
                var tableKeyGenerator = (TableKeyGeneratorAttribute)field.GetCustomAttribute(typeof(TableKeyGeneratorAttribute));
                if (tableKeyGenerator == null) continue;

                var dataField = new DataFieldAttribute(tableKeyGenerator.AliasName,tableKeyGenerator.FieldName);
                var fieldMap = dataField.CreateFieldMap(field);
                TableKeyGeneratorList.Add(fieldMap);
            }
        }

        public FieldMap GetPrimaryKeyField(int index)
        {
            var fieldName =  _primaryKey[index];

            var field =  FieldCollection.FirstOrDefault(collection => string.Equals(collection.FieldName, fieldName));

            return field;
        }

        public string GetPrimaryKeyFieldName(int index)
        {
            return _primaryKey[index];
        }
    }
}
