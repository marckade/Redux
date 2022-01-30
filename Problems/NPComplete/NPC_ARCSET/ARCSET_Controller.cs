using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_ARCSET;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_ARCSET;

[ApiController]
[Route("[controller]")]
public class testController : ControllerBase {
    
    public String test() {
        ARCSET testObj = new ARCSET();

        if (testObj.phi == null) {
            return testObj.defaultInstance;
        }
        else {
            return "REALLY? API!";
        }
    }
}

[ApiController]
[Route("[controller]")]
public class ARCSETGenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new ARCSET(), options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new ARCSET(), options);
        return jsonString;
    }


}

