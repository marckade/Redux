using API.Interfaces;



namespace API.Problems.NPComplete.NPC_VERTEXCOVER.Solvers;
class VCSolverJanita : ISolver {

    // --- Fields ---
    private string _solverName = "Generic Solver";
    private string _solverDefinition = "This approximation solver is a naive solver for Vertex Cover that does not have a clear origination, although there have been many improvements upon it published. It returns the cover of size 2n when the optimal solution is n. This solver was sourced from the book: Cormen, Thomas H.; Leiserson, Charles E.; Rivest, Ronald L.; Stein, Clifford (2001) [1990]. 'Section 35.1: The vertex-cover problem'. Introduction to Algorithms (2nd ed.). MIT Press and McGraw-Hill. pp. 1024–1027. ISBN 0-262-03293-7.";
    private string _source = "Cormen, Thomas H.; Leiserson, Charles E.; Rivest, Ronald L.; Stein, Clifford (2001) [1990]. 'Section 35.1: The vertex-cover problem'. Introduction to Algorithms (2nd ed.). MIT Press and McGraw-Hill. pp. 1024–1027. ISBN 0-262-03293-7.";

    private string _complexity = "";

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
    public VCSolverJanita() {

    }


    public List<KeyValuePair<string, string>> Solve(String G){
        //{{a,b,c,d,e,f,g} : {(a,b) & (a,c) & (c,d) & (c,e) & (d,f) & (e,f) & (e,g)}}
        List<KeyValuePair<string, string>> edges = getEdges(G);
        List<KeyValuePair<string, string>> C = new List<KeyValuePair<string, string>>();
        Random rnd = new Random();

        while (edges.Count > 0){
            int index = rnd.Next(edges.Count);
            KeyValuePair<string, string> edge = edges[index];
            KeyValuePair<string,string> fullEdge = new KeyValuePair<string,string>(edge.Key, edge.Value);
            C.Add(fullEdge);
            foreach (KeyValuePair<string, string> e in new List<KeyValuePair<string, string>>(edges)){
                if (e.Key.Equals(edge.Key)){
                    KeyValuePair<string,string> rmEdge = new KeyValuePair<string,string>(edge.Key, edge.Value);
                    edges.Remove(rmEdge);
                }
                if (e.Key.Equals(edge.Value)){
                    KeyValuePair<string,string> rmEdge = new KeyValuePair<string,string>(edge.Key, edge.Value);
                    edges.Remove(rmEdge);
                }
                if (e.Value.Equals(edge.Key)){
                    KeyValuePair<string,string> rmEdge = new KeyValuePair<string,string>(edge.Key, edge.Value);
                    edges.Remove(rmEdge);
                }
                if (e.Value.Equals(edge.Value)){
                    KeyValuePair<string,string> rmEdge = new KeyValuePair<string,string>(edge.Key, edge.Value);
                    edges.Remove(rmEdge);
                }
            }
        }
        return C; 

    }

    private static List<KeyValuePair<string, string>> getEdges(string Ginput) {

        List<KeyValuePair<string, string>> allGEdges = new List<KeyValuePair<string, string>>();

        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");
        
        // [0] is nodes,  [1] is edges,  [2] is k.
        string[] Gsections = strippedInput.Split(':');
        string[] Gedges = Gsections[1].Split('&');
        
        foreach (string edge in Gedges) {
            string[] fromTo = edge.Split(',');
            string nodeFrom = fromTo[0];
            string nodeTo = fromTo[1];
            
            KeyValuePair<string,string> fullEdge = new KeyValuePair<string,string>(nodeFrom, nodeTo);
            allGEdges.Add(fullEdge);
        }

        return allGEdges;
    }
}
