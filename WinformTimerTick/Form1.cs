using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformTimerTick
{
    /*
     * <시나리오>
     * 윈폼타이머의 인터벌을 짧게 준다음, 타이머 Tick에서 무거운 작업을 수행한다.
     * 비교를 위해 가벼운 Tick도 확인한다.
     * 각각 무거운 Tick 버튼 두개를 눌렀을 때 UI가 Freeze 되는 걸 확인한다.
     * 타이머 interval이 매우 짧더라도 Tick 작업이 무거우면 또 Tick 발생하지 않는다는 것을 Console 로그를 통해 확인한다.
     * 즉, 타이머 내부에서 Enable을 사용해 '혹시모를' 오류를 방지하는 것은 의미가 없고, UI가 Freeze 되지 않으며, 무거운 작업을 수행하고 싶다면 Background Thread처리가 맞다.
     * 다섯번째 Thread 버튼을 누르면 문제없이 동작하는 것을 볼 수 있다.
     */

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnWorkStart1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 10; // 10 ms 마다 동작 (시스템환경에 따라 정확하지 않을 수 있음.)
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("무거운 Tick 시작");

            for (long i = 0; i < 100000000000000; i++)
            {
                label1.Text = i.ToString(); // 같은 UI Thread 상에서 동작하므로, Invoke는 필요없다.
            }
        }

        private void BtnWorkStart2_Click(object sender, EventArgs e)
        {
            timer2.Interval = 10; // 10 ms 마다 동작 (시스템환경에 따라 정확하지 않을 수 있음.)
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("무거운 Enable 추가 Tick 시작");

            timer2.Enabled = false;

            for (long i = 0; i < 100000000000000; i++)
            {
                label2.Text = i.ToString(); // 같은 UI Thread 상에서 동작하므로, Invoke는 필요없다.
            }

            timer2.Enabled = true;
        }

        private void BtnWorkStart3_Click(object sender, EventArgs e)
        {
            timer3.Interval = 10; // 10 ms 마다 동작 (시스템환경에 따라 정확하지 않을 수 있음.)
            timer3.Enabled = true;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("가벼운 Tick 시작");

            timer3.Enabled = false;

            for (long i = 0; i < 100; i++)
            {
                label3.Text = i.ToString(); // 같은 UI Thread 상에서 동작하므로, Invoke는 필요없다.
            }

            timer3.Enabled = true;
        }

        private void BtnWorkStart4_Click(object sender, EventArgs e)
        {
            timer4.Interval = 10; // 10 ms 마다 동작 (시스템환경에 따라 정확하지 않을 수 있음.)
            timer4.Enabled = true;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("가벼운 Enable 추가 Tick 시작");

            timer4.Enabled = false;

            for (long i = 0; i < 100; i++)
            {
                label4.Text = i.ToString(); // 같은 UI Thread 상에서 동작하므로, Invoke는 필요없다.
            }

            timer4.Enabled = true;
        }

        private void BtnWorkStart5_Click(object sender, EventArgs e)
        {
            Task.Run(() => {
                while (true)
                {
                    Console.WriteLine("무거운 작업 worker thread로 시작");

                    for (long i = 0; i < 100000000000000; i++)
                    {
                        Invoke(new MethodInvoker(delegate () { label5.Text = i.ToString(); })); // 다른 Worker Thread 상에서 동작하므로 Invoke가 필요하다.
                    }

                    Task.Delay(10);
                }
            });
        }
    }
}
