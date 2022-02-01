using API.Interfaces;

namespace API.Problems.NPComplete.NPC_3DM.Verifiers;

class GenericVerifier : IVerifier {

    // --- Fields ---
    private string _verifierName = "3-DImensional Matching Verifier";
    private string _verifierDefinition = "This is a verifier for 3DM";
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

    /*************************************************
parseSet(List<string> Set,string phiInput,int start), is meant to take one set inside of a string, and put it into an array
it is refferenced in PareseCertificate,  Set should be an empty List<string>, created by ParseProblem, phiInput should be 
input of PareseProblem, and start should be the index of the '{' at the begining of the set in the string. It works by
iterating through each charecter from the start index, until it reaches the end of the set '}'. and crestes string of anything
between ','s excluding spaces, and places those strings inside Set.
**************************************************/
    private void parseSet(List<string> Set,string phiInput,int start){
        int i = start + 1;
        string temp = "";
        while(phiInput[i]!= '}'){
            if(phiInput[i] == ','){
                if(temp != ""){Set.Add(temp);}
                temp = "";
                i++;
            }
            else if(phiInput[i] != ' '){
                temp += phiInput[i];
                i++;
            }
            else{i++;}
        }
        Set.Add(temp);
        return;
    }

/*************************************************
ParseCertificate(string certificate) takes the string representation of the 3-Dimensional Matching solution, and returns a 
2 dimensional list, Each inner lists will be sets of 3 elements.
***************************************************/
    private List<List<string>> ParseCertificate(string certificate) {
        List<List<string>> Certificate = new List<List<string>>();
        for(int i = 0; i< certificate.Length; i++){
            if(certificate[i] == '{'){
                List<string> Set = new List<string>();
                parseSet(Set,certificate,i);
                Certificate.Add(Set);
            }
        }
        return Certificate;

    }

    
    public bool Verify(List<List<List<string>>> Problem, string c){
        bool match;
        List<List<string>> certificate = ParseCertificate(c);
        if(!certificate.Except(Problem[1]).Any()){  // Checks if c is a subset of U
            Console.WriteLine("c is not a subset");
            return false;
        }
        if(certificate.Count != Problem[0][0].Count){   //Checks is c is the size of A, if not it cannot conatin each element.
            Console.WriteLine("c is not the right size");
            return false;   
        }
        foreach(var set in Problem[0]){   //Checks that each element of A B and C are in a set of c
            foreach(var item in set){
                match = false;
                foreach(var c_set in certificate){
                    if(c_set.Contains(item)){
                        match = true;
                    }
                }
                if(match == false){
                    Console.WriteLine(item + " is not in the certificate");
                    return false;
                }
            }
        }

        return true;
    }
}

