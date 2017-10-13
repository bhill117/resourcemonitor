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



        }
    }
}
