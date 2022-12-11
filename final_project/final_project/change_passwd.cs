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
    public partial class change_passwd : Form
    {
        string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|MyDB.mdf;" + "Integrated Security=True";

        public change_passwd()
        {
            InitializeComponent();
            this.Text = "借還書系統";
            txt_changepasswd_curpasswd.PasswordChar =  '•'; 
            txt_changepasswd_newpasswd.PasswordChar = '•';
            txt_changepasswd_checkpasswd.PasswordChar = '•';


        }

        private void change_passwd_Load(object sender, EventArgs e)
        {

        }

        private void btn_changepasswd_Click(object sender, EventArgs e)
        {
            if(txt_changepasswd_checkpasswd.Text == "" || txt_changepasswd_curpasswd.Text == "" || txt_changepasswd_newpasswd.Text == "")
            {
                MessageBox.Show("所有欄位不得為空");
            }
            else
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection())
                    {
                        cn.ConnectionString = cnstr;
                        cn.Open();

                        string account = Login.acc;
                        string passwd = Login.pass;

                        string curpasswd = txt_changepasswd_curpasswd.Text;
                        string newpasswd = txt_changepasswd_newpasswd.Text;
                        string checlpasswd = txt_changepasswd_checkpasswd.Text;

                        if (curpasswd!=passwd)
                        {
                            MessageBox.Show("目前密碼不正確");
                        }
                        else if(newpasswd != checlpasswd)
                        {
                            MessageBox.Show("確認密碼不符合，請重試");
                        }
                        else
                        {
                            string sqlstr = $"UPDATE 個人資料 SET 密碼 ='{newpasswd}' WHERE 帳號='{account}'";
                            SqlCommand cmd2 = new SqlCommand(sqlstr, cn);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("更改成功");
                            this.Hide();
                        }


                    };
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + ",更改密碼時發生錯誤");
                }
            }
            txt_changepasswd_checkpasswd.Text = "";
            txt_changepasswd_curpasswd.Text = "";
            txt_changepasswd_newpasswd.Text = "";
        }
    }
}
