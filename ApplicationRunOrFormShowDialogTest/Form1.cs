using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationRunOrFormShowDialogTest
{
    public partial class Form1 : Form
    {
        Thread NewUIThread;
        Form2 form2;
        Form3 form3_ShowDialog;
        Form3 form3_NewUIThread;

        public Form1(string Text)
        {
            InitializeComponent();
            label1.Text = Text;

            form2 = new Form2();

            NewUIThread = new Thread(new ThreadStart(ApplicationRun));
            NewUIThread.SetApartmentState(ApartmentState.STA);

            // 새 Thread를 만들고 Application Run을하면 새로 만든 Thread에 운영체제가 MessageLoop를 새로 할당해 준다.
            // 하지만 유지보수하다보면 꼬이는 경우가 많으므로 비추하며,
            // 그럼에도 불구하고 사용해야할 경우 추가 Thread가 생성되지 않도록 STA를 걸어서 Single Thread임을 명시해야한다.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewUIThread.Start();

            // 여기서 생성하면 새로운 UI Thread가 생성되어 Form3가 호출된다.
            // Form3 내에서 Form2.ShowDialog를 호출하면 Form2의 부모는 Form3가 되며 
            // NewUIThread의 MessageLoop는 일시중지되고, Form2에 대한 새 MessageLoop가 생성되어 동작한다.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form3_ShowDialog = new Form3("Form3.ShowDialog()로 생성");
            form3_ShowDialog.ShowDialog();
        }

        private void ApplicationRun()
        {
            form3_NewUIThread = new Form3("Application.Run()으로 생성");
            Application.Run(form3_NewUIThread);
        }
    }
}
