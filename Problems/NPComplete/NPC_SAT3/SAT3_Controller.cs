using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_SAT3;
using API.Problems.NPComplete.NPC_CLIQUE;
using API.Problems.NPComplete.NPC_3DM;
using API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_CLIQUE;
using API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_3DM;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_SAT3;

[ApiController]
[Route("[controller]")]
public class testController : ControllerBase {
    
    public String test() {
        SAT3 testObj = new SAT3();

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
public class SAT3GenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SAT3(), options);
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
public class Sipser_ReduceTo_CLIQUEController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3();
        SipserReduction reduction = new SipserReduction(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction.reductionTo, options);
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
public class GandJ_ReduceTo_3DMController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        SAT3 defaultSAT3 = new SAT3();
        GareyAndJohnsonReduction reduction = new GareyAndJohnsonReduction(defaultSAT3);
        string jsonString = JsonSerializer.Serialize(reduction.reductionTo, options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new SAT3(), options);
        return jsonString;
    }

}

