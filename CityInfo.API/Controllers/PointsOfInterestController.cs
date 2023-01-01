using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/cities/{cityId}/pointsofinterest")]
public class PointsOfInterestController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointOfInterest(int cityId)
    {
        // find the city
        var City = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (City == null)
        {
            return NotFound();
        }

        return Ok(City.PointOfInterest);
    }

    [HttpGet("{pointofinterestid}")]
    public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
    {
        // find the city
        var City = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (City == null)
        {
            return NotFound();
        }
        
        // find the PointOfInterest
        var PointOfInterest = City.PointOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

        if (PointOfInterest == null)
        {
            return NotFound();
        }

        return Ok(PointOfInterest);

    }
}