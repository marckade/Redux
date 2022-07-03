using API.Interfaces;

namespace API.Problems.NPComplete.NPC_SUBSETSUM.Verifiers;

class GenericVerifier : IVerifier {

    // --- Fields ---
    private string _verifierName = "Generic Verifier";
    private string _verifierDefinition = "This is a verifier for Subset Summ";
    private string _source = " ";

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

     public string certificate {
        get {
            return _certificate;
        }
    }


    // --- Methods Including Constructors ---
    public GenericVerifier() {
        
    }
}