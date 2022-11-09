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
        public String getInfo(){
            var options = new JsonSerializerOptions {WriteIndented = true};
            IgbokweSATVerifier verifier = new IgbokweSATVerifier();


            string jsonString  = JsonSerializer.Serialize(verifier, options);
            return jsonString; 
        }

        //[HttpGet("{certificate}/{problemInstance}")]
        [HttpGet("verify")]
        public String getInstance([FromQuery]string certificate, [FromQuery]string problemInstance) {
            var options = new JsonSerializerOptions { WriteIndented = true };
            SAT SATProblem = new SAT(problemInstance);
            IgbokweSATVerifier verifier = new IgbokweSATVerifier();

            Boolean response = verifier.verify(SATProblem,certificate);
            // Send back to API user
            string jsonString = JsonSerializer.Serialize(response.ToString(), options);
            return jsonString;

            
            //return "";
        }

    }

    [ApiController]
    [Route("[controller]")]
    public class SATBruteForceSolverController : ControllerBase {
        [HttpGet("info")]
        public String getInfo(){
            var options = new JsonSerializerOptions { WriteIndented = true };
            SATBruteForceSolver solver = new SATBruteForceSolver();

            // Send back to API user
            string jsonString = JsonSerializer.Serialize(solver, options);
            return jsonString;
        }

        [HttpGet("solve")]
        public String solveInstance([FromQuery]string problemInstance) {
            var options = new JsonSerializerOptions { WriteIndented = true };
            SATBruteForceSolver solver = new SATBruteForceSolver();

            string testString = solver.Solver(problemInstance);
            string jsonString = JsonSerializer.Serialize(testString, options);
            return jsonString;
        }
    }




