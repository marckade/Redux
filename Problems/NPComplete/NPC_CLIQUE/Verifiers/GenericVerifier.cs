using API.Interfaces;

namespace API.Problems.NPComplete.NPC_CLIQUE.Verifiers;

class GenericVerifier : IVerifier<CLIQUE> {

    // --- Fields ---
    private string _verifierDefinition = "This is a verifier for 3SAT";
    private string _source = " ";
    private CLIQUE _verifierFor = null;

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
    public CLIQUE verifierFor {
        get {
            return _verifierFor;
        }
        set {
            _verifierFor = value;
        }
    }

    // --- Methods Including Constructors ---
    public GenericVerifier(CLIQUE verifyingFor) {
        _verifierFor = verifyingFor;
    }
}