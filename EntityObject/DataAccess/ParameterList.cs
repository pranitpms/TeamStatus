using System;
using System.Collections;
using System.Data;

namespace EntityObject.DataAccess
{
    [Serializable]
    public class ParameterList
    {
		
		protected ArrayList m_arParameterList; 

		
		protected int m_nCounter;

		public ParameterList()
		{
			m_arParameterList = new ArrayList();
			m_nCounter = 0;
		}

		
		public void Clear()
		{
			m_arParameterList.Clear();
			m_nCounter = 0;
		}

		
		public int Count
		{
			get	{ return m_arParameterList.Count; }
		}

		
		public ParameterInfo this[int nIndex]
		{
			get
			{
				return (ParameterInfo)m_arParameterList[nIndex];
			}
		}

		public ParameterInfo this[string name]
		{
			get
			{
				return this.GetParameter(name);
			}
		}

		public string SetParameter (string strName,object objValue)
		{
			m_nCounter++;
			
			strName = strName + m_nCounter.ToString();

			ParameterInfo paramInfo=null;
			
			if (objValue == null)
				paramInfo = new ParameterInfo(strName,null);
			else
				paramInfo = new ParameterInfo(strName, objValue, DataTypeUtility.SystemTypeToDataType(objValue.GetType()), ParameterDirection.Input);

			m_arParameterList.Add(paramInfo);
	
			return (strName);
		}

		internal void AddParameter (string Name, object Value, DataType dataType, int Size, ParameterDirection direction)
		{
			//ParameterInfo constructor checks for NULL Name
			AddParameter (new ParameterInfo (Name, Value, dataType, Size,direction));
		}

	
		protected void AddParameter (string Name, object Value, DataType dataType, int Size, ParameterDirection direction, int precision, int scale)
		{
			//ParameterInfo constructor checks for NULL Name
			AddParameter (new ParameterInfo(Name, Value, dataType, Size,direction, precision, scale));
		}

		protected void AddParameter (ParameterInfo parameterInfo)
		{
			//Orion.Utility.ParameterValidation.AssureNotNull(parameterInfo,"ParInfo");
			
			int iIndex = FindParameterIndex(parameterInfo.Name);

			//if already exist, remove it and add the new one in.
			if (iIndex >= 0)
			{
				m_arParameterList.RemoveAt(iIndex);
			}

			m_arParameterList.Add(parameterInfo);
		}

	
		public void AddInputParameter (string Name,object Value)
		{
			if (Value == null)
				AddInputParameter (Name, Value, DataType.String);
			else
				AddInputParameter (Name, Value,DataTypeUtility.SystemTypeToDataType(Value.GetType()));
		}

	
		public void AddInputParameter(string Name, object Value, Type type)
		{
			this.AddInputParameter(Name, Value, DataTypeUtility.SystemTypeToDataType(type));
		}


	
		public void AddInputParameter (string Name,object Value, DataType dataType)
		{
			AddParameter (Name, Value, dataType, 0, ParameterDirection.Input);
		}


		public void AddOutputParameter (string Name, Type type, int Size)
		{
			AddOutputParameter(Name, DataTypeUtility.SystemTypeToDataType(type), Size);
		}


		public void AddOutputParameter (string Name, DataType dataType)
		{
			AddParameter (Name, null, dataType, 0, ParameterDirection.Output);
		}

		
		public void AddOutputParameter (string Name, DataType dataType, int Size)
		{
			if (dataType == DataType.String || dataType == DataType.Binary)
			{
				if (Size <= 0)
				{
					//To Do: throw error.
					//throw new System.ArgumentException("For Ouput Parameters of type String and Binary, the size can not be less than 0.","Size");
				}
			}

			AddParameter(Name, null, dataType, Size, ParameterDirection.Output);
		}


		public void AddOutputParameter (string Name, DataType dataType, int precision, int scale)
		{
			AddParameter(Name, null, dataType, 0, ParameterDirection.Output, precision, scale);
		}

		public ParameterInfo GetParameter (string strName)
		{
	//		Orion.Utility.ParameterValidation.AssureValidString(strName,"strName");

			int iIndex = FindParameterIndex (strName);

			if (iIndex >= 0)
			{
				return ((ParameterInfo)m_arParameterList[iIndex]);
			}

			return null;
		}

		public DataType GetParameterType (int iIndex)
		{
	//		Orion.Utility.ParameterValidation.AssureValidRange(iIndex,0,m_arParameterList.Count - 1,"iIndex");

			ParameterInfo parameterInfo = this[iIndex];
			return parameterInfo.Type;
		}

		public DataType GetParameterType (string strName)
		{
		//	Orion.Utility.ParameterValidation.AssureValidString (strName, "strName");
		    
			ParameterInfo parameterInfo = GetParameter (strName);

			if (null == parameterInfo)
			{
				return DataType.Unknown;
			}
			
			return parameterInfo.Type;
		}


		public object GetParameterValue (int iIndex)
		{
		//	Orion.Utility.ParameterValidation.AssureValidRange (iIndex, 0, m_arParameterList.Count - 1, "iIndex");

			ParameterInfo parameterInfo = (ParameterInfo)m_arParameterList[iIndex];
			
			return parameterInfo.Value;
		}

	
		public object GetParameterValue (string strName)
		{
		//	Orion.Utility.ParameterValidation.AssureValidString (strName, "strName");

			ParameterInfo parameterInfo = GetParameter (strName);

			if (null == parameterInfo)
			{
				return null;
			}
			
			return parameterInfo.Value;
		}

		public void SetParameterValue(int iIndex,object Value)
		{
			//Orion.Utility.ParameterValidation.AssureValidRange(iIndex,0,m_arParameterList.Count - 1,"iIndex");

			ParameterInfo parameterInfo = (ParameterInfo)m_arParameterList[iIndex];

			parameterInfo.Value = Value;
		}


		public bool SetParamenterValue(string strName, object Value)
		{
			//Orion.Utility.ParameterValidation.AssureValidString(strName,"strName");

			ParameterInfo parameterInfo = GetParameter (strName);

			if (null != parameterInfo)
			{
				parameterInfo.Value = Value;
				return true;
			}

			return false;
		}


		
		public bool RemoveParameter (int iIndex)
		{
		//	Orion.Utility.ParameterValidation.AssureValidRange(iIndex,0,m_arParameterList.Count - 1,"iIndex");

			m_arParameterList.RemoveAt(iIndex);

			return true;
		}


		
		public bool RemoveParameter (string strName)
		{
			int iIndex = FindParameterIndex (strName);

			if (iIndex < 0)
			{
				return false;
			}

			return RemoveParameter(iIndex);

		}

		
		protected int FindParameterIndex (string strName)
		{
			for (int i = 0; i < m_arParameterList.Count; i++)
			{
				if (((ParameterInfo)m_arParameterList[i]).Name.Equals (strName))
					return i;
			}

			return -1;
		}
	}
    }

