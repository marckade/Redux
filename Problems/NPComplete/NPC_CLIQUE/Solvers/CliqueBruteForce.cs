using API.Interfaces;
using API.Interfaces.Graphs.GraphParser;
using API.Interfaces.Graphs;

namespace API.Problems.NPComplete.NPC_CLIQUE.Solvers;
class CliqueBruteForce : ISolver {

    // --- Fields ---
    private string _solverName = "Clique Brute Force";
    private string _solverDefinition = "This is a brute force solver for the NP-Complete Clique problem";
    private string _source = "This person Caleb Eardley";
    private string[] _contributers = {"Caleb Eardley", "Kaden Marchetti"};


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
    public CliqueBruteForce() {
        
    }
    private long factorial(long x){
        long y = 1;
        for(long i=1; i<=x; i++){
            y *= i;
        }
        return y;
    }
    private string indexListToCertificate(List<int> indecies, List<string> nodes){
        string certificate = "";
        foreach(int i in indecies){
            certificate += ","+nodes[i];
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
    public string solve(CLIQUE clique){
        List<int> combination = new List<int>();
        for(int i=0; i<clique.K; i++){
            combination.Add(i);
        }
        long reps = factorial(clique.nodes.Count) / (factorial(clique.K) * factorial(clique.nodes.Count - clique.K));
        for(int i=0; i<reps; i++){
            string certificate = indexListToCertificate(combination,clique.nodes);
            if(clique.defaultVerifier.verify(clique, certificate)){
                return certificate;
            }
            combination = nextComb(combination, clique.nodes.Count);

        }
        // Console.WriteLine(combination.ToString());
        // Console.WriteLine("n={0} k={1} reps={2}, 5! = {3}",clique.nodes.Count,clique.K,reps,factorial(5));
        return "{}";
    }

    /// <summary>
    /// Given Clique instance in string format and solution string, outputs a solution dictionary with 
    /// true values mapped to nodes that are in the solution set else false. 
    /// </summary>
    /// <param name="problemInstance"></param>
    /// <param name="solutionString"></param>
    /// <returns></returns>
    public Dictionary<string,bool> getSolutionDict(string problemInstance, string solutionString){

        Dictionary<string, bool> solutionDict = new Dictionary<string, bool>();
        GraphParser gParser = new GraphParser();
        CliqueGraph cGraph = new CliqueGraph(problemInstance, true);
        List<string> problemInstanceNodes = cGraph.nodesStringList;
        List<string> solvedNodes = gParser.getNodesFromNodeListString(solutionString);
        
        // Remove solvedNodes from instanceNodes
        foreach(string node in solvedNodes){
        problemInstanceNodes.Remove(node);
        // Console.WriteLine("Solved nodes: "+node);
        solutionDict.Add(node, true);
       }
        // Add solved nodes to dict as {name, true}
        // Add remaining instance nodes as {name, false}

        foreach(string node in problemInstanceNodes){
          
                solutionDict.Add(node, false);
        }


        return solutionDict;
    }
}
