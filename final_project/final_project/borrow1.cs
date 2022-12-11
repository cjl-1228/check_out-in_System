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

using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using System.IO;
using System.Threading;


namespace final_project
{
    public partial class borrow1 : Form
    {
        string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|MyDB.mdf;" + "Integrated Security=True";


        string[] Scopes = { GmailService.Scope.GmailSend };
        string ApplicationName = "GmailApp";

        public PictureBox book_pic;
        public static borrow1 borrow1Instance;
        public Label a;
        public borrow1()
        {
            InitializeComponent();
            this.Text = "借還書系統";
            borrow1Instance = this;
            book_pic = pictureBox1;
            a = label1;
        }

        private void borrow1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        string Base64UrlEncode(string input)
        {
            var data = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(data).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string date2 = DateTime.Now.ToShortDateString();

            DateTime endDate2 = Convert.ToDateTime(date2);
            endDate2 = endDate2.AddDays(7);
            DateTime end2 = endDate2;
            string expiry_date2 = end2.ToShortDateString();
            DialogResult Result = MessageBox.Show($"借閱期限為7天，到期日期為{expiry_date2}\r\n確認是否要借閱此書本?", "提示", MessageBoxButtons.OKCancel);

            if (Result == DialogResult.OK)
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection())
                    {
                        cn.ConnectionString = cnstr;
                        cn.Open();

                        string account = Login.acc;

                        int book_id = home.book_id;

                        string time = DateTime.Now.ToShortTimeString();
                        string date = DateTime.Now.ToShortDateString();

                        DateTime endDate = Convert.ToDateTime(date);
                        endDate = endDate.AddDays(7);
                        DateTime end = endDate;
                        string expiry_date = end.ToShortDateString();

                        string sqlstr = $"UPDATE 書籍借閱 SET 借閱者 ='{account}', 借閱日 = '{date}', 到期日 = '{expiry_date}' WHERE 書籍編號='{book_id}'";
                        SqlCommand cmd2 = new SqlCommand(sqlstr, cn);
                        cmd2.ExecuteNonQuery();

                        string sqlstr2 = $"INSERT INTO 借閱紀錄(帳號, 編號, 借閱日)VALUES('{account}','{book_id}','{date}')";
                        SqlCommand cmd3 = new SqlCommand(sqlstr2, cn);
                        cmd3.ExecuteNonQuery();

                        MessageBox.Show("借閱成功");
                        this.Hide();

                        DataSet ds = new DataSet();
                        SqlDataAdapter data;
                        data = new SqlDataAdapter("SELECT 書籍.書籍編號 AS 編號, Photo AS 封面, 書名, 作者, 出版社, 出版年, 分類名稱 AS 分類 FROM 書籍, 書籍分類, 書籍借閱 WHERE 書籍.分類 = 書籍分類.分類編號 AND 書籍.書籍編號 = 書籍借閱.書籍編號 AND 書籍借閱.借閱者 IS NULL", cn);
                        data.Fill(ds, "書籍");
                        DataTable dt = ds.Tables["書籍"];
                        home.homeInstance.bor_dgv.DataSource = dt;

                        UserCredential credential;
                        //read your credentials file
                        using (FileStream stream = new FileStream(Application.StartupPath + @"/credentials.json", FileMode.Open, FileAccess.Read))
                        {
                            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                            path = Path.Combine(path, ".credentials/gmail-dotnet-quickstart.json");
                            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(path, true)).Result;
                        }


                        string username = home.username;
                        string bookname = home.bookame;
                        string author = home.author;
                        string useremail = home.useremail;

                        string email = "zaq5891097@gmail.com";
                        string subject = "Hi, You checked out a book is successfully!";
                        string message2 = $"{username}您好<br>您在{date} {time}借閱了此本書<br><br> 編號：{book_id}<br>書名：{bookname}<br>作者：{author}<br>到期日：{expiry_date} <br><br> 借還書系統";

                        string message = $"To: {useremail}\r\nSubject: {subject}\r\nContent-Type: text/html;charset=utf-8\r\n\r\n<h1>{message2}</h1>";
                        //call your gmail service
                        var service = new GmailService(new BaseClientService.Initializer() { HttpClientInitializer = credential, ApplicationName = ApplicationName });
                        var msg = new Google.Apis.Gmail.v1.Data.Message();
                        msg.Raw = Base64UrlEncode(message.ToString());
                        service.Users.Messages.Send(msg, "me").Execute();



                    };

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + ",gmail api over load");
                }
            }
            else if (Result == DialogResult.Cancel)
            {
                MessageBox.Show("已取消借閱");
                this.Hide();
            }
        }
    }
}
