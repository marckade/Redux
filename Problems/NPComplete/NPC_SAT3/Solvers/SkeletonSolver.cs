using API.Interfaces;

namespace API.Problems.NPComplete.NPC_SAT3.Solvers;
class SkeletonSolver : ISolver {

    // --- Fields ---
    private string _solverName = "Generic Solver";
    private string _solverDefinition = "This is a skeleton for the solver for SAT3";
    private string _source = "Kaden";

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
    // --- Methods Including Constructors ---
    public SkeletonSolver(SAT3 sat3) {
        //The example problem can be solved in O(n) time just by evaluating if any of the expressions dont have conflicting variables ie(!x and x)

        ////O(n!*pruning function?)
        // while(!solutionFound && !satQueue.isEmpty()):
        // 	var = varQueue.pop() //O(1)
        // 	if(var != null):
        // 		//O(1+n*2)
        // 		//create new satNode with var.truthVal = true, evaluate the statement and update the neccisary variables then if the solution is still valid push it to satQueue
        // 		//create new satNode with var.truthVal = false, evaluate the statement and update the neccisary variables then if the solution is still valid push it to satQueue
        // 		//prioritize evaluating depth over breadth
        // 		//when evaluating statements if any evaluate to true set solution equal to the satNode and set solutionFound to True (exiting the loop)
        // 		//a potential pruning function would be to prioritize any variable that is alone in a statement and evaluating that to its required value (ex (y) must evaluate to true)
        // 		//to resolve cases where there are two contradicting statements ex (y), (!y) always choose the first statment to satisfy before evaluating the whole expression
        // 		//this pruning function would attempt to imediatly evaluate the first standalone expression as the next node (after current processing is done)
        bool solutionFound = false;
        PriorityQueue<SAT3PQObject, int> satPQ = new PriorityQueue<SAT3PQObject, int>();


        // string var;
        SAT3PQObject curSat;

        //add initial SAT3 to PQ
        
        //O(n!*pruning function)
        while(!solutionFound && satPQ.Count > 0){
            curSat = satPQ.Dequeue();
            List<SAT3PQObject> childSATs = curSat.
            //curSat.evaluate()
            //TODO: WRITE evaluate() method

        }



        // if(solutionFound):
        // 	return true
        // else:
        // 	return false


    }

    // Return type varies
    public Dictionary<string, bool> solve() {

        // Logic goes here
        return new Dictionary<string, bool>();
    }
}