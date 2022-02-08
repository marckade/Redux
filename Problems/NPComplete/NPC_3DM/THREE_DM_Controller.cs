using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_3DM;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_3DM;

[ApiController]
[Route("[controller]")]
public class testController : ControllerBase {
    
    public String test() {
        THREE_DM testObj = new THREE_DM();

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
public class THREE_DMGenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new THREE_DM(), options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new THREE_DM(), options);
        return jsonString;
    }


}

