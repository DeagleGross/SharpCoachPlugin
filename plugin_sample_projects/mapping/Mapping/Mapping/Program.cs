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

        private Response Convert(Request request)
        {
            return new Response() {Id = request.Id};
        }
    }

    public class Request
    {
        public long Id { get; set; }
    }

    public class Response
    {
        public long Id { get; set; }
    }
}