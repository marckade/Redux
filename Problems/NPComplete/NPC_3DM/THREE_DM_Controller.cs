using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_3DM;
using API.Problems.NPComplete.NPC_3DM.Verifiers;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Problems.NPComplete.NPC_3DM;

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

[ApiController]
[Route("[controller]")]
public class GenericVerifier3DMController : ControllerBase {

    [HttpGet]
    public String getInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        THREE_DM THREE_DM_PROBLEM = new THREE_DM(problemInstance);
        GenericVerifier3DM verifier = new GenericVerifier3DM();

        Boolean response = verifier.verify(THREE_DM_PROBLEM,certificate);
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }

}

}

