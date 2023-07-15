using System;

namespace Library.Models.Mapping 
{
    public class ExtensionMethodTest
    {
        static UserResponse ToResponse{caret}(UserRequest request)
        {
            int a = 1;
            if (a == 1) { Console.WriteLine("hello"); }
            return new UserResponse();
        }
    }

    public class UserRequest
    {
        public int Prop { get; set; }
    }

    public class UserResponse
    {
        public int Prop { get; set; }
    }
}