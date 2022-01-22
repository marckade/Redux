using API.Interfaces;

namespace API.Problems.NPComplete.NPC_SAT3.Verifiers;

class GenericVerifier : IVerifier<SAT3> {

    // --- Fields ---
    private string _verifierDefinition = "This is a verifier for 3SAT";
    private string _source = " ";
    private SAT3 _verifierFor = null;

    // --- Properties ---
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
    public SAT3 verifierFor {
        get {
            return _verifierFor;
        }
        set {
            _verifierFor = value;
        }
    }

    // --- Methods Including Constructors ---
    public GenericVerifier(SAT3 verifyingFor) {
        _verifierFor = verifyingFor;
        
    }
}