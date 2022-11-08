using API.Interfaces;

namespace API.Problems.NPComplete.NPC_KNAPSACK.Solvers;
class GarrettKnapsackSolver : ISolver {

    // --- Fields ---
    private string _solverName = "GarrettKnapsack Solver";
    private string _solverDefinition = "This a Dynamic programming solver for the 0-1 Knapsack problem made by Garrett Stouffer.";
    private string _source = "This person ____";
    private string[] _contributers = { "Garrett Stouffer"};


    private string _complexity = " O(v*w). Complexity of this problem depends on size of input values. When inputs are binary it's complexity is exponential.";

    // --- Properties ---
    public string solverName {
        get {
            return _solverName;
        }
    }
    public string solverDefinition {
        get {
            return _solverDefinition;
        }
    }
    public string source {
        get {
            return _source;
        }
    }
     public string[] contributers{
        get{
            return _contributers;
        }
    }
    public string complexity {
        get {
            return _complexity;
        }
    }
    // --- Methods Including Constructors ---
    //solver for 0-1 knapsack problem
    public string solve(KNAPSACK knapsack) {
        // returns the maximum value achievable given the the weight constraints on the given knapsack.
        
        List<KeyValuePair<String, String>> allitems = knapsack.items; 
        int Capacity = knapsack.W;

     // {{10,20,30},{(10,60),(20,100),(30,120)},50}
     // {{1,2,3,5,7,9},{(1,5),(2,7),(3,9),(1,7)},5}

        int[,] matrix = new int[allitems.Count +1 , Capacity + 1];
        //iterate through each item
        for (int i=0; i < allitems.Count + 1; i++){
            //iterate through each of the different weight values starting at 0 until W
            for(int j=0; j<Capacity +1; j++){
                //initializing all matrix[0,j] and matrix[i,0] to 0
                if(i==0 || j==0){
                    matrix[i,j] = 0;
                    //break to the next iteration
                    continue;
                }
                var currentItem = allitems[i-1];
 
                if (Int32.Parse(currentItem.Key) > j){
                    matrix[i,j] = matrix[i-1,j];
                }
                else {
                    matrix[i,j] = Math.Max(Int32.Parse(currentItem.Value) + matrix[i-1, j- Int32.Parse(currentItem.Key)], matrix[i-1,j]);

                }
            }
        }

        int count = allitems.Count;
        int tempCap = Capacity;
        string solution = "{(";
   

        while(count != 0){

            if(matrix[count , tempCap] != matrix[count -1, tempCap]){
                
                var current = allitems[count-1];
                tempCap = tempCap - Int32.Parse(current.Key);
              //  Console.WriteLine("Package " + count.ToString() + "with W = " + Int32.Parse(current.Key).ToString() + " and Value = "+Int32.Parse(current.Value).ToString());
            // solution +=  current.Key + " : " + current.Value + ", ";
                solution +=  current.Key + ":" + current.Value + ",";
            }

            count--;

        }
        solution = solution.Trim(',');

        solution += "):"+ matrix[allitems.Count, Capacity].ToString()+"}";
        
        return solution;
    }

}