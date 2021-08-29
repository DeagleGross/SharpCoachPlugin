using System;

public class MapClassToClassActionTest
{
    public static UserResponse ToResponse{caret}(UserRequest request)
}

public class UserRequest
{
    public User UserDto { get; set; }
    
    public class User
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
    }
}

public class UserResponse
{
    public User UserDto { get; set; }
    
    public class User
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
    }
}