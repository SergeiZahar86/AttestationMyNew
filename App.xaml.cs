using Attestation;
using System;
using System.Diagnostics;
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
				global.user = Global.ShortName(e.Args[1].ToString());
			}

			// проверяем и убиваем раннее запущенные процессы этого приложения
			int id = Process.GetCurrentProcess().Id;
			string appname = Process.GetCurrentProcess().ProcessName;
			Process[] process = Process.GetProcessesByName(appname);
			foreach(Process p in process)
            {
				if(p.Id!= id)
                {
					try
					{
						p.Kill();
					} catch (Exception ex) { }
                }				
				
            }
			MainWindow wnd = new MainWindow();
			wnd.Show();
		}
	}
}
