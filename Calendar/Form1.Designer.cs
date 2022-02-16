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
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.Datum = new System.Windows.Forms.Label();
            this.Vecka = new System.Windows.Forms.Label();
            this.TimerButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.AnnuallyBoldedDates = new System.DateTime[] {
        new System.DateTime(2022, 1, 20, 0, 0, 0, 0),
        new System.DateTime(2022, 1, 21, 0, 0, 0, 0)};
            this.monthCalendar1.FirstDayOfWeek = System.Windows.Forms.Day.Monday;
            this.monthCalendar1.Location = new System.Drawing.Point(17, 382);
            this.monthCalendar1.Margin = new System.Windows.Forms.Padding(8, 9, 8, 9);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.ShowWeekNumbers = true;
            this.monthCalendar1.TabIndex = 0;
            // 
            // Datum
            // 
            this.Datum.AutoSize = true;
            this.Datum.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Datum.Location = new System.Drawing.Point(71, 40);
            this.Datum.Name = "Datum";
            this.Datum.Size = new System.Drawing.Size(52, 21);
            this.Datum.TabIndex = 2;
            this.Datum.Text = "label1";
            // 
            // Vecka
            // 
            this.Vecka.AutoSize = true;
            this.Vecka.Location = new System.Drawing.Point(106, 61);
            this.Vecka.Name = "Vecka";
            this.Vecka.Size = new System.Drawing.Size(38, 15);
            this.Vecka.TabIndex = 3;
            this.Vecka.Text = "label1";
            // 
            // TimerButton
            // 
            this.TimerButton.Location = new System.Drawing.Point(29, 16);
            this.TimerButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TimerButton.Name = "TimerButton";
            this.TimerButton.Size = new System.Drawing.Size(220, 22);
            this.TimerButton.TabIndex = 4;
            this.TimerButton.Text = "Starta";
            this.TimerButton.UseVisualStyleBackColor = true;
            this.TimerButton.Click += new System.EventHandler(this.TimerButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(4, 102);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(255, 268);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 566);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TimerButton);
            this.Controls.Add(this.Vecka);
            this.Controls.Add(this.Datum);
            this.Controls.Add(this.monthCalendar1);
            this.Name = "Form1";
            this.Text = "Date";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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