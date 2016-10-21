using System;
using System.Collections.Generic;
using System.Text;
using EntityObject.DataAccess;

namespace EntityObject
{
    public class QueryBuilder
    {
        public QueryBuilder(DataMap map)
        {
            _map = map;
        }

        private DataMap _map;

        public SQLQuery FetchByKeyQuery(object[]keys)
        {
            if (_map == null) return null;

            var select = new StringBuilder();
            var where  = new StringBuilder();

            BuildSelectClause(select);
            var from = new FromClauseBuilder(_map);
           
            SelectQuery selectQuery = new SelectQuery();
            selectQuery.SelectClause = Convert.ToString(select);
            selectQuery.FromClause = from.BuildFromClause();

             CreateWhereForPrimaryKey(_map,where,selectQuery,keys);
             selectQuery.WhereClause = Convert.ToString(where);

            return selectQuery;
        }

        private void BuildSelectClause(StringBuilder select)
        {

            var fields = _map.FieldCollection;
            Dictionary<string,int> aliases = new Dictionary<string,int>();
            string alias;
            foreach (var field in fields)
            {

                if (aliases.ContainsKey(field.AliasName))
                {
                    aliases[field.AliasName]++;
                    alias = string.Format("{0}_{1}", field.AliasName, aliases[field.AliasName]);
                }
                else
                {
                    alias = field.AliasName;
                    aliases.Add(alias, 0);
                }

                AddFieldToSelect(field.FieldName, alias, select);
            }

        }

        public virtual SQLQuery BuildDeleteSQL(object[] keys)
        {
            DeleteQuery query = new DeleteQuery();
            query.Table = _map.TableName;

            StringBuilder where = new StringBuilder();
            CreateWhereForPrimaryKey(_map, where, query, keys);

            query.WhereClause = where.ToString();
            return query;
        }


        protected virtual void AddFieldToSelect(string fieldName, string alias, StringBuilder select)
        {
            this.AppendText(select, ", ", string.Format("{0} \"{1}\"", fieldName, alias));
        }

        protected virtual void AppendText(StringBuilder buildText, string separator, string textToAdd)
        {
            AppendTextGenerally(buildText, separator, textToAdd);
        }

        public static void AppendTextGenerally(StringBuilder buildText, string separator, string textToAdd)
        {
            if (buildText.Length > 0)
                buildText.Append(separator);

            buildText.AppendLine(textToAdd);
        }

        public static void CreateWhereForPrimaryKey(DataMap map,StringBuilder where,SQLQuery query,object[] keys)
        {
            if (map == null) return;

            for (int index = keys.GetLowerBound(0); index <= keys.GetUpperBound(0); index++)
            {
                var fieldName = map.GetPrimaryKeyFieldName(index);

                var paramName = fieldName.ToLowerInvariant();
                var param = keys[index];
                AppendTextGenerally(where, " AND ", fieldName + " = :" + paramName);
                paramName = ":" + paramName;
                query.Parameters.AddInputParameter(paramName, param);
            }
        }

    }
}
