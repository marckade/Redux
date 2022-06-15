using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;

// Get all Verifiers regardless of complexity class
[ApiController]
[Route("Navigation/[controller]")]
public class All_VerifiersController : ControllerBase {
    
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

        string?[] subfiles = Directory.GetFiles("Problems/" + problemTypeDirectory + "/" + chosenProblem + "/Verifiers")
                            .Select(Path.GetFileName)
                            .ToArray();

        // Not completed. Needs to loop through these directories to get the rest of the problems
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(subfiles, options);
        return jsonString;
    }
}
// Get all Verifiers for a specific problem\
[ApiController]
[Route("Navigation/[controller]")]
public class Problem_VerifiersController : ControllerBase {
    
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

        string?[] subfiles = Directory.GetFiles("Problems/" + problemTypeDirectory + "/" + chosenProblem + "/Verifiers")
                            .Select(Path.GetFileName)
                            .ToArray();

        // Not completed. Needs to loop through these directories to get the rest of the problems
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(subfiles, options);
        return jsonString;
    }
}

// Get all Verifiers for a specific problem\
[ApiController]
[Route("Navigation/[controller]")]
public class Problem_VerifiersRefactorController : ControllerBase {
    
    [HttpGet]
    public String getDefault([FromQuery]string chosenProblem,[FromQuery]string problemType) {
                string NOT_FOUND_ERR_VERIFIER = "entered a verifier that does not exist";

        // Determine the directory to search based on prefix. chosenProblem expected to be a problemName like "NPC_PROBLEM"\
        string problemTypeDirectory = "";
        string jsonString = "";
        var options = new JsonSerializerOptions { WriteIndented = true };

        if (problemType == "NPC") {
            problemTypeDirectory = "NPComplete";
        }
        else if (problemType == "P") {
            problemTypeDirectory = "Polynomial";
        }

        try
        {

            string?[] subfiles = Directory.GetFiles("Problems/" + problemTypeDirectory + "/" + problemType + "_" + chosenProblem + "/Verifiers")
                                .Select(Path.GetFileName)
                                .ToArray();

            ArrayList subFilesList = new ArrayList();

            foreach (string file in subfiles)
            {
                string fileNoExt = file.Split('.')[0]; //gets the file without the file extension
                subFilesList.Add(fileNoExt);
            }

            // Not completed. Needs to loop through these directories to get the rest of the problems
            jsonString = JsonSerializer.Serialize(subFilesList, options);
        }
        catch (System.IO.DirectoryNotFoundException)
        {
            jsonString = JsonSerializer.Serialize(NOT_FOUND_ERR_VERIFIER, options);

        }
        return jsonString;
    }
}