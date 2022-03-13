using API.Interfaces;

namespace API.Problems.NPComplete.NPC_ARCSET.Solvers;
class AlexNaiveSolver : ISolver {

    // --- Fields ---
    private string _solverName = "Alex's Naive Arcset Solver";
    private string _solverDefinition = @"This solver has a complexity of 1/2. Essentially you order edges into two categories, decending and ascending. Then you only return the bigger set. 
                                        This will guarantee all cycles are broken. This solver specifically makes use of a DFS, where the graph is ordered into descending edges and back edges.
                                        This allows us to remove all backedges to break cycles in a less arbitrary way. 
                                        Note that technilly, we will leave one back edge in, because we want to return an instance of ARCSET, not the maximum acyclical subgraph.";
    private string _source = "wikipedia: https://en.wikipedia.org/wiki/Feedback_arc_set";

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
    public AlexNaiveSolver() {



    }
}