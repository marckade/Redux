
using API.Interfaces;

namespace API.Problems.NPComplete.NPC_SAT3;

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

}