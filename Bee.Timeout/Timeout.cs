using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Bee.Timeout
{
    public class Timeout
    {
        private bool active;
        private int seconds;
        public string token;

        public event Action<string> TimeoutComplate;
        public event Action<string> TimeoutProcess;
        public event Action<string> TimeoutStop;

        public Timeout() 
        { 
            this.active = false;

            //30 seconds
            this.seconds = 30;

            this.token = null;

            Version.version(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Version.txt"), "TimeoutVersion:" + Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString());
        }

        public void version(string path = null)
        {
            if (path != null)
            {
                Version.version(path, "TimeoutVersion:" + Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString());
            }
        }

        public static void sleep(int seconds = 10, Action callback = null, Action process = null)
        {
            if (seconds == 0)
            {
                return;
            }

            var t = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);

                    if (seconds == 0)
                        break;

                    seconds--;

                    if (process != null)
                        process();
                }

                if (callback != null)
                    callback();
            });

            t.IsBackground = true;
            t.Start();
        }

        public void start(int seconds = 30)
        {
            if (active == true)
            {
                return;
            }

            this.seconds = seconds;
            this.active = true;

            if (this.seconds == 0)
            {
                return;
            }

            var t = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);

                    if (this.seconds == 0)
                        break;

                    this.seconds--;

                    OnTimeoutProcess(token);
                }

                if (this.active == true)
                {
                    active = false;
                    OnTimeoutComplate(token);
                }
            });

            t.IsBackground = true;
            t.Start();
        }

        public void stop()
        { 
            this.seconds = 0;
            this.active = false;

            OnTimeoutStop(token);
        }

        public void refresh(int seconds = 30)
        {
            this.seconds = seconds;
        }

        public int getSeconds()
        {
            return this.seconds;
        }

        protected void OnTimeoutComplate(string token)
        {
            if (TimeoutComplate != null)
                TimeoutComplate(token);
        }

        protected void OnTimeoutProcess(string token)
        {
            if (TimeoutProcess != null)
                TimeoutProcess(token);
        }

        protected void OnTimeoutStop(string token)
        {
            if (TimeoutStop != null)
                TimeoutStop(token);
        }
    }
}
