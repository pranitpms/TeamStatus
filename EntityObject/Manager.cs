using System;

namespace EntityObject
{
    public class Manager<TEntity, TKey> : IManager
    {
        public object CreateNew()
        {
            return OnCreateNew();
        }

        public object Fetch(TKey keys)
        {
            return OnFetch(keys);
        }

        public void Delete(params object[] keys)
        {
            OnDelete(keys);
        }

        public void Save(object objInstance)
        {
            OnSave(objInstance);
        }

        public void Update(object objInstance)
        {
            OnUpdate(objInstance);
        }

        protected virtual void OnUpdate(object objInstance)
        {
            var rep = new Repository(EntityType);
            rep.Update(objInstance);
        }

        protected virtual object OnFetch(TKey keys)
        {
            var rep = new Repository(EntityType);
            return rep.FetchByKey(ConvertKeys(keys));
        }

        protected TEntity OnCreateNew()
        {
            AppContext context = new AppContext();
            return (TEntity)context.OnCreateNewInstance(EntityType,null);
        }

        protected virtual void OnSave(object instance)
        {
            var rep = new Repository(EntityType);
            rep.Add(instance);
        }

        protected virtual void OnDelete(params object[] keys)
        {
            var rep = new Repository(EntityType);
            rep.DeleteByKey(keys);
        }

        protected virtual object[] ConvertKeys(TKey key)
        {
            return new object[] { key };
        }

        public Type EntityType
        {
            get { return typeof(TEntity); }
        }



        public object Fetch(params object[] keys)
        {
            throw new NotImplementedException();
        }
    }
}
