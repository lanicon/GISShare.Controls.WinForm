﻿namespace GISShare.Controls.WinForm.Demo.WFNew
{
    partial class DemoOfViewItemListBoxForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            GISShare.Controls.WinForm.WFNew.View.ViewItem viewItem1 = new GISShare.Controls.WinForm.WFNew.View.ViewItem();
            GISShare.Controls.WinForm.WFNew.View.TextViewItem textViewItem1 = new GISShare.Controls.WinForm.WFNew.View.TextViewItem();
            GISShare.Controls.WinForm.WFNew.View.ImageViewItem imageViewItem1 = new GISShare.Controls.WinForm.WFNew.View.ImageViewItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoOfViewItemListBoxForm));
            GISShare.Controls.WinForm.WFNew.View.ColorViewItem colorViewItem1 = new GISShare.Controls.WinForm.WFNew.View.ColorViewItem();
            this.viewItemListBox1 = new GISShare.Controls.WinForm.WFNew.View.ViewItemListBox();
            this.SuspendLayout();
            // 
            // viewItemListBox1
            // 
            this.viewItemListBox1.BackColor = System.Drawing.SystemColors.Window;
            this.viewItemListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewItemListBox1.Location = new System.Drawing.Point(0, 0);
            this.viewItemListBox1.Name = "viewItemListBox1";
            this.viewItemListBox1.Padding = new System.Windows.Forms.Padding(0);
            this.viewItemListBox1.SelectedIndex = -1;
            this.viewItemListBox1.ShowHScrollBar = true;
            this.viewItemListBox1.Size = new System.Drawing.Size(584, 274);
            this.viewItemListBox1.TabIndex = 0;
            this.viewItemListBox1.Text = "viewItemListBox1";
            viewItem1.Text = "简单视图项（ViewItem）";
            textViewItem1.Font = new System.Drawing.Font("隶书", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            textViewItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            textViewItem1.Name = null;
            textViewItem1.Text = "文本视图项（TextViewItem）";
            imageViewItem1.Font = new System.Drawing.Font("宋体", 9F);
            imageViewItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            imageViewItem1.Image = ((System.Drawing.Image)(resources.GetObject("imageViewItem1.Image")));
            imageViewItem1.Name = null;
            imageViewItem1.Text = "图片视图项（ImageViewItem）";
            colorViewItem1.Color = System.Drawing.Color.Red;
            colorViewItem1.Font = new System.Drawing.Font("宋体", 9F);
            colorViewItem1.ForeColor = System.Drawing.SystemColors.ControlText;
            colorViewItem1.Name = null;
            colorViewItem1.Text = "颜色视图项（ColorViewItem）";
            this.viewItemListBox1.ViewItems.Add(viewItem1);
            this.viewItemListBox1.ViewItems.Add(textViewItem1);
            this.viewItemListBox1.ViewItems.Add(imageViewItem1);
            this.viewItemListBox1.ViewItems.Add(colorViewItem1);
            // 
            // DemoOfViewItemListBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 274);
            this.Controls.Add(this.viewItemListBox1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "DemoOfViewItemListBoxForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ViewItemListBox控件";
            this.ResumeLayout(false);

        }

        #endregion

        private GISShare.Controls.WinForm.WFNew.View.ViewItemListBox viewItemListBox1;
    }
}