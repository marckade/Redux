using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Collections;


// Get all problems regardless of complexity class
[ApiController]
[Route("Navigation/[controller]")]
public class All_ProblemsController : ControllerBase {
    
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

        //Response.Headers.Add("Access-Control-Allow-Origin", "http://127.0.0.1:5500");
        return jsonString;
    }

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