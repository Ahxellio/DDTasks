using DDTasks_Infrastructure;
using System.Diagnostics;
using System.Reflection;

namespace DDTasks
{
    public class Program
    {
        static void Main(string[] args)
        {
            NonParallelTask();
            ParallelTask();
        }
        //Task 1
        public static void NonParallelTask()
        {
            var inf = new Infrastructure();
            using (StreamReader reader = new StreamReader("text.txt"))
            {
                string text = reader.ReadToEnd();
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                MethodInfo meth = inf.GetType().
                    GetMethod("FileAnalyzer", BindingFlags.NonPublic | BindingFlags.Instance);
                var result = meth.Invoke(inf, new object[] { text });
                stopwatch.Stop();
                TimeSpan time = stopwatch.Elapsed;
                string elapsedTimeNonParallel = String.Format("{0:0}", time.Seconds);
                Console.WriteLine($"Private method: {elapsedTimeNonParallel} sec");


                using (StreamWriter writer = new StreamWriter("NonParallelOutput.txt"))
                {
                    foreach (var word in (Dictionary<string, int>)result)
                    {
                        writer.WriteLine($"{word.Key}\t{word.Value}");

                    }
                }

            }
        }
        //Task 2
        public static void ParallelTask()
        {
            var inf = new Infrastructure();
            using (StreamReader reader = new StreamReader("text.txt"))
            {
                string text = reader.ReadToEnd();
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                MethodInfo meth = inf.GetType().
                    GetMethod("ParallelFileAnalyzer", BindingFlags.Public | BindingFlags.Instance);
                var result = meth.Invoke(inf, new object[] { text });
                stopwatch.Stop();
                TimeSpan time = stopwatch.Elapsed;
                string elapsedTimeNonParallel = String.Format("{0:00}", time.Milliseconds);
                Console.WriteLine($"Public method: {elapsedTimeNonParallel}ms");


                using (StreamWriter writer = new StreamWriter("ParallelOutput.txt"))
                {
                    foreach (var word in (Dictionary<string, int>)result)
                    {
                        writer.WriteLine($"{word.Key}\t{word.Value}");

                    }
                }

            }
        }
    }
}