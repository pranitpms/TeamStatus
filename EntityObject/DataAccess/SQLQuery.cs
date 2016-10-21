using System;
using System.Data;
using System.Globalization;
using System.Text;

namespace EntityObject.DataAccess
{
    [Serializable]
    public class SQLQuery
    {
        public SQLQuery()
        {
            Initialize();
        }

        public SQLQuery(string strQuery, CommandType commandType)
        {
            Initialize();
            _mStrQuery = strQuery;
            _mCommandType = commandType;
        }

        public ParameterList Parameters
        {
            get { return _mAParameters; }
        }


        /// <summary>
        /// Construct and return the SQL Statment
        /// </summary>
        /// <returns></returns>
        public string GetQuery()
        {
            return this.ConstructQuery();
        }

        public override string ToString()
        {
            return this.GetQuery();
        }
        /// <summary>
        /// Sets the SQL statement.
        /// </summary>
        /// <param name="strQuery"></param>
        public void SetQuery(string strQuery)
        {
            _mStrQuery = strQuery;
        }

        public virtual CommandType CommandType
        {
            get
            {
                return (_mCommandType);
            }
            set
            {
                _mCommandType = CommandType;
            }
        }

        /// <summary>
        /// Construct the Query
        /// </summary>
        /// <returns></returns>
        public virtual string ConstructQuery()
        {
            return _mStrQuery;
        }


        protected bool GetValue(out string strQueryValue, Field field, string paramPrefix)
        {
            return (GetValue(out strQueryValue, field, paramPrefix, ParameterDirection.Input));
        }

        protected bool GetValue(out string strQueryValue, Field field, string paramPrefix, ParameterDirection paramDirection)
        {
            
            if (field.ValueIsActualFieldName)
            {
                strQueryValue = (string)field.Value;
                return false;
            }

            StringBuilder strValue = new StringBuilder(50);
            bool bValueNull = false;

            if (field.Value == null || field.Value is DBNull)
            {
                strValue.Append("NULL");
                bValueNull = true;
            }
            else
            {
                string strParamName = paramPrefix + field.Name;

                switch (field.Type)
                {
                    case DataType.DateTime:
                        DateTime datValue = (DateTime)field.Value;
                        if (datValue == DateTime.MinValue)
                        {
                            strValue.Append("NULL");
                            bValueNull = true;
                        }
                        else
                        {
                            strValue.Append(":");
                            strValue.Append(strParamName);
                            this.Parameters.AddParameter(strParamName, field.Value, field.Type, 0, ParameterDirection.Input);
                        }

                        break;

                    case DataType.Binary:
                        strValue.Append(":");
                        strValue.Append(strParamName);

                        this.Parameters.AddParameter(strParamName, field.Value, field.Type, 0, paramDirection);

                        break;

                    case DataType.Char:
                        char chValue = (char)field.Value;

                        if (chValue == 0)
                        {
                            strValue.Append("NULL");
                            bValueNull = true;
                        }
                        else
                        {
                            strValue.Append("'");
                            strValue.Append(field.Value.ToString());
                            strValue.Append("'");
                        }

                        break;

                    case DataType.Decimal:
                        if (field.Value is Double)
                            strValue.Append((((Double)(field.Value)).ToString(CultureInfo.InvariantCulture.NumberFormat)));
                        else if (field.Value is Decimal)
                            strValue.Append((((Decimal)(field.Value)).ToString(CultureInfo.InvariantCulture.NumberFormat)));
                        else
                            throw new Exception(string.Format("Expecting field.value to be data type Decimal not data type {0}", field.Value.GetType().ToString()));

                        break;

                    case DataType.Integer:
                        strValue.Append(field.Value);

                        break;

                    case DataType.String:
                        strValue.Append(":");
                        strValue.Append(strParamName);

                        this.Parameters.AddParameter(strParamName, field.Value, field.Type, 0, paramDirection);

                        break;
                }
            }

            strQueryValue = strValue.ToString();
            return (bValueNull);
        }
        /// <summary>
        /// Rinitalize Member variable
        /// </summary>
        protected void Initialize()
        {
            _mAParameters = new ParameterList();
        }

        #region private members
        private string _mStrQuery = "";
        private CommandType _mCommandType = CommandType.Text;
        private ParameterList _mAParameters;
        #endregion
    }
}
