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

}

[ApiController]
[Route("[controller]")]

public class KarpReduceGRAPHCOLORINGController : ControllerBase {

    [HttpGet]
    public String getDefault(){

        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3();
        KarpReduction reduction = new KarpReduction(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }


    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SAT3(), options);
        return jsonString;
    }

}
// public class Karp_ReduceTo_INTPROGRAMMING0_1Controller : ControllerBase {


[ApiController]
[Route("[controller]")]
public class KarpIntProgStandardController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3();
        KarpIntProgStandard reduction = new KarpIntProgStandard(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SAT3(), options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
public class GJDM3Controller : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3();
        GJDM3 reduction = new GJDM3(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction, options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SAT3(), options);
        return jsonString;
    }
}

[ApiController]
[Route("[controller]")]
public class KadensSimpleVerifierController : ControllerBase {

    // Return Generic Solver Class
    [HttpGet]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        KadensSimple verifier = new KadensSimple();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(verifier, options);
        return jsonString;
    }

    // Solve a instance given a certificate
    [HttpGet]
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
    public String solveInstance([FromQuery]string problemInstance) {
        // Implement solver here
        return "RETURN YOUR SOLVER RESULTS HERE";
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
