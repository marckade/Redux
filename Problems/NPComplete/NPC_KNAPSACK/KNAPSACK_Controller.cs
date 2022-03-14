using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_KNAPSACK;
using System.Text.Json;
using API.Problems.NPComplete.NPC_KNAPSACK.Verifiers;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_KNAPSACK;

[ApiController]
[Route("[controller]")]
public class KNAPSACKGenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new KNAPSACK(), options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new KNAPSACK(), options);
        return jsonString;
    }



}

[ApiController]
[Route("[controller]")]
public class GarrettVerifierController : ControllerBase {
    [HttpGet]
    public String getInstance([FromQuery]string certifcate, [FromQuery] string problemInstance){
        var options = new JsonSerializerOptions {WriteIndented = true};
        KNAPSACK kNAPSACKProblem = new KNAPSACK(problemInstance);
        GarrettsSimple verifier = new GarrettsSimple();

        Boolean response = verifier.verify(kNAPSACKProblem, certifcate);
        //send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }
}
