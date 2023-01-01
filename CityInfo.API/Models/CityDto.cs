namespace CityInfo.API.Models;

public class CityDto
{
    public int Id { get; set; }
    public string Name { get; set; }  = string.Empty;
    public string? Description { get; set; }

    public int NumberOfPointInterest
    {
        get
        {
            return PointOfInterest.Count();
        }
    }
    public IEnumerable<PointOfInterestDto> PointOfInterest { get; set; }
        = new List<PointOfInterestDto>();
}