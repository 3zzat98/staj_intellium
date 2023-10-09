using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalSystem
{
    public partial class frmDatabase : Form
    {
        public frmDatabase()
        {
            InitializeComponent();
        }

        private void GetDatabase()
        {
            dgvDatabase.DataSource = User.ListDatabase();
        }

        private void Database_Load(object sender, EventArgs e)
        {
            GetDatabase();
        }
    }
}
