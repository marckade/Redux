using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_VERTEXCOVER;
using API.Problems.NPComplete.NPC_ARCSET;
using API.Problems.NPComplete.NPC_VERTEXCOVER.Verifiers;
using API.Problems.NPComplete.NPC_VERTEXCOVER.Solvers;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Problems.NPComplete.NPC_VERTEXCOVER.ReduceTo.NPC_ARCSET;
using API.Interfaces.JSON_Objects.API_Solution;
using API.Interfaces.JSON_Objects.API_UndirectedGraphJSON;
using System.Collections;


namespace API.Problems.NPComplete.NPC_VERTEXCOVER;

[ApiController]
[Route("[controller]")]
public class testController : ControllerBase {
    
    public String test() {
        VERTEXCOVER testObj = new VERTEXCOVER();

        if (testObj.instance == null) {
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
    public String getInstance([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new VERTEXCOVER(problemInstance), options);
        return jsonString;
    }

    [HttpGet("visualize")]
    public String getVisualization([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        VERTEXCOVER vCov = new VERTEXCOVER(problemInstance);
        VertexCoverGraph vGraph = vCov.VCAsGraph;
        API_UndirectedGraphJSON apiGraph = new API_UndirectedGraphJSON(vGraph.getNodeList, vGraph.getEdgeList);
        string jsonString = JsonSerializer.Serialize(apiGraph, options);
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

    [HttpGet("verify")]
    public String verifyInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
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
        ArrayList testDataArr = new ArrayList();
        testDataArr.Add("DATA ARRAYLIST");
        API_Solution api_instance = new API_Solution("HELLO WORLD", testDataArr);
        string jsonString = JsonSerializer.Serialize(api_instance, options);

     return jsonString;

    }

    [HttpGet("solve")]
    public String solveInstance([FromQuery]string problemInstance){
        var options = new JsonSerializerOptions { WriteIndented = true };
        VERTEXCOVER problem = new VERTEXCOVER(problemInstance);
        List<string> solvedInstance = problem.defaultSolver.Solve(problem);

        string undirectedSetString = "{";
        foreach(string nodeStr in solvedInstance){
            undirectedSetString = undirectedSetString + nodeStr + ",";
        }
        undirectedSetString = undirectedSetString.TrimEnd(',');
        undirectedSetString = undirectedSetString + "}";
        // List<KeyValuePair<string, string>> solvedInstance = problem.defaultSolver.Solve(problem);
        // string stringVC = "{";
        // stringVC += Environment.NewLine;
        // foreach (KeyValuePair<string, string> keyValue in solvedInstance)
        // {
        //     string key = keyValue.Key;
        //     string value = keyValue.Value;    
        //     stringVC += "{Key: ";
        //     stringVC += key;
        //     stringVC += ", Value: ";
        //     stringVC += value;
        //     stringVC += "}, "; 
        //     stringVC += Environment.NewLine;
        // } 
        // stringVC += "}";

        // Console.Write(stringVC);



        string jsonString = JsonSerializer.Serialize(undirectedSetString, options);
        return jsonString;
    }

}


[ApiController]
[Route("[controller]")]
public class LawlerKarpController : ControllerBase {

      [HttpGet("info")] // url parameter

      public String getInfo(){
            var options = new JsonSerializerOptions { WriteIndented = true };
            LawlerKarp reduction = new LawlerKarp();
    
            String jsonString = JsonSerializer.Serialize(reduction,options);
            return jsonString;
      }

    
      [HttpGet("reduce")]
    public String getReduce([FromQuery]string problemInstance) {
        Console.Write("VertexCover controller getReduce:");
        Console.Write(problemInstance);
        //from query is a query parameter

        Console.WriteLine(problemInstance);
        var options = new JsonSerializerOptions { WriteIndented = true };
        //UndirectedGraph UG = new UndirectedGraph(problemInstance);
        //string reduction = UG.reduction();
        //Boolean response = verifier.verify(ARCSETProblem,certificate);
        // Send back to API user
        Console.Write(problemInstance);
        VERTEXCOVER vCover = new VERTEXCOVER(problemInstance);
        LawlerKarp reduction = new LawlerKarp(vCover);
       // ARCSET reducedArcset = reduction.reduce();
        //string reducedStr = reducedArcset.instance;

        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;

    }

}

