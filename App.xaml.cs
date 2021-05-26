using Attestation;
using System.Windows;

namespace TestWPF
{
    public partial class App : Application
    {
		private Global global;
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			global = Global.getInstance();
			global.ArmAttestation = true;

			if (e.Args.Length == 2)
            {
				global.ArmAttestation = false;
				global.Login = e.Args[0].ToString();
				global.user = e.Args[1].ToString();
				//MessageBox.Show("Now opening file: \n\n" + e.Args[0].ToString() + e.Args[1].ToString());
			}
			MainWindow wnd = new MainWindow();
			wnd.Show();
		}
	}
}
