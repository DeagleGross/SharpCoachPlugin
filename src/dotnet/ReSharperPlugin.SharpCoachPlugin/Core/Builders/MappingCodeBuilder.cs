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
                if (_stringBuilder[^3] == ',')
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

        public void AddSimplePropertyBinding(string propertyName)
        {
            _stringBuilder.AppendLine($"{propertyName} = {FromVariableName}.{propertyName},");
        }

        public void AddWithCastPropertyBinding(string propertyName, string castType)
        {
            _stringBuilder.AppendLine($"{propertyName} = ({castType}){FromVariableName}.{propertyName},");
        }

        public void AddWithToStringCall(string propertyName)
        {
            _stringBuilder.AppendLine($"{propertyName} = {FromVariableName}.{propertyName}.ToString(),");
        }

        public void AddWithNumericTryParseCast(string propertyName, string numericCastType)
        {
            _stringBuilder.AppendLine(
                $"{propertyName} = {numericCastType}.TryParse({FromVariableName}.{propertyName}, out var tmpCastedValue) ? tmpCastedValue : default,");
        }
        
        public void AddWithEnumTryParseCast(string propertyName, string enumTypeName)
        {
            _stringBuilder.AppendLine(
                $"{propertyName} = Enum.TryParse<{enumTypeName}>({FromVariableName}.{propertyName}, out var tmpCastedValue) ? tmpCastedValue : default,");
        }

        public void AddClassPropertyBinding(string propertyName, string toFullClassName, string internalClassMappingCode)
        {
            _stringBuilder.AppendLine(@$"{propertyName} = new {toFullClassName}()
{{
    {internalClassMappingCode}
}},");
        }
    }
}