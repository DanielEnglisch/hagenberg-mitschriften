//class __declspec(dllexport) Calc {
public ref class Calc {
protected:
  double sum;
  int    n;

public:
  Calc();
  ~Calc();
  !Calc(); // net finalizer

  void Add(double number);

  double GetSum();
};
