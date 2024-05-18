namespace ConsoleApp6;
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine($"Main начал работать в {Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine("Нажмите любую кнопку для старта");
        Console.ReadKey();

        var threadPoolWorker = new ThreadPoolWorker<int>(SumNumber);
        threadPoolWorker.Start(1000);

        /*        for (int i = 0; i < 20; i++)
                {
                    Console.Write('-');
                    Thread.Sleep(50);
                }*/

        Report();
        var res = threadPoolWorker.Result;

        Console.WriteLine(res);
    } 

    private static int SumNumber(object number)
    {
        int num = (int)number;
        int sum = 0;
        for (int i = 0; i < num; i++)
        {
            sum += i;
            Thread.Sleep(10);
        }
        return sum;
    }

    private static void Example1(object arg)
    {
        Console.WriteLine($"Example1 начал работать в {Thread.CurrentThread.ManagedThreadId}");
        throw new Exception();
        Thread.Sleep(5000);
        Console.WriteLine($"Example1 закончил работать в {Thread.CurrentThread.ManagedThreadId}");
    }

    private static void Example2(object arg)
    {
        Console.WriteLine($"Example2 начал работать в {Thread.CurrentThread.ManagedThreadId}");
        Thread.Sleep(2000);
        Console.WriteLine($"Example2 закончил работать в {Thread.CurrentThread.ManagedThreadId}");
    }

    private static void Report()
    {
        ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxPortThreads);
        ThreadPool.GetAvailableThreads(out int availableWorkerThreads, out int availablePortThreads);

        Console.WriteLine($"Рабочие потоки {availableWorkerThreads} из {maxWorkerThreads}");
    }
    private static void WriteChar(object arg) 
    { 
        Console.WriteLine($"WriteChar начал работать в {Thread.CurrentThread.ManagedThreadId}"); 
        char item = (char)arg; 
        for (int i = 0; i < 120; i++) 
        { 
            Console.Write(item); 
            Thread.Sleep(50); 
        } 
    }
}
