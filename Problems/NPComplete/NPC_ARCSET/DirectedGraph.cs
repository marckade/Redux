
using System;
using System.Collections.Generic;

namespace API.Problems.NPComplete.NPC_ARCSET;

class DirectedGraph{


    // --- Fields ---

    //private list nodeList // Node obj
    private List<Node> nodeList;
    //private list edge list // edge obj
    private List<Edge> edgeList;
    
    //This field makes this graph ARCSET Specific
    private int _K;

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

//This constructors takes in a list of nodes (in string format) and a list of edges (in string format) and creates a graph
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
//"{{1,2,3,4} : {(4,1) & (1,2) & (4,3) & (3,2) & (2,4)} : 1}"
    public override string ToString(){

        string nodeListStr = "";
        foreach(Node node in nodeList){
    
            nodeListStr= nodeListStr+ node.name +",";
        }
        nodeListStr = nodeListStr.TrimEnd(',');

        string edgeListStr = "";
        foreach(Edge edge in edgeList){
           string edgeStr = edge.ToString() +" & ";
            edgeListStr = edgeListStr+ edgeStr+""; 
        }
        edgeListStr = edgeListStr.TrimEnd('&',' ');
        //edgeListStr = edgeListStr.TrimEnd(' ');
        string toStr = "{{"+nodeListStr+"}"+ " : {" + edgeListStr+"}"+" : "+_K+"}";
        return toStr;
    }  



    // public static void main(){
    // Node node1 = new Node("1");
    // Node node2 = new Node("2");
    // Node node3 = new Node("3");
    // List<Node> nl = new List<>();
    // nl.Add(node1);
    // nl.Add(node2);
    // nl.Add(node3);


    // Edge edge1 = new Edge(node1,node2);
    // Edge edge2 = new Edge(node2,node3);
    // Edge edge3 = new Edge(node3,node1);
    // List<Edge> el = new List<>();
    // el.Add(edge1);
    // el.Add(edge2);
    // el.Add(edge3);
    // int kTest = 1;
    // DirectedGraph testG = new DirectedGraph(nl,el,kTest);
    // print(testG.ToString());


    // }

}