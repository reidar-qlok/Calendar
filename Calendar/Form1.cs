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
            g.DrawEllipse(new Pen(Color.Black, 1f), 10, 10, WIDTH, HEIGHT);

            // Draw numbers with refined positions
            int radius = WIDTH / 2 - 22; // Justera avståndet från centrum

            // Justerade positioner för varje siffra och mindre fontstorlek
            g.DrawString("12", new Font("Arial", 13), Brushes.Black, new PointF(cx - 15, cy - radius + 5)); // Mindre och närmare centrum
            g.DrawString("3", new Font("Arial", 13), Brushes.Black, new PointF(cx + radius - 30, cy - 10)); // Mindre och närmare centrum
            g.DrawString("6", new Font("Arial", 13), Brushes.Black, new PointF(cx - 10, cy + radius - 32)); // Mindre och närmare centrum
            g.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(cx - radius + 5, cy - 10));  // Mindre och närmare centrum

            for (int i = 0; i < 60; i++)
            {
                int tickLength = (i % 5 == 0) ? 15 : 7; // Större tickar för varje 5:e minut (timmarkeringar)

                // Beräkna start- och slutpositioner för varje tick
                int x1 = cx + (int)((WIDTH / 2 - 10) * Math.Sin(i * 6 * Math.PI / 180)); // Startposition nära kanten
                int y1 = cy - (int)((WIDTH / 2 - 10) * Math.Cos(i * 6 * Math.PI / 180)); // Startposition nära kanten
                int x2 = cx + (int)((WIDTH / 2 - 10 - tickLength) * Math.Sin(i * 6 * Math.PI / 180)); // Slutposition lite längre in
                int y2 = cy - (int)((WIDTH / 2 - 10 - tickLength) * Math.Cos(i * 6 * Math.PI / 180)); // Slutposition lite längre in

                // Om det är en timmarkering (varje 5:e minut), rita den blå och tjockare
                if (i % 5 == 0)
                {
                    g.DrawLine(new Pen(Color.DarkSlateBlue, 3f), new Point(x1, y1), new Point(x2, y2)); // Tjockare och blå timmarkeringar
                }
                else
                {
                    // Rita minutstreck som vanliga svarta tunna linjer
                    g.DrawLine(new Pen(Color.Black, 1f), new Point(x1, y1), new Point(x2, y2));
                }
                // Justera position och storlek på datumrutan
                int dateBoxX = cx + radius / 3;  // Flyttar rutan åt vänster
                int dateBoxY = cy - 7;               // Flyttar rutan uppåt
                int dateBoxWidth = 50;           // Mindre bredd på rutan
                int dateBoxHeight = 25;          // Mindre höjd på rutan

                /// Simulera en inre skugga genom att fylla rutan med en ljusare färg (ljusgrå)
                g.FillRectangle(Brushes.LightGray, dateBoxX, dateBoxY, dateBoxWidth, dateBoxHeight);

                // Rita en grå ram runt rutan
                g.DrawRectangle(new Pen(Color.DarkGray, 2f), dateBoxX, dateBoxY, dateBoxWidth, dateBoxHeight);

                // Skriv in dagens datum (dag/månad) i rutan med bruna siffror
                string dayMonth = DateTime.Now.ToString("dd/MM", CultureInfo.InvariantCulture); // Använder InvariantCulture för att säkerställa dd/MM-format
                Font dayFont = new Font("Arial", 10);
                SizeF stringSize = g.MeasureString(dayMonth, dayFont);


                // Skriv in dagens datum (dagnummer) i rutan med bruna siffror
                string dayNumber = DateTime.Now.Day.ToString();



                // Centrera texten i rutan
                float textX = dateBoxX + (dateBoxWidth - stringSize.Width) / 2; // Beräkna x-position för att centrera
                float textY = dateBoxY + (dateBoxHeight - stringSize.Height) / 2; // Beräkna y-position för att centrera

                // Rita texten (dag/månad) med bruna siffror
                g.DrawString(dayMonth, dayFont, Brushes.Brown, new PointF(textX, textY));
                /////////////////////////////////////////////
                // Second hand
                handCoord = msCoord(ss, secHAND);
                g.DrawLine(new Pen(Color.Red, 1f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            }


            // Minute hand
            handCoord = msCoord(mm, minHAND);
            g.DrawLine(new Pen(Color.Black, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            // Hour hand
            handCoord = hrCoord(hh % 12, mm, hrHAND);
            g.DrawLine(new Pen(Color.Gray, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            // Load bmp in pictureBox1
            pictureBox1.Image = bmp;

            // Display time
            this.Text = "RN Clock -  " + hh + ":" + mm + ":" + ss;



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