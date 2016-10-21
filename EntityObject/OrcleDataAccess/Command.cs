using System;
using System.Data;
using EntityObject.DataAccess;
using Oracle.DataAccess.Client;

namespace EntityObject.OrcleDataAccess
{
    public class Command : IDisposable
    {
        public Command()
        {
            
        }

        public Command(SQLQuery sqlQuery)
        {
            SQLQuery = sqlQuery;
            
        }

        public Command(SQLQuery query, CommandType type)
        {
            SQLQuery = query;
            CommandType = type;
        }

        private OracleCommand _command;

        public SQLQuery SQLQuery { get; set; }

        public CommandType CommandType { get; set; }

        public OracleConnection OracleConnection { get; set; }

        public OracleCommand OracleCommand
        {
            get { return _command;}
            set { _command = value; }
        }

        public OracleTransaction OracleTransaction { get; set; }


        public void Dispose()
        {
          //  _transaction.Dispose();
            _command.Dispose();
        }

        public OracleDataReader ExceuteReader()
        {
            var query = SQLQuery.ConstructQuery();
            var parameters = SQLQuery.Parameters;

            _command.CommandText = query;

            for (int i = 0; i < parameters.Count; i++)
            {
                _command.Parameters.Add(parameters[i].Name, parameters[i].Value);
            }

            try
            {
                return _command.ExecuteReader();
            }
            catch (OracleException exception)
            {
                throw new Exception(exception.Message);
            }
            return null;
        }

        public int ExecuteNonQuery()
        {
            var query = SQLQuery.ConstructQuery();

            //ValidateQuery();

            _command.CommandText = query;

            var parameters = SQLQuery.Parameters;
            for (var i = 0; i < parameters.Count; i++)
            {
                _command.Parameters.Add(parameters[i].Name, parameters[i].Value);
            }
            try
            {
                return _command.ExecuteNonQuery();
            }
            catch (OracleException exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public object ExecuteScalar()
        {
            var query = SQLQuery.ConstructQuery();
            
            _command.CommandText = query;

            var parameters = SQLQuery.Parameters;
            for (var i = 0; i < parameters.Count; i++)
            {
                _command.Parameters.Add(parameters[i].Name, parameters[i].Value);
            }

            try
            {
                return _command.ExecuteScalar();
            }
            catch (OracleException exception)
            {
                throw new Exception(exception.Message);
            }

        }

        public DataSet ExecuteDataSet()
        {
            return ExecuteDataSet("");
        }


        public DataSet ExecuteDataSet(string tableName)
        {
            var query = SQLQuery.ConstructQuery();

            _command.CommandText = query;

            var parameters = SQLQuery.Parameters;
            for (var i = 0; i < parameters.Count; i++)
            {
                _command.Parameters.Add(parameters[i].Name, parameters[i].Value);
            }

            var dataSet = new DataSet(tableName);
            try
            {
                using (var oracleDataAdapter = new OracleDataAdapter(_command))
                {
                    if (string.IsNullOrEmpty(tableName))
                        oracleDataAdapter.Fill(dataSet);
                    else
                        oracleDataAdapter.Fill(dataSet, tableName);
                }
            }
            catch (OracleException exception)
            {
                throw new Exception(exception.Message);
            }
            return dataSet;
        }
    }
}
