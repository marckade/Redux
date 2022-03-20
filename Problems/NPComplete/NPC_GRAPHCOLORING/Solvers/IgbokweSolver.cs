using API.Interfaces;

namespace API.Problems.NPComplete.NPC_GRAPHCOLORING.Solvers;
 class IgbokweSolver : ISolver {



#region Fields
    private string _solverName = "Generic Solver";
    private string _solverDefinition = "This is a generic solver for GRAPHCOLORING";
    private string _source = "";
    private Dictionary<string, Node> _nodeList = new Dictionary<string, Node>();
    private List<string> _uncoloredNodes = new List<string>();
    private SortedSet<int> _colors = new SortedSet<int>(){0,1,2,3};

#endregion

#region Properties 
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

    public Dictionary<string, Node> nodeList{
        get {
            return _nodeList;
        }

        set {
            _nodeList = value;
        }
    }

     public List<string> nodes {
        get {
            return _uncoloredNodes;
        }
        set {
            _uncoloredNodes = value;
        }
    }

    public  SortedSet<int> colors {
        get {
            return _colors;
        }
        set {
            _colors = value;
        }
    }
#endregion

#region  Constructors 
    public IgbokweSolver() {

    }
#endregion 

#region Methods

    public Tuple<Dictionary<string, string>, int> Solve(GRAPHCOLORING problem){
        _nodeList = initialize(problem);
        _uncoloredNodes = problem.nodes;
        computeSaturation(problem, _uncoloredNodes);
        Dsatur(problem);
       

        return Tuple.Create(problem.nodeColoring, getChromaticNumber(problem.nodeColoring));
    }

    private int getChromaticNumber( Dictionary<string, string> nodeColoring){

        int colors = 0;
        SortedSet<int> colorList = new SortedSet<int>();
        

        foreach(var elem in nodeColoring){
            colorList.Add(Int32.Parse(elem.Value));
        }

        colors = colorList.Count();

        return colors;
    }

     private void computeSaturation(GRAPHCOLORING problem, IEnumerable<string> nodes){


        foreach(string elem in nodes){

            List<Node> adjNodes = getAdjNodes(problem, elem);
            SortedSet<int> adjColors = new SortedSet<int>();
            int uncoloredDegree = 0;

            foreach(Node node in adjNodes){

                if(node.color > -1){
                    adjColors.Add(node.color);

                }else{
                    uncoloredDegree++;

                }
            }

             // Read node value 
             Node tempNode = _nodeList[elem];
             
             // update node saturation and degree  
             tempNode.adjDegree = uncoloredDegree;
             tempNode.satDegree = adjColors.Count();

             // update dictionary 
             _nodeList[elem] = tempNode;
        }
    }


    private void Dsatur(GRAPHCOLORING problem){

        while(_uncoloredNodes.Count > 0){


            PriorityQueue<Node> pq = new PriorityQueue<Node>(
                createUncoloredNodes(_uncoloredNodes));

            // remove node with the highest priority

            Node currentNode = pq.Dequeue();

            // get used colors from adjacent nodes 
            List<Node> adjNodes =  getAdjNodes(problem, currentNode.name);
            SortedSet<int> adjColors = new SortedSet<int>();

            foreach(Node node in adjNodes){

        

                if(node.color > -1){

                    adjColors.Add(node.color);
                }
            }   

            // remove used colors from list
            SortedSet<int> checkColors = new SortedSet<int>(colors);
            foreach(int color in adjColors){
                checkColors.Remove(color);
            }
            
        
            if(checkColors.Count == 0){
                colors.Add(colors.Count);
                checkColors.Add(colors.Count);
            }


           int newColor = checkColors.ElementAt(0);
            Node tempNode = _nodeList[currentNode.name];
            tempNode.color = newColor;

            //update in dictionary 
            _nodeList[currentNode.name] =  tempNode;
            problem.nodeColoring[currentNode.name] = newColor.ToString();


           _uncoloredNodes.Remove(currentNode.name);

        computeSaturation(problem, problem.getAdjNodes(currentNode.name));
        }
    }

    private Dictionary<string, Node> initialize(GRAPHCOLORING problem){

        Dictionary<string, Node> nodeMap = new Dictionary<string, Node>();

        foreach(string node in problem.nodes){
            nodeMap.Add(node, new Node(node));
        }
        return nodeMap;

    }

    private List<Node> createUncoloredNodes(List<string> list){
         List<Node> adjNodes = new List<Node>();

        foreach(string elem in list){
            adjNodes.Add(_nodeList[elem]);
        }

        return adjNodes;

    }

    private List<Node> getAdjNodes(GRAPHCOLORING problem, string name){
        List<string> tempList  =  problem.getAdjNodes(name);
        List<Node> adjNodes = new List<Node>();

        foreach(string elem in tempList){
            adjNodes.Add(_nodeList[elem]);
           // _nodeList[elem] = 
        }

        return adjNodes;
    }
#endregion

}

class Node : IComparable {

#region Fields 

    private string _name;

    private int _adjDegree;

    private int _satDegree;

    private int _color;
#endregion

#region Properties 
    public string name
    {
        get
        {

            return _name;
        }

        set
        {
            _name = value;
        }

    }



    public int adjDegree
    {
        get
        {
            return _adjDegree;
        }
        set
        {

            _adjDegree = value;
        }
    }

    public int satDegree
    {
        get
        {
            return _satDegree;
        }
        set
        {

            _satDegree = value;
        }
    }

    public int color {
        get{
            return _color;
        }
        set {
            _color = value;
        }
    }
#endregion 

#region Constructor

    public Node(string label)
    {
        _name = label;
        _adjDegree = 0;
        _satDegree = 0;
        _color = -1;
    }


#endregion

#region Methods


    public override bool Equals(Object ? obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {

            Node node = (Node)obj;

            return this.name.Equals(node.name);
        }
    }

    public override int GetHashCode()
    {
        return 1;
    }


    public int CompareTo(Object ? elem) {

        if (elem == null) {

            throw new NullReferenceException("This cannot be null");
        }

        Node obj = (Node)elem;


        if (this.satDegree == obj.satDegree) {

            if (this.adjDegree > obj.adjDegree) {

                return 1;

            }else if (this.adjDegree < obj.adjDegree) {

                return -1;

            } else {

                return 0;
            }

        } else if (this.satDegree > obj.satDegree){

            return 1;

        } else{

            return -1; 
        }



    }
    public override string ToString()  {
        return "Node: {name: " + this.name + ", satDegree:" + 
            this.satDegree + ", adjDegree: " + this.adjDegree + ",  color: "
              + this.color + "} ";

    }





#endregion

}
class PriorityQueue<T> where T : IComparable {

    #region Fields
    private List<T> _queue;
   
    #endregion

    #region Properties

    public int Count
    {
        get
        {
            return this.queue.Count;
        }

    }


    public List<T> queue
    {
        get
        {
            return _queue;
        }

        set
        {
            _queue = value;
        }
    }

    #endregion

    #region Constructor
    public PriorityQueue() {
        _queue = new List<T>();
    }

    public PriorityQueue(IEnumerable<T> collection) : this() {

        foreach (var item in collection) {
            Enqueue(item);
        }

    }

    #endregion

    #region Methods
   protected int parent(int i) {
        return (i - 1) / 2;
    }

    protected int leftChild(int i) {
        return ((2 * i) + 1);
    }

    protected int rightChild(int i) {
        return ((2 * i) + 2);
    }

    protected void bubbleUp(int i) {

        // while parent is less than current child swap position 
        while (i > 0 &&
               queue[parent(i)].CompareTo(queue[i]) == -1) {

            // Swap parent and current node
            swap(parent(i), i);

            // Update i to parent of i
            i = parent(i);
        }
    }


    protected void bubbleDown(int i) {
        int maxIndex = i;

        // Left Child
        int l = leftChild(i);

        if (l < Count &&
         queue[l].CompareTo(queue[maxIndex]) == 1) {
            maxIndex = l;
        }

        // Right Child
        int r = rightChild(i);

        if (r < Count &&
         queue[r].CompareTo(queue[maxIndex]) == 1) {
            maxIndex = r;
        }

        // If i not same as maxIndex
        if (i != maxIndex) {
            swap(i, maxIndex);
            bubbleDown(maxIndex);
        }
    }


    public void Enqueue(T x) {

        queue.Add(x);
        int i = Count - 1;

        bubbleUp(i);
    }

    public T Dequeue(){

        if (Count == 0) { throw new InvalidOperationException("Queue is empty."); }

        T val = queue[0];


        queue[0] = queue[Count - 1];
        queue.RemoveAt(Count - 1);

        bubbleDown(0);
        return val;
    }

    public T Peek(){
        if (Count == 0) { throw new InvalidOperationException("Queue is empty."); }
        return queue[0];
    }

    protected void swap(int i, int j){
        T temp = queue[i];
        queue[i] = queue[j];
        queue[j] = temp;
    }


#endregion

 
 }

