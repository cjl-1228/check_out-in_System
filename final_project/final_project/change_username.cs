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
    public partial class change_username : Form
    {
        string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|MyDB.mdf;" + "Integrated Security=True";

        public change_username()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.Text = "借還書系統";
        }

        private void btn_changeusername_Click(object sender, EventArgs e)
        {
            string newname = txt_changeusername_name.Text;
            if (newname == "")
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

                        string sqlstr = $"UPDATE 個人資料 SET 名稱 ='{newname}' WHERE 帳號='{account}'";
                        SqlCommand cmd2 = new SqlCommand(sqlstr, cn);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("更改成功");

                        home.homeInstance.per_tbx_username.Text = "名稱：" + newname;

                        this.Hide();
                    };
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + ",更改名稱時發生錯誤");
                }
            }
        }
    }
}
