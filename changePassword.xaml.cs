using System.Collections.Generic;
using System.Windows;

namespace Attestation
{
    /// <summary>
    /// Логика взаимодействия для TestDialog.xaml
    /// </summary>
    public partial class changePassword : Window
    {
        public string Login;
        private Global global;
        private List<cause_t> Cause;
        public changePassword()
        {
            InitializeComponent();
            global = Global.getInstance();
        }
        private void ok_Click(object sender, RoutedEventArgs e)
        {

            //Login = tbLogin.Password;
            this.DialogResult = true;

            //String FIO = global.chLogin("oper","oper", null);
            //List<cause_t> list = global.getCauses();
            //part_t part = global.getPart(1);
            //photo_t photo = global.getPhoto(1, 1);
            //Cause = global.getCauses();
            this.Close();

        }
        
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false ;
            this.Close();
        }
    }
}
