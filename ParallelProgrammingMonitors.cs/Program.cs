using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgrammingMonitors
{
    public class Program
    {
        public static int numberOfKnights = 6;
        public static int cupsOfWineInBottle = 10;
        public static int[] cucumberPlates = new int[numberOfKnights / 2];
        public static Knight[] knights = new Knight[numberOfKnights];
        public static Thread[] knightThreads = new Thread[numberOfKnights];
        public static Monitor monitor = new Monitor();
        public static Semaphore knightStatesSemaphore = new Semaphore(1, 1);
        public static Semaphore bottleEmptySemaphore = new Semaphore(10, 10);

        static void Main(string[] args)
        {
            for ( int i = 0; i < numberOfKnights/2; i++ )
            {
                cucumberPlates[i] = 10;
            }

            for ( int i = 0; i < numberOfKnights; i++)
            {
                knights[i] = new Knight(i);
                knightThreads[i] = new Thread(knights[i].knightActions);
                knightThreads[i].Start();
            }

            Console.ReadKey();
        }
    }
}
