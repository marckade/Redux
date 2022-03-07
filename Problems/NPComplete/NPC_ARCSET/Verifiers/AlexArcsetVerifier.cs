using API.Interfaces;

namespace API.Problems.NPComplete.NPC_ARCSET.Verifiers;
using API.Problems.NPComplete.NPC_ARCSET;
using API.Problems.NPComplete.NPC_VERTEXCOVER;

class AlexArcsetVerifier : IVerifier {

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


    public AlexArcsetVerifier(){

    }

    /**
    * This method should take in an arcset problem and a list of edges to remove from that problem. It removes those edges and then checks if the problem is still an instance of ARCSET
    * ie. Does this input graph continue to have cycles after these input edges are removed? 
    **/
    public Boolean verify(ARCSET problem, string userInput){

        DirectedGraph graph = problem.directedGraph; 
        graph.processCertificate(userInput);
        bool isInARCSET = graph.DFS();

        //when userInput is removed from graph is it still Cyclical? 
        return isInARCSET;
    }

}