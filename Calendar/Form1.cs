using System.Globalization;
using System.Media;

namespace Calendar
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();
        int WIDTH = 300, HEIGHT = 300, secHAND = 140, minHAND = 120, hrHAND = 90;
        // Center
        int cx, cy;
        Bitmap bmp;
        Graphics g;

        // Variabler för att hålla minutvärdena för markörerna
        private int? markerMinute = null;
        private int? markerMinute10 = null;

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
                SystemSounds.Beep.Play();
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
            // Kontrollera att bmp inte är null och har rätt storlek
            if (bmp == null || bmp.Width != WIDTH + 20 || bmp.Height != HEIGHT + 20)
            {
                bmp = new Bitmap(WIDTH + 20, HEIGHT + 20); // Initiera bitmappen om den är null eller har fel storlek
            }

            // Initiera grafikobjekt från bitmappen
            using (g = Graphics.FromImage(bmp))
            {
                // Hämta aktuell tid
                int ss = DateTime.Now.Second;
                int mm = DateTime.Now.Minute;
                int hh = DateTime.Now.Hour;

                int[] handCoord = new int[2];

                // Rensa ritytan
                g.Clear(Color.White);

                // Rita cirkeln som representerar klockan med padding
                g.DrawEllipse(new Pen(Color.Black, 1f), 10, 10, WIDTH, HEIGHT);

                // Rita siffrorna på klockan
                int radius = WIDTH / 2 - 22; // Justera avståndet från centrum
                g.DrawString("12", new Font("Arial", 13), Brushes.Black, new PointF(cx - 15, cy - radius + 5));
                g.DrawString("3", new Font("Arial", 13), Brushes.Black, new PointF(cx + radius - 30, cy - 10));
                g.DrawString("6", new Font("Arial", 13), Brushes.Black, new PointF(cx - 10, cy + radius - 32));
                g.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(cx - radius + 5, cy - 10));

                // Rita tim- och minutmarkeringar
                for (int i = 0; i < 60; i++)
                {
                    int tickLength = (i % 5 == 0) ? 15 : 7; // Större markeringar för varje 5:e minut

                    // Beräkna start- och slutkoordinater för varje markering
                    int x1 = cx + (int)((WIDTH / 2 - 10) * Math.Sin(i * 6 * Math.PI / 180));
                    int y1 = cy - (int)((WIDTH / 2 - 10) * Math.Cos(i * 6 * Math.PI / 180));
                    int x2 = cx + (int)((WIDTH / 2 - 10 - tickLength) * Math.Sin(i * 6 * Math.PI / 180));
                    int y2 = cy - (int)((WIDTH / 2 - 10 - tickLength) * Math.Cos(i * 6 * Math.PI / 180));

                    // Rita tim- och minutmarkeringar
                    if (i % 5 == 0)
                    {
                        g.DrawLine(new Pen(Color.DarkSlateBlue, 3f), new Point(x1, y1), new Point(x2, y2)); // Tjockare markeringar
                    }
                    else
                    {
                        g.DrawLine(new Pen(Color.Black, 1f), new Point(x1, y1), new Point(x2, y2)); // Tunna markeringar
                    }
                }

                // Rita datumrutan
                int dateBoxX = cx + radius / 3;
                int dateBoxY = cy - 7;
                int dateBoxWidth = 50;
                int dateBoxHeight = 25;

                // Rita ljusgrå bakgrund och ram
                g.FillRectangle(Brushes.LightGray, dateBoxX, dateBoxY, dateBoxWidth, dateBoxHeight);
                g.DrawRectangle(new Pen(Color.DarkGray, 2f), dateBoxX, dateBoxY, dateBoxWidth, dateBoxHeight);

                // Skriv dagens datum i rutan
                string dayMonth = DateTime.Now.ToString("dd/MM", CultureInfo.InvariantCulture);
                Font dayFont = new Font("Arial", 10);
                SizeF stringSize = g.MeasureString(dayMonth, dayFont);
                float textX = dateBoxX + (dateBoxWidth - stringSize.Width) / 2;
                float textY = dateBoxY + (dateBoxHeight - stringSize.Height) / 2;
                g.DrawString(dayMonth, dayFont, Brushes.Brown, new PointF(textX, textY));

                // Rita sekundvisaren
                handCoord = msCoord(ss, secHAND);
                g.DrawLine(new Pen(Color.Red, 1f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

                // Rita minutvisaren
                handCoord = msCoord(mm, minHAND);
                g.DrawLine(new Pen(Color.Black, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

                // Rita timvisaren
                handCoord = hrCoord(hh % 12, mm, hrHAND);
                g.DrawLine(new Pen(Color.Gray, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

                // Om en markör är satt, rita den (grön triangel)
                if (markerMinute.HasValue)
                {
                    DrawMarkerAtMinute(markerMinute.Value, Brushes.Green);
                }

                // Om 10-minutersmarkören är satt, rita den (blå triangel)
                if (markerMinute10.HasValue)
                {
                    DrawMarkerAtMinute(markerMinute10.Value, Brushes.Blue);
                }

                // Ladda upp bilden i PictureBox
                pictureBox1.Image = bmp;

                // Uppdatera fönstertiteln med tiden
                this.Text = "RN Clock - " + hh + ":" + mm + ":" + ss;
            }
        }

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

        // Klickhändelse för 15-minutersmarkören (grön triangel)
        private void SetMarkerButton_Click(object sender, EventArgs e)
        {
            int minutesToAdd = 15;
            markerMinute = (DateTime.Now.Minute + minutesToAdd) % 60; // Hitta minutpositionen
        }

        // Klickhändelse för 10-minutersmarkören (blå triangel)
        private void SetMarker10Button_Click(object sender, EventArgs e)
        {
            int minutesToAdd = 10;
            markerMinute10 = (DateTime.Now.Minute + minutesToAdd) % 60; // Hitta minutpositionen för 10 minuter framåt
        }

        private void DrawMarkerAtMinute(int targetMinute, Brush fillBrush)
        {
            // Beräkna vinkeln för minutpositionen (varje minut motsvarar 6 grader)
            double angle = targetMinute * 6 * Math.PI / 180;

            // Triangelns dimensioner
            int triHeight = 15; // Triangelns höjd
            int triBase = 6;   // Triangelns bredd vid basen (mindre för att göra triangeln smalare)

            // Placera spetsen av triangeln precis på cirkelns kant
            int tipRadius = WIDTH / 2 - 5; // Spetsen precis på cirkelns kant
            int xTip = cx + (int)(tipRadius * Math.Sin(angle));
            int yTip = cy - (int)(tipRadius * Math.Cos(angle));

            // Placera basen av triangeln utanför cirkeln
            int baseRadius = WIDTH / 2 + 10; // Basen placeras utanför cirkeln
            int xBaseLeft = cx + (int)(baseRadius * Math.Sin(angle + Math.PI / 36)); // Vänster baspunkt, smalare vinkel
            int yBaseLeft = cy - (int)(baseRadius * Math.Cos(angle + Math.PI / 36));

            int xBaseRight = cx + (int)(baseRadius * Math.Sin(angle - Math.PI / 36)); // Höger baspunkt, smalare vinkel
            int yBaseRight = cy - (int)(baseRadius * Math.Cos(angle - Math.PI / 36));

            // Definiera triangeln med spetsen och de två baspunkterna
            Point[] trianglePoints = {
        new Point(xTip, yTip),      // Spetsen på triangeln (på cirkeln)
        new Point(xBaseLeft, yBaseLeft),   // Vänstra hörnet av basen (utanför cirkeln)
        new Point(xBaseRight, yBaseRight)  // Högra hörnet av basen (utanför cirkeln)
    };

            // Fyll triangeln med vald färg
            g.FillPolygon(fillBrush, trianglePoints);
        }


    }
}
