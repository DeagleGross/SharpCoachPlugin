namespace Mapping.BLL.Models.Responses
{
    public class UserResponse
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public bool IsActive { get; set; }
        
        public UserRole Role { get; set; }
    }
}