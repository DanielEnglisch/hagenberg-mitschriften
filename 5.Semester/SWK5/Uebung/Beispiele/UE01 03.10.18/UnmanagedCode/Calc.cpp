#include <cstdio>
#include "Calc.h"

Calc::Calc() {
	sum = 0;
	n   = 0;
}

Calc::~Calc() {
  printf("~Calc\n");
}

void Calc::Add(double number) {
	sum += number;
	n++;
}

double Calc::GetSum() {
	return sum;
}
