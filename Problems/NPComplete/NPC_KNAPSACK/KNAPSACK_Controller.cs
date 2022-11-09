using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_KNAPSACK;
using System.Text.Json;
using API.Problems.NPComplete.NPC_KNAPSACK.Verifiers;
using API.Problems.NPComplete.NPC_KNAPSACK.Solvers;
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
    public String getInstance([FromQuery] string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new KNAPSACK(problemInstance), options);
        return jsonString;
    }



}

[ApiController]
[Route("[controller]")]
public class GarrettVerifierController : ControllerBase {
        [HttpGet("info")]
    public String getGeneric(){
        var options = new JsonSerializerOptions {WriteIndented = true};
        GarrettVerifier verifier = new GarrettVerifier();
        string jsonString  = JsonSerializer.Serialize(verifier, options);
        return jsonString; 
    }
    [HttpGet]
    public String getInstance([FromQuery]string certifcate, [FromQuery] string problemInstance){
        var options = new JsonSerializerOptions {WriteIndented = true};
        KNAPSACK KNAPSACKProblem = new KNAPSACK(problemInstance);
        GarrettVerifier verifier = new GarrettVerifier();

        Boolean response = verifier.verify(KNAPSACKProblem, certifcate);
        //send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }
}


[ApiController]
[Route("[controller]")]
public class GarrettKnapsackSolverController : ControllerBase {

    [HttpGet("info")]
    public String getGeneric(){
        var options = new JsonSerializerOptions {WriteIndented = true};
        GarrettKnapsackSolver solver = new GarrettKnapsackSolver();
        string jsonString  = JsonSerializer.Serialize(solver, options);
        return jsonString; 
    }

    [HttpGet("solve")]
    public String solvedInstance([FromQuery]string problemInstance) {
         
        var options = new JsonSerializerOptions { WriteIndented = true };
        KNAPSACK KNAPSACKProblem = new KNAPSACK(problemInstance);
        GarrettKnapsackSolver solver = new GarrettKnapsackSolver();
        string solvedInstance = solver.solve(KNAPSACKProblem);
      
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(solvedInstance, options);
        return jsonString;
    }


}
