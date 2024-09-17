using System.Globalization;
using System.Media;

namespace Calendar
{
    public partial class Form1 : Form
    {

        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();
        int WIDTH = 300, HEIGHT = 300, secHAND = 140, minHAND = 120, hrHAND = 90;
        //Center
        int cx, cy;
        Bitmap bmp;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
            this.Resize += new System.EventHandler(this.Form1_Resize);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Datum.Text = DateTime.Now.ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo("sv-SE"));
            Vecka.Text = "Vecka " + WeekOfYearISO8601(DateTime.Now).ToString();

            // Adjust pictureBox1 size and position
            pictureBox1.Size = new Size(WIDTH + 20, HEIGHT + 20); // Add padding
            pictureBox1.Location = new Point((this.ClientSize.Width - pictureBox1.Width) / 2, (this.ClientSize.Height - pictureBox1.Height) / 3);

            // Create bitmap
            bmp = new Bitmap(WIDTH + 20, HEIGHT + 20); // Match the size of the PictureBox

            // Center
            cx = (WIDTH + 20) / 2;
            cy = (HEIGHT + 20) / 2;

            this.BackColor = Color.White;
            t.Interval = 1000; // milliseconds
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }

        private void TimerButton_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now.AddMinutes(45);
            MyTimer.Interval = (45 * 60 * 1000); // 45 minuter
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
            // Create graphics
            g = Graphics.FromImage(bmp);

            // Get time
            int ss = DateTime.Now.Second;
            int mm = DateTime.Now.Minute;
            int hh = DateTime.Now.Hour;

            int[] handCoord = new int[2];

            // Clear
            g.Clear(Color.White);

            // Draw circle with padding
            g.DrawEllipse(new Pen(Color.Black, 1f), 10, 10, WIDTH, HEIGHT); // Adjusted to start with padding of 10

            // Draw numbers with refined positions
            int radius = WIDTH / 2;

            // Adjusted positions for each number
            g.DrawString("12", new Font("Arial", 16), Brushes.Black, new PointF(cx - 20, cy - radius + 15)); // Move "12" slightly left and up
            g.DrawString("3", new Font("Arial", 16), Brushes.Black, new PointF(cx + radius - 45, cy - 12)); // Move "3" further to the left
            g.DrawString("6", new Font("Arial", 16), Brushes.Black, new PointF(cx - 10, cy + radius - 40)); // Move "6" slightly up
            g.DrawString("9", new Font("Arial", 16), Brushes.Black, new PointF(cx - radius + 15, cy - 12)); // Adjust "9" as a reference

            // Second hand
            handCoord = msCoord(ss, secHAND);
            g.DrawLine(new Pen(Color.Red, 1f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            // Minute hand
            handCoord = msCoord(mm, minHAND);
            g.DrawLine(new Pen(Color.Black, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            // Hour hand
            handCoord = hrCoord(hh % 12, mm, hrHAND);
            g.DrawLine(new Pen(Color.Gray, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            // Load bmp in pictureBox1
            pictureBox1.Image = bmp;

            // Display time
            this.Text = "Analog Clock -  " + hh + ":" + mm + ":" + ss;

            // Dispose
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
        private void Form1_Resize(object sender, EventArgs e)
        {
            // Center pictureBox1 on resize
            pictureBox1.Location = new Point((this.ClientSize.Width - pictureBox1.Width) / 2, (this.ClientSize.Height - pictureBox1.Height) / 2);
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