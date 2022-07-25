using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using API.Interfaces.Graphs;
namespace API.Problems.NPComplete.NPC_GRAPHCOLORING;

class GraphColoringGraph : UndirectedGraph {

   /// <summary>
 /// Takes a String and creates a VertexCoverGraph from it
 /// NOTE: DEPRECATED format, ex: {{a,b,c} : {{a,b} & {b,c}} : 1}
 /// </summary>
 /// <param name="stringInput"> string input</param>
  public GraphColoringGraph(string stringInput) {

     string pattern =
      @"{{(([\w!]+)(,([\w!]+))*)+},{(\{([\w!]+),([\w!]+)\}(,\{([\w!]+),([\w!]+)\})*)*},\d+}";

       Regex reg = new Regex(pattern);
        bool inputIsValid = reg.IsMatch(stringInput);
        if(inputIsValid){
          //nodes 

            string nodePattern = @"{((([\w!]+))*(([\w!]+),)*)+}";
            MatchCollection nMatches =  Regex.Matches(stringInput,nodePattern);
            string nodeStr = nMatches[0].ToString();
            nodeStr = nodeStr.TrimStart('{');
            nodeStr = nodeStr.TrimEnd('}');
            string[] nodeStringList = nodeStr.Split(',');
            foreach(string nodeName in nodeStringList){
               _nodeList.Add(new Node(nodeName));
                _nodeStringList.Add(nodeName);
           }

        
             //edges
            string edgePattern = @"{(\{([\w!]+),([\w!]+)\}(,\{([\w!]+),([\w!]+)\})*)*}";
            MatchCollection eMatches = Regex.Matches(stringInput
            ,edgePattern);
            string edgeStr = eMatches[0].ToString();
            //Console.WriteLine(edgeStr);
            string edgePatternInner = @"([\w!]+),([\w!]+)";
            MatchCollection eMatches2 = Regex.Matches(edgeStr,edgePatternInner);
            foreach(Match medge in eMatches2){
                string[] edgeSplit = medge.ToString().Split(',');
                Node n1 = new Node(edgeSplit[0]);
                Node n2 = new Node(edgeSplit[1]);
                _edgeList.Add(new Edge(n1,n2));
                KeyValuePair<string, string> tempKVP = new KeyValuePair<string, string>(edgeSplit[0], edgeSplit[1]);
                  KeyValuePair<string, string> reverseKVP = new KeyValuePair<string, string>(edgeSplit[1], edgeSplit[0]);
                _edgesKVP.Add(tempKVP);
                _edgesKVP.Add(reverseKVP);
            }

             //end num
            string endNumPatternOuter = @"},\d+}"; //gets the end section of the graph string
            MatchCollection numMatches = Regex.Matches(stringInput,endNumPatternOuter);
            string outerString = numMatches[0].ToString();
            string endNumPatternInner = @"\d+"; //parses out number from end section.
            MatchCollection numMatches2 = Regex.Matches(outerString,endNumPatternInner);
            string innerString = numMatches2[0].ToString();

            int convNum = Int32.Parse(innerString);

            _K = convNum;

        }else{
            Console.WriteLine("NOT VALID INPUT for Regex evaluation! INITIALIZATION FAILED"); 
        }
        
    }

    //Constructor for standard graph formatted string input.
     /// <summary>
     /// 
     /// </summary>
     /// <param name="cliqueInput"> Undirected Graph string input
     /// ex. {{1,2,3},{{1,2},{2,3}},0}
     /// </param>
     /// <param name="decoy"></param>
    public GraphColoringGraph(string cliqueInput, bool decoy) : base (cliqueInput, decoy){

    }


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
             KeyValuePair<string,string> reverseEdge = new KeyValuePair<string,string>(nodeTo, nodeFrom);
            allGEdges.Add(fullEdge);
            allGEdges.Add(reverseEdge);
        }

        return allGEdges;
    }


}