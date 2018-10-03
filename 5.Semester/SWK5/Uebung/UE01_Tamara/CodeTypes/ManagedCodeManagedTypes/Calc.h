public ref class Calc {
protected:
  double sum;
  int    n;

public:
  Calc();
  ~Calc();
  !Calc();    //Finalizer

  void Add(double number);

  double GetSum();
};
