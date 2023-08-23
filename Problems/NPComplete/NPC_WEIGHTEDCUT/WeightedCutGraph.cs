using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using API.Interfaces.Graphs;
namespace API.Problems.NPComplete.NPC_WEIGHTEDCUT;

class WeightedCutGraph : WeightedUndirectedGraph
{

   /// <summary>
 /// Takes a String and creates a VertexCoverGraph from it
 /// NOTE: DEPRECATED format, ex: {{a,b,c} : {{a,b} &amp; {b,c}} : 1}
 /// </summary>
 /// <param name="cutInput"> string input</param>
  public WeightedCutGraph(string cutInput) : base (cutInput){
        
    }

    //Constructor for standard graph formatted string input.
     /// <summary>
     /// 
     /// </summary>
     /// <param name="cutInput"> Undirected Graph string input
     /// ex. {{1,2,3},{{1,2},{2,3}},0}
     /// </param>
     /// <param name="decoy"></param>
    public WeightedCutGraph(string cutInput, bool decoy) : base (cutInput, decoy){
    

    }

  /// <summary>
  /// This is an alternative constructor that would add native custom node support. This would mean that a WeightedCutGraph could have an arbitrary 
  /// amount of, and naming convention for, its nodes. 
  /// </summary>
  /// <param name="cutInput"></param>
  /// <param name="usingCutNodes"></param>
  public WeightedCutGraph(string cutInput, string usingCutNodes){
        string pattern;
        string patternWithTerminals;
        pattern = @"{{(([\w!]+)(,([\w!]+))*)+},{(\{([\w!]+),([\w!]+),([\d]+)\}(,\{([\w!]+),([\w!]+),([\d]+)\})*)*},\d+}"; //checks for undirected graph format with weights
        patternWithTerminals = @"{{(([\w!]+)(,([\w!]+))*)+},{(\{([\w!]+),([\w!]+)\}(,\{([\w!]+),([\w!]+)\})*)*},{(([\w!]+)(,([\w!]+))*)+},\d+}"; //checks for undirected graph format with terminals
        Regex reg = new Regex(pattern);
        Regex regTerminal = new Regex(patternWithTerminals);
        bool inputIsValid = reg.IsMatch(cutInput);
        bool terminalInputIsValid = regTerminal.IsMatch(cutInput);
        if (inputIsValid || terminalInputIsValid)
        {

            //nodes
            string nodePattern = @"{((([\w!]+))*(([\w!]+),)*)+}";
            MatchCollection nMatches = Regex.Matches(cutInput, nodePattern);
            string nodeStr = nMatches[0].ToString();
            nodeStr = nodeStr.TrimStart('{');
            nodeStr = nodeStr.TrimEnd('}');
            string[] nodeStringList = nodeStr.Split(',');
            foreach (string nodeName in nodeStringList)
            {
                _nodeList.Add(new Node(nodeName));
            }
            //Console.WriteLine(nMatches[0]);

            //edges
            string edgePattern = @"{(\{([\w!]+),([\w!]+),([\d]+)\}(,\{([\w!]+),([\w!]+),([\d]+)\})*)*}";
            MatchCollection eMatches = Regex.Matches(cutInput, edgePattern);
            string edgeStr = eMatches[0].ToString();
            string edgePatternInner = @"([\w!]+),([\w!]+),([\d]+)";
            MatchCollection eMatches2 = Regex.Matches(edgeStr, edgePatternInner);
            foreach (Match medge in eMatches2)
            {
                string[] edgeSplit = medge.ToString().Split(',');
                Node n1 = new Node(edgeSplit[0]);
                Node n2 = new Node(edgeSplit[1]);
                int weight = Int32.Parse(edgeSplit[2]);
                
                _edgeList.Add(new Edge(n1, n2, weight));
            }

            //end num
            string endNumPatternOuter = @"},\d+}"; //gets the end section of the graph string
            MatchCollection numMatches = Regex.Matches(cutInput, endNumPatternOuter);
            string outerString = numMatches[0].ToString();
            string endNumPatternInner = @"\d+"; //parses out number from end section.
            MatchCollection numMatches2 = Regex.Matches(outerString, endNumPatternInner);
            string innerString = numMatches2[0].ToString();

            int convNum = Int32.Parse(innerString);

            _K = convNum;


            foreach (Node n in _nodeList)
            {
                _nodeStringList.Add(n.name);
            }
            foreach (Edge e in _edgeList)
            {
                _edgesTuple.Add((e.source.name, e.target.name, e.weight)); //
            }

        }
        else
        {
            Console.WriteLine("NOT VALID INPUT for Regex evaluation! INITIALIZATION FAILED");
        }

    }


}