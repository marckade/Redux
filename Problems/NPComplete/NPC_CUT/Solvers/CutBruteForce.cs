using API.Interfaces;
using API.Interfaces.Graphs.GraphParser;
using API.Interfaces.Graphs;

namespace API.Problems.NPComplete.NPC_CUT.Solvers;
class CutBruteForce : ISolver {

    // --- Fields ---
    private string _solverName = "Cut Brute Force";
    private string _solverDefinition = "This is a brute force solver for the Cut problem";
    private string _source = "Andrija Sevaljevic";
    private string[] _contributers = {"Andrija Sevaljevic"};

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

public CutBruteForce() {
        
    }
    private long factorial(long x){
        long y = 1;
        for(long i=1; i<=x; i++){
            y *= i;
        }
        return y;
    }
    private string indexListToCertificate(List<int> indecies, List<string> nodes){
        // Console.WriteLine("indecies: ", indecies.ToString());
        string certificate = "";
        foreach(int i in indecies){
            certificate += nodes[i]+",";
        }
        certificate = certificate.TrimEnd(',');
        // Console.WriteLine("returned statement: {"+certificate+"}");
        return "{" + certificate + "}"; 
    }

    private List<string> parseCertificate(string certificate){

        List<string> nodeList = GraphParser.parseNodeListWithStringFunctions(certificate);
        return nodeList;
    }

// Function below turns certificate into list of edges
  /*  private string certificateToEdges(CUT cut, string certificate) {
        List<string> nodeList = parseCertificate(certificate);
        certificate = "{";
        foreach(var i in nodeList){
             foreach(var j in cut.nodes){
                    
                    KeyValuePair<string, string> pairCheck1 = new KeyValuePair<string, string>(i,j);
                    KeyValuePair<string, string> pairCheck2 = new KeyValuePair<string, string>(j,i);
                        if (cut.edges.Contains(pairCheck1) || cut.edges.Contains(pairCheck2)) { // checks if is being cut
                        certificate += "(" + i + "," + j +"),"; // adds edge
                    }
                
            }
        }
        certificate = certificate.TrimEnd(',');
        certificate += "}";
        return certificate;

    }*/


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
    public string solve(CUT cut){
        for(int i=0; i<cut.K; i++) {
        List<int> combination = new List<int>();
        for(int j=0; j<=i; j++){
            combination.Add(j);
        }
        long reps = factorial(cut.nodes.Count) / (factorial(i + 1) * factorial(cut.nodes.Count - i - 1));
        for(int k=0; k<reps; k++){
            string certificate = indexListToCertificate(combination, cut.nodes);
            if(cut.defaultVerifier.verify(cut, certificate)) {
               // certificate = certificateToEdges(cut, certificate);
                return certificate;
            }
            combination = nextComb(combination, cut.nodes.Count);

        }
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
        CutGraph cGraph = new CutGraph(problemInstance, true);
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

   