using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_ExactCover.Verifiers;
using API.Problems.NPComplete.NPC_ExactCover.Solvers;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_ExactCover;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
#pragma warning disable CS1591
public class ExactCoverGenericController : ControllerBase
{
#pragma warning restore CS1591

    ///<summary>Returns a default Exact Cover object</summary>

    [ProducesResponseType(typeof(ExactCover), 200)]
    [HttpGet]
    public String getDefault()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new ExactCover(), options);
        return jsonString;
    }

    ///<summary>Returns an Exact Cover object created from a given instance </summary>{
    ///<param name="problemInstance" example="{{ (), (1 &amp; 3), (2 &amp; 3), (2 &amp; 4)} : {1,2,3,4} : {(1 &amp; 3), (2 &amp; 4)}}">Exact Cover problem instance string.</param>
    ///<response code="200">Returns ExactCover problem object</response>

    [ProducesResponseType(typeof(ExactCover), 200)]
    [HttpGet("instance")]
    public String getInstance(string problemInstance)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new ExactCover(problemInstance), options);
        return jsonString;

    }

}


[ApiController]
[Route("[controller]")]
public class ExactCoverVerifierController : ControllerBase {

    [HttpGet("info")]
    public String getGeneric() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        ExactCoverVerifier verifier = new ExactCoverVerifier();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(verifier, options);
        return jsonString;
    }
    [HttpGet("verify")]
    public String solveInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        ExactCover problem = new ExactCover(problemInstance);
        ExactCoverVerifier verifier = new ExactCoverVerifier();

        bool response = verifier.verify(problem,certificate);
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
public class ExactCoverBruteForceController : ControllerBase {

    // Return Generic Solver Class
    [HttpGet("info")]
    public String getGeneric() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        ExactCoverBruteForce solver = new ExactCoverBruteForce();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(solver, options);
        return jsonString;
    }

    // Solve a instance given a certificate
    [HttpGet("solve")]
    public String solveInstance([FromQuery]string problemInstance) {
        // Implement solver here
        var options = new JsonSerializerOptions { WriteIndented = true };
        ExactCover problem = new ExactCover(problemInstance);
        string solution = problem.defaultSolver.solve(problem);
        
        string jsonString = JsonSerializer.Serialize(solution, options);
        return jsonString;
    }

}