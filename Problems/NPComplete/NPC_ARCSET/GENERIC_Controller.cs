// using Microsoft.AspNetCore.Mvc;
// using API.Problems.NPComplete.NPC_SAT3;
// using System.Text.Json;
// using System.Text.Json.Serialization;

// namespace API.Problems.NPComplete.NPC_SAT3;

// [ApiController]
// [Route("[controller]")]
// public class testController : ControllerBase {
    
//     public String test() {
//         SAT3 testObj = new SAT3();

//         if (testObj.phi == null) {
//             return testObj.defaultInstance;
//         }
//         else {
//             return "REALLY? API!";
//         }
//     }
// }

// [ApiController]
// [Route("[controller]")]
// public class SAT3GenericController : ControllerBase {

//     [HttpGet]
//     public String getDefault() {
//         var options = new JsonSerializerOptions { WriteIndented = true };
//         string jsonString = JsonSerializer.Serialize(new SAT3(), options);
//         return jsonString;
//     }

//     [HttpGet("{instance}")]
//     public String getInstance() {
//         var options = new JsonSerializerOptions { WriteIndented = true };
//         string jsonString = JsonSerializer.Serialize(new SAT3(), options);
//         return jsonString;
//     }


// }

