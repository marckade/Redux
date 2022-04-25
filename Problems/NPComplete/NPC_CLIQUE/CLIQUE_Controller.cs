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
public class CliqueToVERTEXCOVERController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        CLIQUE defaultCLIQUE = new CLIQUE();
        Clique_to_VertexCoverReduction reduction = new Clique_to_VertexCoverReduction(defaultCLIQUE);
        string jsonString = JsonSerializer.Serialize(reduction.reductionTo, options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new CLIQUE(), options);
        return jsonString;
    }

}