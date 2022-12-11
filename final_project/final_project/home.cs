using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace final_project
{
    public partial class home : Form
    {
        string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|MyDB.mdf;" + "Integrated Security=True";

        bool sidebarExpand =true;

        public static home homeInstance;

        public Label per_tbx_username;
        public Label per_tbx_useremail;

        public PictureBox per_picbox_userimg;
        public PictureBox home_self_pic;

        public DataGridView bor_dgv;

        public static string username = "";
        public static string bookame = "";
        public static string author = "";
        public static string useremail = "";
        public home()
        {
            InitializeComponent();
            this.Text = "借還書系統";
            homeInstance = this;
            per_tbx_username = per_name;
            per_picbox_userimg = per_userpic;
            bor_dgv = dataGridView1;
            per_tbx_useremail = per_email;
            home_self_pic = self_pic;

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, self_pic.Width, self_pic.Height);
            System.Drawing.Drawing2D.GraphicsPath path2 = new System.Drawing.Drawing2D.GraphicsPath();
            path2.AddEllipse(0, 0, per_userpic.Width, per_userpic.Height);
            self_pic.Region = new Region(path);
            per_userpic.Region = new Region(path2);
        }

        private void home_Load(object sender, EventArgs e)
        {
            dataGridView1.RowTemplate.Height = 250;
            dataGridView2.RowTemplate.Height = 250;
            dataGridView3.RowTemplate.Height = 40;
            dataGridView4.RowTemplate.Height = 35;
            dv_QA.RowTemplate.Height = 75;
            timer1.Start();

            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = cnstr;
                cn.Open();
                string account = Login.acc;
                string selectCmd = $"SELECT * FROM 個人資料 WHERE 帳號 = '{account}'";
                SqlCommand cmd = new SqlCommand(selectCmd, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string c = dr["PhotoUrl"].ToString();
                    if(c != "no")
                    {
                        byte[] img = (byte[])dr["Photo"];
                        MemoryStream ms = new MemoryStream(img);
                        self_pic.Image = Image.FromStream(ms);
                    }





                }
                dr.Close();
            }

        }

        private void siderbarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {

                sidebar.Width -= 10;
                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    siderbarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if(sidebar.Width == sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    siderbarTimer.Stop();
                }
            }
        }

        private void home_btn_menu_Click(object sender, EventArgs e)
        {
            siderbarTimer.Start();
        }

        private void home_btn_home_Click(object sender, EventArgs e)
        {
            panel_QA.Visible = false;
            panel_returnbook.Visible= false;
            panel_home.Visible = true;
            panel_personal.Visible = false;
            panel_sale.Visible = false;
            home_btn_home.BackColor = Color.FromArgb(67, 99, 64);
            home_btn_personal.BackColor = Color.FromArgb(30, 40, 45);
            btn_sale.BackColor = Color.FromArgb(30, 40, 45);
            btn_QA.BackColor = Color.FromArgb(30, 40, 45);
            btn_buy.BackColor = Color.FromArgb(30, 40, 45);

            DataSet ds = new DataSet();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = cnstr;
            cn.Open();
            SqlDataAdapter data = new SqlDataAdapter($"SELECT 借閱日, 書籍.書名 FROM 借閱紀錄,書籍 WHERE 帳號 = '{Login.acc}' AND 書籍.書籍編號 = 借閱紀錄.編號 ", cn);
            data.Fill(ds, "書籍");
            DataTable dt = ds.Tables["書籍"];
            

            dataGridView3.DataSource = dt;
            dataGridView3.ClearSelection();
            DataGridViewColumn column0 = dataGridView3.Columns[0];
            column0.Width = 200;
            DataGridViewColumn column1 = dataGridView3.Columns[1];
            column1.Width = 200;

            using (SqlConnection cn2 = new SqlConnection())
            {
                cn2.ConnectionString = cnstr;
                cn2.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn2;
                cmd.CommandText = "GetBookCount";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds2 = new DataSet();
                da.Fill(ds2, "借閱次數");
                dataGridView4.DataSource = ds2.Tables["借閱次數"];
                cmd.Cancel();

            }

            dataGridView4.ClearSelection();
            DataGridViewColumn column2 = dataGridView4.Columns[0];
            column2.Width = 200;
            DataGridViewColumn column3 = dataGridView4.Columns[1];
            column3.Width = 85;

            string top1 = dataGridView4.Rows[0].Cells[0].Value.ToString();
            using (SqlConnection cn3 = new SqlConnection())
            {
                cn3.ConnectionString = cnstr;
                cn3.Open();
                string selectCmd = $"SELECT * FROM 書籍 WHERE 書名 = '{top1}'";
                SqlCommand cmd = new SqlCommand(selectCmd, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lab_TOP1.Text = dr["書名"].ToString();
                    byte[] img = (byte[])dr["Photo"];
                    MemoryStream ms = new MemoryStream(img);
                    pic_TOP1.Image = Image.FromStream(ms);



                }
                dr.Close();
            }

            string top2 = dataGridView4.Rows[1].Cells[0].Value.ToString();
            using (SqlConnection cn4 = new SqlConnection())
            {
                cn4.ConnectionString = cnstr;
                cn4.Open();
                string selectCmd = $"SELECT * FROM 書籍 WHERE 書名 = '{top2}'";
                SqlCommand cmd = new SqlCommand(selectCmd, cn4);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lab_TOP2.Text = dr["書名"].ToString();
                    byte[] img = (byte[])dr["Photo"];
                    MemoryStream ms = new MemoryStream(img);
                    pic_TOP2.Image = Image.FromStream(ms);



                }
                dr.Close();
            }

            string top3 = dataGridView4.Rows[2].Cells[0].Value.ToString();
            using (SqlConnection cn5 = new SqlConnection())
            {
                cn5.ConnectionString = cnstr;
                cn5.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn5;
                cmd.CommandText = "GetTOP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@bookname", SqlDbType.NVarChar));
                cmd.Parameters["@bookname"].Value = top3;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lab_TOP3.Text = dr["書名"].ToString();
                    byte[] img = (byte[])dr["Photo"];
                    MemoryStream ms = new MemoryStream(img);
                    pic_TOP3.Image = Image.FromStream(ms);
                }
                dr.Close();
            }
        }

        private void home_btn_personal_Click(object sender, EventArgs e)
        {
            panel_QA.Visible = false;
            btn_QA.BackColor = Color.FromArgb(30, 40, 45);
            btn_buy.BackColor = Color.FromArgb(30, 40, 45);
            panel_returnbook.Visible= false;
            panel_home.Visible = false;
            panel_sale.Visible = false;
            panel_personal.Visible = true;
            home_btn_personal.BackColor = Color.FromArgb(67, 99, 64);
            home_btn_home.BackColor = Color.FromArgb(30, 40, 45);
            btn_sale.BackColor = Color.FromArgb(30, 40, 45);
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = cnstr;
                cn.Open();
                string account = Login.acc;
                string selectCmd = $"SELECT * FROM 個人資料 WHERE 帳號 = '{account}'";
                SqlCommand cmd = new SqlCommand(selectCmd, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    per_name.Text = "名稱："+dr["名稱"];
                    per_account.Text = "帳號："+ dr["帳號"];
                    per_date.Text = "創建日期："+ dr["日期"];
                    lab_PhotoUrl.Text = dr["PhotoUrl"].ToString();
                    username = dr["名稱"].ToString() ;
                    useremail = dr["email"].ToString() ;
                    per_email.Text = "電子信箱："+ dr["email"].ToString();
                    if (lab_PhotoUrl.Text != "no")
                    {
                        byte[] img = (byte[])dr["Photo"];
                        MemoryStream ms = new MemoryStream(img);
                        per_userpic.Image = Image.FromStream(ms);
                    }


                }
                dr.Close();
            }
        }

        private void per_close_Click(object sender, EventArgs e)
        {
            panel_personal.Visible = false;
            home_btn_personal.BackColor = Color.FromArgb(30, 40, 45);
        }

        private void per_btn_logout_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("是否要登出?", "提示", MessageBoxButtons.OKCancel);
            if (Result == DialogResult.OK)
            {
                this.Hide();
                var Login = new Login();
                Login.Closed += (s, args) => this.Close();
                Login.Show();
            }

        }

        private void per_lab_delaccount_Click(object sender, EventArgs e)
        {
            DialogResult Result2 = MessageBox.Show("是否要刪除帳號?", "提示", MessageBoxButtons.OKCancel);
            if (Result2 == DialogResult.OK)
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection())
                    {
                        cn.ConnectionString = cnstr;
                        cn.Open();
                        string account = Login.acc;
                        string sqlstr = $"DELETE FROM 個人資料 WHERE 帳號 = '{account}'";
                        SqlCommand cmd = new SqlCommand(sqlstr, cn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("已刪除帳號");
                        this.Hide();
                        var Login2 = new Login();
                        Login2.Closed += (s, args) => this.Close();
                        Login2.Show();
                    };

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + ",刪除帳號時發生錯誤");
                }
            }

        }

        private void per_btn_changepasswd_Click(object sender, EventArgs e)
        {
            var change_passwd = new change_passwd();
            change_passwd.Show();
        }

        private void per_btn_changename_Click(object sender, EventArgs e)
        {
            var change_username = new change_username();
            change_username.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var change_userimg = new change_userimg();
            change_userimg.Show();
        }

        private void btn_sale_Click(object sender, EventArgs e)
        {
            panel_QA.Visible = false;
            btn_QA.BackColor = Color.FromArgb(30, 40, 45);
            btn_buy.BackColor = Color.FromArgb(30, 40, 45);
            
            btn_sale.BackColor = Color.FromArgb(67, 99, 64);
            home_btn_personal.BackColor = Color.FromArgb(30, 40, 45);
            home_btn_home.BackColor = Color.FromArgb(30, 40, 45);
            panel_sale.Visible = true;
            panel_home.Visible = false;
            panel_personal.Visible = false;
            panel_returnbook.Visible = false;

            DataSet ds = new DataSet();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = cnstr;
            cn.Open();
            SqlDataAdapter data = new SqlDataAdapter("SELECT 書籍.書籍編號 AS 編號, Photo AS 封面, 書名, 作者, 出版社, 出版年, 分類名稱 AS 分類 FROM 書籍, 書籍分類, 書籍借閱 WHERE 書籍.分類 = 書籍分類.分類編號 AND 書籍.書籍編號 = 書籍借閱.書籍編號 AND 書籍借閱.借閱者 IS NULL", cn);
            data.Fill(ds, "書籍");
            DataTable dt = ds.Tables["書籍"];
            dataGridView1.DataSource = dt;


            dataGridView1.ClearSelection();
            DataGridViewColumn column0 = dataGridView1.Columns[0];
            column0.Width = 60;
            DataGridViewColumn column1 = dataGridView1.Columns[1];
            column1.Width = 200;
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            column2.Width = 170;
            DataGridViewColumn column3 = dataGridView1.Columns[3];
            column3.Width = 150;
            DataGridViewColumn column4 = dataGridView1.Columns[4];
            column4.Width = 130;
            DataGridViewColumn column5 = dataGridView1.Columns[5];
            column5.Width = 80;
            DataGridViewColumn column6 = dataGridView1.Columns[6];
            column6.Width = 145;

            cn.Close();

        }
        public static int book_id;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var borrow1 = new borrow1();
            borrow1.Show();

            int r = dataGridView1.CurrentCell.RowIndex;
            string n = dataGridView1.Rows[r].Cells[0].Value.ToString();
            string n2 = dataGridView1.Rows[r].Cells[6].Value.ToString();



            try
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = cnstr;
                    cn.Open();

                    string sqlstr = $"SELECT * FROM 書籍  WHERE 書籍編號='{n}'";
                    SqlCommand cmd = new SqlCommand(sqlstr, cn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    string data = null;
                    if (dr.Read())
                    {
                        data = $"編號：{dr["書籍編號"]}\r\n\r\n" +
                               $"書名：{dr["書名"]}\r\n" +
                               $"作者：{dr["作者"]}\r\n\r\n" +
                               $"出版社：{dr["出版社"]}\r\n" +
                               $"出版年：{dr["出版年"]}\r\n" +
                               $"分類：{n2}\r\n" +
                               $"頁數：{dr["頁數"]}\r\n\r\n" +
                               $"備註：{dr["備註"]}\r\n";
                        bookame = dr["書名"].ToString();
                        author = dr["作者"].ToString();

                        byte[] img = (byte[])dr["Photo"];
                        MemoryStream ms = new MemoryStream(img);
                        borrow1.borrow1Instance.book_pic.Image = Image.FromStream(ms);
                        borrow1.borrow1Instance.a.Text = data;

                        book_id = Int32.Parse(dr["書籍編號"].ToString());

                    };
                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ",讀取資料時發生錯誤");
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {

            
            if (cobx_Classification.Text == "選擇查詢分類")
            {
                MessageBox.Show("請選擇查詢分類");
            }
            else
            {

                DataSet ds = new DataSet();
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = cnstr;
                cn.Open();
                SqlDataAdapter data;
                if (txb_search.Text == "")
                {
                    data = new SqlDataAdapter("SELECT 書籍.書籍編號 AS 編號, Photo AS 封面, 書名, 作者, 出版社, 出版年, 分類名稱 AS 分類 FROM 書籍, 書籍分類, 書籍借閱 WHERE 書籍.分類 = 書籍分類.分類編號 AND 書籍.書籍編號 = 書籍借閱.書籍編號 AND 書籍借閱.借閱者 IS NULL", cn);

                }
                else if (cobx_Classification.Text == "編號")
                {
                    data = new SqlDataAdapter($"SELECT 書籍.書籍編號 AS 編號, Photo AS 封面, 書名, 作者, 出版社, 出版年, 分類名稱 AS 分類 FROM 書籍, 書籍分類, 書籍借閱 WHERE 書籍.分類 = 書籍分類.分類編號 AND 書籍.書籍編號 = '{txb_search.Text}' AND 書籍.書籍編號 = 書籍借閱.書籍編號 AND 書籍借閱.借閱者 IS NULL", cn);
                }
                else if(cobx_Classification.Text == "書名")
                {
                    data = new SqlDataAdapter($"SELECT 書籍.書籍編號 AS 編號, Photo AS 封面, 書名, 作者, 出版社, 出版年, 分類名稱 AS 分類 FROM 書籍, 書籍分類, 書籍借閱 WHERE 書籍.分類 = 書籍分類.分類編號 AND (書名 LIKE '%{txb_search.Text}%') AND 書籍.書籍編號 = 書籍借閱.書籍編號 AND 書籍借閱.借閱者 IS NULL", cn);

                }
                else if(cobx_Classification.Text == "作者")
                {
                    data = new SqlDataAdapter($"SELECT 書籍.書籍編號 AS 編號, Photo AS 封面, 書名, 作者, 出版社, 出版年, 分類名稱 AS 分類 FROM 書籍, 書籍分類, 書籍借閱 WHERE 書籍.分類 = 書籍分類.分類編號 AND (作者 LIKE '%{txb_search.Text}%') AND 書籍.書籍編號 = 書籍借閱.書籍編號 AND 書籍借閱.借閱者 IS NULL", cn);

                }
                else if (cobx_Classification.Text == "出版社")
                {
                    data = new SqlDataAdapter($"SELECT 書籍.書籍編號 AS 編號, Photo AS 封面, 書名, 作者, 出版社, 出版年, 分類名稱 AS 分類 FROM 書籍, 書籍分類, 書籍借閱 WHERE 書籍.分類 = 書籍分類.分類編號 AND (出版社 LIKE '%{txb_search.Text}%') AND 書籍.書籍編號 = 書籍借閱.書籍編號 AND 書籍借閱.借閱者 IS NULL", cn);

                }
                else if (cobx_Classification.Text == "出版年")
                {
                    data = new SqlDataAdapter($"SELECT 書籍.書籍編號 AS 編號, Photo AS 封面, 書名, 作者, 出版社, 出版年, 分類名稱 AS 分類 FROM 書籍, 書籍分類, 書籍借閱 WHERE 書籍.分類 = 書籍分類.分類編號 AND 出版年 = {txb_search.Text} AND 書籍.書籍編號 = 書籍借閱.書籍編號 AND 書籍借閱.借閱者 IS NULL", cn);

                }
                else
                {
                    
                    data = new SqlDataAdapter("SELECT 書籍.書籍編號 AS 編號, Photo AS 封面, 書名, 作者, 出版社, 出版年, 分類名稱 AS 分類 FROM 書籍, 書籍分類, 書籍借閱 WHERE 書籍.分類 = 書籍分類.分類編號 AND 書籍.書籍編號 = 書籍借閱.書籍編號 AND 書籍借閱.借閱者 IS NULL", cn);

                }
                try
                {
                    data.Fill(ds, "書籍");
                    DataTable dt = ds.Tables["書籍"];
                    dataGridView1.DataSource = dt;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex+"查詢時發生問題或搜尋格式錯誤");
                }

                cn.Close();
                txb_search.Text = "";
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("osk.exe");
        }

        private void label5_Click(object sender, EventArgs e)
        {
            btn_sale.BackColor = Color.FromArgb(30, 40, 45);
            panel_sale.Visible = false;
        }

        private void cobx_Classification_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cobx_Classification.Text == "分類")
            {
                comboBox1.Visible = true;
                txb_search.Visible = false;
                DataSet ds = new DataSet();
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = cnstr;
                cn.Open();
                SqlDataAdapter data = new SqlDataAdapter("SELECT * FROM 書籍分類 ", cn);
                data.Fill(ds, "書籍分類");
                comboBox1.DataSource = ds;
                comboBox1.DisplayMember = "書籍分類.分類名稱";
                btn1.Enabled = false;
                
            }
            else
            {
                comboBox1.Visible = false;
                txb_search.Visible = true;
                btn1.Enabled = true;
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = cnstr;
            cn.Open();
            SqlDataAdapter data;

            data = new SqlDataAdapter($"SELECT 書籍.書籍編號 AS 編號, Photo AS 封面, 書名, 作者, 出版社, 出版年, 分類名稱 AS 分類 FROM 書籍, 書籍分類, 書籍借閱 WHERE 書籍.分類 = 書籍分類.分類編號 AND 書籍分類.分類名稱 = '{comboBox1.Text}' AND 書籍.書籍編號 = 書籍借閱.書籍編號 AND 書籍借閱.借閱者 IS NULL", cn);
            
            try
            {
                data.Fill(ds, "書籍");
                DataTable dt = ds.Tables["書籍"];
                dataGridView1.DataSource = dt;
            }
            catch
            {
                MessageBox.Show("查詢時發生問題或搜尋格式錯誤");
            }

            

        }

        private void btn_buy_Click(object sender, EventArgs e)
        {
            panel_QA.Visible = false;
            btn_QA.BackColor = Color.FromArgb(30, 40, 45);
            dateTimePicker1.Value = DateTime.Now;
            btn_buy.BackColor = Color.FromArgb(67, 99, 64);
            btn_sale.BackColor = Color.FromArgb(30, 40, 45);
            home_btn_personal.BackColor = Color.FromArgb(30, 40, 45);
            home_btn_home.BackColor = Color.FromArgb(30, 40, 45);
            panel_sale.Visible = false;
            panel_home.Visible = false;
            panel_personal.Visible = false;
            panel_returnbook.Visible = true;

            DataSet ds = new DataSet();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = cnstr;
            cn.Open();
            SqlDataAdapter data = new SqlDataAdapter($"SELECT 書籍.書籍編號 AS 編號, Photo AS 封面, 書名, 作者, 書籍借閱.借閱日, 書籍借閱.到期日 FROM 書籍, 書籍分類, 書籍借閱 WHERE 書籍.分類 = 書籍分類.分類編號 AND 書籍.書籍編號 = 書籍借閱.書籍編號 AND 書籍借閱.借閱者 = '{Login.acc}'", cn);
            data.Fill(ds, "書籍");
            DataTable dt = ds.Tables["書籍"];
            dataGridView2.DataSource = dt;

            dataGridView2.ClearSelection();
            DataGridViewColumn column0 = dataGridView2.Columns[0];
            column0.Width = 80;
            DataGridViewColumn column1 = dataGridView2.Columns[1];
            column1.Width = 200;
            DataGridViewColumn column2 = dataGridView2.Columns[2];
            column2.Width = 200;
            DataGridViewColumn column3 = dataGridView2.Columns[3];
            column3.Width = 170;
            DataGridViewColumn column4 = dataGridView2.Columns[4];
            column4.Width = 150;
            DataGridViewColumn column5 = dataGridView2.Columns[5];
            column5.Width = 150;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var change_email = new change_email();
            change_email.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            btn_buy.BackColor = Color.FromArgb(30, 40, 45);
            panel_returnbook.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("osk.exe");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cobx_Classification2.Text != "選擇查詢分類")
            {
                dateTimePicker1.Enabled = true;
            }
        }

        private void btn_seach2_Click(object sender, EventArgs e)
        {
            if (cobx_Classification2.Text == "選擇查詢分類")
            {
                MessageBox.Show("請選擇查詢分類");
            }
            else
            {

                string date = dateTimePicker1.Value.ToShortDateString();

                DataSet ds = new DataSet();
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = cnstr;
                cn.Open();
                SqlDataAdapter data;
                if (cobx_Classification2.Text == "借閱日")
                {
                    data = new SqlDataAdapter($"SELECT 書籍.書籍編號 AS 編號, Photo AS 封面, 書名, 作者, 書籍借閱.借閱日, 書籍借閱.到期日 FROM 書籍, 書籍分類, 書籍借閱 WHERE 書籍.分類 = 書籍分類.分類編號 AND 書籍.書籍編號 = 書籍借閱.書籍編號 AND 書籍借閱.借閱者 = '{Login.acc}' AND 書籍借閱.借閱日 = '{date}'", cn);
                }
                else
                {
                    data = new SqlDataAdapter($"SELECT 書籍.書籍編號 AS 編號, Photo AS 封面, 書名, 作者, 書籍借閱.借閱日, 書籍借閱.到期日 FROM 書籍, 書籍分類, 書籍借閱 WHERE 書籍.分類 = 書籍分類.分類編號 AND 書籍.書籍編號 = 書籍借閱.書籍編號 AND 書籍借閱.借閱者 = '{Login.acc}' AND 書籍借閱.到期日 = '{date}'", cn);

                }
                try
                {
                    data.Fill(ds, "書籍");
                    DataTable dt = ds.Tables["書籍"];
                    dataGridView2.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex + "查詢時發生問題或搜尋格式錯誤");
                }

                cn.Close();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int r = dataGridView2.CurrentCell.RowIndex;
            string id = dataGridView2.Rows[r].Cells[0].Value.ToString();
            string bookname = dataGridView2.Rows[r].Cells[2].Value.ToString();
            string author = dataGridView2.Rows[r].Cells[3].Value.ToString();
            DialogResult Result = MessageBox.Show($"編號：{id}\r\n書名：{bookname}\r\n作者：{author}\r\n\r\n確定是否要還此書?", "提示", MessageBoxButtons.OKCancel);

            if (Result == DialogResult.OK)
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlConnection cn = new SqlConnection();
                    cn.ConnectionString = cnstr;
                    cn.Open();

                    SqlDataAdapter data;
                    data = new SqlDataAdapter($"UPDATE 書籍借閱 SET 借閱者 = NULL, 借閱日 = NULL, 到期日 = NULL WHERE 書籍編號='{id}'", cn);
                    data.Fill(ds, "書籍");
                    DataTable dt = ds.Tables["書籍"];

                    SqlDataAdapter data2 = new SqlDataAdapter($"SELECT 書籍.書籍編號 AS 編號, Photo AS 封面, 書名, 作者, 書籍借閱.借閱日, 書籍借閱.到期日 FROM 書籍, 書籍分類, 書籍借閱 WHERE 書籍.分類 = 書籍分類.分類編號 AND 書籍.書籍編號 = 書籍借閱.書籍編號 AND 書籍借閱.借閱者 = '{Login.acc}'", cn);
                    data2.Fill(ds, "書籍2");
                    DataTable dt2 = ds.Tables["書籍2"];
                    dataGridView2.DataSource = dt2;


                    cn.Close();
                    MessageBox.Show("還書成功");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex + "還書時時發生問題或錯誤");
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lab_time.Text = DateTime.Now.ToLongTimeString();
            lab_date.Text = DateTime.Now.ToLongDateString();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            panel_home.Visible = false;
            home_btn_home.BackColor = Color.FromArgb(30, 40, 45);
        }

        private void label15_Click(object sender, EventArgs e)
        {
            panel_QA.Visible = false;
            btn_QA.BackColor = Color.FromArgb(30, 40, 45);
        }
        
        private void btn_QA_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            panel_QA.Visible = true;
            btn_QA.BackColor = Color.FromArgb(67, 99, 64);
            home_btn_home.BackColor = Color.FromArgb(30, 40, 45);
            home_btn_personal.BackColor = Color.FromArgb(30, 40, 45);
            btn_sale.BackColor = Color.FromArgb(30, 40, 45);
            btn_buy.BackColor = Color.FromArgb(30, 40, 45);
            panel_sale.Visible = false;
            panel_home.Visible = false;
            panel_personal.Visible = false;
            panel_returnbook.Visible = false;



            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = cnstr;
            cn.Open();
            SqlDataAdapter data = new SqlDataAdapter($"SELECT * FROM 常見問題", cn);
            data.Fill(ds, "常見問題");
            DataTable dt = ds.Tables["常見問題"];
            dv_QA.DataSource = dt;





            dv_QA.ClearSelection();
            DataGridViewColumn column0 = dv_QA.Columns[0];
            column0.Width = 50;
            DataGridViewColumn column1 = dv_QA.Columns[1];
            column1.Width = 345;
            DataGridViewColumn column2 = dv_QA.Columns[2];
            column2.Width = 600;
        }
       
        private void button3_Click(object sender, EventArgs e)
        {

            DataSet1 ds2 = new DataSet1();
            DataSet1TableAdapters.常見問題TableAdapter da = new DataSet1TableAdapters.常見問題TableAdapter();
            da.Fill(ds2.常見問題);
            if (txtbox_QA.Text == "")
            {
                dv_QA.DataSource = ds2.常見問題;
            }
            else
            {

                try
                {
                    var emp = ds2.常見問題
                        .Where(m => m.問題.Contains(txtbox_QA.Text));
                    dv_QA.DataSource = emp.ToList();

                    dv_QA.Columns["RowError"].Visible = false;
                    dv_QA.Columns["RowState"].Visible = false;
                    dv_QA.Columns["Table"].Visible = false;
                    dv_QA.Columns["HasErrors"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            txtbox_QA.Text = "";
            dv_QA.ClearSelection();
            DataGridViewColumn column0 = dv_QA.Columns[0];
            column0.Width = 50;
            DataGridViewColumn column1 = dv_QA.Columns[1];
            column1.Width = 345;
            DataGridViewColumn column2 = dv_QA.Columns[2];
            column2.Width = 600;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
