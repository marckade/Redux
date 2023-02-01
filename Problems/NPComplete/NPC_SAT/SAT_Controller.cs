using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using API.Problems.NPComplete.NPC_SAT;
using API.Problems.NPComplete.NPC_SAT.Solvers;
using API.Problems.NPComplete.NPC_SAT.Verifiers;
namespace API.Problems.NPComplete.NPC_SAT;



    
[ApiController]
[Route("[controller]")]
[Tags("SAT")]
#pragma warning disable CS1591
public class SATGenericController : ControllerBase {
#pragma warning restore CS1591

///<summary>Returns a default SAT problem object</summary>

    [ProducesResponseType(typeof(SAT), 200)]
    [HttpGet]

        public String getDefault() {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(new SAT(), options);
            return jsonString;
        }

///<summary>Returns a SAT problem object created from a given instance </summary>
///<param name="problemInstance" example="(x1 | !x2 | x3) &amp; (!x1 | x3 | x1) &amp; (x2 | !x3 | x1)">SAT problem instance string.</param>
///<response code="200">Returns SAT problem object</response>

    [ProducesResponseType(typeof(SAT), 200)]
    [HttpGet("instance")]
        public String getInstance(string problemInstance) {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(new SAT(problemInstance), options);
            return jsonString;
        }
        
    }

[ApiController]
[Route("[controller]")]
[Tags("SAT")]

    #pragma warning disable CS1591
    public class IgbokweSATVerifierController : ControllerBase {
    #pragma warning restore CS1591

///<summary>Returns a info about the SAT generic Verifier </summary>
///<response code="200">Returns IgbokweSATVerifier object</response>

    [ProducesResponseType(typeof(IgbokweSATVerifier), 200)]
    [HttpGet("info")]
    public String getInfo(){
        var options = new JsonSerializerOptions {WriteIndented = true};
        IgbokweSATVerifier verifier = new IgbokweSATVerifier();


        string jsonString  = JsonSerializer.Serialize(verifier, options);
        return jsonString; 
    }

        //[HttpGet("{certificate}/{problemInstance}")]
///<summary>Verifies if a given certificate is a solution to a given SAT problem</summary>
///<param name="certificate" example="(x1:True)">certificate solution to SAT problem.</param>
///<param name="problemInstance" example="(x1 | !x2 | x3) &amp; (!x1 | x3 | x1) &amp; (x2 | !x3 | x1)">SAT problem instance string.</param>
///<response code="200">Returns a boolean</response>
    
    [ProducesResponseType(typeof(Boolean), 200)]
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
[Tags("SAT")]

    #pragma warning disable CS1591
    public class SATBruteForceSolverController : ControllerBase {
    #pragma warning restore CS1591

///<summary>Returns a info about the SAT brute force solver </summary>
///<response code="200">Returns SATBruteForceSolver solver object</response>

    [ProducesResponseType(typeof(SATBruteForceSolver), 200)]
    [HttpGet("info")]
        public String getInfo(){
            var options = new JsonSerializerOptions { WriteIndented = true };
            SATBruteForceSolver solver = new SATBruteForceSolver();

            // Send back to API user
            string jsonString = JsonSerializer.Serialize(solver, options);
            return jsonString;
        }
///<summary>Returns a solution to a given SAT problem instance </summary>
///<param name="problemInstance" example="(x1 | !x2 | x3) &amp; (!x1 | x3 | x1) &amp; (x2 | !x3 | x1)">SAT problem instance string.</param>
///<response code="200">Returns solution string </response>
    
    [ProducesResponseType(typeof(string), 200)]
    [HttpGet("solve")]
        public String solveInstance([FromQuery]string problemInstance) {
            var options = new JsonSerializerOptions { WriteIndented = true };
            SATBruteForceSolver solver = new SATBruteForceSolver();

            string testString = solver.Solver(problemInstance);
            string jsonString = JsonSerializer.Serialize(testString, options);
            return jsonString;
        }
    }




