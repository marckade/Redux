using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;

// Get all Solvers regardless of complexity class
[ApiController]
[Route("Navigation/[controller]")]
public class All_SolversController : ControllerBase {
    
    [HttpGet]
    public String getDefault() {
        string?[] subdirs = Directory.GetDirectories("Solvers")
                            .Select(Path.GetFileName)
                            .ToArray();

        // Not completed. Needs to loop through these directories to get the rest of the problems
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(subdirs, options);
        return jsonString;
    }
}

// Get all Solvers for a specific problem
[ApiController]
[Route("Navigation/[controller]")]
public class Problem_SolversController : ControllerBase {
    
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

        string?[] subfiles = Directory.GetFiles("Problems/" + problemTypeDirectory + "/" + chosenProblem + "/Solvers")
                            .Select(Path.GetFileName)
                            .ToArray();

        // Not completed. Needs to loop through these directories to get the rest of the problems
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(subfiles, options);
        return jsonString;
    }
}

// Get all Solvers for a specific problem (Refactored)
[ApiController]
[Route("Navigation/[controller]")]
public class Problem_SolversRefactorController : ControllerBase {
    
    [HttpGet]
    public String getDefault([FromQuery]string chosenProblem, [FromQuery]string problemType) {

        // Determine the directory to search based on prefix. chosenProblem expected to be a problemName like "NPC_PROBLEM"\
        string problemTypeDirectory = "";

        if (problemType == "NPC") {
            problemTypeDirectory = "NPComplete";
        }
        else if (problemType == "P") {
            problemTypeDirectory = "Polynomial";
        }

        string?[] subfiles = Directory.GetFiles("Problems/" + problemTypeDirectory + "/" + problemType+ "_" + chosenProblem + "/Solvers")
                            .Select(Path.GetFileName)
                            .ToArray();

        ArrayList subFilesList = new ArrayList();
        
        foreach(string file in subfiles){
            string fileNoExt = file.Split('.')[0]; //gets the file without the file extension
            subFilesList.Add(fileNoExt);
        }

        // Not completed. Needs to loop through these directories to get the rest of the problems
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(subFilesList, options);
        return jsonString;
    }
}