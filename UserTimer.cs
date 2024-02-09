using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR_Lab_6_Task2
{
    class TimerEventArgs: EventArgs
    {
        // Класс аргументов для события InTimerEvent
        public TimeSpan Time { get; set; }
    }

    // Делегат для события  AfterTimerEvent
    delegate void AfterTimerDel(TimeSpan time);
    internal class UserTimer
    {
        // Класс пользовательского таймера, инициирующий 3 события. Одно событие на каждой секунде таймера. Два события по истению времени таймера.
        public TimeSpan TimerStart { get; private set; }
        public TimeSpan TimerCounter { get; private set; }
        private TimeSpan sec_1 = TimeSpan.FromSeconds(1);

        // Срабатывает на каждой итерации цикла внутри таймера. С параметером типа TimerEventArgs
        private event EventHandler<TimerEventArgs>? InTimerEvent;
        // Срабатывает после заврешения таймера. Без параметров
        private event EventHandler? AfterTimerEventNoArgs;
        // Срабатывает после заврешения таймера. С параметером типа TimeSpan
        private event AfterTimerDel? AfterTimerEvent; 

        public UserTimer(int hours, int minutes, int seconds)
        {
            if ( (hours < 0 || hours > 23) || (minutes < 0 || minutes > 59) || (seconds < 0 || seconds > 59) )
            { throw new ArgumentOutOfRangeException("Time incorrect"); }
            else
            {
                TimerStart = new TimeSpan(hours, minutes, seconds);
                TimerCounter = new TimeSpan(hours, minutes, seconds);
            }
        }

        public override string ToString()
        {
            return TimerCounter.ToString();
        }

        private void Reduce_1_Second()
        {
            // Уменьшает таймер на 1 секунду.
            if(TimerCounter > TimeSpan.Zero) { TimerCounter -= sec_1; }         
        }

        private bool ChekEnd()
        {
            // Проверяет закончился ли таймер.
            if (TimerCounter > TimeSpan.Zero) { return true; }
            else { return false; }
        }

        public void Print(object sender, TimerEventArgs args)
        {
            //Печатает в консоль полученное время.
            Console.WriteLine($"Осталось времени: {args.Time}\n");
        }

        public void StartTimer()
        {
            // Запускает таймер.
            do
            {
                TimerEventArgs args = new TimerEventArgs();
                args.Time = TimerCounter;

                InTimerEvent?.Invoke(this, args);
                Reduce_1_Second();
                System.Threading.Thread.Sleep(1000);
            }
            while(ChekEnd());

            AfterTimerEvent?.Invoke(this.TimerCounter);
            AfterTimerEventNoArgs?.Invoke(this, EventArgs.Empty);
        }

        public void AddInTimerHandler(EventHandler<TimerEventArgs> handler)
        {
            // Добавляет подписчика на событие возникающее на каждой секунде таймер.
            InTimerEvent += handler;
        }
        public void AddAfterTimerHandler(AfterTimerDel handler)
        {
            // Добавляет подписчика на событие возникающее после окончания времени таймера.
            AfterTimerEvent += handler;
        }

        public void AddAfterTimerHandlerNoArgs(EventHandler handler)
        {
            // Добавляет подписчика на событие возникающее после окончания времени таймера.
            AfterTimerEventNoArgs += handler;
        }

        public void CleanInTimerHandler()
        {
            AfterTimerEvent = null;
        }
        public void CleanAfterTimerHandler()
        {
            AfterTimerEvent = null;
        }
        public void CleanAfterTimerHandlerNoArgs()
        {
            AfterTimerEventNoArgs = null;
        }
    }
}
