namespace Mapping.BLL.Models.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public bool IsActive { get; set; }
        
        public long[] StockIds { get; set; }
    }
}