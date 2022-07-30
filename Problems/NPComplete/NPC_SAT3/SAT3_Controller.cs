using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_SAT3;
using API.Problems.NPComplete.NPC_CLIQUE;
using API.Problems.NPComplete.NPC_3DM;
using API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_CLIQUE;
using API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_GRAPHCOLORING;
using API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_DM3;
using API.Problems.NPComplete.NPC_INTPROGRAMMING01;
using API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_INTPROGRAMMING01;
using API.Problems.NPComplete.NPC_SAT3.Verifiers;
using API.Problems.NPComplete.NPC_SAT3.Solvers;
using API.Problems.NPComplete.NPC_CLIQUE.Inherited;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace API.Problems.NPComplete.NPC_SAT3;

[ApiController]
[Route("[controller]")]
public class SAT3GenericController : ControllerBase {

    [HttpGet()]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SAT3(), options);
        return jsonString;
    }

    [HttpGet("instance")]
    public String getInstance([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SAT3(problemInstance), options);
        return jsonString;
    }


}

[ApiController]
[Route("[controller]")]
public class SipserReduceToCliqueStandardController : ControllerBase {

    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3();
        SipserReduction reduction = new SipserReduction(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

    [HttpGet("reduce")]
    public String getReduce([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3(problemInstance);
        SipserReduction reduction = new SipserReduction(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

    [HttpGet("solvedVisualization")]
    public String getSolvedVisualization([FromQuery]string problemInstance) {
        //Console.WriteLine("solvedvisualization:" + problemInstance);
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3(problemInstance);
        Console.WriteLine("problemInstance: "+defaultSAT3.instance);
        SkeletonSolver solver = defaultSAT3.defaultSolver;
        Dictionary<string,bool> solutionDict = solver.solve(defaultSAT3);
        bool solBool;
        solutionDict.TryGetValue("x1", out solBool);
        Console.WriteLine(solBool);
        SipserReduction reduction = new SipserReduction(defaultSAT3);
        SipserClique reducedClique = reduction.reduce();
        //string cliqueString = reducedClique.instance;
        //Console.WriteLine(cliqueString);
        SipserClique sClique = reduction.solutionMappedToClusterNodes(reducedClique,solutionDict);
                //Console.WriteLine(sClique.clusterNodes[0].ToString());

        string jsonString = JsonSerializer.Serialize(sClique.clusterNodes, options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]

public class KarpReduceGRAPHCOLORINGController : ControllerBase {

    [HttpGet("info")]
    public String getInfo(){

        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3();
        KarpReduction reduction = new KarpReduction(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }





    [HttpGet("reduce")]
    public String getReduce([FromQuery]string problemInstance){
         
        KarpReduction reduction = new KarpReduction(new SAT3(problemInstance));
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

}


[ApiController]
[Route("[controller]")]
public class KarpIntProgStandardController : ControllerBase {

    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3();
        KarpIntProgStandard reduction = new KarpIntProgStandard(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

    [HttpGet("reduce")]
    public String getReduce([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3(problemInstance);
        KarpIntProgStandard reduction = new KarpIntProgStandard(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
public class GareyJohnsonController : ControllerBase {
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3();
        GareyJohnson reduction = new GareyJohnson(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

    [HttpGet("reduce")]
    public String getReduce([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3(problemInstance);
        GareyJohnson reduction = new GareyJohnson(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }
}

[ApiController]
[Route("[controller]")]
public class KadensSimpleVerifierController : ControllerBase {

    // Return Generic Verifier Class
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        KadensSimple verifier = new KadensSimple();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(verifier, options);
        return jsonString;
    }

    // Verify a instance given a certificate
    [HttpGet("verify")]
    public String getInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 SAT3Problem = new SAT3(problemInstance);
        KadensSimple verifier = new KadensSimple();

        Boolean response = verifier.verify(SAT3Problem,certificate);
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
public class SkeletonSolverController : ControllerBase {

    // Return Generic Solver Class
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SkeletonSolver solver = new SkeletonSolver();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(solver, options);
        return jsonString;
    }

    // Solve a instance given a certificate
    [HttpGet("solve")]
    public String solveInstance([FromQuery]string problemInstance) { //FromQuery]string certificate, 
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 SAT3_PROBLEM = new SAT3(problemInstance);
        Dictionary<string, bool> solution = SAT3_PROBLEM.defaultSolver.solve(SAT3_PROBLEM);

        //ALEX NOTE: This is a temporary fix. This logic should be moved to the SAT3 solver class soon. 
        string solutionString = "(";
        foreach(KeyValuePair<string,bool> kvp in solution){
            solutionString = solutionString + kvp.Key + ":" + kvp.Value.ToString() + ",";
        }
        solutionString = solutionString.TrimEnd(',');
        solutionString = solutionString + ")"; 
        string jsonString = JsonSerializer.Serialize(solutionString, options);
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
