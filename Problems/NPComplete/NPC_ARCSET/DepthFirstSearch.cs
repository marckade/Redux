// //DepthFirstSearch.cs
// using System;
// using System.Collections.Generic;

// namespace API.Problems.NPComplete.NPC_ARCSET;
//  static class DepthFirstSearch{




// //input: G (V,E) is a graph v is in V
// //output: visited(u) is set to true for all nodes u reachable from v
//   private void explore(DirectedGraph inputG, KeyValuePair currentNode, KeyValuePair mapNodeNum,bool[] visited,int[] previsitarr,int[] postVisitArr,Dictionary nodePositionDict,Dictionary nodePreDict,Dictionary nodePostDict,int counter){

//     visited[mapNodeNum.Value] = true; //sets this node as visited.
//     counter++;
//     previsitArr[mapNodeNum.Value] = counter;
//     Dictionary adjMatrix = inputG.adjacencyMatrix;
//     List adjList;
//     adjMatrix.TryGetValue[currentNode.Key, out adjList];

//     Console.Write("Current Node: "+currentNode.Key + "previsit: "+ previsitarr[mapNodeNum.Value]);
//     foreach(KeyValuePair kvp in adjList ){
//         int position;
//         nodePositionDict.TryGetValue(kvp.Key,out position); //get the position associated with the name
//         bool nodeIsVisited = visited[position]; //has this node been visited?
//         if(!nodeIsVisited){
//             explore(inputG,currentNode,mapNodeNum,visited,previsitArr,postvisitArr,nodePositionDict,nodePreDict,nodePostDict,counter);
//         }
//         postVisitArr[position] = counter;
//         Console.Write(" Postvisit: ");
//     }

// }
//  static public void DFS(DirectedGraph inputG){
//     bool[] visited = new bool[inputG.getNodeList.Count]; //makes array equal entry for entry to nodeList
//     int[] previsitArr = new int[inputG.getNodeList.Count];
//     int[] postvisitArr = new int[inputG.getNodeList.Count];
//     int i = 0;
//     KeyValuePair<string,int> mapNodePos; //we want to map our node name to a position int
//     Dictionary<KeyValuePair<string,int>> nodePositionDict = new Dictionary<>(); //creates a dictionary of KVPs

//     KeyValuePair<string,int> mapNodePreVis; //we want to map our node name to a previsit int
//     Dictionary<KeyValuePair<string,int>> nodePreDict = new Dictionary<>(); //creates a dictionary of KVPs

//     KeyValuePair<string,int> mapNodePosVis; //we want to map our node name to a previsit int
//     Dictionary<KeyValuePair<string,int>> nodePostDict = new Dictionary<>(); //creates a dictionary of KVPs

//     foreach(KeyValuePair nodeKVP in inputG.nodeDict){
//     visited[i] = false; //sets initial visit value of every node to false
//     mapNodePos = new KeyValuePair<string, int>(nodeKVP.Key,i); //maps name of node to position
//     nodeNumDict.Add(mapNodePos); //now nodeNumDict will be able to find a position given a name.
//     i++;
//     }
//     int counter = 0;
//     foreach(KeyValuePair entry in inputG.nodeDict){
//     int mappedPos = nodePositionDict.TryGetValue(entry.Key); //looks for a position given name.
//     if(!visited[mappedPos])
//     { //if the boolean visit array sees the position isn't visited
//     explore(inputG,currentNode,mapNodeNum,visited,previsitArr,PostvisitArr,nodePositionDict,nodePreDict,nodePostDict,counter); //explore the position (start recursion).
//     }
//     }
// }

// }

