//Graph.cs
//This is an abstract class for Undirected and Directed graphs to inherit from

using System.Collections.Generic;
namespace API.Problems.NPComplete.NPC_ARCSET;


 abstract class Graph{

 protected List<Node> nodeList;
 protected List<Edge> edgeList;


protected abstract List<string> getNodes(string gInput);
protected abstract List<KeyValuePair<string,string>> getEdges(string gInput);

protected abstract int getK(string gInput);

//  public object Clone(){
//         return this.MemberwiseClone();
//     }

}