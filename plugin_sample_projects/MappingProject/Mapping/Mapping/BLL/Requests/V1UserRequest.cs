using Mapping.BLL.Models;

namespace Mapping.BLL.Requests
{
    public class V1UserRequest
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public bool IsActive { get; set; }
        
        public Role Role { get; set; }
    }
}