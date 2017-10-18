using System;
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
            synth.SelectVoice("Microsoft Eva Mobile");
            synth.Speak("Hey there Chief. Welcome to Resource Monitor");

            #region Performance Counters

            //CPU Usage
            PerformanceCounter perfCpuCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            perfCpuCount.NextValue();

            //Memory Usage
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");
            perfMemCount.NextValue();

            //Running Time
            PerformanceCounter perfUpTimeCount = new PerformanceCounter("System", "System Up Time");
            perfUpTimeCount.NextValue();

            #endregion

            TimeSpan upTimeSpan = TimeSpan.FromSeconds(perfUpTimeCount.NextValue());
            string systemUpTimeMessage = string.Format("The current up time is {0} days {1} hours {2} minutes {3} seconds",
                (int)upTimeSpan.TotalDays,
                (int)upTimeSpan.Hours,
                (int)upTimeSpan.Minutes,
                (int)upTimeSpan.Seconds
                );
            synth.Speak(systemUpTimeMessage);

            #region terminal display
            while (true)
            {
                int currentCpuPercentage = (int)perfCpuCount.NextValue();
                int currentAvailMem = (int)perfMemCount.NextValue();

                // Displays CPU and RAM ussage every 3000ms (3sec)
                Console.WriteLine("CPU Usage:   {0}%", currentCpuPercentage);
                Console.WriteLine("RAM Avail:   {0}MB", currentAvailMem);

                //for fun: if CPU rises above 90%
                if (currentCpuPercentage > 90)
                {
                    if (currentCpuPercentage == 100)
                    {
                        string cpuLoadVocalMessage = string.Format("Whoa Chief. CPU is at {0} percent", currentCpuPercentage);
                        synth.Speak(cpuLoadVocalMessage);
                    }
                    else
                    {
                        string cpuLoadVocalMessage = string.Format("Chief. The current CPU load is {0} percent", currentCpuPercentage);
                        synth.Speak(cpuLoadVocalMessage);
                    }
                }
                //for fun: if avail mem is <1GB
                if (currentAvailMem < 1024)
                {
                    string memAvailableVocalMessage = string.Format("You currently have {0} megabytes of memory available Chief", currentAvailMem);
                    synth.Speak(memAvailableVocalMessage);
                }

                Thread.Sleep(3000);
            }

            #endregion

        }
    }
}
