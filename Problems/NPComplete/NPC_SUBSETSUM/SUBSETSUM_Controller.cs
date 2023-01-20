using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_SUBSETSUM.Verifiers;
using API.Problems.NPComplete.NPC_SUBSETSUM.Solvers;

using System.Text.Json;
using System.Text.Json.Serialization;
using API.Problems.NPComplete.NPC_SUBSETSUM.ReduceTo.NPC_KNAPSACK;

namespace API.Problems.NPComplete.NPC_SUBSETSUM;

[ApiController]
[Route("[controller]")]
public class SUBSETSUMGenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SUBSETSUM(), options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SUBSETSUM(), options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
public class FengController : ControllerBase {
  
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SUBSETSUM defaultSUBSETSUM = new SUBSETSUM();
        //SipserReduction reduction = new SipserReduction(defaultSAT3);
        FengReduction reduction = new FengReduction(defaultSUBSETSUM);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

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

[ApiController]
[Route("[controller]")]
public class SubSetSumVerifierController : ControllerBase {
    [HttpGet("info")]
    public String getGeneric() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SubsetSumVerifier verifier = new SubsetSumVerifier();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(verifier, options);
        return jsonString;
    }

    [HttpGet("verify")]
    public String solveInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SUBSETSUM subsetSum = new SUBSETSUM(problemInstance);
        SubsetSumVerifier verifier = new SubsetSumVerifier();

        bool response = verifier.verify(subsetSum,certificate);

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
public class SubsetSumBruteForceController : ControllerBase {

    // Return Generic Solver Class
    [HttpGet("info")]
    public String getGeneric() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SubsetSumBruteForce solver = new SubsetSumBruteForce();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(solver, options);
        return jsonString;
    }

    // Solve a instance given a certificate
    [HttpGet("solve")]
    public String solveInstance([FromQuery]string problemInstance) {
        // Implement solver here
        var options = new JsonSerializerOptions { WriteIndented = true };
        SUBSETSUM problem = new SUBSETSUM(problemInstance);
        string solution = problem.defaultSolver.solve(problem);
        
        string jsonString = JsonSerializer.Serialize(solution, options);
        return jsonString;
    }

}

