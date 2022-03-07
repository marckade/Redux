using API.Interfaces;

namespace API.Problems.NPComplete.NPC_3DM.Solvers;
class HurkensShrijver : ISolver {

    // --- Fields ---
    private string _solverName = "Generic Solver";
    private string _solverDefinition = "This is a generic solver for 3DM";
    private string _source = "This person ____";

    // --- Properties ---
    public string solverName {
        get {
            return _solverName;
        }
    }
    public string solverDefinition {
        get {
            return _solverDefinition;
        }
    }
    public string source {
        get {
            return _source;
        }
    }
    // --- Methods Including Constructors ---
    public HurkensShrijver() {

    }

    public List<List<string>> solve(THREE_DM problem){
        List<List<string>> S = new List<List<string>>();
        List<List<string>> M = problem.M;
        HashSet<string> SHash = new HashSet<string>();
        

        S.Add(M[0]);
        SHash.Add(S[0][0]);SHash.Add(S[0][1]);SHash.Add(S[0][2]);
        M.RemoveAt(0);

        while(S.Count < problem.X.Count){
            foreach(var setS in S){
                SHash.Remove(setS[0]);SHash.Remove(setS[1]);SHash.Remove(setS[2]);
                foreach(var setM1 in M){
                    foreach(var setM2 in M){
                        if(setM1 == setM2){continue;}
                        bool works = true;
                        foreach(var element in setM1)
                            if(SHash.Contains(element)){
                                works = false;
                            }
                        foreach(var element in setM2)
                            if(SHash.Contains(element)){
                                works = false;
                            }
                        if(works == true){
                            SHash.Add(setM1[0]);SHash.Add(setM1[1]);SHash.Add(setM1[2]);                            
                            SHash.Add(setM2[0]);SHash.Add(setM2[1]);SHash.Add(setM2[2]);
                            S.Add(setM1);S.Add(setM2);
                        }
                        
                    }
                }
                
            }

        }

        return S;
    }
}