using Mapping.BLL.Models;

namespace Mapping.BLL.Responses
{
    public class V1UserResponse
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public bool IsActive { get; set; }
        
        public Role Role { get; set; }
    }
}