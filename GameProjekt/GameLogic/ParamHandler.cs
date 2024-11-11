class Param{
    private string paramName;
    private double curValue;
    private string paramType;

    public Param(string name, double value, string type){
        paramName = name;
        curValue = value;
        paramType = type;
    }

    public string getName(){
        return paramName;
    }

    public string getStatus(){
        return paramName + ": "+ curValue +""+ paramType;
    }

    public double getValue(){
        return curValue;
    }

}

/*
class Balance : Param {
    public Balance(string name, double value): base(name, value){

    }
}

class Energy : Param {
    public Energy(string name, double value): base(name, value){

    }
}

class CO2 : Param {
    public CO2(string name, double value): base(name, value){

    }

    public override string ToString()
    {
        return this.ToString();
    }
}*/
