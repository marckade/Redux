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
using API.Problems.NPComplete.NPC_DM3;
using API.Problems.NPComplete.NPC_GRAPHCOLORING;
using API.Interfaces;

namespace API.Problems.NPComplete.NPC_SAT3;

[ApiController]
[Route("[controller]")]
#pragma warning disable CS1591
public class SAT3GenericController : ControllerBase {
#pragma warning restore CS1591


///<summary>Returns a default 3SAT json object</summary>

    [HttpGet()]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SAT3(), options);
        return jsonString;
    }

///<summary>Returns a 3SAT json object created from a given instance </summary>
///<param name="problemInstance" example="(x1|!x2|x3)&amp;(!x1|x3|x1)&amp;(x2|!x3|x1)">3SAT problem instance string.</param>
///<response code="200">Returns 3SAT Problem Object</response>

    [ProducesResponseType(typeof(SAT3), 200)]
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

///<summary>Returns a a reduction object with info for Sipser's 3SAT to Clique reduction </summary>
///<response code="200">Returns Reduction Object</response>

    [ProducesResponseType(typeof(SipserReduction), 200)]
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3();
        SipserReduction reduction = new SipserReduction(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

///<summary>Returns a reduction from 3SAT to Clique based on the given 3SAT instance  </summary>
///<param name="problemInstance" example="(x1|!x2|x3)&amp;(!x1|x3|x1)&amp;(x2|!x3|x1)">3SAT problem instance string.</param>
///<response code="200">Returns string instance of a graph</response>

    [ProducesResponseType(typeof(string), 200)]
    [HttpGet("reduce")]
    public String getReduce([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3(problemInstance);
        SipserReduction reduction = new SipserReduction(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("solvedVisualization")]
    public String getSolvedVisualization([FromQuery]string problemInstance, string solution) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3(problemInstance);
        Sat3BacktrackingSolver solver = defaultSAT3.defaultSolver;
        SipserReduction reduction = new SipserReduction(defaultSAT3);
        SipserClique reducedClique = reduction.reduce();
        //Turn string into solution dictionary
        List<string> solutionList = solution.Replace("(","").Replace(")","").Replace(" ","").Split(",").ToList();
        Dictionary<string,bool> solutionDict = new Dictionary<string,bool>();
        foreach(var assignment in solutionList){
            string[] assignmentSpit = assignment.Split(":");
            bool value = bool.Parse(assignmentSpit[1]);
            solutionDict.Add(assignmentSpit[0], value);
        }
        SipserClique sClique = reduction.solutionMappedToClusterNodes(reducedClique,solutionDict);
        string jsonString = JsonSerializer.Serialize(sClique.clusterNodes, options);
        return jsonString;
    }

///<summary>Returns a reduction from 3SAT to Clique based on the given 3SAT instance  </summary>
///<param name="problemFrom" example="(x1|!x2|x3)&amp;(!x1|!x3|!x1)&amp;(x2|!x3|x1)">3SAT problem instance string.</param>
///<param name="problemTo" example="{{x1,!x2,x3,!x1,!x3,!x1_1,x2,!x3_1,x1_1},{{x1,!x3},{x1,!x1_1},{x1,x2},{x1,!x3_1},{x1,x1_1},{!x2,!x1},{!x2,!x3},{!x2,!x1_1},{!x2,!x3_1},{!x2,x1_1},{x3,!x1},{x3,!x1_1},{x3,x2},{x3,!x3_1},{x3,x1_1},{!x1,!x2},{!x1,x3},{!x1,x2},{!x1,!x3_1},{!x1,x1_1},{!x3,x1},{!x3,!x2},{!x3,x2},{!x3,!x3_1},{!x3,x1_1},{!x1_1,x1},{!x1_1,!x2},{!x1_1,x3},{!x1_1,x2},{!x1_1,!x3_1},{x2,x1},{x2,x3},{x2,!x1},{x2,!x3},{x2,!x1_1},{!x3_1,x1},{!x3_1,!x2},{!x3_1,x3},{!x3_1,!x1},{!x3_1,!x3},{!x3_1,!x1_1},{x1_1,x1},{x1_1,!x2},{x1_1,x3},{x1_1,!x1},{x1_1,!x3}},3}">Clique problem instance string reduced from 3SAT instance.</param>
///<param name="problemFromSolution" example="(x1:True,x3:False)">Solution to 3SAT problem.</param>
///<response code="200">Returns solution to the reduced Clique instance</response>
///<example>asd</example>
    
    [ProducesResponseType(typeof(string), 200)]
    [HttpGet("mapSolution")]
    public String mapSolution([FromQuery]string problemFrom, string problemTo, string problemFromSolution){
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 sat3 = new SAT3(problemFrom);
        SipserClique clique = new SipserClique(problemTo);
        SipserReduction reduction = new SipserReduction(sat3);
        string mappedSolution = reduction.mapSolutions(sat3,clique,problemFromSolution);
        string jsonString = JsonSerializer.Serialize(mappedSolution, options);
        return jsonString;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("reverseMappedSolution")]
    public String reverseMappedSolution([FromQuery]string problemFrom, string problemTo, string problemToSolution){
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 sat3 = new SAT3(problemFrom);
        SipserClique clique = new SipserClique(problemTo);
        SipserReduction reduction = new SipserReduction(sat3);
        string mappedSolution = reduction.reverseMapSolutions(sat3,clique,problemToSolution);
        string jsonString = JsonSerializer.Serialize(mappedSolution, options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]

public class KarpReduceGRAPHCOLORINGController : ControllerBase {

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("info")]
    public String getInfo(){

        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3();
        KarpReduction reduction = new KarpReduction(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("reduce")]
    public String getReduce([FromQuery]string problemInstance){
         
        KarpReduction reduction = new KarpReduction(new SAT3(problemInstance));
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("mapSolution")]
    public String mapSolution([FromQuery]string problemFrom, string problemTo, string problemFromSolution){
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 sat3 = new SAT3(problemFrom);
        GRAPHCOLORING graphColoring = new GRAPHCOLORING(problemTo);
        KarpReduction reduction = new KarpReduction(sat3);
        string mappedSolution = reduction.mapSolutions(sat3,graphColoring,problemFromSolution);
        string jsonString = JsonSerializer.Serialize(mappedSolution, options);
        return jsonString;
    }

}


[ApiController]
[Route("[controller]")]
public class KarpIntProgStandardController : ControllerBase {

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3();
        KarpIntProgStandard reduction = new KarpIntProgStandard(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("reduce")]
    public String getReduce([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3(problemInstance);
        KarpIntProgStandard reduction = new KarpIntProgStandard(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("mapSolution")]
    public String mapSolution([FromQuery]string problemFrom, string problemTo, string problemFromSolution){
        Console.WriteLine(problemTo);
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 sat3 = new SAT3(problemFrom);
        INTPROGRAMMING01 intProg = new INTPROGRAMMING01(problemTo);
        KarpIntProgStandard reduction = new KarpIntProgStandard(sat3);
        string mappedSolution = reduction.mapSolutions(sat3,intProg,problemFromSolution);
        string jsonString = JsonSerializer.Serialize(mappedSolution, options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
public class GareyJohnsonController : ControllerBase {
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3();
        GareyJohnson reduction = new GareyJohnson(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("reduce")]
    public String getReduce([FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3(problemInstance);
        GareyJohnson reduction = new GareyJohnson(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("mapSolution")]
    public String mapSolution([FromQuery]string problemFrom, string problemTo, string problemFromSolution){
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 sat3 = new SAT3(problemFrom);
        DM3 dm3 = new DM3(problemTo);
        GareyJohnson reduction = new GareyJohnson(sat3);
        string mappedSolution = reduction.mapSolutions(sat3,dm3,problemFromSolution);
        string jsonString = JsonSerializer.Serialize(mappedSolution, options);
        return jsonString;
    }
}

[ApiController]
[Route("[controller]")]
public class KadensSimpleVerifierController : ControllerBase {

    // Return Generic Verifier Class
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        KadensSimple verifier = new KadensSimple();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(verifier, options);
        return jsonString;
    }

    // Verify a instance given a certificate
    [ApiExplorerSettings(IgnoreApi = true)]
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
public class Sat3BacktrackingSolverController : ControllerBase {

    // Return Generic Solver Class
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("info")]
    public String getInfo() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        Sat3BacktrackingSolver solver = new Sat3BacktrackingSolver();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(solver, options);
        return jsonString;
    }

    // Solve a instance given a certificate
    [ApiExplorerSettings(IgnoreApi = true)]
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
public class testSART3InstanceController : ControllerBase {

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    public String getSingleInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        
        string returnString = certificate + problemInstance;
        return returnString;
    }

}
