using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

// Get all problems regardless of complexity class
[ApiController]
[Route("Navigation/[controller]")]
public class All_ProblemsController : ControllerBase {
    
    public String getDefault() {
        string?[] subdirs = Directory.GetDirectories("Problems")
                            .Select(Path.GetFileName)
                            .ToArray();

        // Not completed. Needs to loop through these directories to get the rest of the problems
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(subdirs, options);

        return jsonString;
    }
}

// Get only NP-Complete problems
[ApiController]
[Route("Navigation/[controller]")]
public class NPC_ProblemsController : ControllerBase {
    
    public String getDefault() {
        string?[] subdirs = Directory.GetDirectories("Problems/NPComplete")
                            .Select(Path.GetFileName)
                            .ToArray();
                            
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(subdirs, options);

        //Response.Headers.Add("Access-Control-Allow-Origin", "http://127.0.0.1:5500");
        return jsonString;
    }
}
