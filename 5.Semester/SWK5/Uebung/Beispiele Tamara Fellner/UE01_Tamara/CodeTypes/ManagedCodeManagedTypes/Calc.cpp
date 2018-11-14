#include <iostream>
#include "Calc.h"

using namespace System;

Calc::Calc() {
	sum = 0;
	n   = 0;
}

Calc::~Calc() {
  std::cout << "~Calc" << std::endl;
}

Calc::!Calc(){
  Console::WriteLine("!Calc");
}

void Calc::Add(double number) {
	sum += number;
	n++;
}

double Calc::GetSum() {
	return sum;
}
