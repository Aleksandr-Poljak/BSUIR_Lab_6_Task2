using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR_Lab_6_Task2
{
    internal static class TimerHandlers
    {
        // Предоставляет методы-подписчики для UserTimer,
        public static void PrintTime(TimeSpan time)
        {
            Console.WriteLine($"Вот и пришло то самое время {time}\n");
        }

        public static void PrintTimeJoke(object sender, TimerEventArgs args)
        {
            Console.WriteLine($"Время бежит неумолимо, осталось {args.Time}\n");
        }

        public static void PrintEnd(object sender, EventArgs args)
        {
            Console.WriteLine("Вот и пришел конец.\n");
        }
    }
}
