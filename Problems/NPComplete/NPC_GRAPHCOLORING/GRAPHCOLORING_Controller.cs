
using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_GRAPHCOLORING;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_GRAPHCOLORING;


[ApiController]
[Route("[controller]")]
public class GRAPHCOLORINGGenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new GRAPHCOLORING(), options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new GRAPHCOLORING(), options);
        return jsonString;
    }


}
