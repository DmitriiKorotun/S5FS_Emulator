namespace OSWPF1
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btn_exit = new System.Windows.Forms.Button();
            this.tv_dirView = new System.Windows.Forms.TreeView();
            this.btn_update = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.фСToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delFSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeRightsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.banUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.группыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_getProps = new System.Windows.Forms.Button();
            this.lbl_currUser = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.изменитьФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(12, 226);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(65, 23);
            this.btn_exit.TabIndex = 3;
            this.btn_exit.Text = "Выйти";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // tv_dirView
            // 
            this.tv_dirView.HideSelection = false;
            this.tv_dirView.Location = new System.Drawing.Point(12, 27);
            this.tv_dirView.Name = "tv_dirView";
            this.tv_dirView.Size = new System.Drawing.Size(227, 193);
            this.tv_dirView.TabIndex = 7;
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(94, 226);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(65, 23);
            this.btn_update.TabIndex = 8;
            this.btn_update.Text = "Обновить";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.фСToolStripMenuItem,
            this.fileToolStripMenuItem,
            this.usersToolStripMenuItem,
            this.группыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(253, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // фСToolStripMenuItem
            // 
            this.фСToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFSToolStripMenuItem,
            this.delFSToolStripMenuItem});
            this.фСToolStripMenuItem.Name = "фСToolStripMenuItem";
            this.фСToolStripMenuItem.Size = new System.Drawing.Size(33, 20);
            this.фСToolStripMenuItem.Text = "ФС";
            // 
            // createFSToolStripMenuItem
            // 
            this.createFSToolStripMenuItem.Name = "createFSToolStripMenuItem";
            this.createFSToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.createFSToolStripMenuItem.Text = "Создать ФС";
            this.createFSToolStripMenuItem.Click += new System.EventHandler(this.createFSToolStripMenuItem_Click);
            // 
            // delFSToolStripMenuItem
            // 
            this.delFSToolStripMenuItem.Name = "delFSToolStripMenuItem";
            this.delFSToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.delFSToolStripMenuItem.Text = "Удалить ФС";
            this.delFSToolStripMenuItem.Click += new System.EventHandler(this.delFSToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFileToolStripMenuItem,
            this.delFileToolStripMenuItem,
            this.getFileToolStripMenuItem,
            this.changeRightsToolStripMenuItem,
            this.изменитьФайлToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // addFileToolStripMenuItem
            // 
            this.addFileToolStripMenuItem.Name = "addFileToolStripMenuItem";
            this.addFileToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.addFileToolStripMenuItem.Text = "Добавить файл";
            this.addFileToolStripMenuItem.Click += new System.EventHandler(this.addFileToolStripMenuItem_Click);
            // 
            // delFileToolStripMenuItem
            // 
            this.delFileToolStripMenuItem.Name = "delFileToolStripMenuItem";
            this.delFileToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.delFileToolStripMenuItem.Text = "Удалить файл";
            this.delFileToolStripMenuItem.Click += new System.EventHandler(this.delFileToolStripMenuItem_Click);
            // 
            // getFileToolStripMenuItem
            // 
            this.getFileToolStripMenuItem.Name = "getFileToolStripMenuItem";
            this.getFileToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.getFileToolStripMenuItem.Text = "Получить файл";
            this.getFileToolStripMenuItem.Click += new System.EventHandler(this.getFileToolStripMenuItem_Click);
            // 
            // changeRightsToolStripMenuItem
            // 
            this.changeRightsToolStripMenuItem.Name = "changeRightsToolStripMenuItem";
            this.changeRightsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.changeRightsToolStripMenuItem.Text = "Изменить права";
            this.changeRightsToolStripMenuItem.Click += new System.EventHandler(this.changeRightsToolStripMenuItem_Click);
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addUserToolStripMenuItem,
            this.banUserToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.usersToolStripMenuItem.Text = "Пользователи";
            // 
            // addUserToolStripMenuItem
            // 
            this.addUserToolStripMenuItem.Name = "addUserToolStripMenuItem";
            this.addUserToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.addUserToolStripMenuItem.Text = "Добавить пользователя";
            this.addUserToolStripMenuItem.Click += new System.EventHandler(this.addUserToolStripMenuItem_Click);
            // 
            // banUserToolStripMenuItem
            // 
            this.banUserToolStripMenuItem.Name = "banUserToolStripMenuItem";
            this.banUserToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.banUserToolStripMenuItem.Text = "Заблокировать пользователя";
            this.banUserToolStripMenuItem.Click += new System.EventHandler(this.banUserToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.logoutToolStripMenuItem.Text = "Выйти из учётной записи";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // группыToolStripMenuItem
            // 
            this.группыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddGroupToolStripMenuItem});
            this.группыToolStripMenuItem.Name = "группыToolStripMenuItem";
            this.группыToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.группыToolStripMenuItem.Text = "Группы";
            // 
            // AddGroupToolStripMenuItem
            // 
            this.AddGroupToolStripMenuItem.Name = "AddGroupToolStripMenuItem";
            this.AddGroupToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.AddGroupToolStripMenuItem.Text = "Добавить группу";
            this.AddGroupToolStripMenuItem.Click += new System.EventHandler(this.AddGroupToolStripMenuItem_Click);
            // 
            // btn_getProps
            // 
            this.btn_getProps.Location = new System.Drawing.Point(174, 226);
            this.btn_getProps.Name = "btn_getProps";
            this.btn_getProps.Size = new System.Drawing.Size(65, 23);
            this.btn_getProps.TabIndex = 11;
            this.btn_getProps.Text = "Свойства";
            this.btn_getProps.UseVisualStyleBackColor = true;
            this.btn_getProps.Click += new System.EventHandler(this.btn_getProps_Click);
            // 
            // lbl_currUser
            // 
            this.lbl_currUser.AutoSize = true;
            this.lbl_currUser.Location = new System.Drawing.Point(12, 254);
            this.lbl_currUser.Name = "lbl_currUser";
            this.lbl_currUser.Size = new System.Drawing.Size(132, 13);
            this.lbl_currUser.TabIndex = 12;
            this.lbl_currUser.Text = "Текущий пользователь: ";
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(149, 254);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(10, 13);
            this.lbl_name.TabIndex = 13;
            this.lbl_name.Text = " ";
            // 
            // изменитьФайлToolStripMenuItem
            // 
            this.изменитьФайлToolStripMenuItem.Name = "изменитьФайлToolStripMenuItem";
            this.изменитьФайлToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.изменитьФайлToolStripMenuItem.Text = "Изменить файл";
            this.изменитьФайлToolStripMenuItem.Click += new System.EventHandler(this.изменитьФайлToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 273);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.lbl_currUser);
            this.Controls.Add(this.btn_getProps);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.tv_dirView);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.TreeView tv_dirView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem фСToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createFSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delFSToolStripMenuItem;
        private System.Windows.Forms.Button btn_getProps;
        private System.Windows.Forms.ToolStripMenuItem addUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem banUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem группыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddGroupToolStripMenuItem;
        private System.Windows.Forms.Label lbl_currUser;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.ToolStripMenuItem getFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeRightsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьФайлToolStripMenuItem;
    }
}

