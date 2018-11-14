//class __declspec(dllexport) Calc {
public ref class Calc{
protected:
  double sum;
  int    n;

public:
  Calc();
  !Calc(); // .NET Finalizer
  ~Calc(); // cpp dectructor

  void Add(double number);

  double GetSum();
};
