using API.Interfaces;

namespace API.Problems.NPComplete.NPC_ExactCover.Solvers;
class GenericSolver : ISolver {

    // --- Fields ---
    private string _solverName = "Generic Solver";
    private string _solverDefinition = "This is a generic solver for SAT3";
    private string _source = "This person ____";
    private string[] _contributers = { "Unknown Author"};


    // --- Properties ---
    public string solverName {
        get {
            return _solverName;
        }
    }
    public string solverDefinition {
        get {
            return _solverDefinition;
        }
    }
    public string source {
        get {
            return _source;
        }
    }
    public string[] contributers{
        get{
            return _contributers;
        }
    }
    // --- Methods Including Constructors ---
    public GenericSolver() {
        
    }
}