
using API.Problems.NPComplete.NPC_SAT3;

class SAT3PQObject{
    public SAT3 SATState;
    public int depth;
    public int totalVars;
    public Dictionary<string, int> varWeights;
    public Dictionary<string, bool> varStates;
    public PriorityQueue<string, int> varPQ;

    public SAT3PQObject(SAT3 inSAT3, int newDepth, int totalVariables){
        SATState = inSAT3;
        PriorityQueue<string, int> varPQ = makeVarPQ(inSAT3.literals);
        int depth = newDepth;
        int totalVars = totalVariables;
        varWeights = new Dictionary<string, int>();
        varStates = new Dictionary<string, bool>();
        // int smallestClauseSize = 3;
    }

    //called when initializing the object.
    //reads through the parsed input string removing all ! symbols then assigning them
    // private PriorityQueue<string, int> makeVarPQ(){
    //     //TODO: WRITE PRIORITY QUEUE FOR VARS

    //     //we pop the variable, remove it from the VarPQ and remove and replace all affected variables while updating the var weights in the hashmap
    // }

    private string getHighestVar(){
        string highVar = string.Empty;
        int highVal = 0;
        foreach(KeyValuePair<string, int> kvp in this.varWeights){
            if(kvp.Value > highVal){
                highVal = kvp.Value;
                highVar = kvp.Key;
            }
        }
        return highVar;
    }

    public int getPQWeight(){
        //TODO: WRITE COMPARATOR FUNCTION
        // int priority;
        //if the varQs next element has a size of 1 set PQ weight to -1
        // if(this.SATState)
        //else the method should prioritize depth and lower varQ priority
        return this.totalVars - this.depth - this.varWeights.GetValueOrDefault(getHighestVar());
        
    }

    //Returns the two sats 
    public List<SAT3PQObject> createSATChildren(int depth, int totalNumberOfVariables){
        string var = this.varPQ.Dequeue();
        List<SAT3PQObject> outList = new List<SAT3PQObject>();
        outList.Add(createNewSatState(var, true, depth, totalNumberOfVariables));
        outList.Add(createNewSatState(var, false, depth, totalNumberOfVariables));
        return outList;
    }

    //Takes in a variable and a boolean value and returns the new SATState
    //Returns null if the new state would be unviable
    private SAT3PQObject createNewSatState(string var, bool boolValue, int depth, int totalNumberOfVariables){ //This is the meat and potatoes of the solver
        //update the variables to the string representation of the boolean value
        //pass the modified clause to the evaluateBooleanExpression method
        //if the evaluation returns a viable expression it will return a string else it will returns null
        //return the result

        //adds the variable boolean state dictonary
        this.varStates.Add(var, boolValue);

        
        //create a new phiInput
        //Then construct a new SAT3 with that phi input
        string newPhiExpression = "(";
        string tempExpression = "";

        //THIS IS THE CODE THAT PROCESSES THE CREATION OF THE NEW STATE
        //THIS DOES NOT CHECK SATISFIABILITY
        //Iterates through each clause evaluating and creating the new state and writting it to the appropriate out list
        foreach(List<string> boolExp in this.SATState.clauses){
            //adds the AND clause inbetween statements
            if(newPhiExpression.EndsWith(")")){
                newPhiExpression += "&";
            }
            tempExpression = "";
            //eval code
            foreach(string expVar in boolExp){
                //if the var
                if(expVar.Contains(var)){
                    //checks if the boolean value returns true, if so accepts the expression and breaks the loop
                    if((expVar[0] == '!' && boolValue == false) || boolValue == true){
                        //UPDATE HM
                        updateHM(boolExp, var);
                        tempExpression = null;
                        break;
                    }
                }
                //if the tempExpression is longer than 0 then add the conditional OR statement
                else{
                    if(tempExpression.Length > 0){
                        tempExpression += "|" + expVar;
                    }
                    else{
                        tempExpression += expVar;
                    }
                }
            }
            //if expression was not satisfied adds the modified expression to the phi statement
            if(tempExpression != null){
                newPhiExpression += "(" + tempExpression + ")";
            }
        }
        newPhiExpression += ")";

        //Returns null if its an invalid expression
        // if(newPhiExpression == "()"){
        //     return null;
        // }
        return(new SAT3PQObject(new SAT3(newPhiExpression), depth + 1, totalNumberOfVariables));
    }

    public void updateHM(List<string> exp, string var){
        int itemWeight;
        string tVar;
        foreach(string expVar in exp){
            tVar = expVar[expVar.Length -1].ToString();

            if(!tVar.Equals(var)){
                itemWeight = this.varWeights.GetValueOrDefault(expVar);// TryGetValue(expVar);
                this.varWeights.Remove(expVar);
                this.varWeights.Add(expVar, itemWeight-1);
            }
        }
    }

    //code that generates the variable priority queue
    //modifies the priority value so it sorts high to low
    private PriorityQueue<string, int> makeVarPQ(List<string> literals){
        Dictionary<string, int> numbVars = new Dictionary<string, int>();
        int highestVal = 0;
        foreach(string literal in literals){
            if(!numbVars.ContainsKey(literal[literal.Length - 1].ToString())){
                numbVars.Add(literal, 1);
                // count++;
            }
            else{//increments value
                numbVars.Add(literal, numbVars.GetValueOrDefault(literal)+1);
                //updates highest value
                if(highestVal < numbVars.GetValueOrDefault(literal)){
                    highestVal = numbVars.GetValueOrDefault(literal);
                }
            }
        }

        PriorityQueue<string, int> newVarPQ = new PriorityQueue<string, int>();
        foreach(KeyValuePair<string, int> kvp in numbVars){
            newVarPQ.Enqueue(kvp.Key, highestVal - kvp.Value);
        }

        return newVarPQ;
    }
}