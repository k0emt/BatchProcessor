using System;
using System.Collections.Generic;
using System.Linq;

namespace BPProcessor
{
    public class Processor
    {
        List<string> _jobList;
        int _numberOfProcesses = 2;

        /// <summary>
        /// Initializes a new instance of the Processor class.
        /// </summary>
        public Processor()
            : this(2, new List<string>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the Processor class.
        /// </summary>
        /// <param name="numberOfProcesses"></param>
        /// <param name="jobList"></param>
        public Processor(int numberOfProcesses, List<string> jobList)
        {
            _jobList = jobList;
            _numberOfProcesses = numberOfProcesses;
        }

        public void AddJob(string job)
        {
            _jobList.Add(job);
        }

        private bool _ExecutionCompleted = false;
        public bool executionCompleted
        {
            get { return _ExecutionCompleted; }
        }
        
        public void Execute()
        {
            _ExecutionCompleted = false;
            // loop through and process jobs on the given number of processors
            System.Diagnostics.Process[] proc = new System.Diagnostics.Process[_numberOfProcesses]; // Declare New Process
            System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo(); // Declare New Process Starting Information

            int id = 0;
            bool assigned = false;

            for (int i = 0; i < _numberOfProcesses; i++ )
            {
                proc[i] = new System.Diagnostics.Process();                
            }

            foreach (string job in _jobList)
            {
                Console.WriteLine("  processing: {0}", job);

                procInfo.UseShellExecute = true;  //If this is false, only .exe's can be run.
                // procInfo.WorkingDirectory = "";
                procInfo.FileName = job.Split(' ').First(); // Program or Command to Execute.
                procInfo.Arguments = "";

                // deal with parameters
                string[] parameters = job.Split(' ');
                int len = parameters.Length;
                if (len > 1)
                {
                    for (int i = 1; i < len; i++)
                    {
                        procInfo.Arguments += parameters[i]; //Command line arguments.
                    }
                    // Console.WriteLine("Process: {0} Arguments: {1}", procInfo.FileName, procInfo.Arguments);
                }
                procInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                assigned = false;

                // check for an open process
                while (assigned == false)
                {
                    // these checks must be in this order b/c if the process has not been initialized,
                    // the .HasExited will throw an invalid operation exception
                    if (String.IsNullOrEmpty(proc[id].StartInfo.FileName) || proc[id].HasExited)  
                    {
                        try
                        {
                            proc[id] = System.Diagnostics.Process.Start(procInfo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("ERROR!  Job: {0}\n{1}", job, ex.Message);
                        }
                        assigned = true;
                    }
                    id = (++id % _numberOfProcesses);
                }
            }

            for (int i = 0; i < _numberOfProcesses; i++)
            {
                if (!String.IsNullOrEmpty(proc[id].StartInfo.FileName))
                {
                    proc[i].WaitForExit();
                }
            }

            _ExecutionCompleted = true;
            Console.WriteLine();
        }
    }
}
