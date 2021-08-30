namespace Mapping.BLL.Models.Requests
{
    public class UserRequest
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public bool IsActive { get; set; }
        
        public long[] StockIds { get; set; }
        
        public Stock OwnedStock { get; set; }

        public class Stock
        {
            public int Id { get; set; }
            
            public string Name { get; set; }
        }
    }
}