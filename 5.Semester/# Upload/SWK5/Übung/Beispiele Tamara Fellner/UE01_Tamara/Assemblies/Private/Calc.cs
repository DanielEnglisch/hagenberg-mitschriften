public class Calc{
    protected double sum;
    protected int n;

    public void Add(double value){
        sum += value;
        n++;
    }

    public double GetSum(){
        return sum;
    }
}