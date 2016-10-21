using System;

namespace EntityObject
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class PrimaryKeyFieldAttribute : DataFieldAttribute
    {
        public readonly int PrimaryKeyIndex;

        public PrimaryKeyFieldAttribute(string fieldName, int index) : this(fieldName,index,string.Empty)
        {
            
        }

        public PrimaryKeyFieldAttribute(string fieldName, int index, string aliesName) : base(string.Empty,fieldName)
        {
            PrimaryKeyIndex = index;
        }

    }
}
