using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;

// Get all Solvers regardless of complexity class
[ApiController]
[Route("Navigation/[controller]")]
public class All_SolversController : ControllerBase {
    
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    public String getDefault() {
        string projectSourcePath = ProjectSourcePath.Value;
        string?[] subdirs = Directory.GetDirectories(projectSourcePath+ @"/Solvers")
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
    
    [ApiExplorerSettings(IgnoreApi = true)]
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

        string projectSourcePath = ProjectSourcePath.Value;
        string?[] subfiles = Directory.GetFiles(projectSourcePath+ @"Problems/" + problemTypeDirectory + "/" + chosenProblem + "/Solvers")
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
            string NOT_FOUND_ERR_SOLVER = "entered a solver that does not exist";

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    public String getDefault([FromQuery]string chosenProblem, [FromQuery]string problemType) {

        // Determine the directory to search based on prefix. chosenProblem expected to be a problemName like "NPC_PROBLEM"\
        string problemTypeDirectory = "";
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = "";

        if (problemType == "NPC") {
            problemTypeDirectory = "NPComplete";
        }
        else if (problemType == "P") {
            problemTypeDirectory = "Polynomial";
        }


        try
        {
            string projectSourcePath = ProjectSourcePath.Value;
            string?[] subfiles = Directory.GetFiles(projectSourcePath+ @"Problems/" + problemTypeDirectory + "/" + problemType + "_" + chosenProblem + "/Solvers")
                                .Select(Path.GetFileName)
                                .ToArray();

            ArrayList subFilesList = new ArrayList();

            foreach (string file in subfiles)
            {
                string fileNoExt = file.Split('.')[0]; //gets the file without the file extension
                subFilesList.Add(fileNoExt);
            }

             // Note -Caleb- the following is a temp solution to solve 3SAT using a clique solver remove, when
             // this is implemented to work for all problems

             if(chosenProblem == "SAT3"){
                subFilesList.Add("CliqueBruteForce - via SipserReduceToCliqueStandard");
             }

             jsonString = JsonSerializer.Serialize(subFilesList, options);


        }
        catch(System.IO.DirectoryNotFoundException){
            
            jsonString = JsonSerializer.Serialize(NOT_FOUND_ERR_SOLVER,options);
        }
        
        // Not completed. Needs to loop through these directories to get the rest of the problems
        return jsonString;
    }
}