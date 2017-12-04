namespace OSWPF1
{
    partial class BanUser
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
            this.lbl_User = new System.Windows.Forms.Label();
            this.cb_users = new System.Windows.Forms.ComboBox();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_ban = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_User
            // 
            this.lbl_User.AutoSize = true;
            this.lbl_User.Location = new System.Drawing.Point(8, 12);
            this.lbl_User.Name = "lbl_User";
            this.lbl_User.Size = new System.Drawing.Size(86, 13);
            this.lbl_User.TabIndex = 22;
            this.lbl_User.Text = "Пользователь: ";
            // 
            // cb_users
            // 
            this.cb_users.FormattingEnabled = true;
            this.cb_users.Location = new System.Drawing.Point(100, 9);
            this.cb_users.Name = "cb_users";
            this.cb_users.Size = new System.Drawing.Size(100, 21);
            this.cb_users.TabIndex = 21;
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(9, 54);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(75, 23);
            this.btn_exit.TabIndex = 18;
            this.btn_exit.Text = "Выйти";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_ban
            // 
            this.btn_ban.Location = new System.Drawing.Point(100, 54);
            this.btn_ban.Name = "btn_ban";
            this.btn_ban.Size = new System.Drawing.Size(100, 23);
            this.btn_ban.TabIndex = 17;
            this.btn_ban.Text = "Заблокировать";
            this.btn_ban.UseVisualStyleBackColor = true;
            this.btn_ban.Click += new System.EventHandler(this.btn_ban_Click);
            // 
            // BanUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 84);
            this.Controls.Add(this.lbl_User);
            this.Controls.Add(this.cb_users);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_ban);
            this.Name = "BanUser";
            this.Text = "BanUser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_User;
        private System.Windows.Forms.ComboBox cb_users;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_ban;
    }
}