using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace POSK3
{
    public partial class Login : Form
    {
        const string GLOBAL_SALT = "g\\%*A";
        private List<User> users = new List<User>();
        private int badLogCount = 0;



        public Login()
        {
            users.Add(new User("Jacek", "Wtorek", GLOBAL_SALT));
            users.Add(new User("Admin", "admin", GLOBAL_SALT));
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) //login
        {
            if (textBox1.Text.Equals("")) button1.Enabled = false;
            else button1.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e) //hasło
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool userFound = false;

            foreach (User user in users)
            {
                if (user.login.Equals(textBox1.Text)) //dobry login
                {
                    userFound = true;
                    if (user.CheckPass(textBox2.Text, GLOBAL_SALT)) //dobre hasło
                    {
                        new Linia().Show();
                        this.Hide();
                        break;
                    }
                    else //złe hasło
                    {
                        badLogCount++;
                        if (badLogCount > 2)
                        {
                            MessageBox.Show("Podano 3 razy błędne hasło. Dostęp został tymczasowo zablokowany");
                            Application.Exit();
                        }
                        else
                        {
                            MessageBox.Show("Podano niepoprawne hasło");
                            break;
                        }
                    }

                }

            }
            if (!userFound) //zły login
            {
                MessageBox.Show("Błędny login");
            }
        }
    }
}