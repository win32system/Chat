namespace MultiRoomChatClient
{
    partial class MessageListBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox_messages = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBox_messages
            // 
            this.listBox_messages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_messages.FormattingEnabled = true;
            this.listBox_messages.Location = new System.Drawing.Point(0, 0);
            this.listBox_messages.Name = "listBox_messages";
            this.listBox_messages.Size = new System.Drawing.Size(150, 150);
            this.listBox_messages.TabIndex = 0;
            // 
            // MessageListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBox_messages);
            this.Name = "MessageListBox";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_messages;
    }
}
