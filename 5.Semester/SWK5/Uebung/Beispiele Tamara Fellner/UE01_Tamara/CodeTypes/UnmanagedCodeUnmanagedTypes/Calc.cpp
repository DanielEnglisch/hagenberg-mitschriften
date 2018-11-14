#include <iostream>
#include "Calc.h"

Calc::Calc() {
	sum = 0;
	n   = 0;
}

Calc::~Calc() {
  std::cout << "~Calc" << std::endl;
}

void Calc::Add(double number) {
	sum += number;
	n++;
}

double Calc::GetSum() {
	return sum;
}
