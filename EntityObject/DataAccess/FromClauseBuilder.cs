namespace EntityObject.DataAccess
{
    public class FromClauseBuilder
    {
        public FromClauseBuilder(DataMap map)
        {
            _map = map;
        }

        private DataMap _map;

        public virtual string BuildFromClause()
        {
            var tableName = _map.TableName;

            return tableName;
        }
    }
}
