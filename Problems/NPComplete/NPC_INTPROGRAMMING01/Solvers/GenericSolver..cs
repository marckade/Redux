using API.Interfaces;

namespace API.Problems.NPComplete.NPC_INTPROGRAMMING01.Solvers;
class GenericSolver : ISolver {

    // --- Fields ---
    private string _solverName = "Generic Solver";
    private string _solverDefinition = "This is a generic solver for 0-1 Integer Programming";
    private string _source = "This person ____";

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