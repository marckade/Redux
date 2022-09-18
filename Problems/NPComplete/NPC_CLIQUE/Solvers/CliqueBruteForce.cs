using API.Interfaces;

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
        return certificate.Substring(1);
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
                return "{"+certificate+"}";
            }
            combination = nextComb(combination, clique.nodes.Count);

        }
        return "{}";
    }
}
