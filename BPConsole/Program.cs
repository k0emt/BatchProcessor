using System;

using BPProcessor;

namespace BPConsole
{
    class Program
    {
        static private readonly int SERIAL_PROCESSING = 1;
        static private readonly int PARALLEL_PROCESSING = 4;

        static void Main(string[] args)
        {
            Console.WriteLine("BP Console\n");

            // run through set up tasks
            SetupTasks();
                        
            // work through jobs that can be run in parallel
            ParallelTasks();

            // run through tear down tasks
            TearDownTasks();    

            Console.WriteLine("BP Console run complete.\n");
        }

        private static void SetupTasks()
        {
            Console.WriteLine("BP Console: running set up tasks.\n");
            Processor proc = new Processor(SERIAL_PROCESSING,
                                            ConfigReader.GetTaskList("SetupTasks.config")
                                            );
            proc.Execute();

            Console.WriteLine("BP Console: setup complete.\n");
        }

        private static void ParallelTasks()
        {
            Console.WriteLine("BP Console: running tasks.\n");
            Processor proc = new Processor(PARALLEL_PROCESSING, 
                                           ConfigReader.GetTaskList("ParallelTasks.config")
                                            );

            proc.Execute();
        }

        private static void TearDownTasks()
        {
            Console.WriteLine("BP Console: running tear down tasks.\n");
            Processor proc = new Processor(SERIAL_PROCESSING,
                ConfigReader.GetTaskList("TearDownTasks.config")
                );
            
            proc.Execute();
        }
    }
}
