using CityInfo.API.Models;

namespace CityInfo.API;

public class CitiesDataStore
{
    public List<CityDto> Cities { get; set; }
    public static CitiesDataStore Current { get; set; } = new CitiesDataStore();

    public CitiesDataStore()
    {
        // init dummy data 
        Cities = new List<CityDto>()
        {
            new CityDto()
            {
                Id = 1, Name = "Amman", Description = "Lorem",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 1, Name = "POI1"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 2, Name = "POI2"
                    }
                }
            },
            new CityDto()
            {
                Id = 2, Name = "Zarqa", Description = "Lorem ipson",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 3, Name = "POI3"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 4, Name = "POI2"
                    }
                }
            }
        };
    }
}