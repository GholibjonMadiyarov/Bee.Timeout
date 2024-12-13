using System;
using System.Threading;

namespace Bee.Timeout
{
    public class Timeout
    {
        private  bool b;
        private  DateTime dt;
        private  int s;
        private SynchronizationContext synchronizationContext;

        public Timeout(SynchronizationContext synchronizationContext = null) 
        { 
            b = true;

            //30 seconds
            s = 30;
            dt = DateTime.Now.AddSeconds(s);

            this.synchronizationContext = synchronizationContext;
        }

        public static void start(int seconds = 30, Action callback = null, SynchronizationContext synchronizationContext = null)
        {
            if (seconds <= 0)
                seconds = 30;

            var dt = DateTime.Now.AddSeconds(seconds);


            var t = new Thread(() =>
            {
                while (DateTime.Now < dt)   
                    Thread.Sleep(1000);

                if (callback != null)
                {
                    if (synchronizationContext != null)
                    {
                        synchronizationContext.Post(_ => callback(), null);
                    }
                    else
                    {
                        callback();
                    }
                }
            });

            t.IsBackground = true;
            t.Start();
        }

        public void begin(int seconds, Action callback = null)
        {
            if (seconds <= 0)
                seconds = 30;

            s = seconds;

            dt = DateTime.Now.AddSeconds(s);


            var t = new Thread(() =>
            {
                while (DateTime.Now < dt)  
                    Thread.Sleep(1000); 

                if (b)
                {
                    if (callback != null)
                    {
                        if (synchronizationContext != null)
                        {
                            synchronizationContext.Post(_ => callback(), null);
                        }
                        else
                        {
                            callback();
                        }
                    }
                }
            });

            t.IsBackground = true;
            t.Start();
        }

        public void end()
        { 
            dt = DateTime.Now;
            b = false;
        }

        public void refresh()
        {
            dt = DateTime.Now.AddSeconds(s);
        }
    }
}
