using API.Interfaces;

namespace API.Problems.NPComplete.NPC_ARCSET.Verifiers;
using API.Problems.NPComplete.NPC_ARCSET;
using API.Problems.NPComplete.NPC_VERTEXCOVER;

class AlexArcsetVerifier : IVerifier {

    // --- Fields ---
    private string _verifierName = "ARCSET Verifier";
    private string _verifierDefinition =  @"This Verifier takes in an arcset problem and a list of edges to remove from that problem. It removes those edges and then checks if the problem is still an instance of ARCSET
                                            ie. Does this input graph continue to have cycles after these input edges are removed? Returns true or false ";
    
    private string _source = "This verifier is essentially common knowledge, as it utilizes a widely recognized algorithm in computer science: The Depth First Search.";

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
        //Console.WriteLine(graph.getBackEdges());
        bool isInARCSET = graph.isCyclical();

        //when userInput is removed from graph is it still Cyclical? 
        return isInARCSET;
    }

}