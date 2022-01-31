using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_KNAPSACK;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_KNAPSACK;

[ApiController]
[Route("[controller]")]
public class testController : ControllerBase {
    
    public String test() {
        KNAPSACK testObj = new KNAPSACK();

        if (testObj.HWV == null) {
            return testObj.defaultInstance;
        }
        else {
            return "REALLY? API!";
        }
    }
}

[ApiController]
[Route("[controller]")]
public class KNAPSACKGenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new KNAPSACK(), options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new KNAPSACK(), options);
        return jsonString;
    }


}

