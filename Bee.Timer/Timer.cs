using System;
using System.Threading;

namespace Bee.Timer
{
    public class Timer
    {
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
    }
}
