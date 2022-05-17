using API.Interfaces;
using API.Problems.NPComplete.NPC_SAT;

namespace API.Problems.NPComplete.NPC_SAT.Solvers;

    public class GenericSolver : ISolver {


    #region Fields

    // --- Fields ---
    private string _solverName = "Generic Solver";
    private string _solverDefinition = "This is a generic solver for SAT";
    private string _source = "This person ____";
    private string _complexity = "";


    #endregion


    #region Properties
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

    public string complexity {
        get {
            return _complexity;
        }

        set{
            _complexity = value;
        }
    }

    #endregion

    #region Constructors
    // --- Methods Including Constructors ---
    public GenericSolver() {

    }
    #endregion


    #region Methods 

    #endregion
        
    }
