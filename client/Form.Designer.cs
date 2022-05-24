namespace client
{
    partial class client
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rtbMesssages = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chatSelector = new System.Windows.Forms.TreeView();
            this.chatContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joinChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messageChecker = new System.Windows.Forms.Timer(this.components);
            this.rtbMessageBox = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.chatContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::client.Properties.Resources.logo1;
            this.pictureBox1.Location = new System.Drawing.Point(342, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(822, 117);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // rtbMesssages
            // 
            this.rtbMesssages.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMesssages.Location = new System.Drawing.Point(340, 128);
            this.rtbMesssages.Name = "rtbMesssages";
            this.rtbMesssages.ReadOnly = true;
            this.rtbMesssages.Size = new System.Drawing.Size(823, 348);
            this.rtbMesssages.TabIndex = 2;
            this.rtbMesssages.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chatSelector);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 577);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chats";
            // 
            // chatSelector
            // 
            this.chatSelector.BackColor = System.Drawing.SystemColors.Menu;
            this.chatSelector.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatSelector.ContextMenuStrip = this.chatContextMenu;
            this.chatSelector.Location = new System.Drawing.Point(6, 27);
            this.chatSelector.Name = "chatSelector";
            this.chatSelector.Size = new System.Drawing.Size(310, 544);
            this.chatSelector.TabIndex = 2;
            this.chatSelector.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.chatSelector_AfterSelect);
            // 
            // chatContextMenu
            // 
            this.chatContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addChatToolStripMenuItem,
            this.joinChatToolStripMenuItem});
            this.chatContextMenu.Name = "chatContextMenu";
            this.chatContextMenu.Size = new System.Drawing.Size(125, 48);
            // 
            // addChatToolStripMenuItem
            // 
            this.addChatToolStripMenuItem.Name = "addChatToolStripMenuItem";
            this.addChatToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.addChatToolStripMenuItem.Text = "Add Chat";
            this.addChatToolStripMenuItem.Click += new System.EventHandler(this.addChatToolStripMenuItem_Click);
            // 
            // joinChatToolStripMenuItem
            // 
            this.joinChatToolStripMenuItem.Name = "joinChatToolStripMenuItem";
            this.joinChatToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.joinChatToolStripMenuItem.Text = "Join Chat";
            this.joinChatToolStripMenuItem.Click += new System.EventHandler(this.joinChatToolStripMenuItem_Click);
            // 
            // messageChecker
            // 
            this.messageChecker.Interval = 3000;
            this.messageChecker.Tick += new System.EventHandler(this.checkMessages);
            // 
            // rtbMessageBox
            // 
            this.rtbMessageBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMessageBox.Location = new System.Drawing.Point(340, 482);
            this.rtbMessageBox.Name = "rtbMessageBox";
            this.rtbMessageBox.Size = new System.Drawing.Size(707, 107);
            this.rtbMessageBox.TabIndex = 4;
            this.rtbMessageBox.Text = "";
            this.rtbMessageBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbMessageBox_KeyDown);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1053, 482);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 107);
            this.button1.TabIndex = 5;
            this.button1.Text = "Send message";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.sendMessage);
            // 
            // client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::client.Properties.Resources.bacground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1174, 601);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rtbMessageBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rtbMesssages);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1190, 640);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1190, 640);
            this.Name = "client";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.client_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.chatContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox rtbMesssages;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView chatSelector;
        private System.Windows.Forms.ContextMenuStrip chatContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addChatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem joinChatToolStripMenuItem;
        private System.Windows.Forms.Timer messageChecker;
        private System.Windows.Forms.RichTextBox rtbMessageBox;
        private System.Windows.Forms.Button button1;
    }
}

