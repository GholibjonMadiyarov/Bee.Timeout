using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Bee.Timeout
{
    public class Timeout
    {
        private bool active;
        private int seconds;
        private Action callback;

        public Timeout() 
        { 
            this.active = false;

            //30 seconds
            this.seconds = 30;

            Log.info(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Version.txt"), "TimeoutVersion:" + Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString());
        }

        public void version(string path = null)
        {
            if (path != null)
            {
                Log.info(path, "TimeoutVersion:" + Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString());
            }
        }

        public static void sleep(int seconds = 30, Action callback = null)
        {
            if (seconds > 0)
            {
                var t = new Thread(() =>
                {
                    while (true) 
                    {
                        Thread.Sleep(1000);

                        if (seconds == 0)
                            break;

                        seconds--;
                    }

                    if (callback != null)
                        callback();
                });

                t.IsBackground = true;
                t.Start();
            }
        }

        public void start(int seconds = 30, Action callback = null)
        {
            this.seconds = seconds;
            this.active = true;

            if (this.seconds > 0)
            {
                var t = new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(1000);

                        if (this.seconds == 0)
                            break;

                        this.seconds--;
                    }

                    if (this.active && callback != null)
                        callback();
                });

                t.IsBackground = true;
                t.Start();
            }
        }

        public void stop()
        { 
            this.seconds = 0;
            this.active = false;
        }

        public void refresh(int seconds = 30)
        {
            this.seconds = seconds;
        }
    }
}
