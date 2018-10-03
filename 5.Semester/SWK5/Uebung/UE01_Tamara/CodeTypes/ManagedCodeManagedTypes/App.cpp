#include <cstdio>
#include <iostream>


using namespace System;

int main() {

   //Calc* calc = new Calc(); //mit new erzeugt man unmanaged types 
   Calc^ calc = gcnew Calc(); //mit gcnew erzeugt man managed types und statt dem ptr eine referenz
   for(int i = 0; i < 1000; i++){
       calc->Add((double)rand()/RAND_MAX);
   }
   std::cout << "sum=" << calc->GetSum() << std::endl;

   //delete calc -> würde hier jetzt dispose aufrufen ( hat eine andere bedeutung da es kein normaler ptr sondern eine referenz ist)
   //wenn dispose aufgerufen wurde, wird es markiert und dann wird finalizer nicht mehr aufgerufen

   Console::WriteLine("before GC");
   GC::Collect(); //GarbageCollector //Damit finalizer aufgerufen wird //Läuft in anderem thread. Deswegen ist ausgabe nicht direkt hier 
   Console::WriteLine("after GC");

}