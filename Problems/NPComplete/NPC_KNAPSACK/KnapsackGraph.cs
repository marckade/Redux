using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using API.Interfaces.Graphs;

namespace API.Problems.NPComplete.NPC_KNAPSACK;

    class KnapsackGraph:DirectedGraph {

        
    public KnapsackGraph(string arcInput) : base (arcInput){
        
    }

    public KnapsackGraph(string arcInput, bool decoy) : base (arcInput, decoy){
        _edgeList.Sort();
    }


    public override String toDotJson(){
        string totalString = $"";
        string preStr = @"digraph KNAPSACK{";
        totalString = totalString + preStr;

        //string preStr2 = @"node[style = ""filled""]";
        //totalString = totalString+preStr2;
        
        string dotNode = ""; 
       // string colorRed = "#d62728";
        foreach(Node n in _nodeList){
        dotNode=$"{n.name}";
        //dotNode=$"{n.name} [{colorRed}]";
        totalString = totalString+ dotNode + ";";
        }
        //totalString = totalString.TrimEnd(',');

        foreach(Edge e in _edgeList){
            KeyValuePair<string,string> eKVP = e.toKVP();
            string edgeStr = $" {eKVP.Key} -> {eKVP.Value};";
            totalString = totalString + edgeStr;
        }

        totalString = totalString+ "}";
        
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(totalString, options);
        return jsonString;
    }
        
}
