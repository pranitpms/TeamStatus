using System;
using System.Configuration;
using System.Web.Configuration;
using EntityObject.DataAccess;
using Oracle.DataAccess.Client;

namespace EntityObject.OrcleDataAccess
{
    public class Connection :IDisposable
    {

        public Connection():this("PRANIT")
        {
            
        }

        public Connection(string databaseName)
        {
            CreateConnection(databaseName);
        }

        public void CreateConnection(string databaseName)
        {
            var connectionString = WebConfigurationManager.AppSettings[databaseName];

            if (string.IsNullOrEmpty(connectionString))
                connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            if (string.IsNullOrEmpty(connectionString)) 
                throw new Exception("Connection string is empty");

            try
            {
                _connection = new OracleConnection(connectionString);
                _connection.Open();
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

        public void CreateConnection(string databaseName,string connectionString)
        {
            
        }
        

       #region Private Memeber

        private OracleConnection _connection;
        private OracleCommand _oracleCommand;
        private OracleTransaction _transaction;
        private Command _command;

        #endregion

        public OracleConnection ProviderConection
        {
            get { return _connection; }
        }

        public OracleCommand Command
        {
            get { return _oracleCommand;}
        }

        public Command CreateCommand(SQLQuery sqlQuery)
        {
            if (_connection == null) return null;

            _command = new Command(sqlQuery)
            {
               // OracleTransaction = Transaction,
                OracleConnection = ProviderConection,
            };

            _oracleCommand = ProviderConection.CreateCommand();
            _command.OracleCommand = _oracleCommand;
            return _command;
        }

        public Command CreateCommand()
        {
            if (_connection == null) return null;

            _command = new Command {OracleConnection = ProviderConection};

            _oracleCommand = ProviderConection.CreateCommand();
            _command.OracleCommand = _oracleCommand;
            return _command;

        }

        public OracleTransaction Transaction
        {
            get
            {
                var transation = new Transaction(this);
                _transaction = transation.OracleTransaction;
                return _transaction;
            }
        }

        public void Dispose()
        {
           // _transaction.Dispose();
            _command.Dispose();
            _connection.Dispose();
            _oracleCommand.Dispose();
        }
    }
}
