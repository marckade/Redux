using API.Interfaces;

namespace API.Problems.NPComplete.NPC_ARCSET.Solvers;
class AlexNaiveSolver : ISolver {

    // --- Fields ---
    private string _solverName = "Alex's Naive Arcset Solver";
    private string _solverDefinition = @"This solver has a complexity of 1/2. Essentially you order edges into two categories, decending and ascending. Then you only return the bigger set. 
                                        This will guarantee all cycles are broken. This solver specifically makes use of a DFS, where the graph is ordered into descending edges and back edges.
                                        This allows us to remove all backedges to break cycles in a less arbitrary way. 
                                        Note that technically, we will leave one back edge in, because we want to return an instance of ARCSET, not the maximum acyclical subgraph.";
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

    public string solve(ARCSET arc){
       KeyValuePair<string,string> backEdgeToReAdd = new KeyValuePair<string, string>("DEFAULT","DEFAULT");
       List<Edge> backEdges =arc.directedGraph.DFS();
        List<KeyValuePair<string,string>> allBackEdges = new List<KeyValuePair<string, string>>();

        foreach(Edge e in backEdges){
            allBackEdges.Add(e.toKVP());
            backEdgeToReAdd = e.toKVP(); //I know that this overwrites every time and is inefficient. 
        }
        allBackEdges.Remove(backEdgeToReAdd);

        List<Node> allNodes = arc.directedGraph.getNodeList;
        string nodeListStr = "{{";
        foreach(Node n in allNodes){
            nodeListStr += n.name + ",";
        }
        char[] charsToTrim = {','};
        nodeListStr = nodeListStr.TrimEnd(charsToTrim);
        nodeListStr = nodeListStr + "} :";
        
        string edgeListString = "{";
        foreach(KeyValuePair<string,string> be in allBackEdges){
            edgeListString += " (" + be.Key + "," + be.Value + ")"; 
            edgeListString += " &";
        }
        
        edgeListString = edgeListString.TrimEnd(new char[] {'&'});
        
        edgeListString += "} : ";
        //Console.WriteLine(edgeListString);
        string lString = "" + allBackEdges.Count + "}";
        string solvedInstanceString = nodeListStr + edgeListString + lString;
    
    //ARCSET solvedInstance = new ARCSET(solvedInstanceString);
    return solvedInstanceString;
    }

    public string prettySolve(ARCSET arc){

        KeyValuePair<string,string> backEdgeToReAdd = new KeyValuePair<string, string>("DEFAULT","DEFAULT");
        List<Edge> backEdges =arc.directedGraph.DFS();
        List<KeyValuePair<string,string>> allBackEdges = new List<KeyValuePair<string, string>>();

         foreach(Edge be in backEdges){
             allBackEdges.Add(be.toKVP());
             backEdgeToReAdd = be.toKVP();
         }
        string solvedInstanceString = "";
        allBackEdges.Remove(backEdgeToReAdd);
        foreach(KeyValuePair<string,string> be in allBackEdges){
            solvedInstanceString += "(" + be.Key + "," + be.Value + ") ";
        }
        solvedInstanceString = solvedInstanceString.Substring(0,solvedInstanceString.Length-1);
        return solvedInstanceString;
    }
}