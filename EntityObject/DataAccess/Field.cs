namespace EntityObject.DataAccess
{
    public class Field
    {
        protected string m_strName;
        protected bool m_bValueIsActualFieldName;
        protected object m_objValue;
        protected DataType m_Type;

        // CONSTRUCT-------------------
        public Field(string Name, object Value, DataType Type)
        {
            FieldInit(Name, Value, Type);
            m_bValueIsActualFieldName = false; // default
        }

        public Field(string Name, object Value, DataType Type, bool bValueIsActualFieldName)
        {
            FieldInit(Name, Value, Type);
            m_bValueIsActualFieldName = bValueIsActualFieldName;
        }

        private void FieldInit(string name, object value, DataType type)
        {
            //Orion.Utility.ParameterValidation.AssureValidString(name, "Name", false);

            m_strName = name;
            m_objValue = value;
            //m_objValue = value;
            m_Type = type;

        }

        //-------------------------------


        public override bool Equals(object obj)
        {
            Field field = obj as Field;

            if (null == field)
                return false;

            return this.Name.Equals(field.Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string Name
        {
            get { return m_strName; }
        }

        public bool ValueIsActualFieldName
        {
            get { return m_bValueIsActualFieldName; }
            set { m_bValueIsActualFieldName = value; }
        }

        public object Value
        {
            get { return m_objValue; }
            set
            {
               // Orion.Utility.ParameterValidation.AssureNotNull(value, "Value");
                m_objValue = value;
            }
        }

        public DataType Type
        {
            get { return m_Type; }
        }
    }
}
