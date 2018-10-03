#include <cstdio>
#include <iostream>
#include "Calc.h"


int main() {

   Calc* calc = new Calc();
   for(int i = 0; i < 1000; i++){
       calc->Add((double)rand()/RAND_MAX);
   }
   std::cout << "sum=" << calc->GetSum() << std::endl;

}