using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

// Get all problems regardless of complexity class
[ApiController]
[Route("Navigation/[controller]")]
public class All_ReductionsController : ControllerBase {

    
    [HttpGet]
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
public class NPC_ReductionsController : ControllerBase {
    
    [HttpGet]
    public String getDefault() {
        string?[] subdirs = Directory.GetDirectories("Problems/NPComplete")
                            .Select(Path.GetFileName)
                            .ToArray();
                            
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(subdirs, options);
        return jsonString;
    }
}

// Get all problems we can reduce to for a specific problem
[ApiController]
[Route("Navigation/[controller]")]
public class Problem_ReductionsController : ControllerBase {

    public const string NO_REDUCTIONS_ERROR = "{\"ERROR\": \"No Reductions Available\"}"; //API Response. 

    [HttpGet]
    public String getDefault([FromQuery]string chosenProblem) {
        

        // Determine the directory to search based on prefix. chosenProblem expected to be a problemName like "NPC_PROBLEM"\

        string problemTypeDirectory = "";
        string problemType = chosenProblem.Split('_')[0];

        if (problemType == "NPC") {
            problemTypeDirectory = "NPComplete";
        }
        else if (problemType == "P") {
            problemTypeDirectory = "Polynomial";
        }

        string jsonString = "";
        var options = new JsonSerializerOptions { WriteIndented = true };
  
        try{
        string?[] subdirs = Directory.GetDirectories("Problems/" + problemTypeDirectory + "/" + chosenProblem + "/ReduceTo")
                            .Select(Path.GetFileName)
                            .ToArray();

        jsonString = JsonSerializer.Serialize(subdirs, options);
 
        }
        catch (System.IO.DirectoryNotFoundException dirNotFoundException){
            Console.WriteLine(NO_REDUCTIONS_ERROR + " directory not found, exception was thrown in Nav_Reductions.cs");
            
            jsonString = NO_REDUCTIONS_ERROR;
        }
        return jsonString;
    }
}

// Get all reductions implemented for a specific problem
[ApiController]
[Route("Navigation/[controller]")]
public class PossibleReductionsController : ControllerBase {
    
    [HttpGet]
    public String getDefault([FromQuery]string reducingFrom, [FromQuery]string reducingTo) {

        // Determine the directory to search based on prefix. reducingFrom and reducingTo are both expected to be a problemName like "NPC_PROBLEM"
        string problemTypeDirectory = "";
        string problemType = reducingFrom.Split('_')[0];

        if (problemType == "NPC") {
            problemTypeDirectory = "NPComplete";
        }
        else if (problemType == "P") {
            problemTypeDirectory = "Polynomial";
        }

        string?[] subfiles = Directory.GetFiles("Problems/" + problemTypeDirectory + "/" + reducingFrom + "/ReduceTo/" + reducingTo)
                            .Select(Path.GetFileName)
                            .ToArray();
                  
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(subfiles, options);
        return jsonString;
    }
}

// Get all problems from a chosen reduction
[ApiController]
[Route("Navigation/[controller]")]
public class Reverse_ReductionsController : ControllerBase {
    
    [HttpGet]
    public String getDefault([FromQuery]string chosenReduction) {
        string?[] subdirs = Directory.GetDirectories("Problems/NPComplete")
                            .Select(Path.GetFileName)
                            .ToArray();
                            
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(subdirs, options);
        return jsonString;
    }
}