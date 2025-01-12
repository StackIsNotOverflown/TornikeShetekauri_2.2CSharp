namespace Cat_test
{
    partial class Test
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label questionLabel;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label timeLabel;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            questionLabel = new Label();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton4 = new RadioButton();
            submitButton = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            timeLabel = new Label();
            SuspendLayout();

            this.questionLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            questionLabel.AutoSize = true;
            questionLabel.Location = new Point(15, 15);
            questionLabel.Margin = new Padding(4, 0, 4, 0);
            questionLabel.Name = "questionLabel";
            questionLabel.Size = new Size(55, 15);
            questionLabel.TabIndex = 0;
            questionLabel.Text = "Question";
          

            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(19, 58);
            radioButton1.Margin = new Padding(4, 3, 4, 3);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(94, 19);
            radioButton1.TabIndex = 1;
            radioButton1.TabStop = true;
            radioButton1.Text = "radioButton1";
            radioButton1.UseVisualStyleBackColor = true;
           

            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(19, 92);
            radioButton2.Margin = new Padding(4, 3, 4, 3);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(94, 19);
            radioButton2.TabIndex = 2;
            radioButton2.TabStop = true;
            radioButton2.Text = "radioButton2";
            radioButton2.UseVisualStyleBackColor = true;
            

            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(19, 127);
            radioButton3.Margin = new Padding(4, 3, 4, 3);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(94, 19);
            radioButton3.TabIndex = 3;
            radioButton3.TabStop = true;
            radioButton3.Text = "radioButton3";
            radioButton3.UseVisualStyleBackColor = true;
            

            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(19, 162);
            radioButton4.Margin = new Padding(4, 3, 4, 3);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(94, 19);
            radioButton4.TabIndex = 4;
            radioButton4.TabStop = true;
            radioButton4.Text = "radioButton4";
            radioButton4.UseVisualStyleBackColor = true;
            


            submitButton.Location = new Point(19, 208);
            submitButton.Margin = new Padding(4, 3, 4, 3);
            submitButton.Name = "submitButton";
            submitButton.Size = new Size(88, 27);
            submitButton.TabIndex = 5;
            submitButton.Text = "დადასტურება";
            submitButton.UseVisualStyleBackColor = true;
            submitButton.Click += submitButton_Click;
           
            
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            

            timeLabel.AutoSize = true;
            timeLabel.Location = new Point(19, 242);
            timeLabel.Margin = new Padding(4, 0, 4, 0);
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(34, 15);
            timeLabel.TabIndex = 6;
            timeLabel.Text = "დრო";
            

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 301);
            Controls.Add(timeLabel);
            Controls.Add(submitButton);
            Controls.Add(radioButton4);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(questionLabel);
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainForm";
            Text = "CAT Test";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}