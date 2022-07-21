using API.Interfaces;

namespace API.Problems.NPComplete.NPC_ExactCover.Verifiers;

class GenericVerifier : IVerifier {

    // --- Fields ---
    private string _verifierName = "Generic Verifier";
    private string _verifierDefinition = "This is a verifier for 3SAT";
    private string _source = " ";
    private string[] _contributers = { "Author Unknown"};

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
    public GenericVerifier() {
        
    }
}