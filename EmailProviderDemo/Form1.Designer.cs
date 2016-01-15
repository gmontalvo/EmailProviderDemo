namespace EmailProviderDemo
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this._message = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this._subject = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._to = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._from = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._sendProviders = new System.Windows.Forms.ComboBox();
            this._send = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._metrics = new System.Windows.Forms.ListView();
            this._load = new System.Windows.Forms.Button();
            this._statsProviders = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(560, 537);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this._message);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this._subject);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this._to);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this._from);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this._sendProviders);
            this.tabPage1.Controls.Add(this._send);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(552, 511);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Send";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // _message
            // 
            this._message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._message.Location = new System.Drawing.Point(6, 142);
            this._message.Multiline = true;
            this._message.Name = "_message";
            this._message.Size = new System.Drawing.Size(540, 334);
            this._message.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Message";
            // 
            // _subject
            // 
            this._subject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._subject.Location = new System.Drawing.Point(59, 85);
            this._subject.Name = "_subject";
            this._subject.Size = new System.Drawing.Size(487, 20);
            this._subject.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Subject";
            // 
            // _to
            // 
            this._to.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._to.Location = new System.Drawing.Point(59, 59);
            this._to.Name = "_to";
            this._to.Size = new System.Drawing.Size(487, 20);
            this._to.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "To";
            // 
            // _from
            // 
            this._from.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._from.Location = new System.Drawing.Point(59, 33);
            this._from.Name = "_from";
            this._from.Size = new System.Drawing.Size(487, 20);
            this._from.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "From";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Provider";
            // 
            // _sendProviders
            // 
            this._sendProviders.FormattingEnabled = true;
            this._sendProviders.Location = new System.Drawing.Point(59, 6);
            this._sendProviders.Name = "_sendProviders";
            this._sendProviders.Size = new System.Drawing.Size(138, 21);
            this._sendProviders.TabIndex = 1;
            // 
            // _send
            // 
            this._send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._send.Location = new System.Drawing.Point(477, 482);
            this._send.Name = "_send";
            this._send.Size = new System.Drawing.Size(75, 23);
            this._send.TabIndex = 10;
            this._send.Text = "&Send";
            this._send.UseVisualStyleBackColor = true;
            this._send.Click += new System.EventHandler(this._send_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this._metrics);
            this.tabPage2.Controls.Add(this._load);
            this.tabPage2.Controls.Add(this._statsProviders);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(552, 511);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Statistics";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _metrics
            // 
            this._metrics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._metrics.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this._metrics.FullRowSelect = true;
            this._metrics.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this._metrics.Location = new System.Drawing.Point(10, 35);
            this._metrics.Name = "_metrics";
            this._metrics.Size = new System.Drawing.Size(536, 441);
            this._metrics.TabIndex = 12;
            this._metrics.UseCompatibleStateImageBehavior = false;
            // 
            // _load
            // 
            this._load.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._load.Location = new System.Drawing.Point(471, 482);
            this._load.Name = "_load";
            this._load.Size = new System.Drawing.Size(75, 23);
            this._load.TabIndex = 11;
            this._load.Text = "&Load";
            this._load.UseVisualStyleBackColor = true;
            this._load.Click += new System.EventHandler(this._load_Click);
            // 
            // _statsProviders
            // 
            this._statsProviders.FormattingEnabled = true;
            this._statsProviders.Location = new System.Drawing.Point(59, 6);
            this._statsProviders.Name = "_statsProviders";
            this._statsProviders.Size = new System.Drawing.Size(138, 21);
            this._statsProviders.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Provider";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Bounces";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Clicks";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Opens";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Sends";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Unsubscribes";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "EmailProviderDemo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button _send;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox _sendProviders;
        private System.Windows.Forms.TextBox _from;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _message;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _subject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox _to;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox _statsProviders;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button _load;
        private System.Windows.Forms.ListView _metrics;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;

    }
}

