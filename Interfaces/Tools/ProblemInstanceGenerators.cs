using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace API.Interfaces.Tools;

public static class ProblemInstanceGenerators{
    public static string UndirectedGraphInstance(int n = 5, int k =- 1, int density = 50){
        Random random = new Random();
        List<int> nodes = new List<int>();
        List<KeyValuePair<int,int>> edges = new List<KeyValuePair<int,int>>();
        for(int i=0; i<n; i++){
            nodes.Add(i);
        }
        foreach(var nodeA in nodes){
            foreach(var nodeB in nodes){
                if(nodeA < nodeB){
                    int r = random.Next(100);
                    if(r<density){
                        edges.Add(new KeyValuePair<int, int>(nodeA,nodeB));
                    }
                }
            }
        }

        string nodesString = "";
        foreach(var node in nodes){
            nodesString += ","+node;
        }
        if(nodesString != "") nodesString = string.Format("{{{0}}}",nodesString.Substring(1));

        string edgesString = "";
        foreach(var edge in edges){
            edgesString += string.Format(",{{{0},{1}}}",edge.Key,edge.Value);
        }
        if(edgesString != "") edgesString = string.Format("{{{0}}}",edgesString.Substring(1));

        string kString = "";
        if(k>=0) kString += ","+k;

        string G = string.Format("{{{0},{1}{2}}}",nodesString,edgesString,kString);
        return G;
    }
    public static string DirectedGraphInstance(int n = 5, int k=-1, int density = 50){
        Random random = new Random();
        List<int> nodes = new List<int>();
        List<KeyValuePair<int,int>> edges = new List<KeyValuePair<int,int>>();
        for(int i=0; i<n; i++){
            nodes.Add(i);
        }
        foreach(var nodeA in nodes){
            foreach(var nodeB in nodes){
                if(nodeA != nodeB){
                    int r = random.Next(100);
                    if(r<density){
                        edges.Add(new KeyValuePair<int, int>(nodeA,nodeB));
                    }
                }
            }
        }

        string nodesString = "";
        foreach(var node in nodes){
            nodesString += ","+node;
        }
        if(nodesString != "") nodesString = string.Format("{{{0}}}",nodesString.Substring(1));

        string edgesString = "";
        foreach(var edge in edges){
            edgesString += string.Format(",({0},{1})",edge.Key,edge.Value);
        }
        if(edgesString != "") edgesString = string.Format("{{{0}}}",edgesString.Substring(1));

        string kString = "";
        if(k>=0) kString += ","+k;

        string G = string.Format("{{{0},{1}{2}}}",nodesString,edgesString,kString);
        return G;
    }

    public static string Sat3Instance(int n = 3, int c = 3){
        Random random = new Random();
        List<string> variables = new List<string>();
        List<string> clauses = new List<string>();
        for(int i=0; i<n; i++){
            variables.Add("x"+i);
        }
        for(int i=0; i<c; i++){
            List<string> literals = new List<string>();
            for(int j=0; j<3; j++){
                int truthValue = random.Next(2);
                if (truthValue == 0){
                    string literal = "!" +variables[random.Next(variables.Count)];
                    literals.Add(literal);
                } else {
                    string literal = variables[random.Next(variables.Count)];
                    literals.Add(literal);
                }
            }
            clauses.Add(string.Format("({0} | {1} | {2})",literals[0],literals[1],literals[2]));
        }
        string S = "";
        foreach(var clause in clauses){
            S += " %26 "+clause;
        }
        if(S.Length != 0) S = S.Substring(5);
        return S;
    }
};

[ApiController]
[Route("[controller]")]
public class ProblemGeneratorController : ControllerBase {

    [HttpGet("UndirectedGraph")]
    public String getUndirectedGraph([FromQuery] int n = 5, int density = 50 , int k = -1)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };

        string satInstance = ProblemInstanceGenerators.UndirectedGraphInstance(n,k,density);
        
        string jsonString = JsonSerializer.Serialize(satInstance, options);
        return jsonString;
    }

    [HttpGet("DirectedGraph")]
    public String getDirectedGraph([FromQuery] int n = 5, int density = 50 , int k = -1)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };

        string graphInstance = ProblemInstanceGenerators.DirectedGraphInstance(n,k,density);
        
        string jsonString = JsonSerializer.Serialize(graphInstance, options);
        return jsonString;
    }
    [HttpGet("Sat3")]
    public String getSat3([FromQuery] int n = 3, int c = 3)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };

        string graphInstance = ProblemInstanceGenerators.Sat3Instance(n,c);
        
        string jsonString = JsonSerializer.Serialize(graphInstance, options);
        return jsonString;
    }
}

