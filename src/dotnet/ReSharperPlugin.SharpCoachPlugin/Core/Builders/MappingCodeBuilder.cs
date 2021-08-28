using System;
using System.Text;
using ReSharperPlugin.SharpCoachPlugin.Core.Processors;

namespace DefaultNamespace
{
    public class MappingCodeBuilder
    {
        private readonly StringBuilder _stringBuilder = new();

        private readonly string _fromVariableName;

        public string Result
        {
            get
            {
                if (_stringBuilder.Length == 0)
                {
                    return string.Empty;
                }

                // removing last ','
                if (_stringBuilder[^1] == ',')
                {
                    _stringBuilder.Remove(_stringBuilder.Length - 1, 1);   
                }

                return _stringBuilder.ToString();
            }
        }
        
        public MappingCodeBuilder(string fromVariableName)
        {
            _fromVariableName = fromVariableName;
        }

        public void AddSameTypeAndNamePropertyBinding(string propertyName)
        {
            _stringBuilder.AppendLine($"{propertyName} = {_fromVariableName}.{propertyName},");
        }

        public void AddNumericToEnumPropertyBinding(string enumTypeName, string propertyName)
        {
            _stringBuilder.AppendLine($"{propertyName} = ({enumTypeName}){_fromVariableName}.{propertyName},");
        }

        public void AddNumericPropertyBinding(string propertyName, NumericType fromNumeric, NumericType toNumeric)
        {
            if (fromNumeric > toNumeric)
            {
                _stringBuilder.AppendLine($"{propertyName} = ({toNumeric.GetNumericTypeStringRepresentation()}){_fromVariableName}.{propertyName},");    
            }
            else
            {
                AddSameTypeAndNamePropertyBinding(propertyName);
            }
        }
    }
}