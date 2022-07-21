namespace API.Interfaces;

interface IReduction<T,U> {
    string reductionDefinition{get;}
    string source {get;}
    string[] contributers { get; }
    T reductionFrom {get;}
    U reductionTo {get;}
    U reduce();
}
