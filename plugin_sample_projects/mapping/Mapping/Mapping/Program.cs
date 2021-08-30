using System;
using Mapping.BLL.Models.Requests;
using Mapping.BLL.Models.Responses;

namespace Mapping
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static UserResponse Convert(UserRequest request)
        {
            return new UserResponse()
            {
                Id = request.Id,
                Name = request.Name,
                IsActive = request.IsActive,
                StockIds = request.StockIds,
                OwnedStock = new UserResponse.Stock() { Id = request.OwnedStock.Id, Name = request.OwnedStock.Name }
            };
        }
    }
}