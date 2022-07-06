using API.Interfaces;
using API.Problems.NPComplete.NPC_SAT;

namespace API.Problems.NPComplete.NPC_SAT.Verifiers;

    public class GenericVerifier : IVerifier {

    #region Fields
    private string _verifierName = "Generic Verifier";
    private string _verifierDefinition = "This is a verifier for SAT";
    private string _source = " ";
    private string _complexity = " ";

    private string _certificate = "";
    
    #endregion

    #region Properties

    // --- Properties ---
    public string verifierName
    {
        get
        {
            return _verifierName;
        }
    }
    public string verifierDefinition
    {
        get
        {
            return _verifierDefinition;
        }
    }
    public string source {
        get
        {
            return _source;
        }
    }
    public string complexity {
        get {
            return _complexity;
        }

        set{
            _complexity = value;
        }
    }

      public string certificate {
        get {
            return _certificate;
        }
    }


    #endregion 

    #region Constructors

    // --- Methods Including Constructors ---
    public GenericVerifier() {

    }

    #endregion

    #region Methods

    #endregion
        
    }
