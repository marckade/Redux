//DirectedGraph.cs
//Can take a string representation of a directed graph and turn it into a directed graph object.
using System;
using System.Collections.Generic;

namespace API.Problems.NPComplete.NPC_ARCSET;

class DirectedGraph:Graph{


    // --- Fields ---

    //private list nodeList // Node obj
   // protected List<Node> nodeList;
    //private list edge list // edge obj
    //protected List<Edge> edgeList;
    

    protected int _K;

    //Constructor
    public DirectedGraph(){

        nodeList = new List<Node>();
        edgeList = new List<Edge>();
        _K=0;
    }


    public DirectedGraph(List<Node> nl, List<Edge> el, int kVal){

        nodeList = nl;
        edgeList = el; 
        _K = kVal; 
    }

//This constructors takes in a list of nodes (in string format) and a list of edges (in key value pair format) and constructs a graph
    public DirectedGraph(List<String> nl, List<KeyValuePair<string,string>> el, int kVal){

        nodeList = new List<Node>();
        foreach (string nodeStr in nl){
            Node node = new Node(nodeStr);
            nodeList.Add(node);
        }
        //Note that this is initializing unique node instances. May want to compose edges of already existing nodes instead. 
        edgeList = new List<Edge>();
        foreach(KeyValuePair<string,string> edgeKV in el){
            string eStr1= edgeKV.Key;
            string eStr2 = edgeKV.Value;
            Node n1 = new Node(eStr1);
            Node n2 = new Node(eStr2);
            Edge edge = new Edge(n1,n2);
            edgeList.Add(edge);
        }

        _K = kVal;

    }
    /**
    * takes a string directed graph input and constructs a directed graph
    **/
    public DirectedGraph(String graphStr){
 
        
        List<string> nl = getNodes(graphStr);
        List<KeyValuePair<string,string>> el = getEdges(graphStr);
        int k = getK(graphStr);

         nodeList = new List<Node>();
        foreach (string nodeStr in nl){
            Node node = new Node(nodeStr);
            nodeList.Add(node);
        }
        //Note that this is initializing unique node instances. May want to compose edges of already existing nodes instead. 
        edgeList = new List<Edge>();
        foreach(KeyValuePair<string,string> edgeKV in el){
            string eStr1= edgeKV.Key;
            string eStr2 = edgeKV.Value;
            Node n1 = new Node(eStr1);
            Node n2 = new Node(eStr2);
            Edge edge = new Edge(n1,n2);
            edgeList.Add(edge);
        }

        _K = k;

    }
    public override string ToString(){

        string nodeListStr = "";
        foreach(Node node in nodeList){
    
            nodeListStr= nodeListStr+ node.name +",";
        }
        nodeListStr = nodeListStr.TrimEnd(',');

        string edgeListStr = "";
        foreach(Edge edge in edgeList){
           string edgeStr = edge.directedString() +" & "; //this line is what makes this class distinct from Undirected Graph
            edgeListStr = edgeListStr+ edgeStr+""; 
        }
        edgeListStr = edgeListStr.TrimEnd('&',' ');
        //edgeListStr = edgeListStr.TrimEnd(' ');
        string toStr = "{{"+nodeListStr+"}"+ " : {" + edgeListStr+"}"+" : "+_K+"}";
        return toStr;
    }  


    //ALEX NOTE: Taken from Kaden's Clique class
/**
  * Takes a string representation of a directed graph and returns its Nodes as a list of strings.
**/
    protected override List<string> getNodes(string Ginput) {

        List<string> allGNodes = new List<string>();
        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")",""); //Looks for ( and ) as delimiters for edge pairs
        
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

    protected override List<KeyValuePair<string, string>> getEdges(string Ginput) {

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
    protected override int getK(string Ginput) {
            string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");
            
            // [0] is nodes,  [1] is edges,  [2] is k.
            string[] Gsections = strippedInput.Split(':');
            return Int32.Parse(Gsections[2]);
        }





//This method implements a depth first search to check for cycles. 
    public bool hasCycles(){

        return false;
    }






}