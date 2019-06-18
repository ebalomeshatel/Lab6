using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Lab6_Csharp
{
    class MyThread
    {
        public int kom1 = 10;
        public Thread thrd;

        public MyThread(string name)
        {
            thrd = new Thread(new ThreadStart(this.run));
            thrd.Name = name; //Устанавливаем имя потока
            thrd.Start(); //Запускаем поток на выполнение
        }
        //Входная точка потока (метод run)
        void run()
        {

            Console.WriteLine(thrd.Name + ". Количество участников " + kom1);
            do
            {

                lock (this) //По идее, код заключённый в lock, должен поочерёдно выполняться сначала 1 потоком, потом 2 потоком. Но видимо, я не совсем правильно понимаю работу этого оператора
                {
                    Random rand = new Random();
                    int r = rand.Next(1, 10);
                    int r2 = rand.Next(1, 10);
                    Thread.Sleep(1000);
                    Console.WriteLine("В команду " + thrd.Name + " прибыло " + r + ", убыло " + r2 + ", в команде осталось " + (kom1 + r - r2));
                    kom1 += r;
                    kom1 -= r2;
                }
                Thread.Sleep(100);

            }
            while (kom1 > 0);
        }
    }
    class MoreThread
    {
        public static void Main()
        {
            Console.WriteLine("Игра началась");
            //Создаем два потока
            MyThread mt1 = new MyThread("Команда №1");
            MyThread mt2 = new MyThread("Команда №2");
            do
            {
                Thread.Sleep(100);
            }
            while ((mt1.kom1 > 0 && mt2.kom1 > 0));
            mt1.thrd.Abort();
            mt2.thrd.Abort();
            Console.WriteLine("Игра завершена");
            Console.ReadKey();
        }
    }
}
