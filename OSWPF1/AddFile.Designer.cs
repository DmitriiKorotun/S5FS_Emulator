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
            this.tb_fullpath = new System.Windows.Forms.TextBox();
            this.chb_system = new System.Windows.Forms.CheckBox();
            this.chb_hidden = new System.Windows.Forms.CheckBox();
            this.chb_dir = new System.Windows.Forms.CheckBox();
            this.num_size = new System.Windows.Forms.NumericUpDown();
            this.lbl_size = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.ofd_addFile = new System.Windows.Forms.OpenFileDialog();
            this.btn_openFile = new System.Windows.Forms.Button();
            this.lbl_fullName = new System.Windows.Forms.Label();
            this.tb_name = new System.Windows.Forms.TextBox();
            this.check_uRead = new System.Windows.Forms.CheckBox();
            this.lay_flags = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_flags = new System.Windows.Forms.Label();
            this.lay_name = new System.Windows.Forms.FlowLayoutPanel();
            this.check_aWrite = new System.Windows.Forms.CheckBox();
            this.check_gRead = new System.Windows.Forms.CheckBox();
            this.check_gWrite = new System.Windows.Forms.CheckBox();
            this.check_gEx = new System.Windows.Forms.CheckBox();
            this.check_aRead = new System.Windows.Forms.CheckBox();
            this.check_aEx = new System.Windows.Forms.CheckBox();
            this.check_uEx = new System.Windows.Forms.CheckBox();
            this.check_uWrite = new System.Windows.Forms.CheckBox();
            this.lay_uRights = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.lay_gRights = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_group = new System.Windows.Forms.Label();
            this.lbl_all = new System.Windows.Forms.Label();
            this.lay_aRights = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lay_fileAttribs = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.rtb_data = new System.Windows.Forms.RichTextBox();
            this.lay_data = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_data = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_reset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.num_size)).BeginInit();
            this.lay_flags.SuspendLayout();
            this.lay_name.SuspendLayout();
            this.lay_uRights.SuspendLayout();
            this.lay_gRights.SuspendLayout();
            this.lay_aRights.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.lay_fileAttribs.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.lay_data.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_filename
            // 
            this.lbl_filename.AutoSize = true;
            this.lbl_filename.Location = new System.Drawing.Point(3, 0);
            this.lbl_filename.Name = "lbl_filename";
            this.lbl_filename.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_filename.Size = new System.Drawing.Size(67, 13);
            this.lbl_filename.TabIndex = 0;
            this.lbl_filename.Text = "Имя файла:";
            // 
            // tb_fullpath
            // 
            this.tb_fullpath.Location = new System.Drawing.Point(3, 55);
            this.tb_fullpath.MaxLength = 16;
            this.tb_fullpath.Name = "tb_fullpath";
            this.tb_fullpath.ReadOnly = true;
            this.tb_fullpath.Size = new System.Drawing.Size(100, 20);
            this.tb_fullpath.TabIndex = 1;
            // 
            // chb_system
            // 
            this.chb_system.AutoSize = true;
            this.chb_system.Location = new System.Drawing.Point(3, 39);
            this.chb_system.Name = "chb_system";
            this.chb_system.Size = new System.Drawing.Size(84, 17);
            this.chb_system.TabIndex = 3;
            this.chb_system.Text = "Системный";
            this.chb_system.UseVisualStyleBackColor = true;
            // 
            // chb_hidden
            // 
            this.chb_hidden.AutoSize = true;
            this.chb_hidden.Location = new System.Drawing.Point(3, 62);
            this.chb_hidden.Name = "chb_hidden";
            this.chb_hidden.Size = new System.Drawing.Size(72, 17);
            this.chb_hidden.TabIndex = 4;
            this.chb_hidden.Text = "Скрытый";
            this.chb_hidden.UseVisualStyleBackColor = true;
            // 
            // chb_dir
            // 
            this.chb_dir.AutoSize = true;
            this.chb_dir.Location = new System.Drawing.Point(3, 16);
            this.chb_dir.Name = "chb_dir";
            this.chb_dir.Size = new System.Drawing.Size(88, 17);
            this.chb_dir.TabIndex = 5;
            this.chb_dir.Text = "Директория";
            this.chb_dir.UseVisualStyleBackColor = true;
            // 
            // num_size
            // 
            this.num_size.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.num_size.InterceptArrowKeys = false;
            this.num_size.Location = new System.Drawing.Point(58, 3);
            this.num_size.Maximum = new decimal(new int[] {
            8240,
            0,
            0,
            0});
            this.num_size.Name = "num_size";
            this.num_size.ReadOnly = true;
            this.num_size.Size = new System.Drawing.Size(84, 20);
            this.num_size.TabIndex = 6;
            // 
            // lbl_size
            // 
            this.lbl_size.AutoSize = true;
            this.lbl_size.Location = new System.Drawing.Point(3, 0);
            this.lbl_size.Name = "lbl_size";
            this.lbl_size.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_size.Size = new System.Drawing.Size(49, 13);
            this.lbl_size.TabIndex = 7;
            this.lbl_size.Text = "Размер:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 0);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "КБ";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(257, 203);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 9;
            this.btn_add.Text = "Добавить";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(12, 203);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(75, 23);
            this.btn_exit.TabIndex = 10;
            this.btn_exit.Text = "Выйти";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // ofd_addFile
            // 
            this.ofd_addFile.FileName = "file";
            // 
            // btn_openFile
            // 
            this.btn_openFile.Location = new System.Drawing.Point(3, 29);
            this.btn_openFile.Name = "btn_openFile";
            this.btn_openFile.Size = new System.Drawing.Size(89, 23);
            this.btn_openFile.TabIndex = 15;
            this.btn_openFile.Text = "Выбрать файл";
            this.btn_openFile.UseVisualStyleBackColor = true;
            this.btn_openFile.Click += new System.EventHandler(this.btn_openFile_Click);
            // 
            // lbl_fullName
            // 
            this.lbl_fullName.AutoSize = true;
            this.lbl_fullName.Location = new System.Drawing.Point(3, 39);
            this.lbl_fullName.Name = "lbl_fullName";
            this.lbl_fullName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_fullName.Size = new System.Drawing.Size(71, 13);
            this.lbl_fullName.TabIndex = 16;
            this.lbl_fullName.Text = "Полное имя:";
            // 
            // tb_name
            // 
            this.tb_name.Location = new System.Drawing.Point(3, 16);
            this.tb_name.MaxLength = 16;
            this.tb_name.Name = "tb_name";
            this.tb_name.Size = new System.Drawing.Size(100, 20);
            this.tb_name.TabIndex = 17;
            // 
            // check_uRead
            // 
            this.check_uRead.AutoSize = true;
            this.check_uRead.Location = new System.Drawing.Point(3, 39);
            this.check_uRead.Name = "check_uRead";
            this.check_uRead.Size = new System.Drawing.Size(63, 17);
            this.check_uRead.TabIndex = 18;
            this.check_uRead.Text = "Чтение";
            this.check_uRead.UseVisualStyleBackColor = true;
            // 
            // lay_flags
            // 
            this.lay_flags.Controls.Add(this.lbl_flags);
            this.lay_flags.Controls.Add(this.chb_dir);
            this.lay_flags.Controls.Add(this.chb_system);
            this.lay_flags.Controls.Add(this.chb_hidden);
            this.lay_flags.Location = new System.Drawing.Point(96, 92);
            this.lay_flags.Name = "lay_flags";
            this.lay_flags.Size = new System.Drawing.Size(92, 81);
            this.lay_flags.TabIndex = 19;
            // 
            // lbl_flags
            // 
            this.lbl_flags.AutoSize = true;
            this.lbl_flags.Location = new System.Drawing.Point(3, 0);
            this.lbl_flags.Name = "lbl_flags";
            this.lbl_flags.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_flags.Size = new System.Drawing.Size(44, 13);
            this.lbl_flags.TabIndex = 2;
            this.lbl_flags.Text = "Флаги:";
            // 
            // lay_name
            // 
            this.lay_name.Controls.Add(this.lbl_filename);
            this.lay_name.Controls.Add(this.tb_name);
            this.lay_name.Controls.Add(this.lbl_fullName);
            this.lay_name.Controls.Add(this.tb_fullpath);
            this.lay_name.Location = new System.Drawing.Point(3, 3);
            this.lay_name.Name = "lay_name";
            this.lay_name.Size = new System.Drawing.Size(113, 84);
            this.lay_name.TabIndex = 20;
            // 
            // check_aWrite
            // 
            this.check_aWrite.AutoSize = true;
            this.check_aWrite.Location = new System.Drawing.Point(3, 16);
            this.check_aWrite.Name = "check_aWrite";
            this.check_aWrite.Size = new System.Drawing.Size(63, 17);
            this.check_aWrite.TabIndex = 21;
            this.check_aWrite.Text = "Запись";
            this.check_aWrite.UseVisualStyleBackColor = true;
            // 
            // check_gRead
            // 
            this.check_gRead.AutoSize = true;
            this.check_gRead.Location = new System.Drawing.Point(3, 39);
            this.check_gRead.Name = "check_gRead";
            this.check_gRead.Size = new System.Drawing.Size(63, 17);
            this.check_gRead.TabIndex = 22;
            this.check_gRead.Text = "Чтение";
            this.check_gRead.UseVisualStyleBackColor = true;
            // 
            // check_gWrite
            // 
            this.check_gWrite.AutoSize = true;
            this.check_gWrite.Location = new System.Drawing.Point(3, 16);
            this.check_gWrite.Name = "check_gWrite";
            this.check_gWrite.Size = new System.Drawing.Size(63, 17);
            this.check_gWrite.TabIndex = 23;
            this.check_gWrite.Text = "Запись";
            this.check_gWrite.UseVisualStyleBackColor = true;
            // 
            // check_gEx
            // 
            this.check_gEx.AutoSize = true;
            this.check_gEx.Location = new System.Drawing.Point(3, 62);
            this.check_gEx.Name = "check_gEx";
            this.check_gEx.Size = new System.Drawing.Size(88, 17);
            this.check_gEx.TabIndex = 24;
            this.check_gEx.Text = "Исполнение";
            this.check_gEx.UseVisualStyleBackColor = true;
            // 
            // check_aRead
            // 
            this.check_aRead.AutoSize = true;
            this.check_aRead.Location = new System.Drawing.Point(3, 39);
            this.check_aRead.Name = "check_aRead";
            this.check_aRead.Size = new System.Drawing.Size(63, 17);
            this.check_aRead.TabIndex = 25;
            this.check_aRead.Text = "Чтение";
            this.check_aRead.UseVisualStyleBackColor = true;
            // 
            // check_aEx
            // 
            this.check_aEx.AutoSize = true;
            this.check_aEx.Location = new System.Drawing.Point(3, 62);
            this.check_aEx.Name = "check_aEx";
            this.check_aEx.Size = new System.Drawing.Size(88, 17);
            this.check_aEx.TabIndex = 26;
            this.check_aEx.Text = "Исполнение";
            this.check_aEx.UseVisualStyleBackColor = true;
            // 
            // check_uEx
            // 
            this.check_uEx.AutoSize = true;
            this.check_uEx.Location = new System.Drawing.Point(3, 62);
            this.check_uEx.Name = "check_uEx";
            this.check_uEx.Size = new System.Drawing.Size(88, 17);
            this.check_uEx.TabIndex = 27;
            this.check_uEx.Text = "Исполнение";
            this.check_uEx.UseVisualStyleBackColor = true;
            // 
            // check_uWrite
            // 
            this.check_uWrite.AutoSize = true;
            this.check_uWrite.Location = new System.Drawing.Point(3, 16);
            this.check_uWrite.Name = "check_uWrite";
            this.check_uWrite.Size = new System.Drawing.Size(63, 17);
            this.check_uWrite.TabIndex = 28;
            this.check_uWrite.Text = "Запись";
            this.check_uWrite.UseVisualStyleBackColor = true;
            // 
            // lay_uRights
            // 
            this.lay_uRights.Controls.Add(this.label3);
            this.lay_uRights.Controls.Add(this.check_uWrite);
            this.lay_uRights.Controls.Add(this.check_uRead);
            this.lay_uRights.Controls.Add(this.check_uEx);
            this.lay_uRights.Location = new System.Drawing.Point(3, 3);
            this.lay_uRights.Name = "lay_uRights";
            this.lay_uRights.Size = new System.Drawing.Size(86, 83);
            this.lay_uRights.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Пользователь";
            // 
            // lay_gRights
            // 
            this.lay_gRights.Controls.Add(this.lbl_group);
            this.lay_gRights.Controls.Add(this.check_gWrite);
            this.lay_gRights.Controls.Add(this.check_gRead);
            this.lay_gRights.Controls.Add(this.check_gEx);
            this.lay_gRights.Location = new System.Drawing.Point(3, 92);
            this.lay_gRights.Name = "lay_gRights";
            this.lay_gRights.Size = new System.Drawing.Size(87, 81);
            this.lay_gRights.TabIndex = 30;
            // 
            // lbl_group
            // 
            this.lbl_group.AutoSize = true;
            this.lbl_group.Location = new System.Drawing.Point(3, 0);
            this.lbl_group.Name = "lbl_group";
            this.lbl_group.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_group.Size = new System.Drawing.Size(42, 13);
            this.lbl_group.TabIndex = 31;
            this.lbl_group.Text = "Группа";
            // 
            // lbl_all
            // 
            this.lbl_all.AutoSize = true;
            this.lbl_all.Location = new System.Drawing.Point(3, 0);
            this.lbl_all.Name = "lbl_all";
            this.lbl_all.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_all.Size = new System.Drawing.Size(26, 13);
            this.lbl_all.TabIndex = 32;
            this.lbl_all.Text = "Все";
            // 
            // lay_aRights
            // 
            this.lay_aRights.Controls.Add(this.lbl_all);
            this.lay_aRights.Controls.Add(this.check_aWrite);
            this.lay_aRights.Controls.Add(this.check_aRead);
            this.lay_aRights.Controls.Add(this.check_aEx);
            this.lay_aRights.Location = new System.Drawing.Point(95, 3);
            this.lay_aRights.Name = "lay_aRights";
            this.lay_aRights.Size = new System.Drawing.Size(93, 80);
            this.lay_aRights.TabIndex = 33;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lay_uRights);
            this.flowLayoutPanel1.Controls.Add(this.lay_aRights);
            this.flowLayoutPanel1.Controls.Add(this.lay_gRights);
            this.flowLayoutPanel1.Controls.Add(this.lay_flags);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(122, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(194, 177);
            this.flowLayoutPanel1.TabIndex = 34;
            // 
            // lay_fileAttribs
            // 
            this.lay_fileAttribs.Controls.Add(this.lay_name);
            this.lay_fileAttribs.Controls.Add(this.flowLayoutPanel1);
            this.lay_fileAttribs.Location = new System.Drawing.Point(12, 12);
            this.lay_fileAttribs.Name = "lay_fileAttribs";
            this.lay_fileAttribs.Size = new System.Drawing.Size(320, 185);
            this.lay_fileAttribs.TabIndex = 35;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.lbl_size);
            this.flowLayoutPanel2.Controls.Add(this.num_size);
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Controls.Add(this.btn_openFile);
            this.flowLayoutPanel2.Controls.Add(this.btn_reset);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 137);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(194, 54);
            this.flowLayoutPanel2.TabIndex = 36;
            // 
            // rtb_data
            // 
            this.rtb_data.Location = new System.Drawing.Point(3, 16);
            this.rtb_data.Name = "rtb_data";
            this.rtb_data.Size = new System.Drawing.Size(148, 96);
            this.rtb_data.TabIndex = 38;
            this.rtb_data.Text = "";
            // 
            // lay_data
            // 
            this.lay_data.Controls.Add(this.lbl_data);
            this.lay_data.Controls.Add(this.rtb_data);
            this.lay_data.Location = new System.Drawing.Point(3, 3);
            this.lay_data.Name = "lay_data";
            this.lay_data.Size = new System.Drawing.Size(187, 128);
            this.lay_data.TabIndex = 39;
            // 
            // lbl_data
            // 
            this.lbl_data.AutoSize = true;
            this.lbl_data.Location = new System.Drawing.Point(3, 0);
            this.lbl_data.Name = "lbl_data";
            this.lbl_data.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_data.Size = new System.Drawing.Size(48, 13);
            this.lbl_data.TabIndex = 40;
            this.lbl_data.Text = "Данные";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.lay_data);
            this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(338, 12);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(197, 197);
            this.flowLayoutPanel3.TabIndex = 40;
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(98, 29);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(89, 23);
            this.btn_reset.TabIndex = 16;
            this.btn_reset.Text = "Сбросить";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // AddFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 234);
            this.Controls.Add(this.lay_fileAttribs);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_add);
            this.Name = "AddFile";
            this.Text = "AddFile";
            ((System.ComponentModel.ISupportInitialize)(this.num_size)).EndInit();
            this.lay_flags.ResumeLayout(false);
            this.lay_flags.PerformLayout();
            this.lay_name.ResumeLayout(false);
            this.lay_name.PerformLayout();
            this.lay_uRights.ResumeLayout(false);
            this.lay_uRights.PerformLayout();
            this.lay_gRights.ResumeLayout(false);
            this.lay_gRights.PerformLayout();
            this.lay_aRights.ResumeLayout(false);
            this.lay_aRights.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.lay_fileAttribs.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.lay_data.ResumeLayout(false);
            this.lay_data.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_filename;
        private System.Windows.Forms.TextBox tb_fullpath;
        private System.Windows.Forms.CheckBox chb_system;
        private System.Windows.Forms.CheckBox chb_hidden;
        private System.Windows.Forms.CheckBox chb_dir;
        private System.Windows.Forms.NumericUpDown num_size;
        private System.Windows.Forms.Label lbl_size;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.OpenFileDialog ofd_addFile;
        private System.Windows.Forms.Button btn_openFile;
        private System.Windows.Forms.Label lbl_fullName;
        private System.Windows.Forms.TextBox tb_name;
        private System.Windows.Forms.CheckBox check_uRead;
        private System.Windows.Forms.FlowLayoutPanel lay_flags;
        private System.Windows.Forms.Label lbl_flags;
        private System.Windows.Forms.FlowLayoutPanel lay_name;
        private System.Windows.Forms.CheckBox check_aWrite;
        private System.Windows.Forms.CheckBox check_gRead;
        private System.Windows.Forms.CheckBox check_gWrite;
        private System.Windows.Forms.CheckBox check_gEx;
        private System.Windows.Forms.CheckBox check_aRead;
        private System.Windows.Forms.CheckBox check_aEx;
        private System.Windows.Forms.CheckBox check_uEx;
        private System.Windows.Forms.CheckBox check_uWrite;
        private System.Windows.Forms.FlowLayoutPanel lay_uRights;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel lay_gRights;
        private System.Windows.Forms.Label lbl_group;
        private System.Windows.Forms.Label lbl_all;
        private System.Windows.Forms.FlowLayoutPanel lay_aRights;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel lay_fileAttribs;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.RichTextBox rtb_data;
        private System.Windows.Forms.FlowLayoutPanel lay_data;
        private System.Windows.Forms.Label lbl_data;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button btn_reset;
    }
}