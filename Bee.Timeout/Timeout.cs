using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Bee.Timeout
{
    public class Timeout
    {
        private bool active;
        private DateTime now;

        public string token;

        public event Action<string> TimeoutComplate;
        public event Action<string> TimeoutProcess;
        public event Action<string> TimeoutStop;

        private object sync = new object();

        public Timeout() 
        { 
            this.active = false;

            //Now
            this.now = DateTime.Now;

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

            DateTime now = DateTime.Now.AddSeconds(seconds);

            var t = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);

                    if (DateTime.Now >= now)
                        break;

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
            lock (sync)
            {
                if (this.active == true)
                {
                    return;
                }

                this.now = DateTime.Now.AddSeconds(seconds);

                this.active = true;
            }

            var t = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);

                    lock (sync) 
                    {
                        if (DateTime.Now >= this.now)
                        {
                            break;
                        }
                    }

                    OnTimeoutProcess(token);
                }

                lock (sync) 
                {
                    if (this.active == false)
                    {
                        return;
                    }
                }

                active = false;

                OnTimeoutComplate(token);
            });

            t.IsBackground = true;
            t.Start();
        }

        public void stop()
        { 
            lock (sync) 
            {
                this.now = DateTime.Now;
                this.active = false;
            }
            
            OnTimeoutStop(token);
        }

        public void refresh(int seconds = 30)
        {
            lock (sync)
            {
                this.now = DateTime.Now.AddSeconds(seconds);
            }
        }

        public int getSeconds()
        {
            lock (sync)
            {
                return (int)Math.Round((this.now - DateTime.Now).TotalSeconds);
            }
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
