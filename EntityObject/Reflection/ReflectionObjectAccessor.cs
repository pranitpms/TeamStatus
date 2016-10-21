using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;
using EntityObject.Utility;

namespace EntityObject.Reflection
{
    [Serializable]
    public class ReflectionObjectAccessor : IObjectAccessor
    {
        #region Constructor

        public ReflectionObjectAccessor()
        {
        }

        #endregion

        #region Public Methods

        public void SetInstance(object instance)
        {
            _instance = instance;
            _type = instance.GetType();

            if (TypesTable.ContainsKey(_type))
                _fieldsTable = (ConcurrentDictionary<string, object>)TypesTable[_type];
            if (_fieldsTable != null)
                return;

            lock (TypesTable)
            {
                if (TypesTable.ContainsKey(_type))
                    _fieldsTable = (ConcurrentDictionary<string, object>)TypesTable[_type];
                if (_fieldsTable != null)
                    return;

                _fieldsTable = new ConcurrentDictionary<string, object>();
                TypesTable[_type] = _fieldsTable;
            }
        }

        public void ClearInstance()
        {
            _instance = null;
            _type = null;
            if (_fieldsTable != null)
                _fieldsTable.Clear();
            TypesTable.Clear();
        }

        public object GetInstanceData(string name)
        {
            if (PropertyNameUtility.IsArray(name))
            {
                IList array = this.GetValue(PropertyNameUtility.Property(name), true) as IList;
                if (array == null)
                    throw new ArgumentException("The property '" + name + "' with type '" + this._type.ToString() + "' is null or does not implement IList.");

                return array[PropertyNameUtility.Index(name)];
            }
            else
            {
                return this.GetValue(name, true);
            }
        }

        public void SetInstanceData(string name, object val)
        {
            if (PropertyNameUtility.IsArray(name))
            {
                IList array = this.GetValue(PropertyNameUtility.Property(name), true) as IList;
                if (array == null)
                    throw new ArgumentException("The property '" + name + "' with type '" + this._type.ToString() + "' is null or does not implement IList.");

                array[PropertyNameUtility.Index(name)] = val;
            }
            else
            {
                // Look for a field first
                FieldInfo field = this.LookupField(name);
                if (field != null)
                {
                    field.SetValue(this._instance, val);
                    return;
                }

                // There is no field with this name so look for a property
                PropertyInfo property = this.LookupProperty(name);
                if (property != null)
                {
                    property.SetValue(this._instance, val, null);
                    return;
                }

                // There is no field or property with this name so raise an exception
                throw new InvalidOperationException();
            }
        }

        public bool ContainsField(string name)
        {
            if (PropertyNameUtility.IsArray(name))
            {
                IList array = this.GetValue(PropertyNameUtility.Property(name), false) as IList;
                if (array == null)
                    return false;

                return (array.Count > PropertyNameUtility.Index(name));
            }
            else
            {
                // Look for a field
                FieldInfo field = this.LookupField(name);
                if (field != null)
                    return true;

                // Look for a property
                PropertyInfo property = this.LookupProperty(name);
                if (property != null)
                    return true;
            }

            return false;
        }

        #endregion

        #region Private Methods

        private FieldInfo LookupField(string name)
        {
            FieldInfo field = null;

            object temp = null;
            if (this._fieldsTable.TryGetValue(name, out temp))
                return temp as FieldInfo;

            if (!name.StartsWith("_"))
                field = this._type.GetField("_" + name, CFlags);

            if (field == null)
                field = this._type.GetField(name, CFlags);

            if (field != null)
            {
                //this._FieldsTable[name] = field;
                this._fieldsTable.TryAdd(name, field);
                return field;
            }

            return null;
        }

        private PropertyInfo LookupProperty(string name)
        {
            PropertyInfo property = null;

            object temp = null;
            if (_fieldsTable.TryGetValue(name, out temp))
                return temp as PropertyInfo;

            property = this._type.GetProperty(name, CFlags);

            if (property != null)
            {
                //this._FieldsTable[name] = property;
                this._fieldsTable.TryAdd(name, property);
                return property;
            }

            return null;
        }

        private object GetValue(string name, bool throwOnFail)
        {
            // Look for a field first
            FieldInfo field = this.LookupField(name);
            if (field != null)
                return field.GetValue(this._instance);

            // There is no field with this name so look for a property
            PropertyInfo property = this.LookupProperty(name);
            if (property != null)
                return property.GetValue(this._instance, null);

            if (throwOnFail)
            {
                // There is no field or property with this name so raise an exception
                throw new InvalidOperationException();
            }
            return null;
        }

        #endregion

        #region Private Members

        private static readonly Hashtable TypesTable = new Hashtable();
        private const BindingFlags CFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;

        private object _instance;
        private Type _type;
        private ConcurrentDictionary<string, object> _fieldsTable;

        #endregion
    }
}
