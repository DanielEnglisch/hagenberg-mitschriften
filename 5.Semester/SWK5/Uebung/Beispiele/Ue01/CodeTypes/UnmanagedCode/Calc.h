class __declspec(dllexport) Calc {
protected:
  double sum;
  int    n;

public:
  Calc();
  ~Calc();

  void Add(double number);

  double GetSum();
};
