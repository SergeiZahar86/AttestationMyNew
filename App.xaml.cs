using Attestation;
using System.Windows;

namespace TestWPF
{
    public partial class App : Application
    {
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			MainWindow wnd = new MainWindow();
			if (e.Args.Length > 0)
				MessageBox.Show("Now opening file: \n\n" + e.Args[0].ToString() + e.Args[1].ToString());
			wnd.Show();
		}
	}
}
