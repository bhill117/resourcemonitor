using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Speech.Synthesis;

namespace ResourceMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Greeting
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.Speak("Welcome to Resource Monitor");

            #region Performance Counters

            //CPU Usage
            PerformanceCounter perfCpuCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            perfCpuCount.NextValue();

            //Memory Usage
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MB");
            perfMemCount.NextValue();

            //Running Time
            PerformanceCounter perfUpTimeCount = new PerformanceCounter("System", "System Up Time");
            perfUpTimeCount.NextValue();
            #endregion

            TimeSpan upTimeSpan = TimeSpan.FromSeconds(perfUpTimeCount.NextValue());
            string systemUpTimeMessage = string.Format("The current up time is {0} days {1} hours {2} minutes",
                (int)upTimeSpan.TotalDays,
                (int)upTimeSpan.Hours,
                (int)upTimeSpan.Minutes
                );
            synth.Speak(systemUpTimeMessage);

            #region terminal display
            while (true)
            {
                int currentCpuPercentage = (int)perfCpuCount.NextValue();
                int currentAvailMem = (int)perfMemCount.NextValue();

                // Displays CPU and RAM ussage every 5000ms (5sec)
                Console.WriteLine("CPU Usage:   {0}%", currentCpuPercentage);
                Console.WriteLine("RAM Avail:   {0}MB", currentAvailMem);


            }

            #endregion

        }
    }
}
