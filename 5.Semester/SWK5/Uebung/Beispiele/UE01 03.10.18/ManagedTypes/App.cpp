#include <cstdio>
//#include "Calc.h"
#using "Calc.dll"


int main() {
    
    //Calc* c = new Calc();
    //neue Art von Pointer
    Calc^ c = gcnew Calc(); //nicht user managed sondern vom Garbagecollector 

    c->Add(5);
    c->Add(3);
    c->Add(2);

    // delete c; -> calls the destructor, prevents finalizer

    printf("sum= %f\n", c->GetSum());
    printf("before GC\n");
    System::GC::Collect(); //GC can free Calc object because Calc is a managed tpye.
    printf("after GC\n"); 

    //delete c; -> not neccessary anymore
}