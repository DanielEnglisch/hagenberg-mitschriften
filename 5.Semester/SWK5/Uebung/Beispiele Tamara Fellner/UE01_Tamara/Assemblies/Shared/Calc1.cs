using System;
using System.Reflection;

[assembly: AssemblyVersion("1.0.0.0")] //Versionsnummer durch Attribut angeben (braucht System.Reflection)
 
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