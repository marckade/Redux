using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_ExactCover;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_ExactCover;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
#pragma warning disable CS1591
public class ExactCoverGenericController : ControllerBase {
#pragma warning restore CS1591

///<summary>Returns a default Exact Cover object</summary>

    [ProducesResponseType(typeof(ExactCover), 200)]
    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new ExactCover(), options);
        return jsonString;
    }

///<summary>Returns a Exact Cover object created from a given instance </summary>{
///<param name="problemInstance" example="{{ (), (1 &amp; 3), (2 &amp; 3), (2 &amp; 4)} : {1,2,3,4} : {(1 &amp; 3), (2 &amp; 4)}}">Exact Cover problem instance string.</param>
///<response code="200">Returns ExactCover problem object</response>

    [ProducesResponseType(typeof(ExactCover), 200)]
    [HttpGet("instance")]
    public String getInstance(string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new ExactCover(problemInstance), options);
        return jsonString;
    }


}