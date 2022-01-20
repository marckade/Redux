using Microsoft.AspNetCore.Mvc;

namespace API.Problems.NPComplete.SAT3;

[ApiController]
[Route("[controller]")]
public class testController : ControllerBase {
    
    public String test() {
        return "REALLY? API!";
    }
}