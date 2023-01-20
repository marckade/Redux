using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_ExactCover.Verifiers;
using API.Problems.NPComplete.NPC_ExactCover.Solvers;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_ExactCover;

[ApiController]
[Route("[controller]")]
public class ExactCoverGenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new ExactCover(), options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance(string instance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new ExactCover(instance), options);
        return jsonString;
    }


}

[ApiController]
[Route("[controller]")]
public class ExactCoverVerifierController : ControllerBase {

    [HttpGet("info")]
    public String getGeneric() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        ExactCoverVerifier verifier = new ExactCoverVerifier();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(verifier, options);
        return jsonString;
    }
    [HttpGet("verify")]
    public String solveInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        ExactCover problem = new ExactCover(problemInstance);
        ExactCoverVerifier verifier = new ExactCoverVerifier();

        bool response = verifier.verify(problem,certificate);
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
public class ExactCoverBruteForceController : ControllerBase {

    // Return Generic Solver Class
    [HttpGet("info")]
    public String getGeneric() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        ExactCoverBruteForce solver = new ExactCoverBruteForce();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(solver, options);
        return jsonString;
    }

    // Solve a instance given a certificate
    [HttpGet("solve")]
    public String solveInstance([FromQuery]string problemInstance) {
        // Implement solver here
        var options = new JsonSerializerOptions { WriteIndented = true };
        ExactCover problem = new ExactCover(problemInstance);
        string solution = problem.defaultSolver.solve(problem);
        
        string jsonString = JsonSerializer.Serialize(solution, options);
        return jsonString;
    }

}