namespace MultiRoomChatClient
{
    partial class AdminForm : SuperDuperChat
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
            this.button1 = new System.Windows.Forms.Button();
            this.btn_banForever = new System.Windows.Forms.Button();
            this.dateTime = new System.Windows.Forms.DateTimePicker();
            this.button3 = new System.Windows.Forms.Button();
            this.tb_selectedUser = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(506, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Unban";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Unban_Click);
            // 
            // btn_banForever
            // 
            this.btn_banForever.Location = new System.Drawing.Point(506, 68);
            this.btn_banForever.Name = "btn_banForever";
            this.btn_banForever.Size = new System.Drawing.Size(105, 23);
            this.btn_banForever.TabIndex = 8;
            this.btn_banForever.Text = "Ban Forever";
            this.btn_banForever.UseVisualStyleBackColor = true;
            this.btn_banForever.Click += new System.EventHandler(this.btn_banForever_Click);
            // 
            // dateTime
            // 
            this.dateTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTime.Location = new System.Drawing.Point(507, 126);
            this.dateTime.Name = "dateTime";
            this.dateTime.Size = new System.Drawing.Size(104, 20);
            this.dateTime.TabIndex = 9;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(506, 97);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Ban Till";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tb_selectedUser
            // 
            this.tb_selectedUser.Enabled = false;
            this.tb_selectedUser.Location = new System.Drawing.Point(506, 13);
            this.tb_selectedUser.Name = "tb_selectedUser";
            this.tb_selectedUser.Size = new System.Drawing.Size(104, 20);
            this.tb_selectedUser.TabIndex = 11;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 290);
            this.Controls.Add(this.tb_selectedUser);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dateTime);
            this.Controls.Add(this.btn_banForever);
            this.Controls.Add(this.button1);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.btn_banForever, 0);
            this.Controls.SetChildIndex(this.dateTime, 0);
            this.Controls.SetChildIndex(this.button3, 0);
            this.Controls.SetChildIndex(this.tb_selectedUser, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_banForever;
        private System.Windows.Forms.DateTimePicker dateTime;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tb_selectedUser;
    }
}