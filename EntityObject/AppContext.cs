using System;
using System.Reflection;

namespace EntityObject
{
    public class AppContext
    {
       private const BindingFlags _bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

       protected object CreateNewInstance(Type type)
       {
           return OnCreateNewInstance(type,null);
       }

       public object OnCreateNewInstance(Type type, object[] objArgs)
       {
           if (type == null)
               return null;

           var instance = Activator.CreateInstance(type, _bindingFlags,null, objArgs, null);
           return instance;
       }

       
    }
}
