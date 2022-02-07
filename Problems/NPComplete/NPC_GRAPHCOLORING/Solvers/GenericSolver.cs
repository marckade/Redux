using API.Interfaces;

namespace API.Problems.NPComplete.NPC_GRAPHCOLORING.Solvers;
class GenericSolver : ISolver {

    // --- Fields ---
    private string _solverName = "Generic Solver";
    private string _solverDefinition = "This is a generic solver for GRAPHCOLORING";
    private string _source = "";

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
    // --- Methods Including Constructors ---
    public GenericSolver() {

    }

}