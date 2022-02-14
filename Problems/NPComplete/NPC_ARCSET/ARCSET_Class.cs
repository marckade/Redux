using API.Interfaces;
using API.Problems.NPComplete.NPC_ARCSET.Solvers;
using API.Problems.NPComplete.NPC_ARCSET.Verifiers;
using System;
using System.Collections.Generic;
namespace API.Problems.NPComplete.NPC_ARCSET;

class ARCSET : IProblem<GenericSolver,GenericVerifier>{

    // --- Fields ---
    private string _problemName = "ARCSET";
    private string _formalDefinition = "{<G,k> | G is a directed graph that can be rendered acyclic by removal of at most k edges}";
     private string _problemDefinition = "ARCSET, or the Feedback Arc Set satisfiability problem, is an NP-complete problem that can be described like the following. Given a directed graph, does removing a given set of edges render the graph acyclical? That is, does removing the edges break every cycle in the graph?";

    // How we want format
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";

    //ALEX NOTE: The standard mathematical form for a DIGRAPH is A = { x,y,z} r = {(x,y),(y,z),(z,x)} where A is a set of nodes and r is a set of pairs of edges. (r stands for relation)
    // G = {A,r} 
    //private string _defaultInstance = "A = {1,2,3,4} r = {(4,1),(1,2),(4,3),(3,2),(2,4)} k = 1";
    private string _defaultInstance = "{{1,2,3,4} : {(4,1) & (1,2) & (4,3) & (3,2) & (2,4)} : 1}";
    private string _arcset = string.Empty; 
    private GenericSolver _defaultSolver = new GenericSolver();
    private GenericVerifier _defaultVerifier = new GenericVerifier(); //Verifier needs to implement a Depth First Search. 

    // --- Properties ---
    public string problemName {
        get {
            return _problemName;
        }
    }
    public string formalDefinition {
        get {
            return _formalDefinition;
        }
    }
    public string problemDefinition {
        get {
            return _problemDefinition;
        }
    }

    public string source {
        get {
            return _source;
        }
    }
    public string defaultInstance {
        get {
            return _defaultInstance;
        }
    }
    public string arcset {
        get {
            return _arcset;
        }
        set {
            _arcset = value;
        }
    }
    public GenericSolver defaultSolver {
        get {
            return _defaultSolver;
        }
    }
    public GenericVerifier defaultVerifier {
        get {
            return _defaultVerifier;
        }
    }


    // --- Methods Including Constructors ---
    public ARCSET() {
       // _arcset = defaultInstance;
        //Should be a graph object.

       string arcDefaultString = _defaultInstance;

        _arcset= new DirectedGraph().ToString();

        
    }
    public ARCSET(string arcInput) {
        //_arcset = arcInput;
        // List<string> nodeList = getNodes(arcInput);
        // List<KeyValuePair<string,string>> edgeList = getEdges(arcInput);
        // int k = getK(arcInput);

    

        _arcset = new DirectedGraph(arcInput).ToString();


    }
    public void ParseProblem(string arcInput) {


    }

    //ALEX NOTE: Taken from Kaden's Clique class
/**
  * Takes a string representation of a directed graph and returns its Nodes as a list of strings.
**/
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


        //ALEX NOTE: Taken from Kaden's Clique class
  /**
  * Takes a string representation of a directed graph and returns its edges as a list of strings.
  **/

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

        //ALEX NOTE: Taken from Kaden's Clique class
  /**
  * Takes a string representation of a directed graph and returns its k value as a list of strings.
  **/
    public int getK(string Ginput) {
            string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");
            
            // [0] is nodes,  [1] is edges,  [2] is k.
            string[] Gsections = strippedInput.Split(':');
            return Int32.Parse(Gsections[2]);
        }

}