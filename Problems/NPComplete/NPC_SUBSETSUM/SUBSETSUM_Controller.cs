using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_SUBSETSUM;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Problems.NPComplete.NPC_SUBSETSUM.ReduceTo.NPC_KNAPSACK;

namespace API.Problems.NPComplete.NPC_SUBSETSUM;

[ApiController]
[Route("[controller]")]
[Tags("Subset Sum")]
[ApiExplorerSettings(IgnoreApi = true)]

#pragma warning disable CS1591

public class SUBSETSUMGenericController : ControllerBase {
#pragma warning restore CS1591


///<summary>Returns a default Subset Sum problem object</summary>

    [ProducesResponseType(typeof(SUBSETSUM), 200)]
    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SUBSETSUM(), options);
        return jsonString;
    }

///<summary>Returns a Subset Sum problem object created from a given instance </summary>
///<param name="problemInstance" example="{{1,7,12,15} : 28}">Subset Sum problem instance string.</param>
///<response code="200">Returns SUBSETSUM problem object</response>

    [ProducesResponseType(typeof(SUBSETSUM), 200)]
    [HttpGet("instance")]
    public String getInstance(string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SUBSETSUM(problemInstance), options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
[Tags("Subset Sum")]
[ApiExplorerSettings(IgnoreApi = true)]

#pragma warning disable CS1591

public class FengController : ControllerBase {
#pragma warning restore CS1591

  
///<summary>Returns a a reduction object with info for Feng's Subset Sum to Knapsack reduction </summary>
///<response code="200">Returns FengReduction bbject</response>

    [ProducesResponseType(typeof(FengReduction), 200)]
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SUBSETSUM defaultSUBSETSUM = new SUBSETSUM();
        //SipserReduction reduction = new SipserReduction(defaultSAT3);
        FengReduction reduction = new FengReduction(defaultSUBSETSUM);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

///<summary>Returns a reduction from Subset Sum to Knapsack based on the given Subset Sum instance  </summary>
///<param name="problemInstance" example="{{1,7,12,15} : 28}">Subset Sum problem instance string.</param>
///<response code="200">Returns Fengs's Subset Sum to Knapsack FengReduction object</response>

    [ProducesResponseType(typeof(FengReduction), 200)]
    [HttpGet("reduce")]
    public String getReduce([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SUBSETSUM defaultSUBSETSUM = new SUBSETSUM(problemInstance);
        FengReduction reduction = new FengReduction(defaultSUBSETSUM);
        string jsonString = JsonSerializer.Serialize(reduction, options);
       // Console.WriteLine("reduced form is: "+ jsonString);
        return jsonString;
    }
}


