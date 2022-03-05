using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_ARCSET;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using API.Problems.NPComplete.NPC_ARCSET.Verifiers;

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

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new ARCSET(), options);
        return jsonString;
    }
}
public class GraphTestController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };

    DirectedGraph testG = new DirectedGraph();

        Console.WriteLine(testG.ToString());
        string jsonString = JsonSerializer.Serialize(new ARCSET(testG.ToString()), options);
        return jsonString;
    }

        [HttpGet("{instance}")]

        //ALEX NOTE: edit this method to get custom ARCSET instances.
        public String getInstance() {
      //  string inputStrUndirected = "{{A,B,C} : {[A,B] & [B,C] & [C,A]} : 1}";
        string inputStrDirected = "{{1,2,3,4} : {(1,2) & (2,3) & (3,4) & (4,1) & (1,4)} : 1}";
        string testCertificate = "{{}} : {(1,4)} : 0}"; //this is a graph with 0 nodes and one edge, which doesn't make sense logically but is needed for our getEdges string parser of ARCSET/Directed Graph
        //testCertificate = String.Empty;
        ARCSET testArc = new ARCSET(inputStrDirected);
        GenericVerifier defaultVer = testArc.defaultVerifier;
        
        Console.WriteLine(defaultVer.verify(testArc,testCertificate));
        //DirectedGraph testG = new DirectedGraph(inputStringDirected);
        
       //Console.Write(testG.adjToString());
        //testG.explore();
        
        //Console.WriteLine("Our input string: "+inputStr);
      //  UndirectedGraph testUG = new UndirectedGraph(inputStr);
        //string testReduction = testUG.reduction();
       // Console.WriteLine();
        //Console.WriteLine(testG.DFS());
       // Console.Write(testReduction);

        //string testGStr = testG.ToString();
        //Console.WriteLine(testGStr);
       // Console.WriteLine(testG.adjToString(testG.getNodeList));
        var options = new JsonSerializerOptions { WriteIndented = true };
            
        //Console.WriteLine(testG.ToString());
        string jsonString = JsonSerializer.Serialize(testArc, options);
        return jsonString;
    }
}


