using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_DM3;
using API.Problems.NPComplete.NPC_DM3.Verifiers;
using API.Problems.NPComplete.NPC_DM3.Solvers;


using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_3DM;

[ApiController]
[Route("[controller]")]
public class DM3GenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new DM3(), options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new DM3(problemInstance), options);
        return jsonString;
    }
}

[ApiController]
[Route("[controller]")]
public class GenericVerifierDM3Controller : ControllerBase {
    [HttpGet("info")]
    public String getGeneric() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        GenericVerifierDM3 verifier = new GenericVerifierDM3();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(verifier, options);
        return jsonString;
    }

    [HttpGet("solve")]
    public String solveInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        DM3 DM3_PROBLEM = new DM3(problemInstance);
        GenericVerifierDM3 verifier = new GenericVerifierDM3();

        Boolean response = verifier.verify(DM3_PROBLEM,certificate);
        string responseString;
        if(response){
            responseString = "True";
        }
        else{responseString = "False";}
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(responseString, options);
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
        DM3 problem = new DM3(problemInstance);
        List<List<string>> solution = problem.defaultSolver.solve(problem);

        string solutionString = string.Empty;
        foreach(var list in solution){
            solutionString += "{" + list[0]+", "+list[1]+", "+list[2]+"}";
                
        }

        string jsonString = JsonSerializer.Serialize(solutionString, options);
        return jsonString;
    }

}

