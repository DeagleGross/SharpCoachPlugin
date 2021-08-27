using System.Text;

namespace DefaultNamespace
{
    public class MappingCodeBuilder
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        private readonly string _fromVariableName;

        public string Result => _stringBuilder.ToString();
        
        public MappingCodeBuilder(string fromVariableName)
        {
            _fromVariableName = fromVariableName;
        }

        public void AddSimplePropertyBinding(string propertyName)
        {
            _stringBuilder.AppendLine($"{propertyName} = {_fromVariableName}.{propertyName},");
        }
    }
}