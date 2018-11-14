//#include <cstdio>
#include "Calc.h"

using namespace System;

Calc::Calc() {
	sum = 0;
	n   = 0;
}

Calc::~Calc() {
  //printf("~Calc\n");
  Console::WriteLine("~Calc");
}

Calc::!Calc() {
  Console::WriteLine("!Calc");
}

void Calc::Add(double number) {
	sum += number;
	n++;
}

double Calc::GetSum() {
	return sum;
}
