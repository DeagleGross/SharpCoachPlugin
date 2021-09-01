using System.Text;

namespace DefaultNamespace
{
    public class MappingCodeBuilder
    {
        private readonly StringBuilder _stringBuilder = new();

        public string FromVariableName { get; }

        public string Result
        {
            get
            {
                if (_stringBuilder.Length == 0)
                {
                    return string.Empty;
                }

                // removing last ','
                if (_stringBuilder[_stringBuilder.Length - 3] == ',')
                {
                    _stringBuilder.Remove(_stringBuilder.Length - 3, 1);   
                }

                return _stringBuilder.ToString();
            }
        }
        
        public MappingCodeBuilder(string fromVariableName)
        {
            FromVariableName = fromVariableName.Trim();
        }

        public void AddPropertyBindingStandard(string propertyName)
        {
            _stringBuilder.AppendLine($"{propertyName} = {FromVariableName}.{propertyName},");
        }

        public void AddPropertyBindingWithCast(string propertyName, string castType)
        {
            _stringBuilder.AppendLine($"{propertyName} = ({castType}){FromVariableName}.{propertyName},");
        }

        public void AddPropertyBindingWithToStringCall(string propertyName)
        {
            _stringBuilder.AppendLine($"{propertyName} = {FromVariableName}.{propertyName}.ToString(),");
        }

        public void AddPropertyBindingWithNumericTryParseCast(string propertyName, string numericCastType)
        {
            _stringBuilder.AppendLine(
                $"{propertyName} = {numericCastType}.TryParse({FromVariableName}.{propertyName}, out var tmpCastedValue) ? tmpCastedValue : default,");
        }
        
        public void AddPropertyBindingWithEnumTryParseCast(string propertyName, string enumTypeName)
        {
            _stringBuilder.AppendLine(
                $"{propertyName} = Enum.TryParse<{enumTypeName}>({FromVariableName}.{propertyName}, out var tmpCastedValue) ? tmpCastedValue : default,");
        }

        public void AddPropertyBindingForClass(string propertyName, string toFullClassName, string internalClassMappingCode)
        {
            _stringBuilder.AppendLine(@$"{propertyName} = new {toFullClassName}()
{{
    {internalClassMappingCode}
}},");
        }
        
        public void AddPropertyBindingForLinqSelect(string propertyName, string lambdaMappingCode, string castToCollectionMethod)
        {
            _stringBuilder.AppendLine(@$"{propertyName} = {FromVariableName}.{propertyName}.Select({lambdaMappingCode}){castToCollectionMethod},");
        }
    }
}