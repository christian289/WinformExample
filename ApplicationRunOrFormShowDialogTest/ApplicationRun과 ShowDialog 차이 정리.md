#Application.Run 과 Form.ShowDialog

##공통점
- Message Loop를 운영체제에서 할당받는다.

##차이점
- Application.Run은 Thread 당 1번만 사용할 수 있다. 
  Main 함수에서 Application.Run을 사용하고 있으므로, Application.Run을 사용하고 싶다면 새로 Thread를 만들어서 사용.
  단, 이 경우 동시에 UI Thread Message Loop가 2개가 도는 것이므로, Window Message 처리에 버그가 발생할 수 있음.
  Application.Run 으로 생성된 Form을 X 버튼으로 클릭하여 닫는 경우, FormClosing, FormClosed Event 다음으로 ApplicationExit 이벤트 핸들러가 호출됨. (Application.Exit() 와 같은 효과)
  이후 현재 Thread의 Message Loop가 종료되므로, 현재 Thread에서 Form.Show를 하는 경우 이미 종료된 Message Loop를 참조하게 되어 Exception 발생.

- Form.ShowDialog는 Thread를 새로 생성하지는 않지만, 운영체제로부터 현재 Thread에 새 Message Loop를 새로 할당받아, 
  ShowDialog를 호출한 Thread의 기존 Message Loop는 일시정지시키고 새로 받은 Message Loop를 우선 처리함. (이 때부터 마우스 이벤트 등등 Window Message를 ShowDialog의 Message Loop가 처리하게 됨.)
  때문에 ShowDialog를 호출한 폼의 Message Loop가 일시정지되어 UI 업데이트를 할 수 없음.
  당연한 말이지만, Application.Run으로 생성된 Form이 아니기 때문에 ApplicationExit 이벤트 핸들러가 호출되지 않음.

##참고
- Form.ShowDialog MessageLoop 관련 : https://docs.microsoft.com/ko-kr/dotnet/framework/winforms/advanced/com-interop-by-displaying-a-windows-form-shadow
Message Loop 2개 이상일 경우 버그 발생 경고 : https://social.msdn.microsoft.com/Forums/windows/en-US/ac65ea58-9f21-404d-9a01-52a2ec4af05b/can-a-windows-application-have-two-message-pumps?forum=winforms
Invoke, BeginInvoke의 차이 및 Form Message Queue 관련: https://www.codeproject.com/Articles/10311/What-s-up-with-BeginInvoke
