using AddressBookApi.Models;
using AddressBookApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AddressBookApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AddressesController : ControllerBase
{
    private readonly IAddressService _addressService;
    private readonly ILogger<AddressesController> _logger;

    public AddressesController(IAddressService addressService, ILogger<AddressesController> logger)
    {
        _addressService = addressService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Address>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Address>>> GetAllAddresses()
    {
        var addresses = await _addressService.GetAllAddressesAsync();
        return Ok(addresses);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Address), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Address>> GetAddressById(int id)
    {
        var address = await _addressService.GetAddressByIdAsync(id);
        
        if (address == null)
        {
            return NotFound(new { message = $"ID {id} ile adres bulunamadı." });
        }

        return Ok(address);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Address), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Address>> CreateAddress([FromBody] CreateAddressRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { message = "Name alanı zorunludur." });
        }

        var address = await _addressService.CreateAddressAsync(request);
        return CreatedAtAction(nameof(GetAddressById), new { id = address.Id }, address);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Address), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Address>> UpdateAddress(int id, [FromBody] UpdateAddressRequest request)
    {
        var address = await _addressService.UpdateAddressAsync(id, request);
        
        if (address == null)
        {
            return NotFound(new { message = $"ID {id} ile adres bulunamadı." });
        }

        return Ok(address);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(void))]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        var deleted = await _addressService.DeleteAddressAsync(id);
        
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(List<Address>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Address>>> SearchAddresses([FromQuery] string q)
    {
        var addresses = await _addressService.SearchAddressesAsync(q ?? string.Empty);
        return Ok(addresses);
    }
}

