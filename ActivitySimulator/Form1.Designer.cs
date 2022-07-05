namespace ActivitySimulator
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.lbxLog = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPeriod = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.txtBuMac = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.timeLinePick = new ActivitySimulator.TimeLineCtrl();
            this.timeLineCtrl4 = new ActivitySimulator.TimeLineCtrl();
            this.timeLineCtrl3 = new ActivitySimulator.TimeLineCtrl();
            this.timeLineCtrl2 = new ActivitySimulator.TimeLineCtrl();
            this.timeLineCtrl5 = new ActivitySimulator.TimeLineCtrl();
            this.timeLineCtrl1 = new ActivitySimulator.TimeLineCtrl();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(202, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "Server IP";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(235, 16);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(55, 21);
            this.txtPort.TabIndex = 16;
            this.txtPort.Text = "46000";
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(93, 16);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(100, 21);
            this.txtAddress.TabIndex = 15;
            this.txtAddress.Text = "127.0.0.1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(466, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "시작";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(547, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "중지";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(619, 45);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 23;
            this.button3.Text = "전송";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(790, 47);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 24;
            this.button4.Text = "Serialize";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(700, 45);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(84, 23);
            this.button5.TabIndex = 25;
            this.button5.Text = "DeSerialize";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(538, 45);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 31;
            this.button6.Text = "LifeLog 추가";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Visible = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // lbxLog
            // 
            this.lbxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxLog.FormattingEnabled = true;
            this.lbxLog.ItemHeight = 12;
            this.lbxLog.Location = new System.Drawing.Point(10, 412);
            this.lbxLog.Name = "lbxLog";
            this.lbxLog.Size = new System.Drawing.Size(1107, 436);
            this.lbxLog.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(315, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 36;
            this.label2.Text = "전송 주기";
            // 
            // txtPeriod
            // 
            this.txtPeriod.Location = new System.Drawing.Point(375, 15);
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.Size = new System.Drawing.Size(55, 21);
            this.txtPeriod.TabIndex = 35;
            this.txtPeriod.Text = "30";
            this.txtPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(436, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 37;
            this.label5.Text = "초";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(1042, 16);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 38;
            this.button7.Text = "단건 전송";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // txtBuMac
            // 
            this.txtBuMac.Location = new System.Drawing.Point(936, 17);
            this.txtBuMac.Name = "txtBuMac";
            this.txtBuMac.Size = new System.Drawing.Size(100, 21);
            this.txtBuMac.TabIndex = 39;
            this.txtBuMac.Text = "60120912456b";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(839, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 12);
            this.label1.TabIndex = 40;
            this.label1.Text = "게이트웨이 Mac";
            // 
            // cbYear
            // 
            this.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Location = new System.Drawing.Point(712, 17);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(121, 20);
            this.cbYear.TabIndex = 41;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(644, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 12);
            this.label6.TabIndex = 42;
            this.label6.Text = "Body 년도";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // timeLinePick
            // 
            this.timeLinePick.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLinePick.BackColor = System.Drawing.SystemColors.HighlightText;
            this.timeLinePick.Location = new System.Drawing.Point(10, 353);
            this.timeLinePick.Name = "timeLinePick";
            this.timeLinePick.Size = new System.Drawing.Size(1107, 53);
            this.timeLinePick.TabIndex = 33;
            // 
            // timeLineCtrl4
            // 
            this.timeLineCtrl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLineCtrl4.BackColor = System.Drawing.SystemColors.HighlightText;
            this.timeLineCtrl4.Location = new System.Drawing.Point(10, 235);
            this.timeLineCtrl4.Name = "timeLineCtrl4";
            this.timeLineCtrl4.Size = new System.Drawing.Size(1107, 53);
            this.timeLineCtrl4.TabIndex = 30;
            // 
            // timeLineCtrl3
            // 
            this.timeLineCtrl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLineCtrl3.BackColor = System.Drawing.SystemColors.HighlightText;
            this.timeLineCtrl3.Location = new System.Drawing.Point(10, 176);
            this.timeLineCtrl3.Name = "timeLineCtrl3";
            this.timeLineCtrl3.Size = new System.Drawing.Size(1107, 53);
            this.timeLineCtrl3.TabIndex = 29;
            // 
            // timeLineCtrl2
            // 
            this.timeLineCtrl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLineCtrl2.BackColor = System.Drawing.SystemColors.HighlightText;
            this.timeLineCtrl2.Location = new System.Drawing.Point(10, 117);
            this.timeLineCtrl2.Name = "timeLineCtrl2";
            this.timeLineCtrl2.Size = new System.Drawing.Size(1107, 53);
            this.timeLineCtrl2.TabIndex = 28;
            // 
            // timeLineCtrl5
            // 
            this.timeLineCtrl5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLineCtrl5.BackColor = System.Drawing.SystemColors.HighlightText;
            this.timeLineCtrl5.Location = new System.Drawing.Point(10, 294);
            this.timeLineCtrl5.Name = "timeLineCtrl5";
            this.timeLineCtrl5.Size = new System.Drawing.Size(1107, 53);
            this.timeLineCtrl5.TabIndex = 27;
            // 
            // timeLineCtrl1
            // 
            this.timeLineCtrl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLineCtrl1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.timeLineCtrl1.Location = new System.Drawing.Point(10, 58);
            this.timeLineCtrl1.Name = "timeLineCtrl1";
            this.timeLineCtrl1.Size = new System.Drawing.Size(1107, 53);
            this.timeLineCtrl1.TabIndex = 26;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 855);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbYear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBuMac);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPeriod);
            this.Controls.Add(this.lbxLog);
            this.Controls.Add(this.timeLinePick);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.timeLineCtrl4);
            this.Controls.Add(this.timeLineCtrl3);
            this.Controls.Add(this.timeLineCtrl2);
            this.Controls.Add(this.timeLineCtrl5);
            this.Controls.Add(this.timeLineCtrl1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtAddress);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Activity Simulator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private TimeLineCtrl timeLineCtrl1;
        private TimeLineCtrl timeLineCtrl5;
        private TimeLineCtrl timeLineCtrl2;
        private TimeLineCtrl timeLineCtrl3;
        private TimeLineCtrl timeLineCtrl4;
        private System.Windows.Forms.Button button6;
        private TimeLineCtrl timeLinePick;
        private System.Windows.Forms.ListBox lbxLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPeriod;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox txtBuMac;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.Label label6;
    }
}

