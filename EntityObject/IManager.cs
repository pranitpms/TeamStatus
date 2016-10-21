namespace EntityObject
{
    interface IManager
    {
        object CreateNew();

        object Fetch(params object[] keys);

        void Delete(params object[] keys);

        void Save(object objInstance);

        void Update(object objInstance);
    }
}
