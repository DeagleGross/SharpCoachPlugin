using JetBrains.ReSharper.Psi;

namespace DefaultNamespace
{
    public class PropertyDescriptor
    {
        public IType Type { get; }

        public string Name { get; }
        
        public PropertyDescriptor(IType type, string name)
        {
            Type = type;
            Name = name;
        }

        #region Equality Members

        private bool Equals(PropertyDescriptor other)
        {
            return Equals(Type, other.Type) && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PropertyDescriptor)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Type != null ? Type.GetHashCode() : 0) * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        #endregion
    }
}