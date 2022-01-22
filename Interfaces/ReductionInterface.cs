namespace API.Interfaces;

interface IReduction<T,U> {
    string reductionDefinition{get;}
    string source {get;}
    T reductionFrom {get;}
    U reductionTo {get;}
    U reduce(T from, U to);
}
