using API.Interfaces;

namespace API.Problems.NPComplete.NPC_CLIQUE.Solvers;
class GenericSolver : ISolver<CLIQUE> {

    // --- Fields ---
    private string _solverDefinition = "This is a generic solver for SAT3";
    private string _source = "This person ____";
    private CLIQUE _solverFor = null;

    // --- Properties ---
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
    public CLIQUE solverFor {
        get {
            return _solverFor;
        }
        set {
            _solverFor = value;
        }
    }
    // --- Methods Including Constructors ---
    public GenericSolver(CLIQUE solvingFor) {
        _solverFor = solvingFor;
    }
}