namespace OSWPF1
{
    partial class Form1
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
            this.btn_addUser = new System.Windows.Forms.Button();
            this.btn_addGroup = new System.Windows.Forms.Button();
            this.tv_dirView = new System.Windows.Forms.TreeView();
            this.btn_update = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.фСToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delFSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пользователиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_getProps = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(12, 207);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(100, 23);
            this.btn_exit.TabIndex = 3;
            this.btn_exit.Text = "Выйти";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_addUser
            // 
            this.btn_addUser.Location = new System.Drawing.Point(172, 41);
            this.btn_addUser.Name = "btn_addUser";
            this.btn_addUser.Size = new System.Drawing.Size(100, 42);
            this.btn_addUser.TabIndex = 4;
            this.btn_addUser.Text = "Добавить пользователя";
            this.btn_addUser.UseVisualStyleBackColor = true;
            // 
            // btn_addGroup
            // 
            this.btn_addGroup.Location = new System.Drawing.Point(12, 41);
            this.btn_addGroup.Name = "btn_addGroup";
            this.btn_addGroup.Size = new System.Drawing.Size(100, 42);
            this.btn_addGroup.TabIndex = 5;
            this.btn_addGroup.Text = "Добавить группу";
            this.btn_addGroup.UseVisualStyleBackColor = true;
            // 
            // tv_dirView
            // 
            this.tv_dirView.HideSelection = false;
            this.tv_dirView.Location = new System.Drawing.Point(278, 12);
            this.tv_dirView.Name = "tv_dirView";
            this.tv_dirView.Size = new System.Drawing.Size(373, 237);
            this.tv_dirView.TabIndex = 7;
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(12, 89);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(75, 23);
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
            this.пользователиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(705, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // фСToolStripMenuItem
            // 
            this.фСToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFSToolStripMenuItem,
            this.delFSToolStripMenuItem});
            this.фСToolStripMenuItem.Name = "фСToolStripMenuItem";
            this.фСToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.фСToolStripMenuItem.Text = "ФС";
            // 
            // createFSToolStripMenuItem
            // 
            this.createFSToolStripMenuItem.Name = "createFSToolStripMenuItem";
            this.createFSToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.createFSToolStripMenuItem.Text = "Создать ФС";
            this.createFSToolStripMenuItem.Click += new System.EventHandler(this.createFSToolStripMenuItem_Click);
            // 
            // delFSToolStripMenuItem
            // 
            this.delFSToolStripMenuItem.Name = "delFSToolStripMenuItem";
            this.delFSToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.delFSToolStripMenuItem.Text = "Удалить ФС";
            this.delFSToolStripMenuItem.Click += new System.EventHandler(this.delFSToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFileToolStripMenuItem,
            this.delFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // addFileToolStripMenuItem
            // 
            this.addFileToolStripMenuItem.Name = "addFileToolStripMenuItem";
            this.addFileToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.addFileToolStripMenuItem.Text = "Добавить файл";
            this.addFileToolStripMenuItem.Click += new System.EventHandler(this.addFileToolStripMenuItem_Click);
            // 
            // delFileToolStripMenuItem
            // 
            this.delFileToolStripMenuItem.Name = "delFileToolStripMenuItem";
            this.delFileToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.delFileToolStripMenuItem.Text = "Удалить файл";
            // 
            // пользователиToolStripMenuItem
            // 
            this.пользователиToolStripMenuItem.Name = "пользователиToolStripMenuItem";
            this.пользователиToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.пользователиToolStripMenuItem.Text = "Пользователи";
            // 
            // btn_getProps
            // 
            this.btn_getProps.Location = new System.Drawing.Point(146, 89);
            this.btn_getProps.Name = "btn_getProps";
            this.btn_getProps.Size = new System.Drawing.Size(126, 23);
            this.btn_getProps.TabIndex = 11;
            this.btn_getProps.Text = "Получить свойства";
            this.btn_getProps.UseVisualStyleBackColor = true;
            this.btn_getProps.Click += new System.EventHandler(this.btn_getProps_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 261);
            this.Controls.Add(this.btn_getProps);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.tv_dirView);
            this.Controls.Add(this.btn_addGroup);
            this.Controls.Add(this.btn_addUser);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_addUser;
        private System.Windows.Forms.Button btn_addGroup;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.TreeView tv_dirView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пользователиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem фСToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createFSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delFSToolStripMenuItem;
        private System.Windows.Forms.Button btn_getProps;
    }
}

