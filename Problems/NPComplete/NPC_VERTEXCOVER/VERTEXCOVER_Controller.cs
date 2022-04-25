using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_VERTEXCOVER;
using API.Problems.NPComplete.NPC_VERTEXCOVER.Verifiers;
using API.Problems.NPComplete.NPC_VERTEXCOVER.Solvers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_VERTEXCOVER;

[ApiController]
[Route("[controller]")]
public class testController : ControllerBase {
    
    public String test() {
        VERTEXCOVER testObj = new VERTEXCOVER();

        if (testObj.Gk == null) {
            return testObj.defaultInstance;
        }
        else {
            return "REALLY? API!";
        }
    }
}

[ApiController]
[Route("[controller]")]
public class VERTEXCOVERGenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        // VERTEXCOVER v = new VERTEXCOVER();
        // Console.Write(v.defaultSolver.Solve("{{a,b,c,d,e,f,g} : {(a,b) & (a,c) & (c,d) & (c,e) & (d,f) & (e,f) & (e,g)}}"));
        string jsonString = JsonSerializer.Serialize(new VERTEXCOVER(), options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new VERTEXCOVER(), options);
        return jsonString;
    }


}

[ApiController]
[Route("[controller]")]
public class VCVerifierController : ControllerBase {

    [HttpGet("info")]
    public String getGeneric() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        VCVerifierJanita verifier = new VCVerifierJanita();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(verifier, options);
        return jsonString;
    }

    [HttpGet("solve")]
    public String solveInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        VERTEXCOVER VCProblem = new VERTEXCOVER(problemInstance);
        VCVerifierJanita verifier = new VCVerifierJanita();

        Boolean response = verifier.Verify(VCProblem,certificate);
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
public class testInstanceController : ControllerBase {

    [HttpGet]
    public String getSingleInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        
        string returnString = certificate + problemInstance;
        return returnString;
    }
    
}


[ApiController]
[Route("[controller]")]
public class VCSolverController : ControllerBase {

    [HttpGet("info")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        VCSolverJanita solver = new VCSolverJanita();
        
        string jsonString = JsonSerializer.Serialize(solver, options);
        return jsonString;
        
    }

    [HttpGet("solve")]
    public String solveInstance([FromQuery]string problemInstance){
        var options = new JsonSerializerOptions { WriteIndented = true };
        VERTEXCOVER problem = new VERTEXCOVER(problemInstance);
        List<KeyValuePair<string, string>> solvedInstance = problem.defaultSolver.Solve(problemInstance);
        string jsonString = JsonSerializer.Serialize(solvedInstance, options);
        return jsonString;
    }


}


