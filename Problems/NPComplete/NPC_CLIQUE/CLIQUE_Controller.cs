using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_CLIQUE;
using API.Problems.NPComplete.NPC_CLIQUE.ReduceTo.NPC_VertexCover;
using System.Text.Json;
using System.Text.Json.Serialization;

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

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new CLIQUE(), options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]
public class sipserReduceToVCController : ControllerBase {

    // [HttpGet]
    // public String getDefault() {
    //     var options = new JsonSerializerOptions { WriteIndented = true };
    //     CLIQUE defaultCLIQUE = new CLIQUE();
    //     Clique_to_VertexCoverReduction reduction = new Clique_to_VertexCoverReduction(defaultCLIQUE);
    //     string jsonString = JsonSerializer.Serialize(reduction.reductionTo, options);
    //     return jsonString;
    // }

    // [HttpGet("{instance}")]
    // public String getInstance() {
    //     var options = new JsonSerializerOptions { WriteIndented = true };
    //     string jsonString = JsonSerializer.Serialize(new CLIQUE(), options);
    //     return jsonString;
    // }

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
        string jsonString = JsonSerializer.Serialize(reduction.reductionTo, options);
        return jsonString;
    }

}