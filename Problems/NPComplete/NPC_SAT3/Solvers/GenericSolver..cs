using API.Interfaces;

namespace API.Problems.NPComplete.NPC_SAT3.Solvers;
class GenericSolver : ISolver<SAT3> {

    // --- Fields ---
    private string _solverDefinition = "This is a generic solver for SAT3";
    private string _source = "This person ____";
    private SAT3 _solverFor = null;

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
    public SAT3 solverFor {
        get {
            return _solverFor;
        }
        set {
            _solverFor = value;
        }
    }
    // --- Methods Including Constructors ---
    public GenericSolver(SAT3 solvingFor) {
        _solverFor = solvingFor;
    }
}