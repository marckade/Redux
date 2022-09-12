using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_CLIQUE;
using API.Problems.NPComplete.NPC_CLIQUE.Solvers;
using API.Problems.NPComplete.NPC_CLIQUE.ReduceTo.NPC_VertexCover;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Interfaces.JSON_Objects.Graphs;
using API.Problems.NPComplete.NPC_VERTEXCOVER;
using API.Problems.NPComplete.NPC_CLIQUE.Inherited;
using API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_CLIQUE;
using API.Problems.NPComplete.NPC_CLIQUE.Verifiers;
using API.Problems.NPComplete.NPC_CLIQUE.Solvers;
using API.Interfaces.Graphs.GraphParser;


namespace API.Problems.NPComplete.NPC_CLIQUE;

[ApiController]
[Route("[controller]")]
public class CLIQUEGenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new CLIQUE(), options);
        return jsonString;
    }

    [HttpGet("instance")]
    public String getDefault([FromQuery] string problemInstance)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };

        CLIQUE devClique = new CLIQUE(problemInstance);
        
        string jsonString = JsonSerializer.Serialize(devClique, options);
        return jsonString;
    }

      [HttpGet("visualize")]
    public String getVisualization([FromQuery] string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        CLIQUE clique = new CLIQUE(problemInstance);
        CliqueGraph cGraph = clique.cliqueAsGraph;
        API_UndirectedGraphJSON apiFormat = new API_UndirectedGraphJSON(cGraph.getNodeList, cGraph.getEdgeList);

        string jsonString = JsonSerializer.Serialize(apiFormat, options);
        return jsonString;
    }

     [HttpGet("solvedVisualization")]
    public String getSolvedVisualization([FromQuery]string problemInstance) {
        //Console.WriteLine("solvedvisualization:" + problemInstance);
        var options = new JsonSerializerOptions { WriteIndented = true };
        CLIQUE clique = new CLIQUE(problemInstance);
        //Console.WriteLine("problemInstance: "+defaultSAT3.instance);
        CliqueBruteForce solver = new CliqueBruteForce();
        string solution = solver.solve(clique);
        Dictionary<string,bool> solutionDict = solver.getSolutionDict(problemInstance, solution);

    // PRINTS DICTIONARY 

    //    solution = "{ ( ";  
   
    //     for(int i =0; i< solutionDict.Count -1; i++ ){

    //         KeyValuePair < string, bool > value = solutionDict.ElementAt(i);
    //         solution +=  value.Key + " : " + value.Value + ", "; 
    //         Console.WriteLine(solution);
    //     }
      
    //     KeyValuePair < string, bool > keyValue = solutionDict.ElementAt(solutionDict.Count -1);
    
    //     solution += keyValue.Key+" : " + keyValue.Value + " ) }";
    //     Console.WriteLine(solution);

    // PRINTS DICTIONARY 
         
        // SipserReduction reduction = new SipserReduction(new NPC_SAT3.SAT3());
        // SipserClique reducedClique = new SipserClique(problemInstance);
        //string cliqueString = reducedClique.instance;
        //Console.WriteLine(cliqueString);
        SipserClique sClique =new SipserClique(problemInstance,solutionDict);
        // Console.WriteLine(sClique.clusterNodes.Count.ToString());
        string jsonString = JsonSerializer.Serialize(sClique.clusterNodes, options);
        
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
public class sipserReduceToVCController : ControllerBase {


    [HttpGet("info")]

    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        CLIQUE defaultCLIQUE = new CLIQUE();
        //SipserReduction reduction = new SipserReduction(defaultSAT3);
        sipserReduction reduction = new sipserReduction(defaultCLIQUE);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

    [HttpGet("reduce")]
    public String getReduce([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        CLIQUE defaultCLIQUE = new CLIQUE(problemInstance);
        //SAT3 defaultSAT3 = new SAT3(problemInstance);
        //SipserReduction reduction = new SipserReduction(defaultSAT3);
        sipserReduction reduction = new sipserReduction(defaultCLIQUE);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }
    [HttpGet("visualize")]
    public String getVisualization([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        CLIQUE clique = new CLIQUE(problemInstance);
        CliqueGraph cGraph = clique.cliqueAsGraph;
        API_UndirectedGraphJSON apiGraphFrom = new API_UndirectedGraphJSON(cGraph.getNodeList,cGraph.getEdgeList);
        for(int i=0;i<apiGraphFrom.nodes.Count;i++){
            apiGraphFrom.nodes[i].attribute1 = i.ToString();
        }
        //SAT3 defaultSAT3 = new SAT3(problemInstance);
        //SipserReduction reduction = new SipserReduction(defaultSAT3);
        sipserReduction reduction = new sipserReduction(clique);
        VERTEXCOVER reducedVcov = reduction.reductionTo;
        VertexCoverGraph vGraph = reducedVcov.VCAsGraph;
        API_UndirectedGraphJSON apiGraphTo = new API_UndirectedGraphJSON(vGraph.getNodeList, vGraph.getEdgeList);
        API_UndirectedGraphJSON[] apiArr = new API_UndirectedGraphJSON[2];
        apiArr[0] = apiGraphFrom;
        apiArr[1] = apiGraphTo;
        string jsonString = JsonSerializer.Serialize(apiArr, options);
        return jsonString;
    }
}

[ApiController]
[Route("[controller]")]
public class CLIQUEDevController : ControllerBase
{

    [HttpGet]
    public String getDefault()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };

        CLIQUE devClique = new CLIQUE();
        
        string jsonString = JsonSerializer.Serialize(devClique, options);
        return jsonString;
    }
    
        [HttpGet("instance")]
    public String getDefault([FromQuery] string problemInstance)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };

        CLIQUE devClique = new CLIQUE(problemInstance);
        
        string jsonString = JsonSerializer.Serialize(devClique, options);
        return jsonString;
    }


     [HttpGet("visualize")]
    public String getVisualization([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        CLIQUE clique = new CLIQUE(problemInstance);
        CliqueGraph cGraph = clique.cliqueAsGraph;
        API_UndirectedGraphJSON apiGraph = new API_UndirectedGraphJSON(cGraph.getNodeList, cGraph.getEdgeList);
        string jsonString = JsonSerializer.Serialize(apiGraph, options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
public class BruteForceSolverController : ControllerBase
{

    // Return Generic Solver Class
    [HttpGet("info")]
    public String getGeneric()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        CliqueBruteForce solver = new CliqueBruteForce();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(solver, options);
        return jsonString;
    }

    // Solve a instance given a certificate
    [HttpGet("solve")]
    public String solveInstance([FromQuery] string problemInstance)
    {
        // Implement solver here
        var options = new JsonSerializerOptions { WriteIndented = true };
        CLIQUE problem = new CLIQUE(problemInstance);
        string solution = problem.defaultSolver.solve(problem);

        string jsonString = JsonSerializer.Serialize(solution, options);
        return jsonString;
    }

}

    [ApiController]
    [Route("[controller]")]
    public class CliqueVerifierController : ControllerBase {

        [HttpGet("verify")]
        public String verifyInstance([FromQuery]string problemInstance, string certificate){
        string jsonString = String.Empty;
        GraphParser gParser = new GraphParser();
        bool isInvalidString = gParser.isValidUndirectedGraph(problemInstance);
        if (!isInvalidString)
        {
            CLIQUE vClique = new CLIQUE(problemInstance);
            GenericVerifier verifier = vClique.defaultVerifier;
            bool validClique = verifier.verify(vClique, certificate);
            var options = new JsonSerializerOptions { WriteIndented = true };
            jsonString = JsonSerializer.Serialize(validClique, options);
        }
        else{
            jsonString = "ERROR: PROBLEM ENTERED IS INVALID";
        }
        return jsonString;
        }
    }
