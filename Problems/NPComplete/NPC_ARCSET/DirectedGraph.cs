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

    Dictionary<string,Node> _nodeDict;
    Dictionary<string,string> tempEdgeDict;

    protected int _K;
    private int lazyCounter;
    private Dictionary<string,List<KeyValuePair<string,Node>>> _adjacencyMatrix; //Dictionary of Key Value Pairs where keys are node names and values are lists of all the node names that they are connected to.

    //Constructor
    public DirectedGraph(){

        nodeList = new List<Node>();
        edgeList = new List<Edge>();
        _K=0;
        _adjacencyMatrix = new Dictionary<string,List<KeyValuePair<string,Node>>>();
    //     _adjacencyMatrix = new Queue<Node>[nodeList.Count];

    //      for (int i = 0; i < nodeList.Count; ++i){_adjacencyMatrix[i] = new Queue<Node>();}
    // }
    generateAdjacencyMatrix();
    }

    public DirectedGraph(List<Node> nl, List<Edge> el, int kVal){

        nodeList = nl;
        edgeList = el; 
        _K = kVal; 
        _adjacencyMatrix = new Dictionary<string,List<KeyValuePair<string,Node>>>();
    
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
        _adjacencyMatrix = new Dictionary<string,List<KeyValuePair<string,Node>>>();
        generateAdjacencyMatrix();
       

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
        _adjacencyMatrix = new Dictionary<string,List<KeyValuePair<string,Node>>>();
        generateAdjacencyMatrix();
    
         //The following generates the dictionaries for our Nodes and Edges
         _nodeDict = new Dictionary<string,Node>();
         tempEdgeDict = new Dictionary<string, string>();


        //gets nodes. INITIALIZES NODE OBJECTS
        List<string> tempNodeStringList = getNodes(graphStr);
        foreach(string nodeStr in tempNodeStringList){
            Node currentNode = new Node(nodeStr);
           // KeyValuePair<string,Node> currentKVP = new KeyValuePair<string, Node>(nodeStr,currentNode);
            _nodeDict.Add(nodeStr,currentNode);
        }

         //gets edges. no object initialization needed, if we need to grab a node given an edge, we can query the dictionary given the edge's key or value. 
         List<KeyValuePair<string,string>> tempEdgeList = getEdges(graphStr); //REDUNDANT. CHANGE EDGELIST SOON
        foreach(KeyValuePair<string,string> kvp in tempEdgeList){
           tempEdgeDict.Add(kvp.Key,kvp.Value);
        }

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
           //Console.WriteLine("Edge: "+ edge.directedString());
            edgeListStr = edgeListStr+ edgeStr+""; 
        }
        edgeListStr = edgeListStr.TrimEnd('&',' ');
        //edgeListStr = edgeListStr.TrimEnd(' ');
        string toStr = "{{"+nodeListStr+"}"+ " : {" + edgeListStr+"}"+" : "+_K+"}";
        return toStr;
    }  


    //ALEX NOTE: Taken from Kaden's Clique class. May refactor to directly return a node dictionary.
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


        //ALEX NOTE: Taken from Kaden's Clique class. May Refactor to directly return an Edge dictionary.
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
* This method generates a Dictionary of key value pairs to represent the graph so that the explore() function can explore the graph. 
**/
  private void generateAdjacencyMatrix(){

             _adjacencyMatrix =  new Dictionary<string,List<KeyValuePair<string,Node>>>();
            foreach(Node n in nodeList){ //creates the x row
                _adjacencyMatrix.Add(n.name,new List<KeyValuePair<string,Node>>()); //initializes a list of KVP's per node. 
                    //Console.Write(n.name);    
            }
            List<KeyValuePair<string,Node>> posList;
            KeyValuePair<string,Node>kvp;
            foreach(Edge e in edgeList){ //creates the y columns. 
            
               bool listIsEmpty = _adjacencyMatrix.TryGetValue(e.node1.name,out posList); //given name of node 1 in edge, add the KVP (node2.name, node2) to list. 
                  kvp = new KeyValuePair<string, Node>(e.node2.name,e.node2);
                  
                //Console.Write(kvp.Value.GetType().ToString());
                try{
                posList.Add(kvp); //adds the node kvp to the list of kvps associated with "e" (current node).
                }
               catch (NullReferenceException exept) //This exception will be caused by a bad string input. 
                {
                Console.Write("NULL CAST EXCEPTION");
                Console.Write(exept.StackTrace);
                }
               

            }
//         bool testBool =  _adjacencyMatrix.TryGetValue("1",out posStack);
//         if(testBool){
//          Console.WriteLine("AHA!! "+posStack.Peek().Key);
//   }

    }

//This method implements a depth first search to assign pre and post visit numbers to all nodes
    
    private void addEdgeAdj(Node n1, Node n2){ //WARNING: this will add edges to the adjacency Matrix but not the normal list.
         List<KeyValuePair<string,Node>> list;
        
        _adjacencyMatrix.TryGetValue(n1.name,out list);
        KeyValuePair<string,Node> adjEdge = new KeyValuePair<string, Node>(n2.name,n2);        
        list.Add(adjEdge);
    }

    public string adjToString(){
        String toString = "";
        foreach(Node n in nodeList){
            toString= toString+" Node: "+ n.name +" ";
            List<KeyValuePair<string,Node>> adjList = new List<KeyValuePair<string, Node>>();           
             bool testCreation = _adjacencyMatrix.TryGetValue(n.name,out adjList);
              //Console.Write(testStackCreation);
              toString = toString +"Edges: (";
              foreach(KeyValuePair<string,Node> kvp in adjList){
                
                  toString= toString+ "["+ n.name + ","+kvp.Value.name +"]";
              }
              toString= toString+")";
            
        }
        return toString;
    }

  
    public List<Node> getNodeList{
        get{
            return base.nodeList;
        }
    }

   public Dictionary<string,Node> nodeDict{
       get{
           return _nodeDict;
       }
       set{
           _nodeDict = value;
       }
   }
   public Dictionary<string,List<KeyValuePair<string,Node>>> adjacencyMatrix{ 
       get{
           return _adjacencyMatrix;
       }
   }



 private void explore(Node currentNode,bool[] visited,int[] preVisitArr,int[] postVisitArr,Dictionary<string,int> nodePositionDict,Dictionary<string,int> nodePreDict,Dictionary<string,int> nodePostDict){
    // bool hasCycle = false;
     int currPos; //our current node virtual array index.
    // Console.Write("Counter="+lazyCounter);
     nodePositionDict.TryGetValue(currentNode.name,out currPos); //grabs the array position of the current node. 
    lazyCounter++;
     
if(!visited[currPos]){
    visited[currPos] = true; //sets this node as visited.
    preVisitArr[currPos] = lazyCounter; //sets this Node's previsit value to our counter. 
    }  
    //Dictionary adjMatrix = inputG.adjacencyMatrix; 
    
    List<KeyValuePair<string,Node>> adjKVPList; //list of adjacent nodes.
    _adjacencyMatrix.TryGetValue(currentNode.name, out adjKVPList); //given a node name, output a List of KVPs of strings and nodes (ie, the adjacent nodes to this one).

    //Console.Write("Current Node: "+currentNode.name );

    foreach(KeyValuePair<string,Node> kvp in adjKVPList ){ //search the adjacent edges to this one
        int position; //position of adjacent node.
        String nextNodeName = kvp.Key;
        nodePositionDict.TryGetValue(nextNodeName,out position); //get the position associated with the name
        bool nodeIsVisited = visited[position]; //has this node been visited?
        if(nodeIsVisited){ //if a node has been visited then this graph contains a cycle. 
             Console.WriteLine("Node: "+nextNodeName + "has already been visited. CYCLE FOUND!");
           // hasCycle = true; 
        }
        else{ //since this node in the adjacency list isn't visited, visit it. 
            _nodeDict.TryGetValue(nextNodeName,out currentNode); //sets the next node to currentNode. 
            explore(currentNode,visited,preVisitArr,postVisitArr,nodePositionDict,nodePreDict,nodePostDict);
        }
        // foreach(int pre in preVisitArr){
        //     Console.Write(pre +" ");
        // }
    }
    lazyCounter++;
    postVisitArr[currPos] = lazyCounter; //if we are at the bottom of the search then set our current node postVisit to our counter.
    // Console.Write("Previsit Values:  {");
    // foreach(int preVis in preVisitArr){
    //     Console.Write(preVis + ",");
    // }
    // Console.WriteLine("}");
    //  Console.Write("Postvisit Values:  {");
    // foreach(int postVis in postVisitArr){
    //     Console.Write(postVis + ",");
    // }
    // Console.WriteLine("}");

    // Console.Write("Node Names:  {");
    // foreach(KeyValuePair<string,Node> nodeKVP in nodeDict){

    //     Console.Write(nodeKVP.Key + ",");
    // }
    // Console.WriteLine("}");
    //return hasCycle;
}

/**
* This method uses a DFS to check a graph for cycles, returning true if any cycles have been found. 
**/
  public bool DFS(){
      //bool hasCycle = false;
      //while member, no need for static. 
    bool[] visited = new bool[nodeList.Count]; //makes array equal entry for entry to nodeList
   // bool[] mapNodeNum = new bool[nodeList.Count];
    int[] preVisitArr = new int[nodeList.Count];
    int[] postVisitArr = new int[nodeList.Count];
    int i = 0;
    string nameNodeInit = "";
    if(nodeList.Count!=0)
    {

        nameNodeInit = nodeList[0].name; //This will start the DFS using the 

        Node currentNode = new Node(); //Instantiates Object. This is messy solution, but avoids a O(n) search of nodeList. 
        _nodeDict.TryGetValue(nameNodeInit, out currentNode); 

        KeyValuePair<string,int> mapNodePos; //we want to map our node name to a position int
        Dictionary<string,int> nodePositionDict = new Dictionary<string,int>(); //creates a dictionary of KVPs

        KeyValuePair<string,int> mapNodePreVis; //we want to map our node name to a previsit int
        Dictionary<string,int> nodePreDict = new Dictionary<string,int>(); //creates a dictionary of KVPs

        KeyValuePair<string,int> mapNodePosVis; //we want to map our node name to a previsit int
        Dictionary<string,int> nodePostDict = new Dictionary<string,int>(); //creates a dictionary of KVPs

        foreach(var nodeKVP in _nodeDict){
        visited[i] = false; //sets initial visit value of every node to false
        string nodeName = nodeKVP.Key;
        //mapNodePos = new KeyValuePair<string, int>(nodeName,i); //maps name of node to position
        nodePositionDict.Add(nodeName,i); //now nodeNumDict will be able to find a position given a name.
        i++;
        }

        int counter = 0;

        foreach(var entry in _nodeDict){
        int mappedPos = -1;
        nodePositionDict.TryGetValue(entry.Key,out mappedPos); //looks for a position given name.
        if(!visited[mappedPos])
            { //if the boolean visit array sees the position isn't visited
        explore(currentNode,visited,preVisitArr,postVisitArr,nodePositionDict,nodePreDict,nodePostDict); //explore the position (start recursion).
            }

        }
            //checks for backedges.        
            foreach(KeyValuePair<string,string> kvp in tempEdgeDict){
            String nodeFrom = kvp.Key;
            String nodeTo = kvp.Value;

            int node1Pos;
            int node2Pos;
            nodePositionDict.TryGetValue(nodeFrom, out node1Pos);
            nodePositionDict.TryGetValue(nodeTo, out node2Pos);
            
            if(preVisitArr[node1Pos]>preVisitArr[node2Pos]){ //if the previsit value of the from node is greater than the to node, we have a backedge.
                return true;
            } 
        }
   
    // Console.Write("Previsit Values:  {");
    // foreach(int preVis in preVisitArr){
    //     Console.Write(preVis + ",");
    // }
    // Console.WriteLine("}");
    //  Console.Write("Postvisit Values:  {");
    // foreach(int postVis in postVisitArr){
    //     Console.Write(postVis + ",");
    // }
    // Console.WriteLine("}");

    // Console.Write("Node Names:  {");
    // foreach(KeyValuePair<string,Node> nodeKVP in nodeDict){

    //     Console.Write(nodeKVP.Key + ",");
    // }
    // Console.WriteLine("}");

    }
    else{
        Console.Write("NodeList is empty, cannot DFS");
        }

  
    
    
    return false; //no cycle was found. 

}

}