using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgrammingMonitors
{
    public enum State { sleeping = 0, drinking = -1,  telling = 1}

    public class Monitor
    {
        public State[] knightState;
        public Semaphore[] knightSemaphores;
        public int numberOfKnights;

        public Monitor()
        {
            numberOfKnights = Program.numberOfKnights;
            knightState = new State[numberOfKnights];
            knightSemaphores = new Semaphore[numberOfKnights];
            for ( int i = 0; i < numberOfKnights; i++ )
            {
                knightSemaphores[i] = new Semaphore(0, 1);
                knightState[i] = State.sleeping;
            }
        }

        public void drink(int i)
        {
            testForDrinking(i);

            Program.knightStatesSemaphore.Release();

            if (Program.knights[i].currentState != State.drinking)
            {
                knightSemaphores[i].WaitOne();
            }
        }

        public void testForDrinking(int i)
        {
            if (knightState[(i + 1) % numberOfKnights] != State.drinking &&
                knightState[(numberOfKnights + i - 1) % numberOfKnights] != State.drinking &&
                Program.cucumberPlates[(int)i / 2] > 0)
            {
                Program.bottleEmptySemaphore.WaitOne();
                Program.knights[i].currentState = State.drinking;
                Program.knights[i].cupsOfWine++;
                Program.cucumberPlates[(int)i/2]--;
                Program.cupsOfWineInBottle--;
                Console.WriteLine("Knight {0} drinks", i);
                knightSemaphores[i].Release();
            }
            else
                Console.WriteLine("Knight {0} CAN'T drink", i);
        }

        public void sleep(int i)
        {
            Program.knights[i].currentState = State.sleeping;

            Random rn = new Random();
            int rnd = rn.Next(1, 5);

            Console.WriteLine("Knight {0} sleeps {1} s", i, rnd);
            
            Thread.Sleep(rn.Next(1, 5) * 1000);

            testForDrinking((i + 1) % numberOfKnights);
            testForDrinking((i - 1) % numberOfKnights);
        }
    }
}
