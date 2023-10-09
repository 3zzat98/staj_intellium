using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalSystem;

namespace HospitalSystem
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string UserName = txtbxUserName.Text;
            string Password = txtbxPassword.Text;

            if(User.IsExist(UserName, Password) || (UserName == txtbxUserName.Tag.ToString() 
                && Password ==  txtbxPassword.Tag.ToString()))
            {
                frmUsers User = new frmUsers(UserName);

                User.ShowDialog();
            }
        }

        private void txtbxUserName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
