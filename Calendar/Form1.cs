using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System.Globalization;
using System.Media;

namespace Calendar
{
    public partial class Form1 : Form
    {

        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();
        int WIDTH = 254, HEIGHT = 254, secHAND = 115, minHAND = 105, hrHAND = 70;
        //Center
        int cx, cy;
        Bitmap bmp;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Datum.Text = DateTime.Now.ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo("sv-SE"));
            Vecka.Text = "Vecka " + WeekOfYearISO8601(DateTime.Now).ToString();
            //Create bitmap
            bmp = new Bitmap(Width + 1, HEIGHT + 1);
            //Center
            cx = WIDTH / 2;
            cy = HEIGHT / 2;

            this.BackColor = Color.White;
            t.Interval = 1000;//milliseconds
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }

        private void TimerButton_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now.AddMinutes(45);
            MyTimer.Interval = (45 * 60 *1000); // 45 minuter
            MyTimer.Start();
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            TimerButton.Text = "Ready at: " + dateTime.ToString("HH:mm");
            TimerButton.BackColor = Color.Red;
            TimerButton.ForeColor = Color.White;
        }
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            MyTimer.Interval = (60 * 1000);
            if (sender == MyTimer)
            {
                //SystemSounds.Exclamation.Play();
                SystemSounds.Beep.Play();
                //SystemSounds.Hand.Play();
                MessageBox.Show("45 minuter har gått, dags för rast", "Ägg klockan");

                this.Close();
            }
        }
        private static int WeekOfYearISO8601(DateTime date)
        {
            var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        private void t_Tick(object sender, EventArgs e)
        {
            //create graphics
            g = Graphics.FromImage(bmp);

            //get time
            int ss = DateTime.Now.Second;
            int mm = DateTime.Now.Minute;
            int hh = DateTime.Now.Hour;

            int[] handCoord = new int[2];

            //clear
            g.Clear(Color.White);

            //draw circle
            g.DrawEllipse(new Pen(Color.Black, 1f), 0, 0, WIDTH, HEIGHT);

            //draw figure
            g.DrawString("12", new Font("Arial", 12), Brushes.Black, new PointF(116, 6));
            g.DrawString("3", new Font("Arial", 12), Brushes.Black, new PointF(230, 122));
            g.DrawString("6", new Font("Arial", 12), Brushes.Black, new PointF(116, 226));
            g.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(5, 122));

            //second hand
            handCoord = msCoord(ss, secHAND);
            g.DrawLine(new Pen(Color.Red, 1f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //minute hand
            handCoord = msCoord(mm, minHAND);
            g.DrawLine(new Pen(Color.Black, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //hour hand
            handCoord = hrCoord(hh % 12, mm, hrHAND);
            g.DrawLine(new Pen(Color.Gray, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //load bmp in picturebox1
            pictureBox1.Image = bmp;

            //disp time
            this.Text = "Analog Clock -  " + hh + ":" + mm + ":" + ss;

            //dispose
            g.Dispose();
        }
        //coord for minute and second hand
        private int[] msCoord(int val, int hlen)
        {
            int[] coord = new int[2];
            val *= 6;   //each minute and second make 6 degree

            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }

        //coord for hour hand
        private int[] hrCoord(int hval, int mval, int hlen)
        {
            int[] coord = new int[2];

            //each hour makes 30 degree
            //each min makes 0.5 degree
            int val = (int)((hval * 30) + (mval * 0.5));

            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }
    }
}