using API.Interfaces;
using API.Interfaces.Graphs.GraphParser;

namespace API.Problems.NPComplete.NPC_PARTITION.Verifiers;

class PartitionVerifier : IVerifier {

    // --- Fields ---
    private string _verifierName = "Subset Sum Verifier";
    private string _verifierDefinition = "This is a verifier for Subset Summ";
    private string _source = " ";
    private string[] _contributers = { "Garret Stouffer"};

    private string _certificate = "";

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
    public PartitionVerifier() {
        
    }

    public bool verify(PARTITION partition, string certificate){
        string cleanedInput = certificate.Trim('{', '}');
        string[] tupleStrings = cleanedInput.Split("),(");
        List<string> c = tupleStrings[0].Replace("{","").Replace("}","").Replace(" ","").Split(",").ToList();
        List<string> c2 = tupleStrings[1].Replace("{","").Replace("}","").Replace(" ","").Split(",").ToList();

        int sum = 0;
        int secondSum = 0;

        foreach(string a in c){
            if(partition.S.Contains(a)){    
                sum += int.Parse(a);
            }
            else{
                return false;
            }
        }
        

        
        if(sum == secondSum){
            return true;
        }


        return false;
    }

    
}