
using API.Interfaces;

namespace API.Problems.NPComplete.NPC_SAT3;


[Serializable]  
public class SAT3Gadget : IGadget
{

    private string _reductionType;

    private string _problemType;

    private string _gadgetString;



    public SAT3Gadget(string reductionType, string gadgetString){
        _reductionType = reductionType;
        _gadgetString = gadgetString;
        _problemType = "SAT3";

    }




    public string reductionType
    {
        get
        {
            return _reductionType;
        }
        set
        {
            _reductionType = value;
        }
    }

    public string problemType
    {
        get
        {
            return _problemType;
        }
        set
        {
            _problemType = value;
        }
    }

    public string gadgetString
    {
        get
        {
            return _gadgetString;
        }
        set
        {
            _gadgetString = value;
        }
    }

    override public string? ToString(){

        return _gadgetString;
    }


    
    override public int GetHashCode()
    {
        
        return this._reductionType.GetHashCode() + this._problemType.GetHashCode() + this._gadgetString.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
           //Check for null and compare run-time types.
      if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
      {
         return false;
      }
        else
        {
            SAT3Gadget castGadget = (SAT3Gadget)obj;
            bool reductionTypeSame = false;
            bool problemTypeSame = false;
            bool gadgetStringSame = false;
            if (this._reductionType.Equals(castGadget.reductionType)){
                reductionTypeSame = true;
            }
             if (this._problemType.Equals(castGadget.problemType)){
                reductionTypeSame = true;

            } if (this._reductionType.Equals(castGadget.gadgetString)){
                reductionTypeSame = true;
            }

            if(reductionTypeSame==true && problemTypeSame==true && gadgetStringSame==true){
                    return true;
                }
                
            else{
                    return false;
                }
        }
    }
}
