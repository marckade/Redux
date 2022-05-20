using API.Interfaces;
using API.Problems.NPComplete.NPC_SAT;

namespace API.Problems.NPComplete.NPC_GRAPHCOLORING.ReduceTo.NPC_SAT;

    class ReductionTest : IReduction<GRAPHCOLORING, SAT>{

       #region Fields

        private string _reductionDefinition = "Karp's reduction converts each clause from a 3CNF into an OR gadgets to establish the truth assignments using labels.";
        private string _source = "http://cs.bme.hu/thalg/3sat-to-3col.pdf.";
        private GRAPHCOLORING _reductionFrom;
        private SAT _reductionTo;
        private string _complexity = "O(n^2)";

        #endregion

       #region Properties

        public string reductionDefinition{
            get {
                return _reductionDefinition;
            }
        }
        public string source {
            get{
                return _source;
            }

        }

        public string complexity {
            get {
                return _complexity;
            }

            set{
                _complexity = value;
            }
        }

        public GRAPHCOLORING reductionFrom {
            get {
                return _reductionFrom;
             }
            set {
                _reductionFrom = value;
            }
        }


        public SAT reductionTo{
            get{
                return _reductionTo;
            }
            set{
                _reductionTo = value;
            }
        }

         #endregion


        #region Constructors
     public ReductionTest(GRAPHCOLORING from){
        _reductionFrom = from;
        _reductionTo = reduce();
    }


    #endregion
        

        #region Methods

    public SAT reduce(){
        return new SAT();
    }

    #endregion
    
    }

