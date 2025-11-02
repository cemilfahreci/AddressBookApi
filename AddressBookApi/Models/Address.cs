namespace AddressBookApi.Models;

public class Address
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? City { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}

public class CreateAddressRequest
{
    public string Name { get; set; } = string.Empty;
    public string? City { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}

public class UpdateAddressRequest
{
    public string? Name { get; set; }
    public string? City { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}

