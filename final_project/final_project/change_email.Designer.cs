namespace final_project
{
    partial class change_email
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txt_changeemail = new System.Windows.Forms.TextBox();
            this.btn_changeemail = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btn_changeemail);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(5, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(361, 210);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(24, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "編輯信箱";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(24, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "請輸入您要更改的信箱";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Green;
            this.panel2.Controls.Add(this.txt_changeemail);
            this.panel2.Location = new System.Drawing.Point(28, 102);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel2.Size = new System.Drawing.Size(300, 27);
            this.panel2.TabIndex = 5;
            // 
            // txt_changeemail
            // 
            this.txt_changeemail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_changeemail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_changeemail.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txt_changeemail.ForeColor = System.Drawing.Color.Green;
            this.txt_changeemail.Location = new System.Drawing.Point(0, 0);
            this.txt_changeemail.Multiline = true;
            this.txt_changeemail.Name = "txt_changeemail";
            this.txt_changeemail.Size = new System.Drawing.Size(300, 24);
            this.txt_changeemail.TabIndex = 6;
            // 
            // btn_changeemail
            // 
            this.btn_changeemail.BackColor = System.Drawing.Color.Green;
            this.btn_changeemail.FlatAppearance.BorderSize = 0;
            this.btn_changeemail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_changeemail.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_changeemail.ForeColor = System.Drawing.Color.White;
            this.btn_changeemail.Location = new System.Drawing.Point(121, 151);
            this.btn_changeemail.Name = "btn_changeemail";
            this.btn_changeemail.Size = new System.Drawing.Size(108, 34);
            this.btn_changeemail.TabIndex = 13;
            this.btn_changeemail.Text = "更改";
            this.btn_changeemail.UseVisualStyleBackColor = false;
            this.btn_changeemail.Click += new System.EventHandler(this.btn_changeemail_Click);
            // 
            // change_email
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(373, 221);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "change_email";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "change_email";
            this.Load += new System.EventHandler(this.change_email_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txt_changeemail;
        private System.Windows.Forms.Button btn_changeemail;
    }
}