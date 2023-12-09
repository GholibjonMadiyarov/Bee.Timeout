using System;
using System.Threading;

namespace Bee.Timer
{
    public class Timer
    {
        public static void run(int ms, Action callback = null)
        {
            if (!(ms > 0))
                throw new ArgumentException();

            new Thread(() => {
                Thread.Sleep(ms);

                if (callback != null)
                    callback();
            })
            { IsBackground = true }.Start();
        }
    }
}
