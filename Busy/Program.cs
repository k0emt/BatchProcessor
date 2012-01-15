using System;

namespace Busy
{
    class Program
    {
        static void Main(string[] args)
        {
            int seconds;

            if (args.Length == 0)
            {
                Console.WriteLine("usage is: Busy seconds");
                return;
            }

            try
            {
                seconds = Convert.ToInt32(args[0]);

                Console.WriteLine("Getting Busy for {0} seconds", seconds);
                Busy bee = new Busy { Duration = seconds };
                bee.Spin();
            }
            catch (System.FormatException fex)
            {
                Console.WriteLine("usage is: Busy seconds (32bit integer)");
                return;
            }
            catch(System.OverflowException ofe)
            {
                Console.WriteLine("usage is: Busy seconds (32bit integer)");
                return;
            }
            Console.WriteLine("done!");          
        }
    }

    class Busy
    {
        private int _Duration = 5;
        public int Duration
        {
            get { return _Duration; }
            set
            {
                _Duration = value;
            }
        }

        private int _elapsedTime = 0;

        public void Spin()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += timer_Elapsed;
            timer.Interval = 1000;
            timer.Start();

            double workval = 335577.99;

            while (_elapsedTime < _Duration)
            {
                // do some stuff to make the computer work
                workval /= 7.5;
                workval--;
                workval *= 5.5;
                workval++;
                workval.ToString().CompareTo("123456789.0");
            }

            timer.Stop();
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _elapsedTime++;
        }
    }
}
