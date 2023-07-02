using API.Interfaces;
using System;


namespace API.Problems.NPComplete.NPC_KNAPSACK.Verifiers;

class GarrettVerifier : IVerifier
{

    // --- Fields ---
    private string _verifierName = "Generic Verifier";
    private string _verifierDefinition = "This is a verifier for KNAPSACK made by Garret Stouffer and Daniel Igbokwe. It checks to see if weights are repeated, it checks if weights are defined in the certificate but not in the problem weights, and if the sum of the certificate weights is less than or equal to the capacity ";
    private string _source = "Garrett Stouffer";
    private string[] _contributers = { "Garret Stouffer", "Daniel Igbokwe"};

    private string _complexity = "O(n^2)";

    private string _certificate = "{(30:120,20:100):220}";


    // --- Properties ---
    public string verifierName
    {
        get
        {
            return _verifierName;
        }
    }
    public string verifierDefinition
    {
        get
        {
            return _verifierDefinition;
        }
    }
    public string source
    {
        get
        {
            return _source;
        }
    }
    public string[] contributers
    {
        get
        {
            return _contributers;
        }
    }
    public string complexity
    {
        get
        {
            return _complexity;
        }
    }

    public string certificate
    {
        get
        {
            return _certificate;
        }
    }


    // --- Methods Including Constructors ---
    public GarrettVerifier()
    {

    }

    // Needs to verify that the userInput is a subset of the items in the given Knapsack problem 
    //and that each item is only in the subset 0 or 1 times. 
    // Then must check that userInput meets the constraint W. 
    public Boolean verify(KNAPSACK problem, string userInput)
    {
         double combinedWeight = 0;
        double combinedValue = 0;
        //list of items
        List<KeyValuePair<string, string>> items = problem.items;
         List<KeyValuePair<string, string>> doubleItems = new List<KeyValuePair<string, string>> { };
        Dictionary<string, string> inputItems = parseCertificate(userInput);


        foreach (var inputItem in inputItems)
        {
            string nodeFrom = inputItem.Key;
            string nodeTo = inputItem.Value;
            // if the user inputed an item not contained in the KNAPSACK problem, then it is not a solution
            KeyValuePair<string, string> fullItem = new KeyValuePair<string, string>(nodeFrom, nodeTo);
            Console.WriteLine("Current certificate weight, value: "+fullItem.Key + " "+ fullItem.Value);
            if (!containsValue(items, fullItem))
            {
                return false;
            }
            //convert the user list of items to doubles to preform algebra
            double itemweight = Convert.ToDouble(nodeFrom);
            double itemvalue = Convert.ToDouble(nodeTo);
          //  KeyValuePair<double, double> doubleitem = new KeyValuePair<double, double>(itemweight, itemvalue);
            // If the user entered list of items contains duplicates, it is not a solution.
            if (containsValue(doubleItems, fullItem))
            {
               return false;
            }
            doubleItems.Add(fullItem);
            combinedWeight = combinedWeight + itemweight;
            combinedValue = combinedValue + itemvalue;
        }

        Console.WriteLine("Combined Weight: "+ combinedWeight + " Capacity: "+ problem.W );

        //If the weight is over the Costraint, it is not a solution
        if (combinedWeight <= Convert.ToDouble(problem.W) && inputItems.Count > 0)
        {
            return true;
        }



        return false;
    }


    private bool containsValue(List<KeyValuePair<string, string>> listOfItems, KeyValuePair<string, string> item ){

        foreach (var elem in listOfItems ){
            if (elem.Key.ToLower().Equals(item.Key.ToLower())  && elem.Value.ToLower().Equals(item.Value.ToLower()) ){
                return true;
            }
        }


        return false;
    }


    private Dictionary<string, string> parseCertificate(string certificate)
    {

        // string parseCertificate = certificate.Replace("(", "").Replace(")","");
        string parseCertificate = certificate.Trim().Replace("{", "").Replace("}", "").Replace(" ", "");
        string[] splitCertificate = parseCertificate.Split("):");
        string dictionary = splitCertificate[0].Replace("(", "").Replace(")", "");
        int k = Int32.Parse(splitCertificate[1]);

        Dictionary<string, string> weightValues = new Dictionary<string, string>();


        if (parseCertificate.Length != 0)
        {
            // string[] nodes = parseCertificate.Split(',');
            string[] nodes = dictionary.Split(',');

            foreach (string node in nodes)
            {
                string[] nodeColor = node.Split(':');


                string key = nodeColor[0].Trim();
                string val = nodeColor[1].Trim();


                // check if dictionary contains key first

                if(!weightValues.ContainsKey(key)){
                    weightValues.Add(key, val);
                }else{
                    weightValues =  new Dictionary<string, string>();
                    break;
                }

                

            }

        }

        // _coloring =  nodeColoring;
        // _k =  k;
        return weightValues;
    }


}