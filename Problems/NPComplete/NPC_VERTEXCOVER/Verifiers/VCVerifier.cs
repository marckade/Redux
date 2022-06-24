using API.Interfaces;

namespace API.Problems.NPComplete.NPC_VERTEXCOVER.Verifiers;

class VCVerifierJanita : IVerifier {

    // --- Fields ---
    private string _verifierName = "Janita's Generic Verifier";
    private string _verifierDefinition = "This verifier is a naive solver for Vertex Cover that does not have a clear origination. ";
    private string _source = "No clear origination.";
    private string _complexity = "";
    private string _certificate ="";

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
    public VCVerifierJanita() {
        
    }

    public Boolean Verify(VERTEXCOVER Problem, string c){
        //{{a,b,c,d,e,f,g} : {(a,b) & (a,c) & (c,d) & (c,e) & (d,f) & (e,f) & (e,g)} : 3}
        //{{a,d,e} : {(a,b) & (a,c) & (c,d) & (c,e) & (d,f) & (e,f) & (e,g)} }
        List<string> nodes = getNodes(c);
        List<KeyValuePair<string, string>> edges = getEdges(c);
        List<string> GNodes = Problem.nodes;
        List<KeyValuePair<string, string>> Gedges = Problem.edges;


        var list = nodes.Except(GNodes);
        bool result1 = list?.Any() != true;

        int count = 0;
        foreach (KeyValuePair<string, string> edge in edges)
        {
            foreach (KeyValuePair<string, string> Gedge in Gedges){
                if (edge.Key.Equals(Gedge.Key) && edge.Value.Equals(Gedge.Value)){
                    count += 1;
                }
                if (edge.Key.Equals(Gedge.Value) && edge.Value.Equals(Gedge.Key)){
                    count += 1; 
                }
            }                
        }
        int size = Gedges.Count;
        bool result2 = false;
        if (size == count){
            result2 = true;
        }
    
        return (result1 == true) && (result2 == true) ? true : false;

    }

    public List<string> getNodes(string Ginput) {
        List<string> allGNodes = new List<string>();
        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");
        
        // [0] is nodes,  [1] is edges,  [2] is k.
        string[] Gsections = strippedInput.Split(':');
        string[] Gnodes = Gsections[0].Split(',');
        
        foreach(string node in Gnodes) {
            allGNodes.Add(node);
        }

        return allGNodes;
    }

    public List<KeyValuePair<string, string>> getEdges(string Ginput) {

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

    public int getK(string Ginput) {
        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");
        
        // [0] is nodes,  [1] is edges,  [2] is k.
        string[] Gsections = strippedInput.Split(':');
        return Int32.Parse(Gsections[2]);
    }
}
