using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace final_project
{
    public partial class change_email : Form
    {
        string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|MyDB.mdf;" + "Integrated Security=True";

        public change_email()
        {
            InitializeComponent();
            this.Text = "借還書系統";
        }

        private void change_email_Load(object sender, EventArgs e)
        {

        }
        Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");

        private void btn_changeemail_Click(object sender, EventArgs e)
        {
            string email = txt_changeemail.Text;
            if (email == "")
            {
                MessageBox.Show("欄位不得為空");
            }
            else if (!reg.IsMatch(txt_changeemail.Text))
            {
                MessageBox.Show("Email格式錯誤，請重新輸入");
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

                        string sqlstr = $"UPDATE 個人資料 SET email ='{txt_changeemail.Text}' WHERE 帳號='{account}'";
                        SqlCommand cmd2 = new SqlCommand(sqlstr, cn);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("更改成功");

                        home.homeInstance.per_tbx_useremail.Text = "電子信箱：" + txt_changeemail.Text;

                        this.Hide();
                    };
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + ",更改名稱時發生錯誤");
                }
            }
            txt_changeemail.Text = "";
        }
    }
}
