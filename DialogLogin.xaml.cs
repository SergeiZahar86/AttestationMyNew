using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Attestation
{
    /// <summary>
    /// Логика взаимодействия для TestDialog.xaml
    /// </summary>
    public partial class DialogLogin : Window
    {
        public string Login;
        private Global global;
        public DialogLogin()
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
            this.Close();

        }
        
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false ;
            this.Close();
        }
    }
}
