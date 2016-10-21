using System;

namespace EntityObject.Utility
{
    public class PropertyNameUtility
    {
       
        public static bool IsArray(string propertyName)
        {
            if (propertyName.IndexOf('#') == -1)
                return false;
            else
                return true;
        }

       
        public static string Property(string propertyName)
        {
            int nPos = propertyName.IndexOf('#');

            if (nPos <= 0)
                return propertyName;

            return propertyName.Substring(0, nPos);
        }

        public static int Index(string propertyName)
        {
            int result = -1;
            if (propertyName != null)
            {
                int nPos = propertyName.IndexOf('#');

                if (nPos <= 0 || propertyName.Length - 1 == nPos || (!int.TryParse(propertyName.Substring(nPos + 1), out result)))
                {
                    
                    return result;
                }

                return result - 1;
            }

            return result;
        }

        
        public static string CreateFieldName(string propertyName, int index)
        {
            index++;
            return propertyName + "#" + index;
        }

       
        public static string GetSortedColumnName(string sortExpression)
        {
            string sortField = "";

            sortField = sortExpression.IndexOf(" ", StringComparison.Ordinal) != -1 ? sortExpression.Substring(0, sortExpression.IndexOf(" ", StringComparison.Ordinal)) : sortExpression;

            if (sortField.Trim().Length == 0)
                return null;

            return sortField;
        }

        public const string NumberSignPlaceHolder = "_num_";

        public static string EncodeFieldName(string fieldName)
        {
            if (fieldName == null)
                return null;

            return fieldName.Replace("#", NumberSignPlaceHolder);
        }

        public static string DecodeFieldName(string fieldName)
        {
            if (fieldName == null)
                return null;

            return fieldName.Replace(NumberSignPlaceHolder, "#");
        }

        public static bool IsMultiValue(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
                return false;

            return Property(fieldName).ToUpperInvariant() == "MULTIVALUE";
        }


    }
}
