namespace TDay
{
    partial class Conn
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_ServerName = new System.Windows.Forms.TextBox();
            this.textBox_ServerUser = new System.Windows.Forms.TextBox();
            this.textBox_ServerPasswd = new System.Windows.Forms.TextBox();
            this.textBox_Database = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Elephant", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Name :";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Elephant", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.DimGray;
            this.button1.Location = new System.Drawing.Point(72, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Elephant", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.DimGray;
            this.button2.Location = new System.Drawing.Point(205, 177);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 30);
            this.button2.TabIndex = 2;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Elephant", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Server User :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Elephant", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(12, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "Server Passwd :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Elephant", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(12, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "Database:";
            // 
            // textBox_ServerName
            // 
            this.textBox_ServerName.Font = new System.Drawing.Font("Elephant", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_ServerName.ForeColor = System.Drawing.Color.DimGray;
            this.textBox_ServerName.Location = new System.Drawing.Point(153, 17);
            this.textBox_ServerName.Name = "textBox_ServerName";
            this.textBox_ServerName.Size = new System.Drawing.Size(210, 24);
            this.textBox_ServerName.TabIndex = 6;
            // 
            // textBox_ServerUser
            // 
            this.textBox_ServerUser.Font = new System.Drawing.Font("Elephant", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_ServerUser.ForeColor = System.Drawing.Color.DimGray;
            this.textBox_ServerUser.Location = new System.Drawing.Point(153, 51);
            this.textBox_ServerUser.Name = "textBox_ServerUser";
            this.textBox_ServerUser.Size = new System.Drawing.Size(210, 24);
            this.textBox_ServerUser.TabIndex = 7;
            // 
            // textBox_ServerPasswd
            // 
            this.textBox_ServerPasswd.Font = new System.Drawing.Font("Elephant", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_ServerPasswd.ForeColor = System.Drawing.Color.DimGray;
            this.textBox_ServerPasswd.Location = new System.Drawing.Point(153, 85);
            this.textBox_ServerPasswd.Name = "textBox_ServerPasswd";
            this.textBox_ServerPasswd.Size = new System.Drawing.Size(210, 24);
            this.textBox_ServerPasswd.TabIndex = 8;
            this.textBox_ServerPasswd.UseSystemPasswordChar = true;
            // 
            // textBox_Database
            // 
            this.textBox_Database.Font = new System.Drawing.Font("Elephant", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Database.ForeColor = System.Drawing.Color.DimGray;
            this.textBox_Database.Location = new System.Drawing.Point(153, 119);
            this.textBox_Database.Name = "textBox_Database";
            this.textBox_Database.Size = new System.Drawing.Size(210, 24);
            this.textBox_Database.TabIndex = 9;
            // 
            // Conn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 215);
            this.Controls.Add(this.textBox_Database);
            this.Controls.Add(this.textBox_ServerPasswd);
            this.Controls.Add(this.textBox_ServerUser);
            this.Controls.Add(this.textBox_ServerName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Conn";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection";
            this.Load += new System.EventHandler(this.Conn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_ServerName;
        private System.Windows.Forms.TextBox textBox_ServerUser;
        private System.Windows.Forms.TextBox textBox_ServerPasswd;
        private System.Windows.Forms.TextBox textBox_Database;
    }
}