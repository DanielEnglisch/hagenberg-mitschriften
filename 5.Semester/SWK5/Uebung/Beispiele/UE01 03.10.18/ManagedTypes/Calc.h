//class __declspec(dllexport) Calc {
public ref class Calc {
protected:
  double sum;
  int    n;

public:
  Calc();
  !Calc(); //.NET finalizer
  ~Calc(); //C++ destructor called by delete

  void Add(double number);

  double GetSum();
};
