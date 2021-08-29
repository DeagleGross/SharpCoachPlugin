using System;

public class TestMapSameTypeAndNameProperties
{
    public static UserResponse ToResponse{caret}(UserRequest request)
}

public class UserRequest
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public bool IsActive { get; set; }
    
    public long[] StockIds { get; set; }
}

public class UserResponse
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public bool IsActive { get; set; }
    
    public long[] StockIds { get; set; }
}