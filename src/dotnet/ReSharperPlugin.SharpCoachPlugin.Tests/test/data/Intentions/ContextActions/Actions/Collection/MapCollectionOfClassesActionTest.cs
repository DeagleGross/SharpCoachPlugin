using System;

public class MapCollectionOfClassesActionTest
{
    public static UserResponse ToResponse{caret}(UserRequest request)
}

public class UserRequest
{
    public User[] Users { get; set; }
    
    public class User
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
    }
}

public class UserResponse
{
    public User[] Users { get; set; }
    
    public class User
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
    }
}