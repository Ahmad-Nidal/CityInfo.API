using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        return Ok(City.PointsOfInterest);
    }

    [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
    public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
    {
        // find the city
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }
        
        // find the PointOfInterest
        var PointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

        if (PointOfInterest == null)
        {
            return NotFound();
        }

        return Ok(PointOfInterest);

    }

    [HttpPost]
    public ActionResult<PointOfInterestDto> CreatePointOfInterest(
        int cityId,
        [FromBody] PointOfInterestForCreationDto pointOfInterest)
    {
        // find the city
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }

        var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(
            c => c.PointsOfInterest).Max(p => p.Id);

        var finalPointOfInterestDto = new PointOfInterestDto()
        {
            Id = ++maxPointOfInterestId,
            Name = pointOfInterest.Name,
            Description = pointOfInterest.Description
        };

        city.PointsOfInterest.Add(finalPointOfInterestDto);

        return CreatedAtRoute("GetPointOfInterest",
            new
            {
                cityId = cityId,
                pointOfInterestId = finalPointOfInterestDto.Id
            },
            finalPointOfInterestDto);
    }

    [HttpPut("{pointofinterestid}")]
    public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId,
        [FromBody] PointOfInterestForUpdateDto pointOfInterest)
    {
        // find the city
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }
        
        // find the PointOfInterest
        var PointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

        if (PointOfInterestFromStore == null)
        {
            return NotFound();
        }

        PointOfInterestFromStore.Name = pointOfInterest.Name;
        PointOfInterestFromStore.Description = pointOfInterest.Description;

        return NoContent();
    }

    [HttpPatch("{pointofinterestid}")]
    public ActionResult PartiallyUpdatePointOfInterest(
        int cityId, int pointOfInterestId,
        JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
    {
        // find the city
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }
        
        // find the PointOfInterest
        var PointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

        if (PointOfInterestFromStore == null)
        {
            return NotFound();
        }

        var pointOfInterestToPatch =
            new PointOfInterestForUpdateDto()
            {
                Name = PointOfInterestFromStore.Name,
                Description = PointOfInterestFromStore.Description
            };
        
        patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!TryValidateModel(pointOfInterestToPatch))
        {
            return BadRequest(ModelState);
        }

        PointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
        PointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

        return NoContent();
    }

    [HttpDelete("{pointofinterestid}")]
    public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
    {
        // find the city
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
        if (city == null)
        {
            return NotFound();
        }
        
        // find the PointOfInterest
        var PointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

        if (PointOfInterestFromStore == null)
        {
            return NotFound();
        }

        city.PointsOfInterest.Remove(PointOfInterestFromStore);

        return NoContent();
    }
}