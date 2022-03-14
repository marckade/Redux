using API.Interfaces;

namespace API.Problems.NPComplete.NPC_SAT3.Solvers;
class SkeletonSolver : ISolver {

    // --- Fields ---
    private string _solverName = "Generic Solver";
    private string _solverDefinition = "This is a skeleton for the solver for SAT3";
    private string _source = "Kaden";

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
    public SkeletonSolver() {

    }

    // Return type varies
    public Dictionary<string, bool> solve() {

        // Logic goes here
        return new Dictionary<string, bool>();
    }
}