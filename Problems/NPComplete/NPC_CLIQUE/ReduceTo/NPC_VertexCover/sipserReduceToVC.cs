using API.Interfaces;
using API.Problems.NPComplete.NPC_VERTEXCOVER;

namespace API.Problems.NPComplete.NPC_CLIQUE.ReduceTo.NPC_VertexCover;

class sipserReduction : IReduction<CLIQUE, VERTEXCOVER> {


    // --- Fields ---
    private string _reductionDefinition = " This Sipsers reduction converts the Clique problem into a Vertex Cover problem";
    private string _source = "Sipser, Michael. Introduction to the Theory of Computation.ACM Sigact News 27.1 (1996): 27-29.";
    private CLIQUE _reductionFrom;
    private VERTEXCOVER _reductionTo;

    private string _complexity = "";


    // --- Properties ---
    public string reductionDefinition {
        get {
            return _reductionDefinition;
        }
    }
    public string source {
        get {
            return _source;
        }
    }
    public CLIQUE reductionFrom {
        get {
            return _reductionFrom;
        }
        set {
            _reductionFrom = value;
        }
    }
    public VERTEXCOVER reductionTo {
        get {
            return _reductionTo;
        }
        set {
            _reductionTo = value;
        }
    }

    // --- Methods Including Constructors ---
    public sipserReduction(CLIQUE from) {
        _reductionFrom = from;
        _reductionTo = reduce();

    }
    public VERTEXCOVER reduce() {
        CLIQUE CLIQUEInstance = _reductionFrom;
        VERTEXCOVER reducedVERTEXCOVER = new VERTEXCOVER();


        // Assign clique nodes to vertexcover nodes.
        reducedVERTEXCOVER.nodes = CLIQUEInstance.nodes;

        List<KeyValuePair<string, string>> edges = new List<KeyValuePair<string, string>>();



        for (int i = 0; i < reducedVERTEXCOVER.nodes.Count; i++){
            for (int j = 0; j < reducedVERTEXCOVER.nodes.Count; j++){
                if (reducedVERTEXCOVER.nodes[i] != reducedVERTEXCOVER.nodes[j]){
                    KeyValuePair<string,string> fullEdge = new KeyValuePair<string,string>(reducedVERTEXCOVER.nodes[i], reducedVERTEXCOVER.nodes[j]);
                    edges.Add(fullEdge);
                }
            }
        }

        for (int i = 0; i < CLIQUEInstance.edges.Count; i++){
            edges.Remove(new KeyValuePair<string,string>(CLIQUEInstance.edges[i].Key, CLIQUEInstance.edges[i].Value));
            edges.Remove(new KeyValuePair<string,string>(CLIQUEInstance.edges[i].Value, CLIQUEInstance.edges[i].Key));
        }

        for (int i = 0; i < edges.Count; i++){
            for (int j = 0; j < edges.Count; j++){
                if (edges[i].Key == edges[j].Value && edges[i].Value == edges[j].Key){
                    edges.Remove(new KeyValuePair<string,string>(edges[j].Key, edges[j].Value));
                }
            }
        }

        reducedVERTEXCOVER.edges = edges;
        reducedVERTEXCOVER.K = (CLIQUEInstance.nodes.Count - CLIQUEInstance.K); 

        // --- Generate G string for new CLIQUE ---
        string nodesString = "";
        foreach (string nodes in CLIQUEInstance.nodes) {
            nodesString += nodes + ",";
        }
        nodesString = nodesString.Trim(',');

        string edgesString = "";
        foreach (KeyValuePair<string,string> edge in edges) {
            edgesString += "(" + edge.Key + "," + edge.Value + ")" + " & ";
        }
        edgesString = edgesString.Trim('&');

        int kint = reducedVERTEXCOVER.K;

        string G = "{{" + nodesString + "} : {" + edgesString + "} : " + kint.ToString() + "}";
        reducedVERTEXCOVER.instance = G; 
        Console.Write(reducedVERTEXCOVER.instance);
        reductionTo = reducedVERTEXCOVER;
        return reducedVERTEXCOVER;

    }
}
// // return an instance of what you are reducing to