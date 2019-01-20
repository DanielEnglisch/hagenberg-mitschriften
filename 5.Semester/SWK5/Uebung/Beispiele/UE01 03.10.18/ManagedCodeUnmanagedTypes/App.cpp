#include <cstdio>
#include "Calc.h"

int main() {

  Calc* c = new Calc();
  c->Add(5);
  c->Add(3);
  c->Add(2);

  printf("sum=%f\n", c->GetSum());

  printf("before GC\n");
  System::GC::Collect(); // GC can free calc object bc calc is managed type
  printf("After GC\n");
  
  
  //delete c; -> not necessary anymore
}