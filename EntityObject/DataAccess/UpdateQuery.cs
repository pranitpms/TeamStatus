using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace EntityObject.DataAccess
{
    public class UpdateQuery : SQLQuery
    {
        protected string StrTable;
        protected string StrWhere;
        protected SortedList ArSetFieldList;
        protected SortedList ArWhereFieldList;

        public UpdateQuery()
            : this("")
        {
        }

        public UpdateQuery(string strTable)
        {
            ArSetFieldList = new SortedList(new Comparer(CultureInfo.InvariantCulture));
            ArWhereFieldList = new SortedList(new Comparer(CultureInfo.InvariantCulture));
            StrTable = strTable;
        }

        public virtual string Table
        {
            get
            {
                return StrTable;
            }
            set
            {
                StrTable = value;
            }
        }

        public virtual string WhereClause
        {
            get
            {
                return StrWhere;
            }
            set
            {
                StrWhere = value;
            }
        }

        public void AddWhereValue(string strName, object objValue)
        {
            if (objValue == null)
                AddWhereValue(strName, null, DataType.String);
            else
                AddWhereValue(strName, objValue, DataTypeUtility.SystemTypeToDataType(objValue.GetType()));
        }

        public void AddWhereValue(string strName, object objValue, DataType type)
        {

            var fieldinfo = new Field(strName, objValue, type);

            AddWhereValue(fieldinfo);
        }

        public void AddWhereValue(Field fieldInfo)
        {
            ArWhereFieldList[fieldInfo.Name] = fieldInfo;
        }

        public void AddSetValue(string strName, object objValue, DataType type)
        {
            AddSetValue(new Field(strName, objValue, type));
        }

        public void AddSetValue(string strName, object objValue)
        {
            if (objValue == null)
                AddSetValue(strName, null, DataType.String);
            else
                AddSetValue(strName, objValue, DataTypeUtility.SystemTypeToDataType(objValue.GetType()));

        }

        public void AddSetValue(Field fieldInfo)
        {
            ArSetFieldList[fieldInfo.Name] = fieldInfo;
        }

        public SortedList GetUpdateFields()
        {
            return ArSetFieldList;
        }

        public override string ConstructQuery()
        {
            var strQuery = new StringBuilder(200);
            var strSetClause = new StringBuilder(200);
            var strWhereClause = new StringBuilder(100);

            strQuery.Append("Update ");
            strQuery.Append(StrTable);

            foreach (DictionaryEntry objItem in ArSetFieldList)
            {
                if (strSetClause.Length > 0)
                    strSetClause.Append(",\r\n");
                else
                    strSetClause.Append("\r\n");

                Field field = (Field)objItem.Value;

                strSetClause.Append(field.Name);

                string strValue;
                GetValue(out strValue, field, "Set");

                strSetClause.Append("=");
                strSetClause.Append(strValue);
            }

            var bSetCaluseEmpty = (strSetClause.Length <= 0);

            strQuery.Append(" Set ");
            strQuery.Append(strSetClause);

            strWhereClause.Append(StrWhere);

            if (strWhereClause.Length <= 0)
            {
                foreach (DictionaryEntry objItem in ArWhereFieldList)
                {
                    if (strWhereClause.Length > 0)
                    {
                        strWhereClause.Append(" AND ");
                    }

                    Field field = (Field)objItem.Value;

                    strWhereClause.Append(field.Name);

                    string strValue;
                    if (GetValue(out strValue, field, "Where"))
                    {
                        strWhereClause.Append(" is ");
                    }
                    else
                        strWhereClause.Append(" = ");

                    strWhereClause.Append(strValue);
                }
            }

            if (strWhereClause.Length > 0)
            {
                strQuery.Append(" Where ");
                strQuery.Append(strWhereClause);
            }

            if (string.IsNullOrEmpty(StrTable) || (bSetCaluseEmpty))
            {
                var strMsg = new StringBuilder();
                strMsg.Append("Invalid SQL Query:");
                strMsg.Append(" ");
                strMsg.Append(strQuery);

                throw new Exception(strMsg.ToString());
            }

            var query = strQuery.ToString();
            return (query);
        }
    }
}
