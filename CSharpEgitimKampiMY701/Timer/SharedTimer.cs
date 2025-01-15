using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampiMY701.Timer
{
    public class SharedTimer
    {
        public static System.Windows.Forms.Timer MainTimer = new System.Windows.Forms.Timer() { Interval = 1000 };

        // Tick olayını dışarı aç
        public static event EventHandler TimerTick;

        static SharedTimer()
        {
            MainTimer.Tick += (sender, e) => TimerTick?.Invoke(sender, e);
            MainTimer.Start();
        }
    }
}
