using AddressBookApi.Models;

namespace AddressBookApi.Services;

public interface IAddressService
{
    Task<List<Address>> GetAllAddressesAsync();
    Task<Address?> GetAddressByIdAsync(int id);
    Task<Address> CreateAddressAsync(CreateAddressRequest request);
    Task<Address?> UpdateAddressAsync(int id, UpdateAddressRequest request);
    Task<bool> DeleteAddressAsync(int id);
    Task<List<Address>> SearchAddressesAsync(string searchTerm);
}

