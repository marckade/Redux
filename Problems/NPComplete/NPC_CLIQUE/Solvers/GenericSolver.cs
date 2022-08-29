using API.Interfaces;

namespace API.Problems.NPComplete.NPC_CLIQUE.Solvers;
class GenericSolver : ISolver {

    // --- Fields ---
    private string _solverName = "Generic Solver";
    private string _solverDefinition = "This is a generic solver for SAT3";
    private string _source = "This person ____";
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
    public GenericSolver() {
        
    }
    private int factorial(int x){
        int y = 1;
        for(int i=x; i>0; i--){
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
                // Console.WriteLine("i :{0}, size:{1}, count:{2}, i+size-count: {3}",i,size,combination.Count,i + size - combination.Count);
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
        // Console.WriteLine("factorial of 10: {0}",factorial(10));
        List<int> combination = new List<int>();
        for(int i=0; i<clique.K; i++){
            combination.Add(i);
        }
        int reps = factorial(clique.nodes.Count) / factorial(clique.K) * factorial(clique.nodes.Count - clique.K);
        for(int i=0; i<reps; i++){
            // Console.WriteLine(indexListToCertificate(combination,clique.nodes));
            // Console.WriteLine("{0}{1}{2}",combination[0],combination[1],combination[2]);
            // Console.WriteLine(clique.nodes.Count);
            // nextComb(combination, clique.nodes.Count);
            string certificate = indexListToCertificate(combination,clique.nodes);
            Console.WriteLine(certificate);
            if(clique.defaultVerifier.verify(clique, certificate)){
                return certificate;
            }
            combination = nextComb(combination, clique.nodes.Count);

        }
        return "none";
    }
}