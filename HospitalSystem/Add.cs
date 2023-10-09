using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalSystem
{
    public partial class frmAdd : Form
    {
        public frmAdd()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            User user = new User();

            byte[] key = new byte[16];

            byte[] iv = new byte[16];

            using (RandomNumberGenerator range = RandomNumberGenerator.Create())
            {
                range.GetBytes(key);
                range.GetBytes(iv);
            }

            byte[] encryptedPassword = Encrypt(txtbxPassword.Text, key, iv);

            user.UserName = txtbxUserName.Text;
            user.Password = Convert.ToBase64String(encryptedPassword);
            user.Email = txtbxEmail.Text;
            user.Phone = txtbxPhone.Text;
            user.DateOfBirth = dtDateOfBirth.Value;
            user.Address = txtbxAddress.Text;
            user.OriginalPassword = Decrypt(encryptedPassword, key, iv);
            

            if (user.Save())
            {
                MessageBox.Show("User Saved Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("User Not Saved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public byte[] Encrypt(string text, byte[] key, byte[] iv)
        {
            byte[] encryptedText;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(text);
                        }

                        encryptedText = memoryStream.ToArray();
                    }
                }
            }
            return encryptedText;
        }

        public string Decrypt(byte[] encryptedText, byte[] key, byte[] iv)
        {
            string text = String.Empty;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
                using (MemoryStream memoryStream = new MemoryStream(encryptedText))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            text = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            return text;
        }

        private void frmAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
