using UnityEngine;

namespace MyLibrary.Attribute
{
    public class StringVariableToElementName : PropertyAttribute
    {
        public string Varname;

        public StringVariableToElementName(string ElementTitleVar)
        {
            Varname = ElementTitleVar;
        }
    }
}