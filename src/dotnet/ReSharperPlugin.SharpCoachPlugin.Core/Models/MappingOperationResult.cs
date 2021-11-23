using System;
using System.Collections.Generic;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Models
{
    public class MappingOperationResult
    {
        public DateTime OperationDate { get; set; }
        
        public string InputClassName { get; set; }
        
        public string OutputClassName { get; set; }
        
        public IEnumerable<string> InputClassErrorPropertyNames { get; set; } 
        
        public IEnumerable<string> OutputClassErrorPropertyNames { get; set; } 
    }
}