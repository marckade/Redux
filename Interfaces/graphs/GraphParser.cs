
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace API.Interfaces.Graphs;

class GraphParser {
     

public GraphParser(){
}
 /**
 * Checks if an input string is a valid undirected graph. 
 **/
public bool isValidUndirectedGraph(string undirectedGraphStr){
    string pattern;
    pattern = @"{{((\w)*(\w,)*)+},{(({\w,\w})*({\w,\w},)*)*}:\d+}"; //checks for undirected graph format
    Regex reg = new Regex(pattern);
    bool inputIsValid = reg.IsMatch(undirectedGraphStr);
    return inputIsValid;
}

 /**
 * Checks if an input string is a valid directed graph. 
 **/
public bool isValidDirectedGraph(string directedGraphStr){
 string pattern;
    pattern = @"{{((\w)*(\w,)*)+},{((\(\w,\w\))*(\(\w,\w\),)*)*}:\d+}"; //checks for directed graph format
    Regex reg = new Regex(pattern);
    bool inputIsValid = reg.IsMatch(directedGraphStr);
    return inputIsValid;

}

 /**
 * Checks if input is a directed or undirected graph and then if it is, returns a list of edges that the graph contains.
 **/
public List<Edge> getGraphEdgeList(string graphString){
    List<Edge> edgeList;
    if(isValidUndirectedGraph(graphString)){
        string edgePattern = @"{(({\w,\w})*({\w,\w},)*)*}"; //outer edge pattern. from {{a,b,...,z},{{a,b},{c,d},...,{y,z}}:k} --> {{a,b},{b,c},...,{y,z}}. Ie. removes nodes and k from a graph.
        edgeList =edgesGivenValidGraphAndPattern(graphString, edgePattern);
        }
    
    else if(isValidDirectedGraph(graphString)){

        string edgePattern = @"{((\(\w,\w\))*(\(\w,\w\),)*)*}";  //outer edge pattern. from {{a,b,...,z},{(a,b),(c,d),...,(y,z)}:k} --> {(a,b),(b,c),...,(y,z)}
        edgeList = edgesGivenValidGraphAndPattern(graphString,edgePattern);
    }
    else{
        throw new ArgumentException("Invalid Input",graphString);
    }
    return edgeList;
}


/**
* Helper parser method for getGraphEdgeList();
**/
private List<Edge> edgesGivenValidGraphAndPattern(string validGraphStr,string edgePattern){
    List<Edge> edgeList = new List<Edge>();
    MatchCollection eMatches = Regex.Matches(validGraphStr,edgePattern);
    string edgeStr = eMatches[0].ToString();
    string edgePatternInner = @"\w,\w"; //inner edge patten. Spots any "a,b" pattern from {{a,b},{b,c},...,{y,z}} (directed or undirected)
    MatchCollection eMatches2 = Regex.Matches(edgeStr,edgePatternInner);
    foreach(Match medge in eMatches2){
        string[] edgeSplit = medge.ToString().Split(','); //splits "a,b" string literal into ["a","b"] array.
        Node n1 = new Node(edgeSplit[0]);
        Node n2 = new Node(edgeSplit[1]);
        edgeList.Add(new Edge(n1,n2)); //creates an edge from the array positions. 
    }
    return edgeList;

}

}

