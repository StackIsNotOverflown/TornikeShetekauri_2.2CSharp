using System;
using System.IO;
using System.Windows.Forms;

namespace Cat_test
{
    partial class Login 
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
            this.Name1 = new System.Windows.Forms.TextBox();
            this.Surname = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.SurnameLabel = new System.Windows.Forms.Label();
            this.Submit = new System.Windows.Forms.Button();
            this.Text = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Name1
            // 
            this.Name1.Location = new System.Drawing.Point(152, 156);
            this.Name1.Name = "Name1";
            this.Name1.Size = new System.Drawing.Size(117, 23);
            this.Name1.TabIndex = 1;
            // 
            // Surname
            // 
            this.Surname.Location = new System.Drawing.Point(152, 98);
            this.Surname.Name = "Surname";
            this.Surname.Size = new System.Drawing.Size(117, 23);
            this.Surname.TabIndex = 1;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(45, 98);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(38, 15);
            this.NameLabel.TabIndex = 2;
            this.NameLabel.Text = "სახელი";
            // 
            // SurnameLabel
            // 
            this.SurnameLabel.AutoSize = true;
            this.SurnameLabel.Location = new System.Drawing.Point(45, 164);
            this.SurnameLabel.Name = "SurnameLabel";
            this.SurnameLabel.Size = new System.Drawing.Size(38, 15);
            this.SurnameLabel.TabIndex = 3;
            this.SurnameLabel.Text = "გვარი";
            // 
            // Submit
            // 
            this.Submit.Location = new System.Drawing.Point(152, 259);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(117, 23);
            this.Submit.TabIndex = 4;
            this.Submit.Text = "დადასტურება";
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Click += new System.EventHandler(this.Submit_Click);
            // 
            // Text
            // 
            this.Text.AutoSize = true;
            this.Text.Location = new System.Drawing.Point(186, 41);
            this.Text.Name = "Text";
            this.Text.Size = new System.Drawing.Size(55, 15);
            this.Text.TabIndex = 5;
            this.Text.Text = "v Login v";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Submit);
            this.Controls.Add(this.SurnameLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.Surname);
            this.Controls.Add(this.Name1);
            this.Controls.Add(this.Text);
            this.Name = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label Text;
        private System.Windows.Forms.TextBox Name1;
        private System.Windows.Forms.TextBox Surname;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label SurnameLabel;
        private System.Windows.Forms.Button Submit;
        private System.Windows.Forms.Label label1;

        private void Submit_Click(object sender, EventArgs e)
        {
            string name = Name1.Text;
            string surname = Surname.Text;

            try
            {
                SaveToFile(name, surname);
                Application.Run(new Test());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveToFile(string name, string surname)
        {
            string filePath = "\\Cat test\\Cat test\\Results.txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"Name: {name}");
                writer.WriteLine($"Surname: {surname}");
                writer.WriteLine("----------");
            }
        }
    }
}
