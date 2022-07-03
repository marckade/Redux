using API.Interfaces;

namespace API.Problems.NPComplete.NPC_DM3.Verifiers;

class GenericVerifierDM3 : IVerifier {

    // --- Fields ---
    private string _verifierName = "Generic 3-Dimensional Matching Verifier";
    private string _verifierDefinition = "This verifier checks that a given certificate is the correct size, and contains all elements of X, Y and Z";
    private string _source = "Caleb Eardley";


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
    public GenericVerifierDM3() {
        
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
2-dimensional list, Each inner lists will be sets of 3 elements. 
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

    // Take in a problem and a possible solution and evaluate it. Expected userInput follows the format ({Matching in solution}{Matching in solution}{Matching in solution}...)
    // EXAMPLE: "{x1,y2,z4}{x2,y1,z1}{x2,y1,z2}{x2,y2,z1}"
    // ONLY true literal names should be included in the user input seperated by commas
    public Boolean verify(DM3 Problem, string c){
        bool match;
        List<List<string>> certificate = ParseCertificate(c);
        if(!certificate.Except(Problem.M).Any()){  // Checks if c is a subset of M
            //Console.WriteLine("c is not a subset");
            return false;
        }
        if(certificate.Count != Problem.X.Count){   //Checks is c is the size of X, if not it cannot conatin each element.
            //Console.WriteLine("c is not the right size");
            return false;   
        }
        foreach(var set in Problem.problem[0]){   //Checks that each element of X Y and Z are in a set of c
            foreach(var item in set){
                match = false;
                foreach(var c_set in certificate){
                    if(c_set.Contains(item)){
                        match = true;
                    }
                }
                if(match == false){
                    //Console.WriteLine(item + " is not in the certificate");
                    return false;
                }
            }
        }

        return true;
    }
}

