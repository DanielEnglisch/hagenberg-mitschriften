// bezieht sich nicht auf die Klasse sonder auf das file
using System.Reflection; // wird für das Attribut gebraucht
[assembly:AssemblyVersion("1.0.0.0")]
public class Calc
{
    protected double sum = 0;
    protected int n = 0;

    public void Add(double number)
    {
        sum += number;
        n++;
    }

    public double GetSum()
    {
        return sum;
    }
}