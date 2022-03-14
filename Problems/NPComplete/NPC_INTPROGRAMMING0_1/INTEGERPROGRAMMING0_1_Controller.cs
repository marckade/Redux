using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_INTPROGRAMMING0_1;
using API.Problems.NPComplete.NPC_INTPROGRAMMING0_1.Verifiers;
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

[ApiController]
[Route("[controller]")]
public class GenericVerifier0_1INTPController : ControllerBase {

    [HttpGet]
    public String getInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        INTPROGRAMMING0_1 INTPROGRAMMING0_1_Problem = new INTPROGRAMMING0_1(problemInstance);
        GenericVerifier0_1INTP verifier = new GenericVerifier0_1INTP();

        Boolean response = verifier.verify(INTPROGRAMMING0_1_Problem,certificate);
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }

}
    



