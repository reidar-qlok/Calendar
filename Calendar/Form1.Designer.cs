namespace Calendar
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            monthCalendar1 = new MonthCalendar();
            Datum = new Label();
            Vecka = new Label();
            TimerButton = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // monthCalendar1
            // 
            monthCalendar1.AnnuallyBoldedDates = new DateTime[]
    {
    new DateTime(2022, 1, 20, 0, 0, 0, 0),
    new DateTime(2022, 1, 21, 0, 0, 0, 0)
    };
            monthCalendar1.FirstDayOfWeek = Day.Monday;
            monthCalendar1.Location = new Point(13, 514);
            monthCalendar1.Margin = new Padding(11, 15, 11, 15);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.ShowWeekNumbers = true;
            monthCalendar1.TabIndex = 0;
            // 
            // Datum
            // 
            Datum.AutoSize = true;
            Datum.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Datum.Location = new Point(73, 67);
            Datum.Margin = new Padding(4, 0, 4, 0);
            Datum.Name = "Datum";
            Datum.Size = new Size(78, 32);
            Datum.TabIndex = 2;
            Datum.Text = "label1";
            // 
            // Vecka
            // 
            Vecka.AutoSize = true;
            Vecka.Location = new Point(141, 102);
            Vecka.Margin = new Padding(4, 0, 4, 0);
            Vecka.Name = "Vecka";
            Vecka.Size = new Size(59, 25);
            Vecka.TabIndex = 3;
            Vecka.Text = "label1";
            // 
            // TimerButton
            // 
            TimerButton.Location = new Point(41, 27);
            TimerButton.Margin = new Padding(4, 3, 4, 3);
            TimerButton.Name = "TimerButton";
            TimerButton.Size = new Size(275, 37);
            TimerButton.TabIndex = 4;
            TimerButton.Text = "Starta";
            TimerButton.UseVisualStyleBackColor = true;
            TimerButton.Click += TimerButton_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(13, 150);
            pictureBox1.Margin = new Padding(4, 5, 4, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(320, 320);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(354, 791);
            Controls.Add(pictureBox1);
            Controls.Add(TimerButton);
            Controls.Add(Vecka);
            Controls.Add(Datum);
            Controls.Add(monthCalendar1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Date";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MonthCalendar monthCalendar1;
        private Label Datum;
        private Label Vecka;
        private Button TimerButton;
        private PictureBox pictureBox1;


        // 
        // pictureBox1
        // 

    }
}