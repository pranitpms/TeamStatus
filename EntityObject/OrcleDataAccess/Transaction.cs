using Oracle.DataAccess.Client;

namespace EntityObject.OrcleDataAccess
{
    class Transaction
    {
        public Transaction(Connection objConnection)
        {
            //objConnection.ProviderConection.EnlistDistributedTransaction();
        }

        #region Private Memeber

        readonly OracleTransaction _transaction;

        #endregion

        public void Rolleback()
        {
            _transaction.Rollback();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public OracleTransaction OracleTransaction
        {
            get { return _transaction; }
        }
    }
}
