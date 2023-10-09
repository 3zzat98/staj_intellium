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
    public partial class frmUsers : Form
    {

        private void _Refresh()
        {
            dgvUsers.DataSource = User.List();
        }

        public frmUsers(string UserName)
        {
            InitializeComponent();

            _Refresh();

            lblCurrentUser.Text = UserName;
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            _Refresh();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmAdd AddUser = new frmAdd();

            AddUser.ShowDialog();

            _Refresh();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete user [" + dgvUsers.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {

                if (User.Delete((int)dgvUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User Deleted Successfully.");
                    _Refresh();
                }

                else
                    MessageBox.Show("User is not deleted.");

            }
        }

        private void pbDatabase_Click(object sender, EventArgs e)
        {
            frmDatabase frmDatabase = new frmDatabase();

            frmDatabase.ShowDialog();
        }
    }
}
