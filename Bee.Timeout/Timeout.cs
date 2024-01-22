using System;
using System.Threading;

namespace Bee.Timeout
{
    public class Timeout
    {
        DateTime dt;
        long milliseconds;

        public static void run(int milliseconds, Action callback = null)
        {
            if (!(milliseconds > 0))
                throw new ArgumentException();

            var dt = DateTime.Now.AddMilliseconds(milliseconds);

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

        public void start(int milliseconds, Action callback = null)
        {
            if (!(milliseconds > 0))
                throw new ArgumentException();

            this.milliseconds = milliseconds;
            dt = DateTime.Now.AddMilliseconds(this.milliseconds);

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

        public void refresh()
        {
            dt = DateTime.Now.AddMilliseconds(milliseconds);
        }
    }
}
