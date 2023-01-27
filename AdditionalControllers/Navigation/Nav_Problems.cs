using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Collections;


// Get all problems regardless of complexity class
[ApiController]
[Route("Navigation/[controller]")]
public class All_ProblemsController : ControllerBase {
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    public String getDefault() {
        string projectSourcePath = ProjectSourcePath.Value;
        string?[] subdirs = Directory.GetDirectories(projectSourcePath+ @"Problems")
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
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]

    public String getDefault() {
        string projectSourcePath = ProjectSourcePath.Value;
        string?[] subdirs = Directory.GetDirectories(projectSourcePath+ @"Problems/NPComplete")
                            .Select(Path.GetFileName)
                            .ToArray();
                            
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(subdirs, options);

        //Response.Headers.Add("Access-Control-Allow-Origin", "http://127.0.0.1:5500");
        return jsonString;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("json")]
    public string getProblemsJson(){
        string projectSourcePath = ProjectSourcePath.Value;
        string objectPrefix = "problemName";
        string?[] subdirs = Directory.GetDirectories(projectSourcePath+ @"Problems/NPComplete")
                            .Select(Path.GetFileName)
                            .ToArray();

        ArrayList jsonedList = new ArrayList();
        foreach(string problemInstance in subdirs){
        string jsonPair = $"{{\"{objectPrefix}\" : \"{problemInstance}\"}}";
            jsonedList.Add(jsonPair);
        }
        
         var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(jsonedList, options);
        return jsonString;
    }
}


// Get only NP-Complete problems
[ApiController]
[Route("Navigation/[controller]")]
public class NPC_ProblemsRefactorController : ControllerBase {
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]

    public String getDefault() {
        // File system patch that should work on both Window/Linux enviroments
        string projectSourcePath = ProjectSourcePath.Value;
        string?[] subdirs = Directory.GetDirectories(projectSourcePath+ @"/Problems/NPComplete")
                        .Select(Path.GetFileName)
                        .ToArray();

        ArrayList subdirsNoPrefix = new ArrayList();
        foreach(string problemDirName in subdirs){
            string[] splitStr = problemDirName.Split('_');
            string newName = splitStr[1];
            subdirsNoPrefix.Add(newName);
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(subdirsNoPrefix, options);

        // ProblemGraph graph = new ProblemGraph();
        // graph.getConnectedNodes("SAT3");
        // string ring = JsonSerializer.Serialize(graph.getConnectedNodes("SAT3"), options);
        //  Console.WriteLine("\n"+ring );

        //Response.Headers.Add("Access-Control-Allow-Origin", "http://127.0.0.1:5500");
        return jsonString;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("json")]
    public string getProblemsJson(){
        string projectSourcePath = ProjectSourcePath.Value;
        string objectPrefix = "problemName";
        string?[] subdirs = Directory.GetDirectories(projectSourcePath+ @"Problems/NPComplete")
                            .Select(Path.GetFileName)
                            .ToArray();

        ArrayList jsonedList = new ArrayList();
        foreach(string problemInstance in subdirs){
        string jsonPair = $"{{\"{objectPrefix}\" : \"{problemInstance}\"}}";
            jsonedList.Add(jsonPair);
        }
        
         var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(jsonedList, options);
        return jsonString;
    }
}

[ApiController]
[Route("Navigation/[controller]")]
public class NPC_NavGraph : ControllerBase {

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("info")]
    public string getProblemGraph(){
        ProblemGraph nav_graph = new ProblemGraph();
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(nav_graph.graph, options);

        return jsonString;

    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("availableReductions")]
    public string getConnectedProblems([FromQuery]string chosenProblem){
        ProblemGraph nav_graph = new ProblemGraph();
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(nav_graph.getConnectedProblems(chosenProblem.ToLower()), options);

        return jsonString;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("reductionPath")]
    public string getPaths([FromQuery]string reducingFrom, string reducingTo){
        ProblemGraph nav_graph = new ProblemGraph();
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(nav_graph.getReductionPath(reducingFrom.ToLower(),reducingTo.ToLower()), options);

        return jsonString;
    }


}