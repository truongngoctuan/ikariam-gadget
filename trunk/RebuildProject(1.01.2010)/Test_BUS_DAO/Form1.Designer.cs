namespace Test_BUS_DAO
{
    partial class Form1
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
            this.tbResult = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button24 = new System.Windows.Forms.Button();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.button25 = new System.Windows.Forms.Button();
            this.lbNRequest = new System.Windows.Forms.Label();
            this.button30 = new System.Windows.Forms.Button();
            this.button31 = new System.Windows.Forms.Button();
            this.button32 = new System.Windows.Forms.Button();
            this.tbThreaddebug = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(276, 12);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbResult.Size = new System.Drawing.Size(486, 429);
            this.tbResult.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(103, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(103, 12);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(167, 20);
            this.tbUsername.TabIndex = 2;
            this.tbUsername.Text = "hakuna1811";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(103, 38);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(167, 20);
            this.tbPassword.TabIndex = 3;
            this.tbPassword.Text = "741369456";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            // 
            // button24
            // 
            this.button24.Location = new System.Drawing.Point(188, 93);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(82, 23);
            this.button24.TabIndex = 28;
            this.button24.Text = "logout";
            this.button24.UseVisualStyleBackColor = true;
            this.button24.Click += new System.EventHandler(this.button24_Click);
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(103, 65);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(167, 20);
            this.tbServer.TabIndex = 29;
            this.tbServer.Text = "s15.en.ikariam.com";
            // 
            // button25
            // 
            this.button25.Location = new System.Drawing.Point(12, 93);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(75, 23);
            this.button25.TabIndex = 30;
            this.button25.Text = "vn login";
            this.button25.UseVisualStyleBackColor = true;
            this.button25.Click += new System.EventHandler(this.button25_Click);
            // 
            // lbNRequest
            // 
            this.lbNRequest.AutoSize = true;
            this.lbNRequest.Location = new System.Drawing.Point(12, 71);
            this.lbNRequest.Name = "lbNRequest";
            this.lbNRequest.Size = new System.Drawing.Size(13, 13);
            this.lbNRequest.TabIndex = 35;
            this.lbNRequest.Text = "0";
            // 
            // button30
            // 
            this.button30.Location = new System.Drawing.Point(12, 122);
            this.button30.Name = "button30";
            this.button30.Size = new System.Drawing.Size(75, 23);
            this.button30.TabIndex = 36;
            this.button30.Text = "run thread";
            this.button30.UseVisualStyleBackColor = true;
            this.button30.Click += new System.EventHandler(this.button30_Click);
            // 
            // button31
            // 
            this.button31.Location = new System.Drawing.Point(188, 122);
            this.button31.Name = "button31";
            this.button31.Size = new System.Drawing.Size(82, 23);
            this.button31.TabIndex = 38;
            this.button31.Text = "client request";
            this.button31.UseVisualStyleBackColor = true;
            this.button31.Click += new System.EventHandler(this.button31_Click);
            // 
            // button32
            // 
            this.button32.Location = new System.Drawing.Point(103, 122);
            this.button32.Name = "button32";
            this.button32.Size = new System.Drawing.Size(75, 23);
            this.button32.TabIndex = 39;
            this.button32.Text = "stop thread";
            this.button32.UseVisualStyleBackColor = true;
            this.button32.Click += new System.EventHandler(this.button32_Click_1);
            // 
            // tbThreaddebug
            // 
            this.tbThreaddebug.Location = new System.Drawing.Point(12, 184);
            this.tbThreaddebug.Multiline = true;
            this.tbThreaddebug.Name = "tbThreaddebug";
            this.tbThreaddebug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbThreaddebug.Size = new System.Drawing.Size(258, 257);
            this.tbThreaddebug.TabIndex = 40;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(188, 151);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 41;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 451);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbThreaddebug);
            this.Controls.Add(this.button32);
            this.Controls.Add(this.button31);
            this.Controls.Add(this.button30);
            this.Controls.Add(this.lbNRequest);
            this.Controls.Add(this.button25);
            this.Controls.Add(this.tbServer);
            this.Controls.Add(this.button24);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbResult);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button24;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Button button25;
        private System.Windows.Forms.Label lbNRequest;
        private System.Windows.Forms.Button button30;
        private System.Windows.Forms.Button button31;
        private System.Windows.Forms.Button button32;
        private System.Windows.Forms.TextBox tbThreaddebug;
        private System.Windows.Forms.Button button2;
    }
}

