using System;
using System.Text;

namespace EntityObject.DataAccess
{
    public class SelectQuery : SQLQuery
    {
        public SelectQuery()
        {

        }

        #region PrivateMemeber

        private string _selectClause;
        private string _from;
        private string _whereClause;
        private string _orderBy;
        private string _groupBy;
        private bool _isSelectDistinct;

        #endregion

        public virtual string FromClause
        {
            get { return _from; }

            set
            {
                ValidateValue(value, "FormClause");
                _from = value;
            }
        }

        public virtual string SelectClause
        {
            get { return _selectClause; }

            set
            {
                ValidateValue(value, "SelectClause");
                _selectClause = value;
            }
        }

        public string WhereClause
        {
            get { return _whereClause; }

            set { _whereClause = value; }

        }

        public string OrderedBy
        {
            get { return _orderBy; }

            set { _orderBy = value; }
        }

        public string GroupBy
        {
            get { return _groupBy; }
            set { _groupBy = value; }
        }

        public bool IsSelectDistinct
        {
            get { return _isSelectDistinct; }
            set { _isSelectDistinct = value; }
        }

        private void ValidateValue(string value,string obj)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("Invalid " + obj);
        }

        public override string ConstructQuery()
        {
            var strQuery = new StringBuilder();

            strQuery.Append("Select ");

            if (IsSelectDistinct)
                strQuery.Append(" distinct ");

            strQuery.Append(_selectClause);

            strQuery.Append(" From ");
            if (string.IsNullOrEmpty(_whereClause) && !string.IsNullOrEmpty(_groupBy))
            {
                strQuery.Append("( ");
            }
            strQuery.Append(_from);


            if (!string.IsNullOrEmpty(_whereClause))
            {
                if (!string.IsNullOrEmpty(_groupBy))
                {
                    strQuery.AppendFormat(" Where ( {0} ) ", _whereClause);
                }
                else
                {
                    strQuery.Append(" Where ");
                    strQuery.Append(_whereClause);
                }
            }

            if (!string.IsNullOrEmpty(_groupBy))
            {
                if (string.IsNullOrEmpty(_whereClause))
                {
                    strQuery.Append(" )");
                }
                strQuery.Append(" Group By ");
                strQuery.Append(_groupBy);
            }

            if (!string.IsNullOrEmpty(_groupBy))
            {
                strQuery.Append(" Order By ");
                strQuery.Append(_groupBy);
            }

            if (string.IsNullOrEmpty(_selectClause) || string.IsNullOrEmpty(_from))
            {
                var strMsg = new StringBuilder();
                strMsg.Append("Invalid SQL Query:");
                strMsg.Append(" ");
                strMsg.Append(strQuery);
                throw new Exception(Convert.ToString(strMsg));
            }

            var query = strQuery.ToString();
            return (query);
        }	
    }
}
