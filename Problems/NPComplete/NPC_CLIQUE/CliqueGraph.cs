using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using API.Interfaces.Graphs;
namespace API.Problems.NPComplete.NPC_CLIQUE;

class CliqueGraph : UndirectedGraph
{

   /// <summary>
 /// Takes a String and creates a VertexCoverGraph from it
 /// NOTE: DEPRECATED format, ex: {{a,b,c} : {{a,b} & {b,c}} : 1}
 /// </summary>
 /// <param name="graphStr"> string input</param>
  public CliqueGraph(string cliqueInput) : base (cliqueInput){
        
    }

    //Constructor for standard graph formatted string input.
     /// <summary>
     /// 
     /// </summary>
     /// <param name="graphStr"> Undirected Graph string input
     /// ex. {{1,2,3},{{1,2},{2,3}},0}
     /// </param>
     /// <param name="decoy"></param>
    public CliqueGraph(string cliqueInput, bool decoy) : base (cliqueInput, decoy){
    

    }


}