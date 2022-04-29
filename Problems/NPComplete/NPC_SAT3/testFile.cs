

public class testFile{

    static void Main(string[] args)
    {
        // Display the number of command line arguments.
        SAT3 testSAT = new SAT3();
        Dictionary<string, bool> testOneRes = solve(testSAT);
        foreach(KeyValuePair<string, bool> kvp in testOneRes){
            Console.WriteLine(kvp.Key + " : " + kvp.Value.ToString);
        }
    }


    // //Takes in a new SATState with boolean values written to the states and evaluates them
    //Returns -1 if unsolvable
    //Returns 0 if undecided
    //Returns 1 if satisfiable
    private Integer evaluateBooleanExpression(List<List<string>> boolExp){
        //if any entries are "!false" or "true" return null
        //else if "false" or "!true" is encountered remove it from the List and return the modified List
        //Check for satisfiablility
        retVal = null;
        if(boolExp.Length == 1 && boolExp.get(0).toString() == "()"){
            retVal = 1;
        }


        //variables for looping through boolExp
        int index = 0;
        string exp;
        //evaluates the string representation of each clause, if the expression is empty then it is unsatisfiable
        while(retVal == null && index < boolExp.Length){
            exp = boolExp.get(index).toString();
            if(expVar.toString() == "()"){
                retVal = -1;
            }
            index++;
        }

        //If it is not satisfiable or unsolvable it must be undecided
        if(retVal == null){
            retVal = 0;
        }

        return retVal;
    }

    private int findVariables(List<string> literals){
        Dictionary<string, int> numbVars = new Dictionary<string, int>();
        int count = 0;
        foreach(string lit in literals){
            if(!numbVars.Contains(lit[lit.Length - 1])){
                numbVars.Add(lit, 1);
                count++;
            }
        }
        return count;
    }

    // Return type varies
    public Dictionary<string, bool> solve(SAT3 sat3) {


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
        Dictionary<string, bool> solution = null;

        int totalNumberOfVariables = findVariables(sat3.literals);


        // string var;
        SAT3PQObject curSat;
        int eval;

        //add initial SAT3 to PQ
        satPQ.Add(sat3, 0, totalNumberOfVariables);
        
        //O(n!*pruning function)
        while(!solutionFound && satPQ.Count > 0){
            curSat = satPQ.Dequeue();
            List<SAT3PQObject> childSATs = curSat.createSATChildren(curSat.depth, totalNumberOfVariables);
            foreach(SAT3PQObject childSAT in childSATs){
                eval = evaluateBooleanExpression(childSAT);
                if(eval == 0){
                    //undecided
                    satPQ.Add(childSAT, childSAT.getPQWeight());
                }
                else if(eval == 1){
                    //satisfiable
                    //WRITE ASSIGNMENTS OF VARIABLES
                    solutionFound = true;
                    solution = childSAT.varStates;

                }
                //else unsolvable therefore dont add
            }
        }

        // Logic goes here
        return new Dictionary<string, bool>();
    }
}