using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_3DM;
using API.Problems.NPComplete.NPC_3DM.Verifiers;
using API.Problems.NPComplete.NPC_3DM.Solvers;


using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_3DM;

[ApiController]
[Route("[controller]")]
public class THREE_DMGenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new THREE_DM(), options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new THREE_DM(), options);
        return jsonString;
    }
}

[ApiController]
[Route("[controller]")]
public class GenericVerifier3DMController : ControllerBase {
    [HttpGet("info")]
    public String getGeneric() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        GenericVerifier3DM verifier = new GenericVerifier3DM();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(verifier, options);
        return jsonString;
    }

    [HttpGet("solve")]
    public String solveInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        THREE_DM THREE_DM_PROBLEM = new THREE_DM(problemInstance);
        GenericVerifier3DM verifier = new GenericVerifier3DM();

        Boolean response = verifier.verify(THREE_DM_PROBLEM,certificate);
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
public class HurkensShriverSolverController : ControllerBase {

    // Return Generic Solver Class
    [HttpGet("info")]
    public String getGeneric() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        HurkensShrijver solver = new HurkensShrijver();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(solver, options);
        return jsonString;
    }

    // Solve a instance given a certificate
    [HttpGet("solve")]
    public String solveInstance([FromQuery]string problemInstance) {
        // Implement solver here
        var options = new JsonSerializerOptions { WriteIndented = true };
        THREE_DM problem = new THREE_DM(problemInstance);
        List<List<string>> solution = problem.defaultSolver.solve(problem);

        string jsonString = JsonSerializer.Serialize(solution, options);
        return jsonString;
    }

}

