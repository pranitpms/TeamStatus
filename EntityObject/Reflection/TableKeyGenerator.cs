using EntityObject.DataAccess;
using EntityObject.OrcleDataAccess;
using EntityObject.Utility;

namespace EntityObject.Reflection
{
    public class TableKeyGenerator
    {
        public TableKeyGenerator(DataMap map)
        {
            _map = map;
        }

        #region Private Memeber

        private readonly DataMap _map;

        #endregion

        public object GetNextValue()
        {
            var tableName = _map.TableName;

            return IncreamentValue(tableName);
        }

        public long IncreamentValue(string tableName)
        {
            var selectQuery = new SelectQuery
            {
                FromClause = "TBL",
                SelectClause = "LAST_VALUE",
                WhereClause = string.Format("TBL_NAME = '{0}'", tableName)
            };

            using (var con = new Connection())
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.SQLQuery = selectQuery;
                    var lastValue = ConvertValue.ToLongValue(cmd.ExecuteScalar());

                    var updateQuery = new UpdateQuery("TBL");
                    updateQuery.AddSetValue("LAST_VALUE", lastValue + 1);
                    updateQuery.AddWhereValue("TBL_NAME", tableName);

                    cmd.SQLQuery = updateQuery;
                    cmd.ExecuteNonQuery();

                    return (lastValue + 1);
                }
            }
        }
    }
}
