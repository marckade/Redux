using API.Interfaces;
using API.Interfaces.Graphs.GraphParser;

namespace API.Problems.NPComplete.NPC_CUT.Verifiers;

class CutVerifier : IVerifier {

    // --- Fields ---
    private string _verifierName = "Generic Verifier";
    private string _verifierDefinition = "This is a verifier for the Cut problem";
    private string _source = " ";
    private string[] _contributers = {"Caleb Eardley", "Kaden Marchetti, Andrija Sevaljevic"};


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
    public CutVerifier() {
        
    }
    private List<string> parseCertificate(string certificate){

        List<string> nodeList = GraphParser.parseNodeListWithStringFunctions(certificate);
        return nodeList;
    }
    public bool verify(CUT problem, string certificate, string secondSet){
        
        List<string> nodeList = parseCertificate(certificate);
        List<string> nodeList2 = parseCertificate(secondSet);
        int counter = 0;
        //Check k value
        //if(nodeList.Count != problem.K){
        //    return false;
        //}
        foreach(var i in nodeList){
            foreach(var j in nodeList2){
                KeyValuePair<string, string> pairCheck1 = new KeyValuePair<string, string>(i,j);
                KeyValuePair<string, string> pairCheck2 = new KeyValuePair<string, string>(j,i);
                if ((problem.edges.Contains(pairCheck1) || problem.edges.Contains(pairCheck2)) && !i.Equals(j)) {
                    counter++;
                }
                //if(!(problem.edges.Contains(pairCheck1) || problem.edges.Contains(pairCheck2) || i.Equals(j))){
                //    return false;
                //}
            }
        }
        if (counter != problem.K) {
            return false;
        }
        return true;
    }
}