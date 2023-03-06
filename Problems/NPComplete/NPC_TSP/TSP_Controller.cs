using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_TSP;
using API.Problems.NPComplete.NPC_TSP.Verifiers;
using API.Problems.NPComplete.NPC_TSP.Solvers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_TSP;

[ApiController]
[Route("[controller]")]
[Tags("Traveling Sales Person")]

#pragma warning disable CS1591
public class TSPGenericController : ControllerBase
#pragma warning restore CS1591

{
///<summary>Returns a default Trveling Sales Person problem object</summary>

    [ProducesResponseType(typeof(TSP), 200)]
    [HttpGet]
    public String getDefault()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new TSP(), options);
        return jsonString;
    }

///<summary>Returns a Traveling Sales Person problem object created from a given instance </summary>
///<param name="problemInstance" example="{ { int.MaxValue, 5, 12, 8 },{ 4, int.MaxValue, 10, 7 },{ 9, 14, int.MaxValue, 5 },{ 16, 3, 10, int.MaxValue } }">Traveling Sales Person problem instance string.</param>
///<response code="200">Returns TSP problem object</response>

    [ProducesResponseType(typeof(TSP), 200)]
    [HttpGet("instance")]
    public String getInstance(string problemInstance)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new TSP(problemInstance), options);
        return jsonString;
    }
}

[ApiController]
[Route("[controller]")]
[ApiExplorerSettings(IgnoreApi = true)]
[Tags("Traveling Sales Person")]
#pragma warning disable CS1591
public class TSPVerifierTestController : ControllerBase

{
    [HttpGet]
    public String getInstance()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        TSP TSPProblem = new TSP();
        GreedySolver solver = new GreedySolver();
        string certificate = solver.greedy(TSPProblem.D, true);
        TSPVerifier verifier = new TSPVerifier();

        Boolean response = verifier.isTour(certificate, TSPProblem);
        //send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }
}
#pragma warning restore CS1591


[ApiController]
[Route("[controller]")]
[ApiExplorerSettings(IgnoreApi = true)]
[Tags("Traveling Sales Person")]
#pragma warning disable CS1591

public class TSPVerifierBranchTestController : ControllerBase

{
    [HttpGet]
    public String getInstance()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        TSP TSPProblem = new TSP();
        BranchAndBoundSolver solver = new BranchAndBoundSolver();
        string certificate = solver.branchAndBound(TSPProblem);
        TSPVerifier verifier = new TSPVerifier();

        Boolean response = verifier.isTour(certificate, TSPProblem);
        //send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }
}
#pragma warning restore CS1591


[ApiController]
[Route("[controller]")]
[Tags("Traveling Sales Person")]
#pragma warning disable CS1591
public class TSPVerifierController : ControllerBase
#pragma warning restore CS1591
{
///<summary>Returns a info about the Travelling Sales Person generic Verifier </summary>
///<response code="200">Returns TSPVerifier object</response>

    [ProducesResponseType(typeof(TSPVerifier), 200)]
    [HttpGet("info")]
    public String getInfo(){
        var options = new JsonSerializerOptions {WriteIndented = true};
        TSPVerifier verifier = new TSPVerifier();


        string jsonString  = JsonSerializer.Serialize(verifier, options);
        return jsonString; 
    }

///<summary>Verifies if a given certificate is a solution to a given Traveling Sales Person problem</summary>
///<param name="certificate" example="0, 2, 3, 1">certificate solution to Traveling Sales Person problem.</param>
///<param name="problemInstance" example="{ { int.MaxValue, 5, 12, 8 },{ 4, int.MaxValue, 10, 7 },{ 9, 14, int.MaxValue, 5 },{ 16, 3, 10, int.MaxValue } }">Traveling Sales Person problem instance string.</param>
///<response code="200">Returns a boolean</response>
    
    [ProducesResponseType(typeof(Boolean), 200)]
    [HttpGet("verify")]
    public String getInstance([FromQuery] string certificate, [FromQuery] string problemInstance)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        TSP TSPProblem = new TSP(problemInstance);
        TSPVerifier verifier = new TSPVerifier();

        Boolean response = verifier.isTour(certificate, TSPProblem);
        //send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }
}

[ApiController]
[Route("[controller]")]
[Tags("Traveling Sales Person")]
#pragma warning disable CS1591
public class BranchAndBoundSolverController : ControllerBase
#pragma warning restore CS1591

{
///<summary>Returns a info about the Traveling Sales Person branch and bound solver </summary>
///<response code="200">Returns BranchAndBoundSolver solver object</response>

    [ProducesResponseType(typeof(BranchAndBoundSolver), 200)]
    [HttpGet("info")]
    public String getInfo()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        TSP TSPProblem = new TSP();
        BranchAndBoundSolver solver = new BranchAndBoundSolver();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(solver, options);
        return jsonString;
    }

///<summary>Returns a solution to a given Traveling Sales Person problem instance </summary>
///<param name="problemInstance" example="{ { int.MaxValue, 5, 12, 8 },{ 4, int.MaxValue, 10, 7 },{ 9, 14, int.MaxValue, 5 },{ 16, 3, 10, int.MaxValue } }">Traveling Sales Person problem instance string.</param>
///<response code="200">Returns solution string </response>
    
    [ProducesResponseType(typeof(string), 200)]
    [HttpGet("solve")]
    public String solveInstance([FromQuery] string problemInstance)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        TSP TSPProblem = new TSP(problemInstance);
        BranchAndBoundSolver solver = new BranchAndBoundSolver();
        string certificate = solver.branchAndBound(TSPProblem);

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(certificate, options);
        return jsonString;
    }
}

[ApiController]
[Route("[controller]")]
[Tags("Traveling Sales Person")]
#pragma warning disable CS1591
public class GreedySolverController : ControllerBase
#pragma warning restore CS1591
{
///<summary>Returns a info about the Traveling Sales Person branch and bound solver </summary>
///<response code="200">Returns BranchAndBoundSolver solver object</response>

    [ProducesResponseType(typeof(GreedySolver), 200)]
    [HttpGet("info")]
    public String getInfo()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        TSP TSPProblem = new TSP();
        GreedySolver solver = new GreedySolver();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(solver, options);
        return jsonString;
    }

///<summary>Returns a solution to a given Traveling Sales Person problem instance </summary>
///<param name="problemInstance" example="{ { int.MaxValue, 5, 12, 8 },{ 4, int.MaxValue, 10, 7 },{ 9, 14, int.MaxValue, 5 },{ 16, 3, 10, int.MaxValue } }">Traveling Sales Person problem instance string.</param>
///<response code="200">Returns solution string </response>
    
    [ProducesResponseType(typeof(string), 200)]
    [HttpGet("solve")]
    public String solverInstance([FromQuery] string problemInstance)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        TSP TSPProblem = new TSP(problemInstance);
        GreedySolver solver = new GreedySolver();
        string certificate = solver.greedy(TSPProblem.D, true);

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(certificate, options);
        return jsonString;
    }
}




