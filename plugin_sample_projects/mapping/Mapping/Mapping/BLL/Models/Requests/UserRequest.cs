namespace Mapping.BLL.Models.Requests
{
    public class UserRequest
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public bool IsActive { get; set; }
        
        public UserRole Role { get; set; }
    }
}