using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgrammingMonitors
{
    //{ sleeping = 0, drinking = -1,  telling = 1}

    public class Knight
    {
        public bool isKing;
        public int index;
        public State currentState;
        public int cupsOfWine;

        public Knight(int i)
        {
            isKing = false;
            index = i;
            currentState = 0;
            cupsOfWine = 0;
        }

        public void knightActions()
        {
            while (Program.cupsOfWineInBottle > 0)
            {
                Program.knightStatesSemaphore.WaitOne();
                Program.monitor.drink(index);
                //Program.knightStatesSemaphore.Release();
                Console.WriteLine("Knight {0} eats cucumber and take one cups of wine", index);
                //Thread.Sleep(2000);
                Program.knightStatesSemaphore.WaitOne();
                Program.monitor.sleep(index);
                Program.knightStatesSemaphore.Release();
            }
        }
    }
}
