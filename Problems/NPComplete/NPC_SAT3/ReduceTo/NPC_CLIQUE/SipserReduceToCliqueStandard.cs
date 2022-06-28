using API.Interfaces;
using API.Problems.NPComplete.NPC_CLIQUE;
using API.Problems.NPComplete.NPC_CLIQUE.Inherited;

namespace API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_CLIQUE;

class SipserReduction : IReduction<SAT3, SipserClique> {

    // --- Fields ---
    private string _reductionDefinition = "Sipsers reduction converts clauses from 3SAT into clusters of nodes in a graph for which CLIQUES exist";
    private string _source = "Sipser, Michael. Introduction to the Theory of Computation.ACM Sigact News 27.1 (1996): 27-29.";
    private SAT3 _reductionFrom;
    private SipserClique _reductionTo;


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
    public SAT3 reductionFrom {
        get {
            return _reductionFrom;
        }
        set {
            _reductionFrom = value;
        }
    }
    public SipserClique reductionTo {
        get {
            return _reductionTo;
        }
        set {
            _reductionTo = value;
        }
    }

    // --- Methods Including Constructors ---
    public SipserReduction(SAT3 from) {
        _reductionFrom = from;
        _reductionTo = reduce();

    }
    public SipserClique reduce() {
        SAT3 SAT3Instance = _reductionFrom;
        SipserClique reducedCLIQUE = new SipserClique();
        // SAT3 literals become nodes.
        reducedCLIQUE.nodes = SAT3Instance.literals;
        List<KeyValuePair<string, string>> edges = new List<KeyValuePair<string, string>>();
        List<string> usedNames = new List<string>(); // Used to track what names have been used for nodes

        // define what makes the edges. Not in same cluster & not inverse

        // I is the cluster
        for(int i = 0; i < SAT3Instance.clauses.Count; i++) {

            for(int j = 0; j < SAT3Instance.clauses[i].Count; j++) {
                string nodeFrom = SAT3Instance.clauses[i][j];
                nodeFrom = duplicateName(nodeFrom, usedNames, 1, nodeFrom);

                SipserNode newNode = new SipserNode(nodeFrom,  i.ToString());
                reducedCLIQUE.clusterNodes.Add(newNode);
                usedNames.Add(nodeFrom);
                //Four loops? Sounds efficent
                for(int a = 0; a < SAT3Instance.clauses.Count; a++) {

                    for(int b = 0; b < SAT3Instance.clauses[a].Count; b++) {
                        string nodeTo = SAT3Instance.clauses[a][b];
                        bool inverse = false;
                        bool samecluser = false;

                        // Check if nodes are inverse of one another
                        if (nodeFrom != nodeTo && nodeFrom.Replace("!", "") == nodeTo.Replace("!", "")) {
                            inverse = true;
                        }
                        // Check if nodes belong to same cluster
                        if (i == a) {
                            samecluser = true;
                        }

                        if (!inverse && !samecluser) {
                            KeyValuePair<string,string> fullEdge = new KeyValuePair<string,string>(nodeFrom, nodeTo);
                            edges.Add(fullEdge);
                        }
                    }
                }
            }
        }
        reducedCLIQUE.edges = edges;
        reducedCLIQUE.K = SAT3Instance.clauses.Count;

        // --- Generate G string for new CLIQUE ---
        string nodesString = "";
        foreach (string literal in SAT3Instance.literals) {
            nodesString += literal + ",";
        }
        nodesString = nodesString.Trim(',');

        string edgesString = "";
        foreach (KeyValuePair<string,string> edge in edges) {
            edgesString += "(" + edge.Key + "," + edge.Value + ")" + " & ";
        }
        edgesString = edgesString.Trim('&');

        int kint = SAT3Instance.clauses.Count;
        // "{{1,2,3,4} : {(4,1) & (1,2) & (4,3) & (3,2) & (2,4)} : 1}";
        string G = "{{" + nodesString + "} : {" + edgesString + "} : " + kint.ToString() + "}";

        // Assign and return
        reducedCLIQUE.instance = G;
        reductionTo = reducedCLIQUE;
        return reducedCLIQUE;
    }

    private string duplicateName(string name, List<string> usedNames, int version, string originalName){
        if (usedNames.Contains(name)) {
            // usedNames.Add(name);
            string newName = originalName + '_' + version;
            version = version + 1;
            return duplicateName(newName, usedNames, version, originalName);
        }

        return name;
    }

}
// return an instance of what you are reducing to