
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace API.Interfaces.Graphs.GraphParser;

class GraphParser {
     

public GraphParser(){
}
 /**
 * Checks if an input string is a valid undirected graph. 
 **/
public bool isValidUndirectedGraph(string undirectedGraphStr){
    string pattern;
    pattern = @"{{((\w)*(\w,)*)+},{(({\w,\w})*({\w,\w},)*)*},\d+}"; //checks for undirected graph format
    Regex reg = new Regex(pattern);
    bool inputIsValid = reg.IsMatch(undirectedGraphStr);
    return inputIsValid;
}

 /**
 * Checks if an input string is a valid directed graph. 
 **/
public bool isValidDirectedGraph(string directedGraphStr){
 string pattern;
    pattern = @"{{((\w)*(\w,)*)+},{((\(\w,\w\))*(\(\w,\w\),)*)*},\d+}"; //checks for directed graph format
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
        string edgePattern = @"{(({\w,\w})*({\w,\w},)*)*}"; //outer edge pattern. from {{a,b,...,z},{{a,b},{c,d},...,{y,z}},k} --> {{a,b},{b,c},...,{y,z}}. Ie. removes nodes and k from a graph.
        edgeList =edgesGivenValidGraphAndPattern(graphString, edgePattern);
        }
    
    else if(isValidDirectedGraph(graphString)){

        string edgePattern = @"{((\(\w,\w\))*(\(\w,\w\),)*)*}";  //outer edge pattern. from {{a,b,...,z},{(a,b),(c,d),...,(y,z)},k} --> {(a,b),(b,c),...,(y,z)}
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

/// <summary>
/// Given a list of nodes in the string format {a,b,c} 
/// returns a list of strings ["a","b","c"]
/// </summary>
/// <returns></returns>
/// <remarks>
/// only supports word characters  (multicharacter supported) currently, not special characters or ! symbols.
public List<string> getNodesFromNodeListString(string input){
        List<string> retList = new List<string>();

        try{
        string pattern = @"{(\w+)(,\w+)*}";
        MatchCollection matches = Regex.Matches(input,pattern);
        string foundString = matches[0].ToString();
        string innerPattern = @"(\w+)(,\w+)*";
        MatchCollection matchesInner = Regex.Matches(input,innerPattern);
        string foundString2 = matchesInner[0].ToString();
            string[] splitStr = foundString2.Split(',');
            foreach(string n in splitStr){
                retList.Add(n);
            }


        }
        catch(Exception e){
            Console.WriteLine("Invalid input GraphParser getNodesFromNodeListString");
        }
        

        return retList;
    }

}