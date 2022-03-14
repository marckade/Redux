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

    [HttpGet]
    public String getInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
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

      [HttpGet]
    public String getInstance([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        //VERTEXCOVER VCProblem = new VERTEXCOVER(problemInstance);
        VCSolverJanita solver = new VCSolverJanita();
        List<KeyValuePair<string, string>> solvedInstance = solver.Solve(problemInstance);
        //Boolean response = verifier.verify(ARCSETProblem,certificate);
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(solvedInstance, options);
        return jsonString;
    }


}


