using System;

public class MapModelsActionTestText
{
    public static UserResponse ToResponse{caret}(UserRequest request)
}

public class UserRequest
{
    public long Id { get; set; }
        
    public string Name { get; set; }
        
    public bool IsActive { get; set; }
        
    public UserRole Role { get; set; }
    
    public int RoleEnumNumeric { get; set; }
        
    public long[] ParentIds { get; set; }
        
    public Stock[] Stocks { get; set; }

    public class Stock
    {
        public long Id { get; set; }
            
        public string Name { get; set; }
    }
    
    public enum UserRole
    {
        User,
        Admin,
        Owner
    }
}

public class UserResponse
{
    public long Id { get; set; }
        
    public string Name { get; set; }
        
    public bool IsActive { get; set; }
        
    public UserRole Role { get; set; }
    
    public RoleEnum RoleEnumNumeric { get; set; }
        
    public long[] ParentIds { get; set; }
        
    public Stock[] Stocks { get; set; }

    public class Stock
    {
        public long Id { get; set; }
            
        public string Name { get; set; }
    }

    public Enum RoleEnum
    {
        A,
        B,
        C
    }
    
    public enum UserRole
    {
        User,
        Admin,
        Owner
    }
}