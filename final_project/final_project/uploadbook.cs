using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace final_project
{
    public partial class uploadbook : Form
    {
        public uploadbook()
        {
            InitializeComponent();
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        
        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = cnstr;
                    cn.Open();

                    Image img = pictureBox1.Image;

                    img = resizeImage(img, new Size(150, 200));

                    byte[] arr;
                    ImageConverter converter = new ImageConverter();
                    arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                    string sqlstr = null;

                    sqlstr = $"INSERT INTO 書籍(書名, 作者, 出版社, 出版年, 分類, 頁數, Photo, PhotoUrl)VALUES('釋放創傷, 從呼吸開始', '湯柯夫(Tonkov, Giten) , 祝家康','楓樹林','2021',6,'477', @Photo, @PhotoUrl)";

                    SqlCommand cmd3 = new SqlCommand(sqlstr, cn);
                    cmd3.Parameters.AddWithValue("@Photo", arr);
                    cmd3.Parameters.AddWithValue("@PhotoUrl", imageUrl);
                    cmd3.ExecuteNonQuery();



                };
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ",建立帳號時發生錯誤");
            }
        }
        string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|MyDB.mdf;" + "Integrated Security=True";

        string imageUrl = null;
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        imageUrl = ofd.FileName;
                        pictureBox1.Image = Image.FromFile(ofd.FileName);

                    }
                    catch
                    {
                        MessageBox.Show("檔案格式錯誤或其他問題");
                    }

                }

            }
        }
    }
}
