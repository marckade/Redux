using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_ARCSET;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;

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

        Node node1 = new Node("1");
        Node node2 = new Node("2");
        Node node3 = new Node("3");
        Node node4 = new Node("4");
        Node node5 = new Node("5");

        List<Node> nl = new List<Node>();
        nl.Add(node1);
        nl.Add(node2);
        nl.Add(node3);
        nl.Add(node4);
        nl.Add(node5);
        Edge edge1 = new Edge(node1,node2);
        Edge edge2 = new Edge(node2,node3);
        Edge edge3 = new Edge(node3,node1);
        Edge edge4 = new Edge(node4,node1);
        Edge edge5 = new Edge(node5,node1);

        List<Edge> el = new List<Edge>();
        el.Add(edge1);
        el.Add(edge2);
        el.Add(edge3);
        el.Add(edge4);
        el.Add(edge5);
        int kTest = 1;
        //DirectedGraph testG = new DirectedGraph(nl,el,kTest);
        DirectedGraph testG = new DirectedGraph("{{1,2,3} : {(1,2) & (2,3) } : 1}");
        
       Console.Write(testG.adjToString());
        //testG.explore();
        
        string inputStr = "{{A,B,C} : {[A,B] & [B,C] & [C,A]} : 1}";
        //Console.WriteLine("Our input string: "+inputStr);
        UndirectedGraph testUG = new UndirectedGraph(inputStr);
        string testReduction = testUG.reduction();
        Console.WriteLine();
        Console.WriteLine(testG.DFS());
       // Console.Write(testReduction);

        //string testGStr = testG.ToString();
        //Console.WriteLine(testGStr);
       // Console.WriteLine(testG.adjToString(testG.getNodeList));
        var options = new JsonSerializerOptions { WriteIndented = true };
            
        //Console.WriteLine(testG.ToString());
        string jsonString = JsonSerializer.Serialize(new ARCSET(testG.ToString()), options);
        return jsonString;
    }
}


