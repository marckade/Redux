
using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_GRAPHCOLORING;
using System.Text.Json;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.Verifiers;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.Solvers;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.ReduceTo.NPC_SAT;

namespace API.Problems.NPComplete.NPC_GRAPHCOLORING;


[ApiController]
[Route("[controller]")]
public class GRAPHCOLORINGGenericController : ControllerBase {

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new GRAPHCOLORING(), options);
        return jsonString;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("{instance}")]
    public String getInstance([FromQuery] string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new GRAPHCOLORING(problemInstance), options);
        return jsonString;
    }

}



[ApiController]
[Route("[controller]")]
public class IgbokweVerifierController : ControllerBase {

    //string testVerifyString = "{{a,b,c,d,e,f,g,h,i},{{a,b},{b,a},{b,c},{c,a},{a,c},{c,b},{a,d},{d,a},{d,e},{e,a},{a,e},{e,d},{a,f},{f,a},{f,g},{g,a},{a,g},{g,f},{a,h},{h,a},{h,i},{i,a},{a,i},{i,h}},3}";

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("info")]
    public String getGeneric(){
        var options = new JsonSerializerOptions {WriteIndented = true};
        IgbokweVerifier verifier = new IgbokweVerifier();


        string jsonString = JsonSerializer.Serialize(verifier, options);
        return jsonString; 
    }

    //[HttpGet("{certificate}/{problemInstance}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("verify")]
    public String getInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {

        // Example  certificate = "(a:0, b:1, c:2, d:1, e:2, f:1, g:2, h:1, i:2)";
        // Example newCertificate = {(a:0, b:1, c:2, d:1, e:2, f:1, g:2, h:1, i:2),3}
        // Example problemInstance = "{{a,b,c,d,e,f,g,h,i},{{a,b},{b,a},{b,c},{c,a},{a,c},{c,b},{a,d},{d,a},{d,e},{e,a},{a,e},{e,d},{a,f},{f,a},{f,g},{g,a},{a,g},{g,f},{a,h},{h,a},{h,i},{i,a},{a,i},{i,h}},3}";

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

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("info")]
    public String getGeneric(){
        var options = new JsonSerializerOptions {WriteIndented = true};
        DanielBrelazSolver solver = new DanielBrelazSolver();


        string jsonString  = JsonSerializer.Serialize(solver, options);
        return jsonString; 
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("solve")]
    public String solvedInstance([FromQuery]string problemInstance) {
         
        var options = new JsonSerializerOptions { WriteIndented = true };
        GRAPHCOLORING GRAPHCOLORINGProblem = new GRAPHCOLORING(problemInstance);
        DanielBrelazSolver solver = new DanielBrelazSolver();
        string solvedInstance = solver.solve(GRAPHCOLORINGProblem);
      
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(solvedInstance, options);
        return jsonString;
    }


}


[ApiController]
[Route("[controller]")]

public class KarpReduceSATController : ControllerBase {

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("info")]
    public String getInfo(){

        var options = new JsonSerializerOptions { WriteIndented = true };
        GRAPHCOLORING defaultGRAPHCOLORING = new GRAPHCOLORING();
        KarpReduceSAT reduction = new KarpReduceSAT(defaultGRAPHCOLORING);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }





    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("reduce")]
    public String getReduce([FromQuery]string problemInstance){
         
        KarpReduceSAT reduction = new KarpReduceSAT(new GRAPHCOLORING(problemInstance));
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

}

