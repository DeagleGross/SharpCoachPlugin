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
            Console.WriteLine("hello");
            return null;
        }
    }
}