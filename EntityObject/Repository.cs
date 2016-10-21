using System;
using EntityObject.DataAccess;
using EntityObject.OrcleDataAccess;
using EntityObject.Reflection;
using Oracle.DataAccess.Client;

namespace EntityObject
{
    public class Repository : AppContext
    {
        public Repository(Type type)
        {
            _type = type;
        }

        private readonly Type _type;
        private DataMap _map;

        public DataMap Map
        {
            get { return _map; }
        }

        protected IObjectAccessor Accessor
        {
            get
            {
                return _accessor;
            }
        }


        public virtual object FetchByKey(object[] keys)
        {
            var instance = CreateNewInstance(_type);
            if (instance != null)
            {
                Initializer(instance);
                var builder = new QueryBuilder(_map);

                var featchQuery = builder.FetchByKeyQuery(keys);

                using (var con = new Connection())
                {
                    using (var cmd = con.CreateCommand(featchQuery))
                    {
                        var reader = cmd.ExceuteReader();
                    }
                }
            }
            return 0;
        }

        public virtual void Add(object instance)
        {
            if (instance == null)
                throw new Exception("Instance Is Null");

            Initializer(instance);

            SetNewInstanceValue(instance);
            var insertQuery = new InsertQuery(_map.TableName);
            InsertIntoTable(insertQuery);

            using (var connection = new Connection())
            {
                using (var command = connection.CreateCommand(insertQuery))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public virtual void DeleteByKey(params object[] keys)
        {
            if (keys == null)
                return;
            try
            {
                var instance = CreateNewInstance(_type);
                _map = new DataMap(instance);

                var builder = new QueryBuilder(_map);

                var deleteQuery = builder.BuildDeleteSQL(keys);
                using (var connection = new Connection())
                {
                    using(var command =  connection.CreateCommand(deleteQuery))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public virtual void Update(object instance)
        {
            if (instance == null)
                throw new Exception("Instance Is Null");

            Initializer(instance);

            UpdateTable(instance);
        }

        #region Private Method

        protected virtual void UpdateTable(object instance)
        {
            var updateSQL = new UpdateQuery(_map.TableName);

            foreach (var fieldMap in _map.FieldCollection)
            {
                var propertyName = fieldMap.AliasName;
                var value = Accessor.GetInstanceData(propertyName);

                if (fieldMap.IsPrimaryKey)
                    updateSQL.AddWhereValue(fieldMap.FieldName, value);
                else
                    updateSQL.AddSetValue(fieldMap.FieldName, value);
            }

            if (_map.IsHasModified)
            {
                updateSQL.AddSetValue("LMOD_DATE", DateTime.Now.Date, DataType.DateTime);
            }

            var p = _map.GetPrimaryKeyField(0);


            using (var connection = new Connection())
            {
                using (var command = connection.CreateCommand(updateSQL))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        protected virtual void SetNewInstanceValue(object instance)
        {
            foreach (var fieldMap in _map.TableKeyGeneratorList)
            {
                var tableKeyGenerator = new TableKeyGenerator(_map);
                var value = tableKeyGenerator.GetNextValue();
                Accessor.SetInstanceData(fieldMap.AliasName, fieldMap.ConvertToClassType(value));
            }
        }

        private void Initializer(object instance)
        {
            if (instance != null)
            {
                _map = new DataMap(instance);
                _accessor = CreateAccessor();
                _accessor.SetInstance(instance);
            }
        }

        private void InsertIntoTable(InsertQuery insertQuery)
        {
            foreach (var fieldMap in _map.FieldCollection)
            {
                if (fieldMap.IsPrimaryKey) continue;
                var propertyName = fieldMap.AliasName;
                var value = Accessor.GetInstanceData(propertyName);
                insertQuery.AddValue(fieldMap.FieldName, value);
            }
            foreach (var fieldMap in Map.TableKeyGeneratorList)
            {
                var propertyName = fieldMap.AliasName;
                var value = Accessor.GetInstanceData(propertyName);
                insertQuery.AddValue(fieldMap.FieldName, value);
            }

            if (_map.IsHasModified)
            {
                insertQuery.AddValue("LMOD_DATE",DateTime.Now.Date,DataType.DateTime);
            }
        }

        private void SetInstanceValue(object instance, OracleDataReader reader)
        {
            foreach (var collection in _map.FieldCollection)
            {
                var propertyName = collection.AliasName;
                object dbValue, propertyValue;

              //  dbValue = reader.GetValue(propertyName);
               // propertyValue = collection.ConvertToClassType(dbValue);

            }
        }

        

        #endregion

        #region Protected Methods

        protected virtual IObjectAccessor CreateAccessor()
        {
            return new ReflectionObjectAccessor();
        }

        #endregion

        #region Private Member

        private IObjectAccessor _accessor;

        #endregion
    }
}
