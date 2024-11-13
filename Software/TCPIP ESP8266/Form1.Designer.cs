
namespace TCPIP_ESP8266
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.button_Disconnect = new System.Windows.Forms.Button();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_ipAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_connect = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxMinS = new System.Windows.Forms.TextBox();
            this.pictureBoxLamp = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxAbout = new System.Windows.Forms.TextBox();
            this.textBoxHour = new System.Windows.Forms.TextBox();
            this.button_led2Off = new System.Windows.Forms.Button();
            this.button_led2On = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_sendGMT = new System.Windows.Forms.Button();
            this.pictureBoxBell = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.textBoxSec = new System.Windows.Forms.TextBox();
            this.textBoxMin = new System.Windows.Forms.TextBox();
            this.textBoxHour2 = new System.Windows.Forms.TextBox();
            this.labelClock = new System.Windows.Forms.Label();
            this.button_led1Off = new System.Windows.Forms.Button();
            this.button_led1On = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.labelH = new System.Windows.Forms.Label();
            this.labelT = new System.Windows.Forms.Label();
            this.textBoxHumi = new System.Windows.Forms.TextBox();
            this.textBoxTemp = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLamp)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBell)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxStatus);
            this.groupBox1.Controls.Add(this.button_Disconnect);
            this.groupBox1.Controls.Add(this.textBox_port);
            this.groupBox1.Controls.Add(this.textBox_ipAddress);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button_connect);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 159);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Location = new System.Drawing.Point(83, 81);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.Size = new System.Drawing.Size(203, 22);
            this.textBoxStatus.TabIndex = 27;
            this.textBoxStatus.Text = "Unknown";
            this.textBoxStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_Disconnect
            // 
            this.button_Disconnect.Location = new System.Drawing.Point(66, 111);
            this.button_Disconnect.Margin = new System.Windows.Forms.Padding(4);
            this.button_Disconnect.Name = "button_Disconnect";
            this.button_Disconnect.Size = new System.Drawing.Size(113, 32);
            this.button_Disconnect.TabIndex = 26;
            this.button_Disconnect.Text = "Disconnect";
            this.button_Disconnect.UseVisualStyleBackColor = true;
            this.button_Disconnect.Click += new System.EventHandler(this.button_Disconnect_Click);
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(113, 52);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(153, 22);
            this.textBox_port.TabIndex = 21;
            this.textBox_port.Text = "8080";
            // 
            // textBox_ipAddress
            // 
            this.textBox_ipAddress.Location = new System.Drawing.Point(113, 22);
            this.textBox_ipAddress.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_ipAddress.Name = "textBox_ipAddress";
            this.textBox_ipAddress.Size = new System.Drawing.Size(153, 22);
            this.textBox_ipAddress.TabIndex = 17;
            this.textBox_ipAddress.Text = "192.168.51.253";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 55);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 16);
            this.label5.TabIndex = 20;
            this.label5.Text = "Port :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "IP Address :";
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(187, 111);
            this.button_connect.Margin = new System.Windows.Forms.Padding(4);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(116, 32);
            this.button_connect.TabIndex = 18;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.textBoxMinS);
            this.groupBox4.Controls.Add(this.pictureBoxLamp);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.textBoxAbout);
            this.groupBox4.Controls.Add(this.textBoxHour);
            this.groupBox4.Controls.Add(this.button_led2Off);
            this.groupBox4.Controls.Add(this.button_led2On);
            this.groupBox4.Location = new System.Drawing.Point(342, 178);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(244, 150);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Lamp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 29;
            this.label2.Text = "Duration";
            // 
            // textBoxMinS
            // 
            this.textBoxMinS.Location = new System.Drawing.Point(89, 46);
            this.textBoxMinS.Name = "textBoxMinS";
            this.textBoxMinS.Size = new System.Drawing.Size(29, 22);
            this.textBoxMinS.TabIndex = 28;
            // 
            // pictureBoxLamp
            // 
            this.pictureBoxLamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxLamp.Image = global::TCPIP_ESP8266.Properties.Resources.off;
            this.pictureBoxLamp.Location = new System.Drawing.Point(134, 24);
            this.pictureBoxLamp.Name = "pictureBoxLamp";
            this.pictureBoxLamp.Size = new System.Drawing.Size(104, 107);
            this.pictureBoxLamp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLamp.TabIndex = 27;
            this.pictureBoxLamp.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 16);
            this.label8.TabIndex = 11;
            this.label8.Text = "Minute";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Hour_Start";
            // 
            // textBoxAbout
            // 
            this.textBoxAbout.Location = new System.Drawing.Point(88, 74);
            this.textBoxAbout.Name = "textBoxAbout";
            this.textBoxAbout.Size = new System.Drawing.Size(30, 22);
            this.textBoxAbout.TabIndex = 3;
            // 
            // textBoxHour
            // 
            this.textBoxHour.Location = new System.Drawing.Point(89, 18);
            this.textBoxHour.Name = "textBoxHour";
            this.textBoxHour.Size = new System.Drawing.Size(29, 22);
            this.textBoxHour.TabIndex = 2;
            // 
            // button_led2Off
            // 
            this.button_led2Off.Location = new System.Drawing.Point(64, 102);
            this.button_led2Off.Name = "button_led2Off";
            this.button_led2Off.Size = new System.Drawing.Size(54, 42);
            this.button_led2Off.TabIndex = 1;
            this.button_led2Off.Text = "OFF";
            this.button_led2Off.UseVisualStyleBackColor = true;
            this.button_led2Off.Click += new System.EventHandler(this.button_led2Off_Click);
            // 
            // button_led2On
            // 
            this.button_led2On.Location = new System.Drawing.Point(14, 102);
            this.button_led2On.Name = "button_led2On";
            this.button_led2On.Size = new System.Drawing.Size(46, 42);
            this.button_led2On.TabIndex = 0;
            this.button_led2On.Text = "ON";
            this.button_led2On.UseVisualStyleBackColor = true;
            this.button_led2On.Click += new System.EventHandler(this.button_led2On_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button_sendGMT);
            this.groupBox3.Controls.Add(this.pictureBoxBell);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.labelDate);
            this.groupBox3.Controls.Add(this.textBoxSec);
            this.groupBox3.Controls.Add(this.textBoxMin);
            this.groupBox3.Controls.Add(this.textBoxHour2);
            this.groupBox3.Controls.Add(this.labelClock);
            this.groupBox3.Controls.Add(this.button_led1Off);
            this.groupBox3.Controls.Add(this.button_led1On);
            this.groupBox3.Location = new System.Drawing.Point(342, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(244, 159);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Alarm";
            // 
            // button_sendGMT
            // 
            this.button_sendGMT.Location = new System.Drawing.Point(9, 123);
            this.button_sendGMT.Name = "button_sendGMT";
            this.button_sendGMT.Size = new System.Drawing.Size(97, 30);
            this.button_sendGMT.TabIndex = 27;
            this.button_sendGMT.Text = "SendGMT";
            this.button_sendGMT.UseVisualStyleBackColor = true;
            this.button_sendGMT.Click += new System.EventHandler(this.button_sendGMT_Click);
            // 
            // pictureBoxBell
            // 
            this.pictureBoxBell.Image = global::TCPIP_ESP8266.Properties.Resources.notification1;
            this.pictureBoxBell.Location = new System.Drawing.Point(160, 81);
            this.pictureBoxBell.Name = "pictureBoxBell";
            this.pictureBoxBell.Size = new System.Drawing.Size(78, 72);
            this.pictureBoxBell.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBell.TabIndex = 26;
            this.pictureBoxBell.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Second";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Minute";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Hour";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(131, 62);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(81, 16);
            this.labelDate.TabIndex = 7;
            this.labelDate.Text = "dd/mm/yyyy";
            // 
            // textBoxSec
            // 
            this.textBoxSec.Location = new System.Drawing.Point(60, 85);
            this.textBoxSec.Name = "textBoxSec";
            this.textBoxSec.Size = new System.Drawing.Size(29, 22);
            this.textBoxSec.TabIndex = 6;
            // 
            // textBoxMin
            // 
            this.textBoxMin.Location = new System.Drawing.Point(60, 55);
            this.textBoxMin.Name = "textBoxMin";
            this.textBoxMin.Size = new System.Drawing.Size(29, 22);
            this.textBoxMin.TabIndex = 5;
            // 
            // textBoxHour2
            // 
            this.textBoxHour2.Location = new System.Drawing.Point(60, 25);
            this.textBoxHour2.Name = "textBoxHour2";
            this.textBoxHour2.Size = new System.Drawing.Size(29, 22);
            this.textBoxHour2.TabIndex = 4;
            // 
            // labelClock
            // 
            this.labelClock.AutoSize = true;
            this.labelClock.Font = new System.Drawing.Font("Agency FB", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClock.Location = new System.Drawing.Point(111, 18);
            this.labelClock.Name = "labelClock";
            this.labelClock.Size = new System.Drawing.Size(127, 44);
            this.labelClock.TabIndex = 2;
            this.labelClock.Text = "00:00:00";
            // 
            // button_led1Off
            // 
            this.button_led1Off.Location = new System.Drawing.Point(112, 123);
            this.button_led1Off.Name = "button_led1Off";
            this.button_led1Off.Size = new System.Drawing.Size(42, 30);
            this.button_led1Off.TabIndex = 1;
            this.button_led1Off.Text = "OFF";
            this.button_led1Off.UseVisualStyleBackColor = true;
            this.button_led1Off.Click += new System.EventHandler(this.button_led1Off_Click);
            // 
            // button_led1On
            // 
            this.button_led1On.Location = new System.Drawing.Point(112, 85);
            this.button_led1On.Name = "button_led1On";
            this.button_led1On.Size = new System.Drawing.Size(42, 32);
            this.button_led1On.TabIndex = 0;
            this.button_led1On.Text = "ON";
            this.button_led1On.UseVisualStyleBackColor = true;
            this.button_led1On.Click += new System.EventHandler(this.button_led1On_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.labelH);
            this.groupBox5.Controls.Add(this.labelT);
            this.groupBox5.Controls.Add(this.textBoxHumi);
            this.groupBox5.Controls.Add(this.textBoxTemp);
            this.groupBox5.Location = new System.Drawing.Point(134, 177);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(202, 97);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Temperature and Humidity";
            // 
            // labelH
            // 
            this.labelH.AutoSize = true;
            this.labelH.Location = new System.Drawing.Point(21, 66);
            this.labelH.Name = "labelH";
            this.labelH.Size = new System.Drawing.Size(38, 16);
            this.labelH.TabIndex = 3;
            this.labelH.Text = "Humi";
            // 
            // labelT
            // 
            this.labelT.AutoSize = true;
            this.labelT.Location = new System.Drawing.Point(21, 24);
            this.labelT.Name = "labelT";
            this.labelT.Size = new System.Drawing.Size(43, 16);
            this.labelT.TabIndex = 2;
            this.labelT.Text = "Temp";
            // 
            // textBoxHumi
            // 
            this.textBoxHumi.Location = new System.Drawing.Point(101, 63);
            this.textBoxHumi.Name = "textBoxHumi";
            this.textBoxHumi.Size = new System.Drawing.Size(63, 22);
            this.textBoxHumi.TabIndex = 1;
            // 
            // textBoxTemp
            // 
            this.textBoxTemp.Location = new System.Drawing.Point(101, 24);
            this.textBoxTemp.Name = "textBoxTemp";
            this.textBoxTemp.Size = new System.Drawing.Size(63, 22);
            this.textBoxTemp.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Tai Le", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Crimson;
            this.label9.Location = new System.Drawing.Point(142, 293);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(182, 29);
            this.label9.TabIndex = 4;
            this.label9.Text = "Designed by N2\r\n";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(13, 178);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(115, 144);
            this.pictureBox2.TabIndex = 25;
            this.pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 340);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = " Every Second Counts";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLamp)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBell)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.TextBox textBox_ipAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button_led2Off;
        private System.Windows.Forms.Button button_led2On;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_led1Off;
        private System.Windows.Forms.Button button_led1On;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button_Disconnect;
        private System.Windows.Forms.TextBox textBoxAbout;
        private System.Windows.Forms.TextBox textBoxHour;
        private System.Windows.Forms.TextBox textBoxHumi;
        private System.Windows.Forms.TextBox textBoxTemp;
        private System.Windows.Forms.Label labelH;
        private System.Windows.Forms.Label labelT;
        private System.Windows.Forms.TextBox textBoxSec;
        private System.Windows.Forms.TextBox textBoxMin;
        private System.Windows.Forms.TextBox textBoxHour2;
        private System.Windows.Forms.Label labelClock;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.PictureBox pictureBoxLamp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBoxBell;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxMinS;
        private System.Windows.Forms.Button button_sendGMT;
    }
}

