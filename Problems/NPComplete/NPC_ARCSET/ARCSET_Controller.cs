using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_ARCSET;
using API.Problems.NPComplete.NPC_VERTEXCOVER;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using API.Problems.NPComplete.NPC_ARCSET.Verifiers;
using API.Problems.NPComplete.NPC_ARCSET.Solvers;
using API.Problems.NPComplete.NPC_VERTEXCOVER.ReduceTo.NPC_ARCSET;
using API.Interfaces.Graphs;

namespace API.Problems.NPComplete.NPC_ARCSET;

[ApiController]
[Route("[controller]")]
public class ARCSETGenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new ARCSET(), options);
        return jsonString;
    }

    [HttpGet("instance")]
    public String getInstance([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new ARCSET(problemInstance), options);
        return jsonString;
    }
}

[ApiController]
[Route("[controller]")]
public class AlexArcsetVerifierController : ControllerBase {

    [HttpGet("info")]
    public String getInstance(){
        var options = new JsonSerializerOptions{WriteIndented = true};
        AlexArcsetVerifier verifier = new AlexArcsetVerifier();
        string jsonString = JsonSerializer.Serialize(verifier,options);
        return jsonString;
    }    

      [HttpGet("verify")]
    public String getInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        ARCSET ARCSETProblem = new ARCSET(problemInstance);
        AlexArcsetVerifier verifier = new AlexArcsetVerifier();
        Boolean response = verifier.verify(ARCSETProblem,certificate);
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
public class AlexNaiveSolverController : ControllerBase {

    [HttpGet("info")]
    //without params, just returns the solver.
    public String getDefault(){
        
        var options = new JsonSerializerOptions { WriteIndented = true };

        ARCSET ARCSETProblem = new ARCSET();
        AlexNaiveSolver solver = new AlexNaiveSolver();
        
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(solver, options);
        return jsonString;
    }

      [HttpGet("solve")]
     //With query.
    public String getInstance([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        ARCSET ARCSETProblem = new ARCSET(problemInstance);
        AlexNaiveSolver solver = new AlexNaiveSolver();


        string solvedInstance = solver.solve(ARCSETProblem);
        //Boolean response = verifier.verify(ARCSETProblem,certificate);
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(solvedInstance, options);
        return jsonString;
    }

}



[ApiController]
[Route("[controller]")]
public class ArcsetJsonPayloadController : ControllerBase {

      [HttpGet]
    public String getInstance([FromQuery]string listType) {
               // Console.WriteLine("RECEIVED REQUEST");

        var options = new JsonSerializerOptions { WriteIndented = true };
        ARCSET defaultArcset = new ARCSET();
        ArcsetGraph defaultGraph = defaultArcset.directedGraph;
        string jsonString = "";
        List<Edge> edgeList = defaultGraph.getEdgeList;
        List<Node> nodeList = defaultGraph.getNodeList;


                if(listType.Equals("nodes")){

                    jsonString = JsonSerializer.Serialize(nodeList,options);
                }
                else if(listType.Equals("edges")){
                    jsonString = JsonSerializer.Serialize(edgeList,options);
                }
                else{
                    jsonString = JsonSerializer.Serialize("BAD INPUT, choose edges or nodes for listType. You chose: "+listType,options);

                }

        //string jsonString = JsonSerializer.Serialize(totalString, options);
        return jsonString;
    }

}
    
[ApiController]
[Route("[controller]")]
public class ARCSETDevController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        ARCSET arcset = new ARCSET();
        ArcsetGraph arcGraph = arcset.directedGraph;
        String jsonString = arcGraph.toDotJson();
                //Console.WriteLine(jsonString);

        string arcRegStr = "{{a,b,c},{(a,b),(b,c)},10}";
        string uStr = "{{a,b,c},{{a,b},{b,c}},10}";
        string uStr2 = "{{a,b,c}:{{a,b} & {b,c}}:10}";

        GraphParser gParser = new GraphParser();
        List<Edge> arcList = gParser.getGraphEdgeList(arcRegStr);
        List<Edge> undGList = gParser.getGraphEdgeList(uStr);
        
        Console.WriteLine("directed:");
        foreach(Edge e in arcList){
            Console.Write(e.directedString()+" ");
        }
        Console.WriteLine();
        Console.WriteLine("undirected:");
       
        foreach(Edge e in undGList){
            
        Console.Write(e.undirectedString()+" ");
        }

        ArcsetGraph arcTest = new ArcsetGraph(arcRegStr,true);
        VertexCoverGraph uTest = new VertexCoverGraph(uStr,true);
        VertexCoverGraph uTest2 = new VertexCoverGraph(uStr2);
        //Console.WriteLine(uTest.ToString());
       // Console.WriteLine(uTest2.ToString());
       // string printString = JsonSerializer.Serialize(arcTest, options);
        string printString2 = JsonSerializer.Serialize(uTest, options);
        //string printString3 = JsonSerializer.Serialize(uTest2);

        //Console.WriteLine(printString);
       // Console.WriteLine(printString2);

        return printString2;
    }

    [HttpGet("instance")]
    public String getInstance([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        
        ARCSET arcset = new ARCSET(problemInstance);
        ArcsetGraph arcGraph = arcset.directedGraph;
        String jsonString = arcGraph.toDotJson();

        return jsonString;
    }
}

    
