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
    private Dictionary<string,Stack<KeyValuePair<string,Node>>> _adjacencyMatrix; //Array of Queues of Key Value Pairs

    //Constructor
    public DirectedGraph(){

        nodeList = new List<Node>();
        edgeList = new List<Edge>();
        _K=0;
        _adjacencyMatrix = new Dictionary<string,Stack<KeyValuePair<string,Node>>>();
    //     _adjacencyMatrix = new Queue<Node>[nodeList.Count];

    //      for (int i = 0; i < nodeList.Count; ++i){_adjacencyMatrix[i] = new Queue<Node>();}
    // }
    generateAdjacencyMatrix();
    }

    public DirectedGraph(List<Node> nl, List<Edge> el, int kVal){

        nodeList = nl;
        edgeList = el; 
        _K = kVal; 
        _adjacencyMatrix = new Dictionary<string,Stack<KeyValuePair<string,Node>>>();
        // _adjacencyMatrix = new Queue<Node>[nodeList.Count];

        //  for (int i = 0; i < nodeList.Count; ++i){_adjacencyMatrix[i] = new Queue<Node>();}
        generateAdjacencyMatrix();

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
        _adjacencyMatrix = new Dictionary<string,Stack<KeyValuePair<string,Node>>>();
        generateAdjacencyMatrix();
        //  _adjacencyMatrix = new Queue<Node>[nodeList.Count];

        //  for (int i = 0; i < nodeList.Count; ++i){_adjacencyMatrix[i] = new Queue<Node>();}


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
        _adjacencyMatrix = new Dictionary<string,Stack<KeyValuePair<string,Node>>>();
        generateAdjacencyMatrix();
      
         //To finish this DFS, now that we have a queue we use the DFS rules to iterate through the graph. May have to implement a boolean visited array. 

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


/**
* This method generates a Dictionary of Stacks to represent the graph so that the explore() function can explore the graph. 
**/
  private void generateAdjacencyMatrix(){

            
            foreach(Node n in nodeList){ //creates the x row
                _adjacencyMatrix.Add(n.name,new Stack<KeyValuePair<string,Node>>());
                
            }
            foreach(Edge e in edgeList){ //creates the y columns. 
                Stack<KeyValuePair<string,Node>> posStack;
                _adjacencyMatrix.TryGetValue(e.node1.name,out posStack); //given name of node 1 in edge, pushes the KVP (node2.name, node2) to stack. 
                posStack.Push(new KeyValuePair<string,Node>(e.node2.name,e.node2));
            }
        // Console.Write("TEST ");
        // Stack<Node> testStack = new Stack<Node>();
        // _adjacencyMatrix.TryGetValue("1",out testStack);
        // Console.Write(testStack.Peek().ToString);

    }


//This method implements a depth first search to assign pre and post visit numbers to all nodes
    private void exploreGadget(KeyValuePair<string,Node> kvp,int timer,Stack<KeyValuePair<string,Node>> currentStack){
        
  
     timer=timer+1;
     
     //we need the starting node. 
        Node n = kvp.Value;
        Console.WriteLine(n.name);
        n.visited = true;
        n.preVisit = timer;
         KeyValuePair<string,Node> nextNode;
         bool canPop = currentStack.TryPop(out nextNode);
        if(canPop){
        exploreGadget(nextNode,timer,currentStack);
        }
    }
    public void explore(){
        int timer =0;
        Dictionary<string, Stack<KeyValuePair<string,Node>>> adj = new Dictionary<string,Stack<KeyValuePair<string,Node>>>(_adjacencyMatrix); //Shallow Copy

    //Console.Write("Keys: " +_adjacencyMatrix.Keys.ToString());
        //Console.Write("Keys: "+ _adjacencyMatrix.ContainsKey("1")+ " "+  _adjacencyMatrix.ContainsKey("2")+ " "+  _adjacencyMatrix.ContainsKey("3")+ " "+  _adjacencyMatrix.ContainsKey("4")+ " " +  _adjacencyMatrix.ContainsKey("5"));

        foreach(Node n in nodeList){
            Stack<KeyValuePair<string,Node>> value;
            adj.TryGetValue(n.name,out value);
            KeyValuePair<string,Node> currentBranch = new KeyValuePair<string, Node>(n.name,n);


            
            exploreGadget(currentBranch,timer,value);
        }

    }
    public bool hasCycles(){
        explore();

        return false; 
    }

    public void addEdge(Node n1, Node n2){
         Stack<KeyValuePair<string,Node>> stack;
        
        _adjacencyMatrix.TryGetValue(n1.name,out stack);
        KeyValuePair<string,Node> adjEdge = new KeyValuePair<string, Node>(n2.name,n2);        
        stack.Push(adjEdge);
    }

    public string adjToString(){
        String toString = "";
        foreach(Node n in nodeList){
            toString= toString+" Node: "+ n.name +" ";
            Stack<KeyValuePair<string,Node>> stack;
              _adjacencyMatrix.TryGetValue(n.name,out stack);
              toString = toString +"Edges: (";
              foreach(KeyValuePair<string,Node> kvp in stack){
                  toString= toString+ kvp.Key + ",";
              }
              toString= toString+")";
            
        }
        return toString;
    }

  



}