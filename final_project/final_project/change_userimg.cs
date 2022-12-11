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
using System.Xml.Linq;

namespace final_project
{

    public partial class change_userimg : Form
    {
        string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|MyDB.mdf;" + "Integrated Security=True";

        public change_userimg()
        {
            InitializeComponent();
            this.Text = "借還書系統";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        string imageUrl = null;
        private void btn_uploadphoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                try
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        imageUrl = ofd.FileName;
                        pictureBox1.Image = Image.FromFile(ofd.FileName);
                        if (btn_uploadphoto.Text == "瀏覽")
                        {
                            btn_uploadphoto.Text = "重新上傳";
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("檔案格式錯誤或其他問題");

                }


            }
        }

        private void btn_savephoto_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = cnstr;
                    cn.Open();
                    string account = Login.acc;
                    Image img = pictureBox1.Image;
                    byte[] arr;
                    ImageConverter converter = new ImageConverter();
                    arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                    string sqlstr = null;
                    if (btn_uploadphoto.Text == "瀏覽")
                    {
                        MessageBox.Show("請上傳頭像");
                    }
                    else
                    {
                        sqlstr = $"UPDATE 個人資料 SET Photo = @Photo, PhotoUrl = @PhotoUrl WHERE 帳號='{account}'";
                        SqlCommand cmd3 = new SqlCommand(sqlstr, cn);
                        cmd3.Parameters.AddWithValue("@Photo", arr);
                        cmd3.Parameters.AddWithValue("@PhotoUrl", imageUrl);
                        cmd3.ExecuteNonQuery();

                        MessageBox.Show("更改成功");
                        home.homeInstance.per_picbox_userimg.Image = img;
                        home.homeInstance.home_self_pic.Image = img;
                        this.Hide();

                    }



                };
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ",建立帳號時發生錯誤");
            }
        }
    }
}
