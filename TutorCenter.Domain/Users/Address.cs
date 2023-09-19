using TutorCenter.Domain.Common.Models;

namespace TutorCenter.Domain.Users;

public class Address : ValueObject
{
    public string City { get; set; } = string.Empty;
    public string CityId { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string DistrictId { get; set; } = string.Empty;
    public string Ward { get; set; } = string.Empty;
    public string WardId { get; set; } = string.Empty;
    public string WardLevel { get; set; } = string.Empty;
    public string? EnglishName { get; set; } = string.Empty;
    public override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
};

public class City
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class District
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string CityId { get; set; } = string.Empty;
    public string? EnglishName { get; set; } = string.Empty;
}

public class Ward
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string WardLevel { get; set; } = string.Empty;
    public string DistrictId { get; set; } = string.Empty;
}