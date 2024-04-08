using System;
using System.Threading;

namespace Bee.Timeout
{
    public class Timeout
    {
        DateTime dt;
        long seconds;

        public static void run(int seconds, Action callback = null)
        {
            if (!(seconds > 0))
                seconds = 30;

            var dt = DateTime.Now.AddSeconds(seconds);

            new Thread(() => {
                while (DateTime.Now < dt)
                {
                    // Sleep
                }

                if (callback != null)
                    callback();
            })
            { IsBackground = true }.Start();
        }

        public void start(int seconds, Action callback = null)
        {
            if (!(seconds > 0))
                seconds = 30;

            this.seconds = seconds;
            this.dt = DateTime.Now.AddSeconds(this.seconds);

            new Thread(() => {
                while (DateTime.Now < this.dt)
                {
                    // Sleep
                }

                if (callback != null)
                    callback();
            })
            { IsBackground = true }.Start();
        }

        public void refresh()
        {
            this.dt = DateTime.Now.AddSeconds(seconds);
        }
    }
}
