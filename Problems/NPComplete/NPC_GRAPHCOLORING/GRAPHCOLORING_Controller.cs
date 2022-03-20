
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

    //[HttpGet("{certificate}/{problemInstance}")]
    [HttpGet]
    public String getInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
        //string certificate, string problemInstance

        Console.WriteLine("go to the route");
        // certificate = "(a:blue, b:red, c:green)";
        // problemInstance = "{ { {a,b,c} : {{a,b} & {b,a} & {b,c} }} : 3}";

        var options = new JsonSerializerOptions { WriteIndented = true };
        GRAPHCOLORING GRAPHCOLORINGProblem = new GRAPHCOLORING(problemInstance);
        IgbokwesSimple verifier = new IgbokwesSimple();

        Boolean response = verifier.verify(GRAPHCOLORINGProblem, certificate);
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
       
    }

}



[ApiController]
[Route("[controller]")]
public class IgbokweSolverController : ControllerBase {

      [HttpGet]
    public String getInstance([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        GRAPHCOLORING GRAPHCOLORINGProblem = new GRAPHCOLORING(problemInstance);
        IgbokweSolver solver = new IgbokweSolver();
       Tuple<Dictionary<string, string>, int> solvedInstance = solver.Solve(GRAPHCOLORINGProblem);
      
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(solvedInstance, options);
        return jsonString;
    }


}

