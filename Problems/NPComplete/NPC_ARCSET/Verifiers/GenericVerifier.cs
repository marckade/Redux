using API.Interfaces;

namespace API.Problems.NPComplete.NPC_ARCSET.Verifiers;

class GenericVerifier : IVerifier {

    // --- Fields ---
    private string _verifierName = "ARCSET Verifier";
    private string _verifierDefinition = "This is a verifier for ARCSET";
    private string _source = " ";

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

    // --- Methods Including Constructors ---
    public GenericVerifier() {
        
        //Note that the certificate of our default ARCSET instance would be the set {(2,4)} where the set has a length 1 and position 0 is the tuple (2,4) 
    }
}