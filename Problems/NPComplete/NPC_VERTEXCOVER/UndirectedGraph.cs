
using System;
using System.Collections.Generic;

namespace API.Problems.NPComplete.NPC_VERTEXCOVER;

class UndirectedGraph:Graph{


    // --- Fields ---
    //Since we are inheriting from the graph abstract class, fields are blank. There is probably a better way to do this though.
    //protected List<Node> nodeList;
    
    //protected List<Edge> edgeList;
    

    protected int _K;

    //Constructor
    public UndirectedGraph(){

        _nodeList = new List<Node>();
        _edgeList = new List<Edge>();
        _K=0;
    }


    public UndirectedGraph(List<Node> nl, List<Edge> el, int kVal){

        this._nodeList = nl;
        this._edgeList = el; 
        _K = kVal; 
    }

//This constructors takes in a list of nodes (in string format) and a list of edges (in string format) and creates a graph
    public UndirectedGraph(List<String> nl, List<KeyValuePair<string,string>> el, int kVal){

        this._nodeList = new List<Node>();
        foreach (string nodeStr in nl){
            Node node = new Node(nodeStr);
            _nodeList.Add(node);
        }
        //Note that this is initializing unique node instances. May want to compose edges of already existing nodes instead. 
        this._edgeList = new List<Edge>();
        foreach(KeyValuePair<string,string> edgeKV in el){
            string eStr1= edgeKV.Key;
            string eStr2 = edgeKV.Value;
            Node n1 = new Node(eStr1);
            Node n2 = new Node(eStr2);
            Edge edge = new Edge(n1,n2);
            this._edgeList.Add(edge);
        }

        _K = kVal;

    }
    /**
    * wrapped constructor
    **/
    public UndirectedGraph(String graphStr){
 
        
        List<string> nl = getNodes(graphStr);
        List<KeyValuePair<string,string>> el = getEdges(graphStr);
        int k = getK(graphStr);

         this._nodeList = new List<Node>();
        foreach (string nodeStr in nl){
            Node node = new Node(nodeStr);
            _nodeList.Add(node);
        }
        //Note that this is initializing unique node instances. May want to compose edges of already existing nodes instead. 
        this._edgeList = new List<Edge>();
        foreach(KeyValuePair<string,string> edgeKV in el){
            string eStr1= edgeKV.Key;
            string eStr2 = edgeKV.Value;
            Node n1 = new Node(eStr1);
            Node n2 = new Node(eStr2);
            Edge edge = new Edge(n1,n2);
            this._edgeList.Add(edge);
        }

        _K = k;

    }

    public override string ToString(){

        string nodeListStr = "";
        foreach(Node node in _nodeList){
    
            nodeListStr= nodeListStr+ node.name +",";
        }
        nodeListStr = nodeListStr.TrimEnd(',');

        string edgeListStr = "";
        foreach(Edge edge in _edgeList){
           string edgeStr = edge.undirectedString() +" & "; //This line makes this distinct from DirectedGraph
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
        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("[", "").Replace("]",""); //uses [ ] as delimiters for edge pairs
        
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

        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("[", "").Replace("]","");
        
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
            string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("[", "").Replace("]","");
            
            // [0] is nodes,  [1] is edges,  [2] is k.
            string[] Gsections = strippedInput.Split(':');
            return Int32.Parse(Gsections[2]);
        }


    public string reduction(){
        List<Node> newNodes = new List<Node>();
        foreach(Node n in _nodeList){
            Node newNode1 = new Node(n.name);
            Node newNode2 = new Node(n.name);
            newNode1.name = n.name+"0";
            newNode2.name = n.name+"1";
            newNodes.Add(newNode1);
            newNodes.Add(newNode2);
        }
        //Turn undirected edges into paired directed edges.
        List<Edge> newEdges = new List<Edge>();
        List<Edge> numberedEdges = new List<Edge>();
        foreach(Edge e in _edgeList){
            Edge newEdge1 = new Edge(e.node1,e.node2);
            Edge newEdge2 = new Edge(e.node2,e.node1);
            newEdges.Add(newEdge1);
            newEdges.Add(newEdge2);
        }

        //map edges to to nodes
        foreach(Edge e in newEdges){
            Node newNode1 = new Node(e.node1.name+"1");
            Node newNode2 = new Node(e.node2.name+"0");
            Edge numberedEdge = new Edge(newNode1,newNode2);
            numberedEdges.Add(numberedEdge);
        }

        //map from every 0 to 1
        for(int i=0;i<newNodes.Count;++i){
            if(i%2==0){
                Edge newEdge = new Edge(newNodes[i],newNodes[i+1]);
                numberedEdges.Add(newEdge);
            }
        }
        newEdges.Clear(); //Getting rid of unsplit edges
        newEdges = numberedEdges;

      //  Console.WriteLine("Nodes: ");
        foreach(Node n in newNodes){
            //Console.Write(n.name+",");
        }
       // Console.WriteLine("Edges: ");

        foreach(Edge e in newEdges){
          //  Console.WriteLine(e.directedString());
        }
        
        //"{{1,2,3,4} : {(4,1) & (1,2) & (4,3) & (3,2) & (2,4)} : 1}" //formatting
        string nodeListStr = "";
        foreach(Node node in newNodes){
    
            nodeListStr= nodeListStr+ node.name +",";
        }
        nodeListStr = nodeListStr.TrimEnd(',');
        string edgeListStr = "";
        foreach(Edge edge in newEdges){
           string edgeStr = edge.directedString() +" & "; //this line is what makes this class distinct from Undirected Graph
           //Console.WriteLine("Edge: "+ edge.directedString());
            edgeListStr = edgeListStr+ edgeStr+""; 
        }
        edgeListStr = edgeListStr.TrimEnd('&',' ');
        //edgeListStr = edgeListStr.TrimEnd(' ');
        string toStr = "{{"+nodeListStr+"}"+ " : {" + edgeListStr+"}"+" : "+_K+"}";
        return toStr;

        //DirectedGraph reductionGraph = new DirectedGraph(newNodes,newEdges,_K);
       // return reductionGraph;

    }

}