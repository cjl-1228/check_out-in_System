using Google.Apis.Gmail.v1.Data;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace final_project
{
    

    public partial class register : Form
    {
        string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|MyDB.mdf;" + "Integrated Security=True";

        public register()
        {
            InitializeComponent();
            this.Text = "借還書系統";
            txt_reg_passwd.PasswordChar = '•';
            txt_reg_checkpasswd.PasswordChar = '•';
        }
        string imageUrl = null;
        private void btn_register_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (txt_reg_account.Text == "" || txt_reg_passwd.Text == "" || txt_reg_checkpasswd.Text == "" || txt_reg_name.Text=="" || txt_reg_email.Text == "")
            {
                MessageBox.Show("所有欄位不得為空");
            }
            else if(txt_reg_passwd.Text != txt_reg_checkpasswd.Text) 
            {
                MessageBox.Show("確認密碼不符合，請重試");
            }
            else if (!reg.IsMatch(txt_reg_email.Text))
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
                        string account = txt_reg_account.Text;
                        string passwd = txt_reg_passwd.Text;
                        string name = txt_reg_name.Text;
                        string date = DateTime.Now.ToShortDateString().ToString();
                        string email = txt_reg_email.Text;

                        Image img = pic_userpic.Image;
                        byte[] arr;
                        ImageConverter converter = new ImageConverter();
                        arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                        string sqlstr = null;
                        if (btn_uploadphoto.Text == "    瀏覽")
                        {
                            sqlstr = $"INSERT INTO 個人資料(帳號, 密碼, 名稱, 日期, PhotoUrl, email)VALUES('{account}','{passwd}','{name}','{date}','no','{email}')";
                            SqlCommand cmd4 = new SqlCommand(sqlstr, cn);
                            cmd4.ExecuteNonQuery();
                        }
                        else
                        {
                            sqlstr = $"INSERT INTO 個人資料(帳號, 密碼, 名稱, 日期, Photo, PhotoUrl, email)VALUES('{account}','{passwd}','{name}','{date}', @Photo, @PhotoUrl,'{email}')";
                            txt_reg_account.Text = "No";
                            SqlCommand cmd3 = new SqlCommand(sqlstr, cn);
                            cmd3.Parameters.AddWithValue("@Photo", arr);
                            cmd3.Parameters.AddWithValue("@PhotoUrl", imageUrl);
                            cmd3.ExecuteNonQuery();
                        }



                        MessageBox.Show("建立帳號成功");
                        this.Hide();
                    };
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + ",建立帳號時發生錯誤");
                }
            }
            txt_reg_account.Text = "";
            txt_reg_passwd.Text = "";
            txt_reg_checkpasswd.Text = "";
            txt_reg_name.Text = "";
            txt_reg_email.Text = "";
        }

        
        private void btn_uploadphoto_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        imageUrl = ofd.FileName;
                        pic_userpic.Image = Image.FromFile(ofd.FileName);
                        if (btn_uploadphoto.Text == "    瀏覽")
                        {
                            btn_uploadphoto.Text = "重新上傳";
                        }
                    }
                    catch {
                        MessageBox.Show("檔案格式錯誤或其他問題");
                    }

                }

            }
        }
    }
}
