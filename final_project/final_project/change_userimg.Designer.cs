namespace final_project
{
    partial class change_userimg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_savephoto = new System.Windows.Forms.Button();
            this.btn_uploadphoto = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btn_savephoto);
            this.panel1.Controls.Add(this.btn_uploadphoto);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(6, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(325, 323);
            this.panel1.TabIndex = 0;
            // 
            // btn_savephoto
            // 
            this.btn_savephoto.BackColor = System.Drawing.Color.Green;
            this.btn_savephoto.FlatAppearance.BorderSize = 0;
            this.btn_savephoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_savephoto.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_savephoto.ForeColor = System.Drawing.Color.White;
            this.btn_savephoto.Location = new System.Drawing.Point(183, 246);
            this.btn_savephoto.Name = "btn_savephoto";
            this.btn_savephoto.Size = new System.Drawing.Size(108, 34);
            this.btn_savephoto.TabIndex = 23;
            this.btn_savephoto.Text = "更改頭像";
            this.btn_savephoto.UseVisualStyleBackColor = false;
            this.btn_savephoto.Click += new System.EventHandler(this.btn_savephoto_Click);
            // 
            // btn_uploadphoto
            // 
            this.btn_uploadphoto.BackColor = System.Drawing.Color.Green;
            this.btn_uploadphoto.FlatAppearance.BorderSize = 0;
            this.btn_uploadphoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_uploadphoto.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_uploadphoto.ForeColor = System.Drawing.Color.White;
            this.btn_uploadphoto.Location = new System.Drawing.Point(34, 246);
            this.btn_uploadphoto.Name = "btn_uploadphoto";
            this.btn_uploadphoto.Size = new System.Drawing.Size(108, 34);
            this.btn_uploadphoto.TabIndex = 22;
            this.btn_uploadphoto.Text = "瀏覽";
            this.btn_uploadphoto.UseVisualStyleBackColor = false;
            this.btn_uploadphoto.Click += new System.EventHandler(this.btn_uploadphoto_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(87, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(120, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "編輯頭像";
            // 
            // change_userimg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(336, 334);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "change_userimg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "change_userimg";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_savephoto;
        private System.Windows.Forms.Button btn_uploadphoto;
    }
}