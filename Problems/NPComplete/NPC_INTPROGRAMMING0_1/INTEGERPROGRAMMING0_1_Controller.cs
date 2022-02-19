using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_INTPROGRAMMING0_1;
using API.Problems.NPComplete.NPC_CLIQUE;
using API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_CLIQUE;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_SAT3;


[ApiController]
[Route("[controller]")]
public class INTPROGRAMMING0_1GenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new INTPROGRAMMING0_1(), options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new INTPROGRAMMING0_1(), options);
        return jsonString;
    }


}



