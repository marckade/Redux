using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_SUBSETSUM;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Problems.NPComplete.NPC_SUBSETSUM.ReduceTo.NPC_KNAPSACK;

namespace API.Problems.NPComplete.NPC_SUBSETSUM;

[ApiController]
[Route("[controller]")]
public class SUBSETSUMGenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SUBSETSUM(), options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SUBSETSUM(), options);
        return jsonString;
    }

}

[ApiController]
[Route("[controller]")]

public class Feng_ReduceTo_KNAPSACKController : ControllerBase {
    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SUBSETSUM defaultSUBSETSUM = new SUBSETSUM();
        FengReduction reduction = new FengReduction(defaultSUBSETSUM);
        string jsonString = JsonSerializer.Serialize(reduction.reductionTo, options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SUBSETSUM(), options);
        return jsonString;
    }
}