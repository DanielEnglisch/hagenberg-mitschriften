using System.Reflection;
[assembly:AssemblyVersion("1.0.0.0")]
public class Calc
{
    protected double sum = 0;
    protected int n = 0;

    public void Add(double number)
    {
        this.sum += number;
        this.n++;
    }

    public double GetSum(){
        return this.sum;
    }
}