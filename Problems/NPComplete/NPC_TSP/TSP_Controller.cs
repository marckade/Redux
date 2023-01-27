using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_TSP;
using API.Problems.NPComplete.NPC_TSP.Verifiers;
using API.Problems.NPComplete.NPC_TSP.Solvers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_TSP;

[ApiController]
[Route("[controller]")]
public class TSPGenericController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    public String getDefault()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new TSP(), options);
        return jsonString;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("{instance}")]
    public String getInstance()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new TSP(), options);
        return jsonString;
    }
}

[ApiController]
[Route("[controller]")]
public class TSPVerifierTestController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
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

[ApiController]
[Route("[controller]")]
public class TSPVerifierBranchTestController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
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

[ApiController]
[Route("[controller]")]
public class TSPVerifierController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
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
public class BranchAndBoundSolverController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("info")]
    public String getInfo()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        TSP TSPProblem = new TSP();
        BranchAndBoundSolver solver = new BranchAndBoundSolver();
        string certificate = solver.branchAndBound(TSPProblem);

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(certificate, options);
        return jsonString;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("solve")]
    public String getInstance([FromQuery] string problemInstance)
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
public class GreedySolverController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("solve")]
    public String getInstance([FromQuery] string problemInstance)
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



