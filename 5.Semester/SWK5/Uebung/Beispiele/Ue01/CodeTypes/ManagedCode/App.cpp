#include <cstdio>
//#include "Calc.h"
#using "Calc.dll"

int main() {

    //Calc* c = new Calc();
    Calc^ c = gcnew Calc();

    c->Add(5);
    c->Add(3);
    c->Add(2);
    printf("sum=%f\n", c->GetSum());

    //delete c;
}