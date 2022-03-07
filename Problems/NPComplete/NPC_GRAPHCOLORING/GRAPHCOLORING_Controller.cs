
using Microsoft.AspNetCore.Mvc;
using API.Problems.NPComplete.NPC_GRAPHCOLORING;
using System.Text.Json;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.Verifiers;

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



[ApiController]
[Route("[controller]")]
public class IgbokwesSimpleVerifierController : ControllerBase {

    [HttpGet("{certificate}/{problemInstance}")]
    public String getInstance(string certificate, string problemInstance) {
        var options = new JsonSerializerOptions { WriteIndented = true };
        GRAPHCOLORING GRAPHCOLORINGProblem = new GRAPHCOLORING(problemInstance);
        IgbokwesSimple verifier = new IgbokwesSimple();

        Boolean response = verifier.verify(GRAPHCOLORINGProblem, certificate);
        // Send back to API user
        string jsonString = JsonSerializer.Serialize(response.ToString(), options);
        return jsonString;
    }

}



}
