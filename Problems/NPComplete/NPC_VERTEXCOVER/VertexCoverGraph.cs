using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using API.Interfaces.Graphs;
namespace API.Problems.NPComplete.NPC_VERTEXCOVER;

class VertexCoverGraph:UndirectedGraph{


    public VertexCoverGraph() : base(){
        
    }
    public VertexCoverGraph(string vertInput) : base (vertInput){
        
    }

    public VertexCoverGraph(string vertInput, bool decoy) : base (vertInput, decoy){
    
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