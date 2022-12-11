using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace final_project
{
    public partial class Login : Form
    {
        string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|MyDB.mdf;" + "Integrated Security=True";
        public static string acc = "";
        public static string pass = "";
        public Login()
        {
            InitializeComponent();
            this.Text = "借還書系統";
            txt_passwd.PasswordChar = '•';
        }


        private void lab_register_Click(object sender, EventArgs e)
        {
            var register = new register();
            register.Show();

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = cnstr;
                cn.Open();
                string account = txt_account.Text;
                
                string passwd = txt_passwd.Text;
                string selectCmd = $"SELECT * FROM 個人資料 WHERE 帳號 = '{account}' collate Chinese_PRC_CS_AI";
                SqlCommand cmd = new SqlCommand(selectCmd, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (account == "" || passwd == "")
                {
                    MessageBox.Show("帳號或密碼不得為空");
                }
                else if (dr.Read())
                {
                    if (passwd != dr["密碼"].ToString())
                    {
                        MessageBox.Show("帳號或密碼錯誤");
                    }
                    else
                    {

                        MessageBox.Show("您好，"+ dr["名稱"],"登入成功");
                        acc = account;
                        pass = passwd;
                        this.Hide();
                        var home = new home();
                        home.Closed += (s, args) => this.Close();
                        home.Show();

                    }
                    
              
                }
                else{
                    MessageBox.Show("帳號或密碼錯誤");
                }
            }
            txt_account.Text = "";
            txt_passwd.Text = "";
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
