using AddressBookApi.Models;

namespace AddressBookApi.Services;

public class AddressService : IAddressService
{
    private readonly List<Address> _addresses = new();
    private int _nextId = 1;

    public AddressService()
    {
        SeedData();
    }

    private void SeedData()
    {
        _addresses.Add(new Address
        {
            Id = _nextId++,
            Name = "Cemil Fahreci",
            Email = "cemilfahreci@gmail.com",
            Phone = "5370181889",
            City = "İzmir"
        });
    }

    public Task<List<Address>> GetAllAddressesAsync()
    {
        return Task.FromResult(_addresses.ToList());
    }

    public Task<Address?> GetAddressByIdAsync(int id)
    {
        var address = _addresses.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(address);
    }

    public Task<Address> CreateAddressAsync(CreateAddressRequest request)
    {
        var address = new Address
        {
            Id = _nextId++,
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            City = request.City
        };

        _addresses.Add(address);
        return Task.FromResult(address);
    }

    public Task<Address?> UpdateAddressAsync(int id, UpdateAddressRequest request)
    {
        var address = _addresses.FirstOrDefault(a => a.Id == id);
        if (address == null)
        {
            return Task.FromResult<Address?>(null);
        }

        if (!string.IsNullOrWhiteSpace(request.Name))
            address.Name = request.Name;
        
        if (request.Email != null)
            address.Email = request.Email;
        
        if (request.Phone != null)
            address.Phone = request.Phone;
        
        if (request.City != null)
            address.City = request.City;

        return Task.FromResult<Address?>(address);
    }

    public Task<bool> DeleteAddressAsync(int id)
    {
        var address = _addresses.FirstOrDefault(a => a.Id == id);
        if (address == null)
        {
            return Task.FromResult(false);
        }

        _addresses.Remove(address);
        return Task.FromResult(true);
    }

    public Task<List<Address>> SearchAddressesAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return GetAllAddressesAsync();
        }

        var term = searchTerm.ToLowerInvariant();
        var results = _addresses.Where(a =>
            a.Name.ToLowerInvariant().Contains(term) ||
            (a.Email != null && a.Email.ToLowerInvariant().Contains(term)) ||
            (a.Phone != null && a.Phone.Contains(term)) ||
            (a.City != null && a.City.ToLowerInvariant().Contains(term))
        ).ToList();

        return Task.FromResult(results);
    }
}

