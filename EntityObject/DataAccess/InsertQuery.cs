using System;
using System.Collections.Generic;
using System.Text;

namespace EntityObject.DataAccess
{
    public class InsertQuery : SQLQuery
    {
        public InsertQuery():this("")
        {
            
        }

        public InsertQuery(string tableName)
        {
            _tableName = tableName;
            _fields = new Dictionary<string, Field>();
        }

        #region Private Memeber

        private readonly string _tableName;
        private readonly Dictionary<string, Field> _fields;

        #endregion

        public override string ConstructQuery()
        {
            var query = new StringBuilder();
            var fields = new StringBuilder();
            var values = new StringBuilder();

            query.Append("Insert Into ");
            query.Append(_tableName);

            foreach (var field in _fields.Values)
            {
                if (fields.Length > 0)
                {
                    fields.Append(",");
                }

                if (values.Length > 0)
                {
                    values.Append(",");
                }

                fields.Append(field.Name);

                string queryValue;

                GetValue(out queryValue, field, "");

                values.Append(queryValue);
            }

            if (fields.Length > 0)
            {
                query.Append(" (");
                query.Append(fields);
                query.Append(") ");
            }

            query.Append(" Values(");
            query.Append(values);
            query.Append(")");

            if (!string.IsNullOrEmpty(_tableName)) return query.ToString();

            var message = new StringBuilder();
            message.Append("Invalid SQL Query:");
            message.Append(" ");
            message.Append(query);

            throw new Exception(message.ToString());
        }

        #region Public Method

        public void AddValue(string strName, object objValue, DataType dataType)
        {
            AddValue(new Field(strName, objValue, dataType));
        }

        public void AddValue(string strName, object objValue, Type type)
        {
            AddValue(strName, objValue, DataTypeUtility.SystemTypeToDataType(type));
        }

        public void AddValue(string strName, object objValue)
        {
            if (objValue == null || objValue == DBNull.Value)
                AddValue(strName, null, DataType.String);
            else
                AddValue(strName, objValue, objValue.GetType());
        }

        public void AddValue(Field fieldInfo)
        {

            _fields[fieldInfo.Name] = fieldInfo;
        }

        #endregion
    }
}
