using System;
using System.Text;

namespace EntityObject.DataAccess
{
    public class DeleteQuery : SQLQuery
    {

        protected string m_strTable = "";
        protected string m_strWhere = "";

        public DeleteQuery()
        {

        }

        public DeleteQuery(string strTable, string strWhere)
        {
            m_strTable = strTable;
            m_strWhere = strWhere;
        }

        public virtual string Table
        {
            get
            {
                return m_strTable;
            }
            set
            {
                m_strTable = value;
            }
        }


        public virtual string WhereClause
        {
            get
            {
                return m_strWhere;
            }
            set
            {
                m_strWhere = value;
            }
        }

        public override string ConstructQuery()
        {

            StringBuilder strQuery = new StringBuilder();

            strQuery.Append("Delete From ");
            strQuery.Append(m_strTable);

            if ((null != m_strWhere) && (m_strWhere.Length > 0))
            {
                strQuery.Append(" Where ");
                strQuery.Append(m_strWhere);
            }

            if ((null == m_strTable) || (0 == m_strTable.Length))
            {
                StringBuilder strMsg = new StringBuilder();
                strMsg.Append("Invalid SQL Query:");
                strMsg.Append(" ");
                strMsg.Append(strQuery.ToString());

                throw new Exception(strMsg.ToString());
            }

            string Query = strQuery.ToString();
            return (Query);
        }
    }
}
