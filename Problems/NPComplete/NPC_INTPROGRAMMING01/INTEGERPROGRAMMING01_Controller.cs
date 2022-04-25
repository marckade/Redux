using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_INTPROGRAMMING01;
using API.Problems.NPComplete.NPC_INTPROGRAMMING01.Verifiers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_INTPROGRAMMING01;


[ApiController]
[Route("[controller]")]
public class INTPROGRAMMING01GenericController : ControllerBase {

    [HttpGet]
    public String getDefault() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new INTPROGRAMMING01(), options);
        return jsonString;
    }

    [HttpGet("{instance}")]
    public String getInstance() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(new INTPROGRAMMING01(), options);
        return jsonString;
    }
}
[ApiController]
[Route("[controller]")]
public class GenericVerifier0_1INTPController : ControllerBase {

    [HttpGet("info")]
    public String getGeneric() {
        var options = new JsonSerializerOptions { WriteIndented = true };
        GenericVerifier01INTP verifier = new GenericVerifier01INTP();

        // Send back to API user
        string jsonString = JsonSerializer.Serialize(verifier, options);
        return jsonString;
    }
    [HttpGet("solve")]
    public String solveInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        INTPROGRAMMING01 INTPROGRAMMING01_Problem = new INTPROGRAMMING01(problemInstance);
        GenericVerifier01INTP verifier = new GenericVerifier01INTP();

        Boolean response = verifier.verify(INTPROGRAMMING01_Problem,certificate);
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }

}
    





