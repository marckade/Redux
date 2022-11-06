using API.Interfaces;
using API.Interfaces.Graphs;

namespace API.Problems.NPComplete.NPC_ARCSET.Solvers;
class ArcSetBruteForce : ISolver {

    // --- Fields ---
    private string _solverName = "Caleb's Bruteforce Arcset Solver";
    private string _solverDefinition = @" This Solver is a bruteForce solver, which checks all combinations of k edges until a solution is found or its determined there is no solution";
    private string _source = "wikipedia: https://en.wikipedia.org/wiki/Feedback_arc_set";

    private string[] _contributers = { "Alex Diviney"};


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
    public ArcSetBruteForce() {

    }
    private long factorial(long x){
        long y = 1;
        for(long i=1; i<=x; i++){
            y *= i;
        }
        return y;
    }
    private string indexListToCertificate(List<int> indecies, List<Edge> edges){
        string certificate = "";
        foreach(int i in indecies){
            certificate += ","+edges[i].directedString();
        }
        return "{" + certificate.Substring(1) + "}";
    }
    private List<int> nextComb(List<int> combination, int size){
        for(int i=combination.Count-1; i>=0; i--){
            if(combination[i]+1 <= (i + size - combination.Count)){
                combination[i] += 1;
                for(int j = i+1; j < combination.Count; j++){
                    combination[j] = combination[j-1]+1;
                }
                return combination;
            }
        }
        return combination;
    }

    /**
    * Returns the set of edges that if removed from arcset would turn it acyclic
    */
    // public string solve(ARCSET arc){
    //     string retStr = "";
    //     List<Edge> backEdges = arc.directedGraph.DFS();
    //     foreach(Edge be in backEdges){
    //         retStr =retStr + be.directedString()+",";
    //     }
    //     retStr = retStr.TrimEnd(',');
    //     return retStr;        

    // }
    public string solve(ARCSET arc){
        ArcsetGraph graph = arc.directedGraph;
        List<int> combination = new List<int>();
        for(int i=0; i<graph.K; i++){
            combination.Add(i);
        }
        long reps = factorial(graph.getEdgeList.Count) / (factorial(graph.K) * factorial(graph.getEdgeList.Count - graph.K));
        for(int i=0; i<reps; i++){
            string certificate = indexListToCertificate(combination,graph.getEdgeList);
            bool verified = arc.defaultVerifier.verify(arc, certificate);
            if(verified){
                return certificate;
            }
            if(i != reps-1)combination = nextComb(combination, graph.getEdgeList.Count);
        }
        return "{}";
    }

}


// public string solve(CLIQUE clique){
//         List<int> combination = new List<int>();
//         for(int i=0; i<clique.K; i++){
//             combination.Add(i);
//         }
//         long reps = factorial(clique.nodes.Count) / (factorial(clique.K) * factorial(clique.nodes.Count - clique.K));
//         for(int i=0; i<reps; i++){
//             string certificate = indexListToCertificate(combination,clique.nodes);
//             if(clique.defaultVerifier.verify(clique, certificate)){
//                 return certificate;
//             }
//             combination = nextComb(combination, clique.nodes.Count);

//         }
//         // Console.WriteLine(combination.ToString());
//         // Console.WriteLine("n={0} k={1} reps={2}, 5! = {3}",clique.nodes.Count,clique.K,reps,factorial(5));
//         return "{}";
//     }