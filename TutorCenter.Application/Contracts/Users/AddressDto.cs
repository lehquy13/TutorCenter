namespace TutorCenter.Application.Contracts.Users;

public record AddressDto(List<CityDto> Cities);

public sealed class CityDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public List<DistrictDto> Districts { get; set; } = new();
}

public sealed class DistrictDto
{
    public string Id { get; set; } = string.Empty;
    public List<WardDto> Wards { get; set; } = new();
    public string Name { get; set; } = string.Empty;
    public string CityId { get; set; } = string.Empty;

    public string? EnglishName { get; set; } = string.Empty;
}

public sealed class WardDto
{
    public string Id { get; set; } = string.Empty;
    public string DistrictId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string WardLevel { get; set; } = string.Empty;
}