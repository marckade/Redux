using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_ExactCover;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_ExactCover;

[ApiController]
[Route("[controller]")]
public class ExactCoverGenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new ExactCover(), options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new ExactCover(), options);
        return jsonString;
    }

}