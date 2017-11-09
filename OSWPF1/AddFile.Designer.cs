namespace OSWPF1
{
    partial class AddFile
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
            this.lbl_filename = new System.Windows.Forms.Label();
            this.tb_filename = new System.Windows.Forms.TextBox();
            this.lbl_flags = new System.Windows.Forms.Label();
            this.chb_system = new System.Windows.Forms.CheckBox();
            this.chb_hidden = new System.Windows.Forms.CheckBox();
            this.chb_dir = new System.Windows.Forms.CheckBox();
            this.num_size = new System.Windows.Forms.NumericUpDown();
            this.lbl_size = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbox_user = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.num_size)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_filename
            // 
            this.lbl_filename.AutoSize = true;
            this.lbl_filename.Location = new System.Drawing.Point(12, 15);
            this.lbl_filename.Name = "lbl_filename";
            this.lbl_filename.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_filename.Size = new System.Drawing.Size(67, 13);
            this.lbl_filename.TabIndex = 0;
            this.lbl_filename.Text = "Имя файла:";
            // 
            // tb_filename
            // 
            this.tb_filename.Location = new System.Drawing.Point(172, 12);
            this.tb_filename.Name = "tb_filename";
            this.tb_filename.Size = new System.Drawing.Size(100, 20);
            this.tb_filename.TabIndex = 1;
            // 
            // lbl_flags
            // 
            this.lbl_flags.AutoSize = true;
            this.lbl_flags.Location = new System.Drawing.Point(12, 49);
            this.lbl_flags.Name = "lbl_flags";
            this.lbl_flags.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_flags.Size = new System.Drawing.Size(44, 13);
            this.lbl_flags.TabIndex = 2;
            this.lbl_flags.Text = "Флаги:";
            // 
            // chb_system
            // 
            this.chb_system.AutoSize = true;
            this.chb_system.Location = new System.Drawing.Point(172, 49);
            this.chb_system.Name = "chb_system";
            this.chb_system.Size = new System.Drawing.Size(84, 17);
            this.chb_system.TabIndex = 3;
            this.chb_system.Text = "Системный";
            this.chb_system.UseVisualStyleBackColor = true;
            // 
            // chb_hidden
            // 
            this.chb_hidden.AutoSize = true;
            this.chb_hidden.Location = new System.Drawing.Point(172, 72);
            this.chb_hidden.Name = "chb_hidden";
            this.chb_hidden.Size = new System.Drawing.Size(72, 17);
            this.chb_hidden.TabIndex = 4;
            this.chb_hidden.Text = "Скрытый";
            this.chb_hidden.UseVisualStyleBackColor = true;
            // 
            // chb_dir
            // 
            this.chb_dir.AutoSize = true;
            this.chb_dir.Location = new System.Drawing.Point(172, 95);
            this.chb_dir.Name = "chb_dir";
            this.chb_dir.Size = new System.Drawing.Size(88, 17);
            this.chb_dir.TabIndex = 5;
            this.chb_dir.Text = "Директория";
            this.chb_dir.UseVisualStyleBackColor = true;
            // 
            // num_size
            // 
            this.num_size.Location = new System.Drawing.Point(172, 118);
            this.num_size.Name = "num_size";
            this.num_size.Size = new System.Drawing.Size(84, 20);
            this.num_size.TabIndex = 6;
            // 
            // lbl_size
            // 
            this.lbl_size.AutoSize = true;
            this.lbl_size.Location = new System.Drawing.Point(12, 120);
            this.lbl_size.Name = "lbl_size";
            this.lbl_size.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_size.Size = new System.Drawing.Size(49, 13);
            this.lbl_size.TabIndex = 7;
            this.lbl_size.Text = "Размер:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(262, 120);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "КБ";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(197, 226);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 9;
            this.btn_add.Text = "Добавить";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(15, 226);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(75, 23);
            this.btn_exit.TabIndex = 10;
            this.btn_exit.Text = "Выйти";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 155);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Пользователь:";
            // 
            // cbox_user
            // 
            this.cbox_user.FormattingEnabled = true;
            this.cbox_user.Location = new System.Drawing.Point(172, 152);
            this.cbox_user.Name = "cbox_user";
            this.cbox_user.Size = new System.Drawing.Size(100, 21);
            this.cbox_user.TabIndex = 12;
            // 
            // AddFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.cbox_user);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_size);
            this.Controls.Add(this.num_size);
            this.Controls.Add(this.chb_dir);
            this.Controls.Add(this.chb_hidden);
            this.Controls.Add(this.chb_system);
            this.Controls.Add(this.lbl_flags);
            this.Controls.Add(this.tb_filename);
            this.Controls.Add(this.lbl_filename);
            this.Name = "AddFile";
            this.Text = "AddFile";
            ((System.ComponentModel.ISupportInitialize)(this.num_size)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_filename;
        private System.Windows.Forms.TextBox tb_filename;
        private System.Windows.Forms.Label lbl_flags;
        private System.Windows.Forms.CheckBox chb_system;
        private System.Windows.Forms.CheckBox chb_hidden;
        private System.Windows.Forms.CheckBox chb_dir;
        private System.Windows.Forms.NumericUpDown num_size;
        private System.Windows.Forms.Label lbl_size;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbox_user;
    }
}