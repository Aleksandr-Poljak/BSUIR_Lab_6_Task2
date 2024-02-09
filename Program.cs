
using BSUIR_Lab_6_Task2;
// Вторая часть. Лабораторная 6. Делегаты и события.

UserTimer timer = new UserTimer(0, 0, 5);


timer.AddInTimerHandler(timer.Print);
timer.AddAfterTimerHandler(delegate (TimeSpan time) { Console.WriteLine($"Конец: {time}"); });
timer.AddAfterTimerHandler(TimerHandlers.PrintTime);
timer.AddAfterTimerHandlerNoArgs(TimerHandlers.PrintEnd);


UserTimer timer2 = new UserTimer(0, 0, 5);
timer2.AddInTimerHandler((object sender, TimerEventArgs args) => Console.WriteLine($"Осталось {args.Time}\n")) ;
timer2.AddInTimerHandler(TimerHandlers.PrintTimeJoke);
timer2.AddAfterTimerHandler((TimeSpan time) => Console.WriteLine($"Время вышло {time}!\n"));



timer.StartTimer();
timer2.StartTimer();