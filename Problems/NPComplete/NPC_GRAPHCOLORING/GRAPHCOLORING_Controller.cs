
using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_GRAPHCOLORING;
using System.Text.Json;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.Verifiers;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.Solvers;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.ReduceTo.NPC_SAT;

namespace API.Problems.NPComplete.NPC_GRAPHCOLORING;


[ApiController]
[Route("[controller]")]
[Tags("Graph Coloring")]
#pragma warning disable CS1591
public class GRAPHCOLORINGGenericController : ControllerBase {
#pragma warning restore CS1591


///<summary>Returns a default Graph Coloring object</summary>

    [ProducesResponseType(typeof(GRAPHCOLORING), 200)]
    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new GRAPHCOLORING(), options);
        return jsonString;
    }

///<summary>Returns a Graph Coloring object created from a given instance </summary>
///<param name="problemInstance" example="{{a,b,c,d,e,f,g,h,i},{{a,b},{b,a},{b,c},{c,a},{a,c},{c,b},{a,d},{d,a},{d,e},{e,a},{a,e},{e,d},{a,f},{f,a},{f,g},{g,a},{a,g},{g,f},{a,h},{h,a},{h,i},{i,a},{a,i},{i,h}},3}">Graph Coloring problem instance string.</param>
///<response code="200">Returns GRAPHCOLORING problem object</response>

    [ProducesResponseType(typeof(GRAPHCOLORING), 200)]
    [HttpGet("instance")]
    public String getInstance([FromQuery] string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new GRAPHCOLORING(problemInstance), options);
        return jsonString;
    }

}



[ApiController]
[Route("[controller]")]
[Tags("Graph Coloring")]
#pragma warning disable CS1591
public class IgbokweVerifierController : ControllerBase {
#pragma warning restore CS1591


    //string testVerifyString = "{{a,b,c,d,e,f,g,h,i},{{a,b},{b,a},{b,c},{c,a},{a,c},{c,b},{a,d},{d,a},{d,e},{e,a},{a,e},{e,d},{a,f},{f,a},{f,g},{g,a},{a,g},{g,f},{a,h},{h,a},{h,i},{i,a},{a,i},{i,h}},3}";

///<summary>Returns a info about the Graph Coloring Igbokwe Verifier </summary>
///<response code="200">Returns IgbokweVerifier Object</response>

    [ProducesResponseType(typeof(IgbokweVerifier), 200)]
    [HttpGet("info")]
    public String getGeneric(){
        var options = new JsonSerializerOptions {WriteIndented = true};
        IgbokweVerifier verifier = new IgbokweVerifier();


        string jsonString = JsonSerializer.Serialize(verifier, options);
        return jsonString; 
    }

    //[HttpGet("{certificate}/{problemInstance}")]
///<summary>Verifies if a given certificate is a solution to a given Graph Coloring problem</summary>
///<param name="certificate" example="{(a:0,b:1,c:2,d:1,e:2,f:1,g:2,h:1,i:2):3}">certificate solution to Graph Coloring problem.</param>
///<param name="problemInstance" example="{{a,b,c,d,e,f,g,h,i},{{a,b},{b,a},{b,c},{c,a},{a,c},{c,b},{a,d},{d,a},{d,e},{e,a},{a,e},{e,d},{a,f},{f,a},{f,g},{g,a},{a,g},{g,f},{a,h},{h,a},{h,i},{i,a},{a,i},{i,h}},3}">Graph Coloring problem instance string.</param>
///<response code="200">Returns a boolean</response>
    
    [ProducesResponseType(typeof(Boolean), 200)]
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
[Tags("Graph Coloring")]
#pragma warning disable CS1591
public class DanielBrelazSolverController : ControllerBase {
#pragma warning restore CS1591


///<summary>Returns a info about the Graph Coloring Daniel Brelaz solver </summary>
///<response code="200">Returns DanielBrelazSolver solver Object</response>

    [ProducesResponseType(typeof(DanielBrelazSolver), 200)]
    [HttpGet("info")]
    public String getGeneric(){
        var options = new JsonSerializerOptions {WriteIndented = true};
        DanielBrelazSolver solver = new DanielBrelazSolver();


        string jsonString  = JsonSerializer.Serialize(solver, options);
        return jsonString; 
    }

///<summary>Returns a solution to a given  Graph Coloring problem instance </summary>
///<param name="problemInstance" example="{{a,b,c,d,e,f,g,h,i},{{a,b},{b,a},{b,c},{c,a},{a,c},{c,b},{a,d},{d,a},{d,e},{e,a},{a,e},{e,d},{a,f},{f,a},{f,g},{g,a},{a,g},{g,f},{a,h},{h,a},{h,i},{i,a},{a,i},{i,h}},3}"> Graph Coloring problem instance string.</param>
///<response code="200">Returns solution string </response>
    
    [ProducesResponseType(typeof(string), 200)]
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
[Tags("Graph Coloring")]
#pragma warning disable CS1591
public class KarpReduceSATController : ControllerBase {
#pragma warning restore CS1591

///<summary>Returns a a reduction object with info for Karps's Graph Coloring to SAT reduction </summary>
///<response code="200">Returns KarpReduceSAT reduction object</response>

    [ProducesResponseType(typeof(KarpReduceSAT), 200)]
    [HttpGet("info")]
    public String getInfo(){

        var options = new JsonSerializerOptions { WriteIndented = true };
        GRAPHCOLORING defaultGRAPHCOLORING = new GRAPHCOLORING();
        KarpReduceSAT reduction = new KarpReduceSAT(defaultGRAPHCOLORING);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

///<summary>Returns a reduction from Graph Coloring to AT based on the given Graph Coloring instance  </summary>
///<param name="problemInstance" example="{{a,b,c,d,e,f,g,h,i},{{a,b},{b,a},{b,c},{c,a},{a,c},{c,b},{a,d},{d,a},{d,e},{e,a},{a,e},{e,d},{a,f},{f,a},{f,g},{g,a},{a,g},{g,f},{a,h},{h,a},{h,i},{i,a},{a,i},{i,h}},3}">Graph Coloring problem instance string.</param>
///<response code="200">Returns Karps's Graph Coloring to SAT KarpReduceSAT reduction object</response>

    [ProducesResponseType(typeof(KarpReduceSAT), 200)]
    [HttpGet("reduce")]
    public String getReduce([FromQuery]string problemInstance){
         
        KarpReduceSAT reduction = new KarpReduceSAT(new GRAPHCOLORING(problemInstance));
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

}

