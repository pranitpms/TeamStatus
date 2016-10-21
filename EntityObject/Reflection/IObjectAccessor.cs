namespace EntityObject.Reflection
{
    public interface IObjectAccessor
    {
        void SetInstance(object instance);
        object GetInstanceData(string name);
        void SetInstanceData(string name, object val);
    }
}
