using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using API.Problems.NPComplete.NPC_SAT;
using API.Problems.NPComplete.NPC_SAT.Solvers;
using API.Problems.NPComplete.NPC_SAT.Verifiers;
namespace API.Problems.NPComplete.NPC_SAT;



    
    [ApiController]
    [Route("[controller]")]
    public class SATGenericController : ControllerBase {
        [HttpGet]

        public String getDefault() {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(new SAT(), options);
            return jsonString;
        }

        [HttpGet("{instance}")]
        public String getInstance() {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(new SAT(), options);
            return jsonString;
        }
        
    }

    [ApiController]
    [Route("[controller]")]
    public class IgbokweSATVerifierController : ControllerBase {

        [HttpGet("info")]
        public String getGeneric(){
            var options = new JsonSerializerOptions {WriteIndented = true};
            IgbokweSATVerifier verifier = new IgbokweSATVerifier();


            string jsonString  = JsonSerializer.Serialize(verifier, options);
            return jsonString; 
        }

        //[HttpGet("{certificate}/{problemInstance}")]
        [HttpGet("solve")]
        public String getInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {

           
         return "";
        }

}




