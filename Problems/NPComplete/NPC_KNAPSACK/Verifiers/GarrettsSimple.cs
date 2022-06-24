using API.Interfaces;
using System;


namespace API.Problems.NPComplete.NPC_KNAPSACK.Verifiers;

class GarrettsSimple : IVerifier {

    // --- Fields ---
    private string _verifierName = "Generic Verifier";
    private string _verifierDefinition = "This is a verifier for KNAPSACK";
    private string _source = " ";
    
    private string _complexity =" _____";
    private string _certificate ="";


    // --- Properties ---
    public string verifierName {
        get {
            return _verifierName;
        }
    }
    public string verifierDefinition {
        get {
            return _verifierDefinition;
        }
    }
    public string source {
        get {
            return _source;
        }
    }
    public string complexity {
        get {
            return _complexity;
        }
    }
     public string certificate {
        get {
            return _certificate;
        }
    }

    // --- Methods Including Constructors ---
    public GarrettsSimple() {
        
    }

    // Needs to verify that the userInput is a subset of the items in the given Knapsack problem 
    //and that each item is only in the subset 0 or 1 times. 
    // Then must check that userInput meets the constraint W. 
    public Boolean verify(KNAPSACK problem, string userInput){
        bool isInKnapsack =true;
         //list of items
         List<KeyValuePair<string,string>> items =  problem.items;
        string strippedInput = userInput.Replace(" ", "").Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");
        string[] inputItems = userInput.Split(":");
        double combinedWeight = 0;
        double combinedValue = 0;
        List<KeyValuePair<double,double>> doubleItems = new List<KeyValuePair<double, double>> {};
        

        foreach(string inputItem in inputItems) {
            string[] fromTo = inputItem.Split(",");
            string nodeFrom = fromTo[0];
            string nodeTo = fromTo[1];
            // if the user inputed an item not contained in the KNAPSACK problem, then it is not a solution
            KeyValuePair<string, string> fullItem = new KeyValuePair<string, string>(nodeFrom, nodeTo);
            if (items.Contains(fullItem)){
                isInKnapsack = false;
            }
            //convert the user list of items to doubles to preform algebra
            double itemweight = Convert.ToDouble(nodeFrom);
            double itemvalue =  Convert.ToDouble(nodeTo);
            KeyValuePair<double, double> doubleitem =new KeyValuePair<double,double>(itemweight, itemvalue);
            // If the user entered list of items contains duplicates, it is not a solution.
            if (doubleItems.Contains(doubleitem)){
                isInKnapsack = false;
            }
            doubleItems.Add(doubleitem);
            combinedWeight = combinedWeight + itemweight;
            combinedValue = combinedValue + itemvalue;
        }

        //If the weight is under the Costraint, it is not a solution
        if (Convert.ToDouble(problem.W) < combinedWeight){
            isInKnapsack = false;
        }
        
        

        return isInKnapsack;
    }

}