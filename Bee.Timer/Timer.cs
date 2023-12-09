using System;
using System.Threading;

namespace Bee.Timer
{
    public class Timer
    {
        public static void run(int seconds, Action callback = null)
        {
            if (!(seconds > 0))
                throw new ArgumentException();

            var dt = DateTime.Now.AddSeconds(seconds);

            new Thread(() => {
                while (DateTime.Now.Second < dt.Second)
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
