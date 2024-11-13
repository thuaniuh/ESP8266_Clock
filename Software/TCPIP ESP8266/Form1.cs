using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;

namespace TCPIP_ESP8266
{
    public partial class Form1 : Form
    {
        SimpleTcpClient client;
        string DataIn;  
        sbyte indexOfT, indexOfH, indexOfD;
        string  temp, hum;
        

        Timer alarmTimer = new Timer();
        Timer lampTimer = new Timer();
        DateTime alarmTime;
        DateTime lampStartTime;
        TimeSpan lampDuration;
        public Form1()
        {
            InitializeComponent();
            alarmTimer.Interval = 1000; // Check every second
            alarmTimer.Tick += AlarmTimer_Tick;

            lampTimer.Interval = 1000; // Check every second
            lampTimer.Tick += LampTimer_Tick;

            // Cập nhật đồng hồ mỗi giây
            Timer clockTimer = new Timer();
            clockTimer.Interval = 1000; // 1 giây
            clockTimer.Tick += ClockTimer_Tick;
            clockTimer.Start();

        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // Cập nhật thời gian hiện tại của máy tính
            DateTime currentTime = DateTime.Now;
            

            // Cập nhật ngày và giờ vào các label
            labelDate.Text = currentTime.ToString("dd/MM/yyyy");  // Hiển thị ngày
            labelClock.Text = currentTime.ToString("HH:mm:ss");   // Hiển thị giờ
        }

        private DateTime GetLabelClockTime()
        {
            string[] timeParts = labelClock.Text.Split(':');
            if (timeParts.Length == 3) // Ensure we have HH:MM:SS
            {
                int hours = int.Parse(timeParts[0]);
                int minutes = int.Parse(timeParts[1]);
                int seconds = int.Parse(timeParts[2]);
                return DateTime.Today.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);
            }
            return DateTime.MinValue; // Invalid time, just return a minimum value
        }
        private bool isLampOn = false;
        private void LampTimer_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now; // Sử dụng thời gian hiện tại của máy tính
            if (currentTime != DateTime.MinValue)
            {
                // Kiểm tra thời gian hiện tại với thời gian đã hẹn
                TimeSpan timeDifference = currentTime - lampStartTime;

                // Chỉ bật đèn nếu khớp với thời gian hẹn (trong khoảng ± 1 phút)
                if (Math.Abs(timeDifference.TotalMinutes) <=1 && !isLampOn)
                {
                    Console.WriteLine("Lamp should be ON. Sending command ON2#");
                    client.Write("ON2#");
                    isLampOn = true;
                    pictureBoxLamp.Image = TCPIP_ESP8266.Properties.Resources.on;
                }
                else if (Math.Abs(timeDifference.TotalMinutes) > 1 && isLampOn)
                {
                    // Tắt đèn nếu vượt quá thời gian
                    Console.WriteLine("Lamp should be OFF. Sending command OFF2#");
                    client.Write("OFF2#");
                    isLampOn = false;
                    lampTimer.Stop();
                    pictureBoxLamp.Image =  TCPIP_ESP8266.Properties.Resources.off;
                }
            }
            else
            {
                Console.WriteLine("Error: Current time is invalid.");
            }
        }

        private void AlarmTimer_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = GetLabelClockTime(); // Use labelClock text
            if (currentTime != DateTime.MinValue && currentTime >= alarmTime)
            {
                alarmTimer.Stop();
                client.Write("BUZZ#"); // Activate buzzer
                pictureBoxBell.Image = TCPIP_ESP8266.Properties.Resources.notification;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += DataReceived;
            DataIn = "";

        }

        private void DataReceived(object sender, SimpleTCP.Message e)
        {
            labelClock.Invoke((MethodInvoker)delegate ()
           {
               DataIn = e.MessageString;
               indexOfD = Convert.ToSByte(DataIn.IndexOf("D"));
               indexOfH = Convert.ToSByte(DataIn.IndexOf("H"));
               indexOfT = Convert.ToSByte(DataIn.IndexOf("T"));
               
               temp = DataIn.Substring(indexOfD + 1,indexOfT - indexOfD -1).Trim();
               hum = DataIn.Substring(indexOfT + 1, indexOfH - indexOfT - 1).Trim();
               textBoxTemp.Text = temp;
               textBoxHumi.Text = hum;
            

           });

        }

        private void button_Disconnect_Click(object sender, EventArgs e)
        {
            client.Disconnect();
            button_Disconnect.Enabled = false;
            button_connect.Enabled = true;
            textBoxStatus.Text = "Disconnected";
            textBoxStatus.BackColor = Color.Red;
            textBoxStatus.ForeColor = Color.White;
        }

        private void button_sendGMT_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy múi giờ cục bộ của máy tính
                TimeZoneInfo localZone = TimeZoneInfo.Local;
                TimeSpan localOffset = localZone.GetUtcOffset(DateTime.Now);

                // Thời gian cục bộ + múi giờ
                DateTime localTime = DateTime.Now;
                string formattedTime = localTime.ToString("HH:mm:ss dd/MM/yyyy");

                // Gửi thời gian c  ùng với múi giờ
                string dataToSend = formattedTime + " Offset: " + localOffset.TotalHours;
                client.Write("GMT#" + dataToSend);
                MessageBox.Show("GMT time sent: " + dataToSend);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending GMT: " + ex.Message);
            }
        }

    

        private void button_connect_Click(object sender, EventArgs e)
        {

            client.Connect(textBox_ipAddress.Text, Convert.ToInt32(textBox_port.Text));
            button_connect.Enabled = false;
            button_Disconnect.Enabled = true;
            textBoxStatus.Text = "Connected";
            textBoxStatus.BackColor = Color.Green;
            textBoxStatus.ForeColor = Color.White;
        }

        private void button_led1On_Click(object sender, EventArgs e)
        {
            int hour = Convert.ToInt32(textBoxHour2.Text);
            int minute = Convert.ToInt32(textBoxMin.Text);
            int second = Convert.ToInt32(textBoxSec.Text);
            alarmTime = DateTime.Today.AddHours(hour).AddMinutes(minute).AddSeconds(second);
            alarmTimer.Start();
            try
            {
                client.Write("ALARM#");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void button_led1Off_Click(object sender, EventArgs e)
        {
            alarmTimer.Stop();
            pictureBoxBell.Image = TCPIP_ESP8266.Properties.Resources.notification1;
            try
            {
                client.Write("OFF1#");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void button_led2On_Click(object sender, EventArgs e)
        {
            int hourStart = Convert.ToInt32(textBoxHour.Text);
            int minuteStart = Convert.ToInt32(textBoxMinS.Text);
            int duration = Convert.ToInt32(textBoxAbout.Text); // in minutes
            lampStartTime = DateTime.Today.AddHours(hourStart).AddMinutes(minuteStart);
            lampDuration = TimeSpan.FromMinutes(duration);
            lampTimer.Start();
            try
            {
                client.Write("LAMP#");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void button_led2Off_Click(object sender, EventArgs e)
        {
            isLampOn = false;
            lampTimer.Stop();
            pictureBoxLamp.Image = TCPIP_ESP8266.Properties.Resources.off;
            try
            {
                client.Write("OFF2#");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

       
    }
}
