﻿namespace GISShare.Controls.WinForm.Demo
{
    partial class Form3
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
            this.viewItemListBox1 = new GISShare.Controls.WinForm.WFNew.View.ViewItemListBox();
            this.SuspendLayout();
            // 
            // viewItemListBox1
            // 
            this.viewItemListBox1.AutoGetFocus = true;
            this.viewItemListBox1.BackColor = System.Drawing.SystemColors.Window;
            this.viewItemListBox1.Location = new System.Drawing.Point(12, 12);
            this.viewItemListBox1.Name = "viewItemListBox1";
            this.viewItemListBox1.Padding = new System.Windows.Forms.Padding(0);
            this.viewItemListBox1.Size = new System.Drawing.Size(419, 273);
            this.viewItemListBox1.TabIndex = 0;
            this.viewItemListBox1.Text = "viewItemListBox1";
            this.viewItemListBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.viewItemListBox1_MouseDown);
            this.viewItemListBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.viewItemListBox1_MouseUp);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 297);
            this.Controls.Add(this.viewItemListBox1);
            this.Name = "Form3";
            this.Text = "ribbonControl1";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WinForm.WFNew.View.ViewItemListBox viewItemListBox1;

    }
}