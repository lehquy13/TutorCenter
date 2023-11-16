using TutorCenter.Application.Contracts.Users;

namespace TutorCenter.Application.Contracts.Interfaces.Services;

public interface IAddressService
{
    AddressDto GetAddresses();
    List<CityDto> GetCities();
}