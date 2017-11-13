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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btn_makeFile = new System.Windows.Forms.Button();
            this.btn_addFile = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_addUser = new System.Windows.Forms.Button();
            this.btn_addGroup = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(172, 181);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // btn_makeFile
            // 
            this.btn_makeFile.Location = new System.Drawing.Point(12, 12);
            this.btn_makeFile.Name = "btn_makeFile";
            this.btn_makeFile.Size = new System.Drawing.Size(100, 23);
            this.btn_makeFile.TabIndex = 1;
            this.btn_makeFile.Text = "Создать ФС";
            this.btn_makeFile.UseVisualStyleBackColor = true;
            this.btn_makeFile.Click += new System.EventHandler(this.btn_makeFile_Click);
            // 
            // btn_addFile
            // 
            this.btn_addFile.Location = new System.Drawing.Point(172, 12);
            this.btn_addFile.Name = "btn_addFile";
            this.btn_addFile.Size = new System.Drawing.Size(100, 23);
            this.btn_addFile.TabIndex = 2;
            this.btn_addFile.Text = "Добавить файл";
            this.btn_addFile.UseVisualStyleBackColor = true;
            this.btn_addFile.Click += new System.EventHandler(this.button1_Click);
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
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(172, 207);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Удалить файл";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 104);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(121, 97);
            this.treeView1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btn_addGroup);
            this.Controls.Add(this.btn_addUser);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_addFile);
            this.Controls.Add(this.btn_makeFile);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btn_makeFile;
        private System.Windows.Forms.Button btn_addFile;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_addUser;
        private System.Windows.Forms.Button btn_addGroup;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TreeView treeView1;
    }
}

