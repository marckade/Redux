//Edge.cs
//This class is used for the creation of Edge objects that can then be used to create a graph. It is composed of two Nodes.


using System;
using System.Collections.Generic;
namespace API.Interfaces.Graphs;
class Edge{

//Fields
private Node _node1;
private Node _node2;

public Edge(){
_node1 = new Node();
_node2 = new Node();
}
public Edge(Node n1,Node n2){
    _node1 = n1;
    _node2 = n2;
}

public Node node1{
    get{
        return _node1;
    }
    set{
        _node1 = value;
    }
}

public Node node2{
    get{
        return _node2;
    }
    set{
        _node2 = value;
    }
}

public override string ToString(){
return node1.name+","+_node2.name;
}

public string undirectedString(){
    return "{"+node1.name+","+_node2.name+"}";
}
public string directedString(){
    return "("+node1.name+","+_node2.name+")";
}
public KeyValuePair<string,string> toKVP(){
    KeyValuePair<string,string> asKVP = new KeyValuePair<string, string>(node1.name,node2.name);
    return asKVP;
}

}