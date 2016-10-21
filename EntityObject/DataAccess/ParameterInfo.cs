using System;
using System.Data;

namespace EntityObject.DataAccess
{
    [Serializable]
    public class ParameterInfo
    {
        protected string m_strName;				//Paramenter Name
        protected object m_objValue;				//Value
        protected DataType m_Type = DataType.String;	//Data Type
        protected int m_nSize = 0;				//Size, a Zero(0) value means not to set it
        protected int m_nPrecision = 0;				//Precision, a Zero(0) value means not to set it
        protected int m_nScale = -1;				//Scale, a Negative(-1) value means not to set it

        //Direction:    ParameterDirection.Input;
        //				ParameterDirection.InputOutput;
        //				ParameterDirection.Output;
        //				ParameterDirection.ReturnValue;
        protected ParameterDirection m_Direction = ParameterDirection.Input;

        /// <summary>
        /// Takes in entities of a parameter: Name, Value, Data Type, Size and direction 
        /// </summary>
        /// <param name="Name" />
        /// <param name="Value"></param>
        /// <param name="Type"></param>
        /// <param name="Size"></param>
        /// <param name="Direction"></param>
        public ParameterInfo(string name, object Value, DataType type, int size, ParameterDirection direction, int precision, int scale)
        {
            if ((null == name) || (0 == name.Length))
            {
                throw new Exception();
            }

            this.m_strName = name;
            this.m_objValue = Value;
            this.m_Type = type;
            this.m_nSize = size;
            this.m_Direction = direction;
            this.m_nPrecision = precision;
            this.m_nScale = scale;

            AssertValueType();
        }

        /// <summary>
        /// Takes in entities of a parameter: Name, Type, Direction, and Value. 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <param name="Type"></param>
        /// <param name="Direction"></param>
        public ParameterInfo(string Name, object Value, DataType Type, ParameterDirection Direction) :
            this(Name, Value, Type, 0, Direction, 0, -1)
        {
        }

        public ParameterInfo(string Name, DataType Type, ParameterDirection Direction) :
            this(Name, null, Type, 0, Direction, 0, -1)
        {
        }

        public ParameterInfo(string Name, DataType Type, int size, ParameterDirection Direction) :
            this(Name, null, Type, size, Direction, 0, -1)
        {
        }

        public ParameterInfo(string Name, object Value, DataType Type) :
            this(Name, Value, Type, 0, ParameterDirection.Input, 0, -1)
        {
        }

        public ParameterInfo(string name, object Value, DataType type, int size, ParameterDirection direction) :
            this(name, Value, type, size, direction, 0, -1)
        {
        }

        public ParameterInfo(string Name, object Value, DataType Type, int precision, int scale, ParameterDirection Direction) :
            this(Name, Value, Type, 0, Direction, precision, scale)
        {
        }

        public ParameterInfo(string Name, DataType Type, int precision, int scale, ParameterDirection Direction) :
            this(Name, null, Type, 0, Direction, precision, scale)
        {
        }

        /// <summary>
        /// Takes in entities of a parameter: Name, Type, Direction.  
        /// For Out Parameters of a  store procedure call
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Direction"></param>
        public ParameterInfo(string Name, ParameterDirection Direction) :
            this(Name, null, DataType.String, 0, Direction, 0, -1)
        {
        }

        public ParameterInfo(string Name, object Value) :
            this(Name, Value, DataType.String, 0, ParameterDirection.Input, 0, -1)
        {
        }
        /// <summary>
        /// Paramenter Name
        /// </summary>
        public string Name
        {
            get { return m_strName; }
        }

        /// <summary>
        /// Value
        /// </summary>
        public object Value
        {
            get
            {
                return m_objValue;
            }
            set
            {
                m_objValue = value;
                AssertValueType();
            }
        }

        /// <summary>
        /// Field Type
        /// </summary>
        public DataType Type
        {
            get { return m_Type; }
        }

        /// <summary>
        /// Size, a Zero(0) value means not to set it
        /// </summary>
        public int Size
        {
            get { return m_nSize; }
        }

        /// <summary>
        /// Precision, a Zero(0) value means not to set it
        /// </summary>
        public int Precision
        {
            get { return m_nPrecision; }
        }

        /// <summary>
        /// Scale, a Negative(-1) value means not to set it
        /// </summary>
        public int Scale
        {
            get { return m_nScale; }
        }

        /// <summary>
        /// Parameter Direction
        /// </summary>
        public ParameterDirection Direction
        {
            get { return m_Direction; }
        }

        private void AssertValueType()
        {
#if DEBUG
            //--dd--
            if (m_objValue != null && Nullable.GetUnderlyingType(m_objValue.GetType()) != null)
            {
                throw new Exception("The ParameterInfo does not handle Nullable types. Please pass a generic type as defined by the Orion.DataAccess.DataType enum. --dd--");
            }
#else
			//--dd--
			Debug.Assert(m_objValue == null || Nullable.GetUnderlyingType(m_objValue.GetType()) == null, "The ParameterInfo does not handle Nullable types. Please pass a generic type as defined by the Orion.DataAccess.DataType enum");
#endif
        }
    }
}
