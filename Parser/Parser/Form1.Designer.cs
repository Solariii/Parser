using System.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.Drawing.Text;
namespace Parser
{
    partial class Parser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Parser));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Browse = new System.Windows.Forms.Button();
            this.BrowseTextBox = new System.Windows.Forms.TextBox();
            this.Restart = new System.Windows.Forms.Button();
            this.Draw = new System.Windows.Forms.Button();
            this.Confirm = new System.Windows.Forms.Button();
            this.InputTextBox = new System.Windows.Forms.TextBox();
            this.InputType = new System.Windows.Forms.GroupBox();
            this.InputTextFile = new System.Windows.Forms.RadioButton();
            this.InputDirectly = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Restart2 = new System.Windows.Forms.Button();
            this.SaveImage = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.InputType.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Browse);
            this.panel1.Controls.Add(this.BrowseTextBox);
            this.panel1.Controls.Add(this.Restart);
            this.panel1.Controls.Add(this.Draw);
            this.panel1.Controls.Add(this.Confirm);
            this.panel1.Controls.Add(this.InputTextBox);
            this.panel1.Controls.Add(this.InputType);
            this.panel1.Location = new System.Drawing.Point(30, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1140, 740);
            this.panel1.TabIndex = 0;
            // 
            // Browse
            // 
            this.Browse.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Browse.Location = new System.Drawing.Point(869, 113);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(252, 36);
            this.Browse.TabIndex = 6;
            this.Browse.Text = "Browse";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // BrowseTextBox
            // 
            this.BrowseTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrowseTextBox.Location = new System.Drawing.Point(19, 114);
            this.BrowseTextBox.Name = "BrowseTextBox";
            this.BrowseTextBox.ReadOnly = true;
            this.BrowseTextBox.Size = new System.Drawing.Size(844, 34);
            this.BrowseTextBox.TabIndex = 5;
            // 
            // Restart
            // 
            this.Restart.Font = new System.Drawing.Font("Bahnschrift SemiBold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Restart.Location = new System.Drawing.Point(576, 660);
            this.Restart.Name = "Restart";
            this.Restart.Size = new System.Drawing.Size(545, 66);
            this.Restart.TabIndex = 4;
            this.Restart.Text = "Restart";
            this.Restart.UseVisualStyleBackColor = true;
            this.Restart.Click += new System.EventHandler(this.Restart_Click);
            // 
            // Draw
            // 
            this.Draw.Font = new System.Drawing.Font("Bahnschrift SemiBold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Draw.Location = new System.Drawing.Point(19, 660);
            this.Draw.Name = "Draw";
            this.Draw.Size = new System.Drawing.Size(551, 66);
            this.Draw.TabIndex = 3;
            this.Draw.Text = "Draw";
            this.Draw.UseVisualStyleBackColor = true;
            this.Draw.Click += new System.EventHandler(this.Draw_Click);
            // 
            // Confirm
            // 
            this.Confirm.Font = new System.Drawing.Font("Bahnschrift SemiBold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Confirm.Location = new System.Drawing.Point(869, 31);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(252, 74);
            this.Confirm.TabIndex = 2;
            this.Confirm.Text = "Confirm";
            this.Confirm.UseVisualStyleBackColor = true;
            this.Confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // InputTextBox
            // 
            this.InputTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputTextBox.Location = new System.Drawing.Point(19, 156);
            this.InputTextBox.Multiline = true;
            this.InputTextBox.Name = "InputTextBox";
            this.InputTextBox.Size = new System.Drawing.Size(1101, 496);
            this.InputTextBox.TabIndex = 1;
            // 
            // InputType
            // 
            this.InputType.Controls.Add(this.InputTextFile);
            this.InputType.Controls.Add(this.InputDirectly);
            this.InputType.Font = new System.Drawing.Font("Bahnschrift SemiBold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputType.Location = new System.Drawing.Point(19, 16);
            this.InputType.Name = "InputType";
            this.InputType.Size = new System.Drawing.Size(844, 89);
            this.InputType.TabIndex = 0;
            this.InputType.TabStop = false;
            this.InputType.Text = "Please Choose Input Method :";
            // 
            // InputTextFile
            // 
            this.InputTextFile.AutoSize = true;
            this.InputTextFile.Font = new System.Drawing.Font("Bahnschrift", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputTextFile.Location = new System.Drawing.Point(506, 39);
            this.InputTextFile.Name = "InputTextFile";
            this.InputTextFile.Size = new System.Drawing.Size(240, 33);
            this.InputTextFile.TabIndex = 1;
            this.InputTextFile.TabStop = true;
            this.InputTextFile.Text = "Input From Text File";
            this.InputTextFile.UseVisualStyleBackColor = true;
            // 
            // InputDirectly
            // 
            this.InputDirectly.AutoSize = true;
            this.InputDirectly.Font = new System.Drawing.Font("Bahnschrift", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputDirectly.Location = new System.Drawing.Point(73, 39);
            this.InputDirectly.Name = "InputDirectly";
            this.InputDirectly.Size = new System.Drawing.Size(176, 33);
            this.InputDirectly.TabIndex = 0;
            this.InputDirectly.TabStop = true;
            this.InputDirectly.Text = "Input Directly";
            this.InputDirectly.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.Restart2);
            this.panel2.Controls.Add(this.SaveImage);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(30, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1140, 740);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 40);
            this.label1.TabIndex = 14;
            this.label1.Text = "Syntax Tree";
            // 
            // Restart2
            // 
            this.Restart2.Font = new System.Drawing.Font("Bahnschrift SemiBold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Restart2.Location = new System.Drawing.Point(576, 660);
            this.Restart2.Name = "Restart2";
            this.Restart2.Size = new System.Drawing.Size(545, 66);
            this.Restart2.TabIndex = 11;
            this.Restart2.Text = "Restart";
            this.Restart2.UseVisualStyleBackColor = true;
            this.Restart2.Click += new System.EventHandler(this.Restart2_Click);
            // 
            // SaveImage
            // 
            this.SaveImage.Font = new System.Drawing.Font("Bahnschrift SemiBold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveImage.Location = new System.Drawing.Point(19, 660);
            this.SaveImage.Name = "SaveImage";
            this.SaveImage.Size = new System.Drawing.Size(551, 66);
            this.SaveImage.TabIndex = 10;
            this.SaveImage.Text = "Save Image";
            this.SaveImage.UseVisualStyleBackColor = true;
            this.SaveImage.Click += new System.EventHandler(this.SaveImage_Click);
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Location = new System.Drawing.Point(19, 55);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1102, 599);
            this.panel3.TabIndex = 15;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(-1, 0);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(600000, 600000);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1102, 599);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Parser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Parser.Properties.Resources.bright_abstract_background_flame_oxygen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Name = "Parser";
            this.Text = "Parser";
            this.Load += new System.EventHandler(this.Parser_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.InputType.ResumeLayout(false);
            this.InputType.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button Confirm;
        private System.Windows.Forms.TextBox InputTextBox;
        private System.Windows.Forms.GroupBox InputType;
        private System.Windows.Forms.RadioButton InputTextFile;
        private System.Windows.Forms.RadioButton InputDirectly;
        private System.Windows.Forms.Button Draw;
        private System.Windows.Forms.Button Restart;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.TextBox BrowseTextBox;
        private System.Windows.Forms.Button Restart2;
        private System.Windows.Forms.Button SaveImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panel3;

    }
}

