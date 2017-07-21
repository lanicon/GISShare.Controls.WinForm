using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [DefaultEvent("CheckedChanged")]
    public class ImageCheckBoxItem : ImageLabelItem, IImageCheckBoxItem
    {
        private const int CRT_CHECKSIZE = 12;
        
        #region 构造函数
        public ImageCheckBoxItem() { }

        public ImageCheckBoxItem(string strText)
            : base(strText) { }

        public ImageCheckBoxItem(string strName, string strText)
            : base(strName, strText) { }

        public ImageCheckBoxItem(string strText, Image image)
            : base(strText, image) { }

        public ImageCheckBoxItem(string strName, string strText, Image image)
            : base(strName, strText, image) { }

        public ImageCheckBoxItem(string strName, string strText, Image image, bool bChecked)
            : base(strName, strText, image)
        {
            this.Checked = bChecked;
        }

        //public ImageCheckBoxItem(GISShare.Controls.Plugin.WFNew.IImageCheckBoxItemP pBaseItemP)
        //{
        //    //IPlugin
        //    this.Name = pBaseItemP.Name;
        //    //ISetEntityObject
        //    GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemP as GISShare.Controls.Plugin.ISetEntityObject;
        //    if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(this);
        //    //IBaseItemP_
        //    this.Checked = pBaseItemP.Checked;
        //    this.Enabled = pBaseItemP.Enabled;
        //    this.Font = pBaseItemP.Font;
        //    this.ForeColor = pBaseItemP.ForeColor;
        //    this.LockHeight = pBaseItemP.LockHeight;
        //    this.LockWith = pBaseItemP.LockWith;
        //    this.Padding = pBaseItemP.Padding;
        //    this.Size = pBaseItemP.Size;
        //    this.Text = pBaseItemP.Text;
        //    this.Visible = pBaseItemP.Visible;
        //    this.Category = pBaseItemP.Category;
        //    this.MinimumSize = pBaseItemP.MinimumSize;
        //    this.UsingViewOverflow = pBaseItemP.UsingViewOverflow;
        //    //ILabelItemP
        //    this.TextAlign = pBaseItemP.TextAlign;
        //    //IImageBoxItemP
        //    this.eImageSizeStyle = pBaseItemP.eImageSizeStyle;
        //    this.Image = pBaseItemP.Image;
        //    this.ImageAlign = pBaseItemP.ImageAlign;
        //    this.ImageSize = pBaseItemP.ImageSize;
        //    //IImageLabelItemP
        //    this.AutoPlanTextRectangle = pBaseItemP.AutoPlanTextRectangle;
        //    this.ITSpace = pBaseItemP.ITSpace;
        //    this.eDisplayStyle = pBaseItemP.eDisplayStyle;
        //    //ImageCheckBoxItemP
        //    this.CDSpace = pBaseItemP.CDSpace;
        //    //ICheckBoxItemP
        //    this.CheckState = pBaseItemP.CheckState;
        //}
        #endregion

        #region ICheckBoxItem
        [Browsable(false), Description("勾选区绘制矩形框"), Category("布局")]
        public virtual Rectangle CheckRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle
                    (
                    rectangle.X + this.Padding.Left,
                    rectangle.Y + this.Padding.Top + (rectangle.Height - this.Padding.Top - this.Padding.Bottom - CRT_CHECKSIZE) / 2,
                    CRT_CHECKSIZE,
                    CRT_CHECKSIZE
                    );
            }
        }

        bool m_InsideCheckRectangleTrigger = false;
        /// <summary>
        /// 鼠标在复选框内弹起方才改变（Checked属性）
        /// </summary>
        [Browsable(true), DefaultValue(false), Description("鼠标在复选框内弹起方才改变（Checked属性）"), Category("属性")]
        public bool InsideCheckRectangleTrigger
        {
            get { return m_InsideCheckRectangleTrigger; }
            set { m_InsideCheckRectangleTrigger = value; }
        }

        private CheckState m_CheckState = CheckState.Unchecked;
        [Browsable(true), DefaultValue(typeof(CheckState), "Unchecked"), Description("选中的状态"), Category("状态")]
        public CheckState CheckState
        {
            get
            {
                return m_CheckState;
            }
            set
            {
                if (m_CheckState == value) return;
                //
                m_CheckState = value;
                if (value == CheckState.Checked && !this.Checked) this.Checked = true;
                else if ((value == CheckState.Unchecked || value == CheckState.Indeterminate) && this.Checked) this.Checked = false;
            }
        }
        #endregion

        #region IImageCheckBoxItem
        int m_CDSpace = 1;
        [Browsable(true), DefaultValue(1), Description("勾选区与绘制区的间距"), Category("布局")]
        public int CDSpace
        {
            get { return m_CDSpace; }
            set
            {
                m_CDSpace = value;
            }
        }
        #endregion

        public override Rectangle DrawRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return Rectangle.FromLTRB(rectangle.Left + this.Padding.Left + CRT_CHECKSIZE + this.CDSpace,
                    rectangle.Top + this.Padding.Top,
                    rectangle.Right - this.Padding.Right,
                    rectangle.Bottom - this.Padding.Bottom);
            }
        }

        protected override bool RefreshBaseItemState
        {
            get
            {
                return true;
            }
        }

        public override bool Checked
        {
            get
            {
                return base.Checked;
            }
            set
            {
                base.Checked = value;
                if (value && this.CheckState != CheckState.Checked) this.CheckState = CheckState.Checked;
                else if (!value && this.CheckState == CheckState.Checked) this.CheckState = CheckState.Unchecked;
            }
        }

        public override bool LockHeight
        {
            get
            {
                return false;
            }
        }

        public override bool LockWith
        {
            get
            {
                return false;
            }
        }

        public override Size MeasureSize(Graphics g)//有待完善
        {
            int iWidth = 0;
            int iHeight = 0;
            SizeF size;
            Rectangle rectangle;
            switch (this.eDisplayStyle)
            {
                case DisplayStyle.eText:
                    size = g.MeasureString(this.Text, this.Font);
                    iWidth = (int)(size.Width + 1);
                    iHeight = (int)(size.Height + 1);
                    break;
                case DisplayStyle.eImage:
                    if (this.Image != null)
                    {
                        rectangle = this.ImageRectangle;
                        iWidth += rectangle.Width;
                        iHeight += rectangle.Height;
                    }
                    break;
                case DisplayStyle.eImageAndText:
                    size = g.MeasureString(this.Text, this.Font);
                    iWidth = (int)(size.Width + 1);
                    iHeight = (int)(size.Height + 1);
                    if (this.Image != null)
                    {
                        switch (this.ImageAlign)
                        {
                            case ContentAlignment.BottomLeft:
                            case ContentAlignment.BottomRight:
                            case ContentAlignment.TopLeft:
                            case ContentAlignment.TopRight:
                                rectangle = this.ImageRectangle;
                                iWidth += rectangle.Width + this.ITSpace;
                                iHeight += rectangle.Height;
                                break;
                            case ContentAlignment.BottomCenter:
                            case ContentAlignment.TopCenter:
                                iHeight += this.ImageRectangle.Height + this.ITSpace;
                                break;
                            case ContentAlignment.MiddleLeft:
                            case ContentAlignment.MiddleRight:
                                iWidth += this.ImageRectangle.Width + this.ITSpace;
                                break;
                            case ContentAlignment.MiddleCenter:
                                rectangle = this.ImageRectangle;
                                if (iWidth < rectangle.Width) iWidth = rectangle.Width;
                                if (iHeight < rectangle.Height) iHeight = rectangle.Height;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;

            }
            iWidth += this.Padding.Left + CRT_CHECKSIZE + this.CDSpace + this.Padding.Right;
            iHeight += this.Padding.Top + this.Padding.Bottom;
            //
            if (iWidth < 16) iWidth = 16;
            if (iHeight < 16) iHeight = 16;
            //
            return new Size(iWidth, iHeight);
        }

        #region Clone
        public override object Clone()
        {
            ImageCheckBoxItem baseItem = new ImageCheckBoxItem();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.Padding = this.Padding;
            baseItem.TextAlign = this.TextAlign;
            baseItem.Visible = this.Visible;
            //
            baseItem.CheckState = this.CheckState;
            //
            baseItem.Image = this.Image;
            baseItem.ImageAlign = this.ImageAlign;
            if (this.GetEventState("VisibleChanged") == EventStateStyle.eUsed) baseItem.VisibleChanged += new EventHandler(baseItem_VisibleChanged);
            if (this.GetEventState("SizeChanged") == EventStateStyle.eUsed) baseItem.SizeChanged += new EventHandler(baseItem_SizeChanged);
            if (this.GetEventState("Paint") == EventStateStyle.eUsed) baseItem.Paint += new PaintEventHandler(baseItem_Paint);
            if (this.GetEventState("MouseUp") == EventStateStyle.eUsed) baseItem.MouseUp += new MouseEventHandler(baseItem_MouseUp);
            if (this.GetEventState("MouseMove") == EventStateStyle.eUsed) baseItem.MouseMove += new MouseEventHandler(baseItem_MouseMove);
            if (this.GetEventState("MouseLeave") == EventStateStyle.eUsed) baseItem.MouseLeave += new EventHandler(baseItem_MouseLeave);
            if (this.GetEventState("MouseEnter") == EventStateStyle.eUsed) baseItem.MouseEnter += new EventHandler(baseItem_MouseEnter);
            if (this.GetEventState("MouseDown") == EventStateStyle.eUsed) baseItem.MouseDown += new MouseEventHandler(baseItem_MouseDown);
            if (this.GetEventState("MouseDoubleClick") == EventStateStyle.eUsed) baseItem.MouseDoubleClick += new MouseEventHandler(baseItem_MouseDoubleClick);
            if (this.GetEventState("MouseClick") == EventStateStyle.eUsed) baseItem.MouseClick += new MouseEventHandler(baseItem_MouseClick);
            if (this.GetEventState("LocationChanged") == EventStateStyle.eUsed) baseItem.LocationChanged += new EventHandler(baseItem_LocationChanged);
            if (this.GetEventState("EnabledChanged") == EventStateStyle.eUsed) baseItem.EnabledChanged += new EventHandler(baseItem_EnabledChanged);
            if (this.GetEventState("CheckedChanged") == EventStateStyle.eUsed) baseItem.CheckedChanged += new EventHandler(baseItem_CheckedChanged);
            return baseItem;
        }
        void baseItem_CheckedChanged(object sender, EventArgs e)
        {
            this.RelationEvent("CheckedChanged", e);
        }
        void baseItem_EnabledChanged(object sender, EventArgs e)
        {
            this.RelationEvent("EnabledChanged", e);
        }
        void baseItem_LocationChanged(object sender, EventArgs e)
        {
            this.RelationEvent("LocationChanged", e);
        }
        void baseItem_MouseClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseClick", e);
        }
        void baseItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseDoubleClick", e);
        }
        void baseItem_MouseDown(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseDown", e);
        }
        void baseItem_MouseEnter(object sender, EventArgs e)
        {
            this.RelationEvent("MouseEnter", e);
        }
        void baseItem_MouseLeave(object sender, EventArgs e)
        {
            this.RelationEvent("MouseLeave", e);
        }
        void baseItem_MouseMove(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseMove", e);
        }
        void baseItem_MouseUp(object sender, MouseEventArgs e)
        {
            this.RelationEvent("MouseUp", e);
        }
        void baseItem_Paint(object sender, PaintEventArgs e)
        {
            this.RelationEvent("Paint", e);
        }
        void baseItem_SizeChanged(object sender, EventArgs e)
        {
            this.RelationEvent("SizeChanged", e);
        }
        void baseItem_VisibleChanged(object sender, EventArgs e)
        {
            this.RelationEvent("VisibleChanged", e);
        }
        #endregion

        protected override void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderCheckBox(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            //
            switch (this.eDisplayStyle)
            {
                case DisplayStyle.eImage:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                        new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                    break;
                case DisplayStyle.eText:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                        new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                    break;
                case DisplayStyle.eImageAndText:
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                        new GISShare.Controls.WinForm.ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.Image, this.ImageRectangle));
                    GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                        new GISShare.Controls.WinForm.TextRenderEventArgs(e.Graphics, this, this.Enabled, this.Text, this.ForeColor, this.Font, this.TextRectangle));
                    break;
                default:
                    break;
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                if (!this.InsideCheckRectangleTrigger || this.CheckRectangle.Contains(mevent.Location))
                {
                    this.Checked = !this.Checked;
                }
            }
            base.OnMouseUp(mevent);
        }
    }
}
