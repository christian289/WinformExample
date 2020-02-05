using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationRunOrFormShowDialogTest
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 MainForm1 = new Form1("Form1.Show()로 생성");
            Form1 MainForm2 = new Form1("Application.Run()으로 생성");

            MainForm1.Show(); // Main UI Thread의 Message Loop 사용

            Application.Run(MainForm2); // Main UI Thread의 Message Loop 사용

            // Application.Run 에서 Message Loop를 잡고 있음.
            // 따라서 Form.ShowDialog 처럼 밑에 줄 코드로 내려가지 않음.
            // Application.Run으로 실행된 Form이 종료되거나 Application.Exit()가 호출되면 ApplicationExit 이벤트핸들러가 동작.
            // 이후 MessageLoop 종료되고 밑에 Form.Show는 예외가 발생함. 왜냐하면,
            // Show는 Show를 호출한 Thread의 MessageLoop를 공유하는데 ApplicationExit의 호출로 Main Thread의 MessageLoop가 종료되었기 때문

            MainForm1 = new Form1("Exception 폼"); // Exception 발생
            MainForm1.Show(); // Main UI Thread의 Message Loop 사용

            // 여담으로.. Main 함수에서 Application.Run을 사용하지 않고 Form1의 인스턴스를 할당하여 ShowDialog로 띄우고,
            // Form1에서 Application.Run을 해도 예외가 발생한다. 두번째 메세지 루프를 사용할 수 없다는 것인데,
            // Main Thread의 메세지 루프가 실행되지도 않았는데 Application.Run이 동작을 안한다는 것은 이해가 안된다.
        }
    }
}
