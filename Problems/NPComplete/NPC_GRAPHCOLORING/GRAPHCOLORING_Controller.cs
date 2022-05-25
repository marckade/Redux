
using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_GRAPHCOLORING;
using System.Text.Json;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.Verifiers;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.Solvers;

namespace API.Problems.NPComplete.NPC_GRAPHCOLORING;


[ApiController]
[Route("[controller]")]
public class GRAPHCOLORINGGenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new GRAPHCOLORING(), options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new GRAPHCOLORING(), options);
        return jsonString;
    }

}



[ApiController]
[Route("[controller]")]
public class IgbokweVerifierController : ControllerBase {

    [HttpGet("info")]
    public String getGeneric(){
        var options = new JsonSerializerOptions {WriteIndented = true};
        IgbokweVerifier verifier = new IgbokweVerifier();


        string jsonString  = JsonSerializer.Serialize(verifier, options);
        return jsonString; 
    }

    //[HttpGet("{certificate}/{problemInstance}")]
    [HttpGet("verify")]
    public String getInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {

        // Example  certificate = "(a:blue, b:red, c:green)";
        // Example problemInstance = "{ { {a,b,c} : {{a,b} & {b,a} & {b,c} }} : 3}";

        var options = new JsonSerializerOptions { WriteIndented = true };
        GRAPHCOLORING GRAPHCOLORINGProblem = new GRAPHCOLORING(problemInstance);
        IgbokweVerifier verifier = new IgbokweVerifier();

        Boolean response = verifier.verify(GRAPHCOLORINGProblem, certificate);
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
       
    }

}



[ApiController]
[Route("[controller]")]
public class DanielBrelazSolverController : ControllerBase {

    [HttpGet("info")]
    public String getGeneric(){
        var options = new JsonSerializerOptions {WriteIndented = true};
        DanielBrelazSolver solver = new DanielBrelazSolver();


        string jsonString  = JsonSerializer.Serialize(solver, options);
        return jsonString; 
    }

    [HttpGet("solve")]
    public String solvedInstance([FromQuery]string problemInstance) {
         //Example problemInstance = "{ { {a,b,c} : {{a,b} & {b,a} & {b,c} }} : 3}";
        var options = new JsonSerializerOptions { WriteIndented = true };
        GRAPHCOLORING GRAPHCOLORINGProblem = new GRAPHCOLORING(problemInstance);
        DanielBrelazSolver solver = new DanielBrelazSolver();
        string solvedInstance = solver.Solve(GRAPHCOLORINGProblem);
      
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(solvedInstance, options);
        return jsonString;
    }


}

