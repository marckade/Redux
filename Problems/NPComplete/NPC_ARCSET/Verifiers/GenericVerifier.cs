using API.Interfaces;

namespace API.Problems.NPComplete.NPC_ARCSET.Verifiers;

class GenericVerifier : IVerifier {

    // --- Fields ---
    private string _verifierName = "ARCSET Verifier";
    private string _verifierDefinition = "This is a verifier for ARCSET";
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
        
        //Note that the certificate of our default ARCSET instance would be the set {(2,4)} where the set has a length 1 and position 0 is the tuple (2,4) 

        //To implement our verifier, we take take some input string and convert it into a data structure that we can use. Remember to remove certificate edges from this graph
        //Then we implement a depth first search where if at any point the depth first search discovers a back edge then we REJECT. (a back edge would mean the graph is cyclic)
        //If no back edges are found, we accept. 

        //DFS Note: When running the depth first search we need to keep track of two attributes for every node in the graph, the previsit and postvisit numbers.

        //Def back edge: the only edges (u,v) where u and v are nodes in a graph for which post(u) < post(v) are back edges. 
    }
}