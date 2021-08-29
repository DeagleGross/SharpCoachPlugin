using System;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Psi;
using ReSharperPlugin.SharpCoachPlugin.Core.Processors;
using ReSharperPlugin.SharpCoachPlugin.Models.Types;

namespace DefaultNamespace
{
    public class CollectionTypeMapper : SpecificTypeMapperBase
    {
        public CollectionTypeMapper(MappingCodeBuilder codeBuilder) 
            : base(codeBuilder)
        {
        }

        public override void MapToType(IProperty fromProperty, IProperty toProperty, TypeKind toType)
        {
            switch (toType)
            {
                case TypeKind.Numeric:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Collection` type to `Numeric`");
                    break;
                
                case TypeKind.Enum:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Collection` type to `Enum`");
                    break;
                
                case TypeKind.String:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Collection` type to `String`");
                    break;
                
                case TypeKind.Class:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Collection` type to `Class`");
                    break;
                
                case TypeKind.Structure:
                    // can not think of a solution for this case
                    LogLog.Info("There is no handler for mapping `Collection` type to `Structure`");
                    break;
                
                case TypeKind.Collection:
                    MapToCollection(fromProperty, toProperty);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(toType), toType, null);
            }
        }

        private void MapToCollection(IProperty fromProperty, IProperty toProperty)
        {
            // TODO finish mapping collections
        }
    }
}