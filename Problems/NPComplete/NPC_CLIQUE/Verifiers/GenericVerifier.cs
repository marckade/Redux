using API.Interfaces;

namespace API.Problems.NPComplete.NPC_CLIQUE.Verifiers;

class GenericVerifier : IVerifier {

    // --- Fields ---
    private string _verifierName = "Generic Verifier";
    private string _verifierDefinition = "This is a verifier for Clique";
    private string _source = " ";
    private string[] _contributers = {"Caleb Eardley", "Kaden Marchetti"};


    private string _certificate =  "";

    // --- Properties ---
    public string verifierName {
        get {
            return _verifierName;
        }
    }
    public string verifierDefinition {
        get {
            return _verifierDefinition;
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

      public string certificate {
        get {
            return _certificate;
        }
    }


    // --- Methods Including Constructors ---
    public GenericVerifier() {
        
    }
    private List<string> parseCertificate(string certificate){
        List<string> nodeList = new List<string>();
        string tempCert = certificate.Replace(" ","");
        string[] nodeArray = tempCert.Split(",");
        foreach(var node in nodeArray){
            nodeList.Add(node);
        }
        return nodeList;
    }
    public bool verify(CLIQUE problem, string certificate){
        List<string> nodeList = parseCertificate(certificate);
        Console.WriteLine(nodeList.Count);
        // for(int i=0; i<problem.nodes.Count; i++){
        //     Console.WriteLine(problem.nodes[i]);
        // }
        // Console.WriteLine("--------------------------");
        // for(int i=0; i<problem.edges.Count; i++){
        //     Console.WriteLine(problem.edges[i]);
        // }
        // Console.WriteLine("--------------------------");
        // for(int i=0; i<nodeList.Count; i++){
        //     Console.WriteLine(nodeList[i]);
        // }
        foreach(var i in nodeList){
            foreach(var j in nodeList){
                KeyValuePair<string, string> pairCheck1 = new KeyValuePair<string, string>(i,j);
                KeyValuePair<string, string> pairCheck2 = new KeyValuePair<string, string>(j,i);
                if(!(problem.edges.Contains(pairCheck1) || problem.edges.Contains(pairCheck2) || i==j)){
                    Console.WriteLine("false");
                    return false;
                }
            }
        }
        Console.WriteLine("true");
        return true;
    }
}