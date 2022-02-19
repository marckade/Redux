using API.Interfaces;

namespace API.Problems.NPComplete.NPC_INTPROGRAMMING0_1.Verifiers;

class GenericVerifier : IVerifier {

    // --- Fields ---
    private string _verifierName = "Generic Verifier";
    private string _verifierDefinition = "This is a verifier for 0-1 Integer Programming";
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
        
    }
    public List<int> parseCertificate(string certificate){
        List<int> c = new List<int>();
        string[] stringVector = certificate.Replace("(","").Replace(")","").Split(" ");
        for(int i=0; i<stringVector.Length; i++){
            c.Add(int.Parse(stringVector[i]));
        }
        return c;
    }
    public bool Verify(INTPROGRAMMING0_1 Problem, string c){
        List<int> certificate = parseCertificate(c);
        
        //checks that the certificate is the correct size
        if(certificate.Count != Problem.C[0].Count){return false;}

        //compute C*certificate, or Cx
        List<int> solution = new List<int>();
        foreach(var row in Problem.C){
            int value = 0;
            for(int i = 0; i< row.Count; i++){
                value += row[i]*certificate[i];
            }
            solution.Add(value);
        }

        //checks that C*solution <= d
        for(int i=0; i<Problem.d.Count; i++){
            if(!(solution[i] <= Problem.d[i])){return false;}
        }

        return true; 
    }
}