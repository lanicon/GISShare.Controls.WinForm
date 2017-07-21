﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.DockBar
{
    [ToolboxBitmap(typeof(MaskedTextBoxItem), "MaskedTextBoxItem.bmp"),
    ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class MaskedTextBoxItem : ToolStripControlHost, IBaseItemDB, WFNew.IObjectDesignHelper
    {
        public event MaskInputRejectedEventHandler MaskInputRejected;

        #region 构造函数
        public MaskedTextBoxItem()
            : base(CreateControlInstance()) 
        {
            base.Size = new Size(100, 15);
            base.Text = "MaskedTextBoxItem";
            this.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WinForm.DockBar.MaskedTextBoxItem.bmp"));
            //
            MaskedTextBox maskedTextBox = this.MaskedTextBoxControl;
            if (maskedTextBox != null) { maskedTextBox.MaskInputRejected += new MaskInputRejectedEventHandler(MaskedTextBox_MaskInputRejected); }
        }        
        private static Control CreateControlInstance()
        {
            ToolStripMaskedTextBoxControl maskedTextBox = new ToolStripMaskedTextBoxControl();
            maskedTextBox.Dock = DockStyle.Fill;
            //
            return maskedTextBox;
        }
        private void MaskedTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            this.OnMaskInputRejected(e);
        }

        //public MaskedTextBoxItem(GISShare.Controls.Plugin.WinForm.DockBar.IMaskedTextBoxItemP pBaseItemDBP)
        //    : this()
        //{
        //    //IPlugin
        //    this.Name = pBaseItemDBP.Name;
        //    //ISetEntityObject
        //    GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemDBP as GISShare.Controls.Plugin.ISetEntityObject;
        //    if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(this);
        //    //IBaseItemP_
        //    this.Category = pBaseItemDBP.Category;
        //    this.DisplayStyle = pBaseItemDBP.DisplayStyle;
        //    this.DoubleClickEnabled = pBaseItemDBP.DoubleClickEnabled;
        //    this.Enabled = pBaseItemDBP.Enabled;
        //    this.Font = pBaseItemDBP.Font;
        //    this.ForeColor = pBaseItemDBP.ForeColor;
        //    this.Image = pBaseItemDBP.Image;
        //    this.ImageAlign = pBaseItemDBP.ImageAlign;
        //    //this.ImageIndex = pBaseItemDBP.ImageIndex;
        //    //this.ImageKey = pBaseItemDBP.ImageKey;
        //    this.ImageScaling = pBaseItemDBP.ImageScaling;
        //    this.ImageTransparentColor = pBaseItemDBP.ImageTransparentColor;
        //    this.Margin = pBaseItemDBP.Margin;
        //    this.MergeAction = pBaseItemDBP.MergeAction;
        //    this.MergeIndex = pBaseItemDBP.MergeIndex;
        //    this.Overflow = pBaseItemDBP.Overflow;
        //    this.Padding = pBaseItemDBP.Padding;
        //    this.RightToLeft = pBaseItemDBP.RightToLeft;
        //    this.RightToLeftAutoMirrorImage = pBaseItemDBP.RightToLeftAutoMirrorImage;
        //    this.Size = pBaseItemDBP.Size;
        //    this.Text = pBaseItemDBP.Text;
        //    this.TextAlign = pBaseItemDBP.TextAlign;
        //    this.TextDirection = pBaseItemDBP.TextDirection;
        //    this.TextImageRelation = pBaseItemDBP.TextImageRelation;
        //    this.ToolTipText = pBaseItemDBP.ToolTipText;
        //    this.Visible = pBaseItemDBP.Visible;
        //}
        #endregion

        public MaskedTextBox MaskedTextBoxControl
        {
            get { return base.Control as MaskedTextBox; } 
        }

        public override string Text
        {
            get
            {
                MaskedTextBox maskedTextBox = this.Control as MaskedTextBox;
                if (maskedTextBox != null) return maskedTextBox.Text;
                return base.Text;
            }
            set
            {
                MaskedTextBox maskedTextBox = this.Control as MaskedTextBox;
                if (maskedTextBox != null)
                {
                    maskedTextBox.Text = value;
                }
                else
                {
                    base.Text = value;
                }
            }
        }

        #region WFNew.IObjectDesignHelper
        public void Refresh()
        {
            this.Invalidate(this.Bounds);
        }
        #endregion

        #region WinForm.IFontItem
        bool m_ShowBackColor = false;
        [Browsable(false), DefaultValue(false), Description("显示自定义列表区的背景色"), Category("外观")]
        public bool ShowBackColor
        {
            get { return m_ShowBackColor; }
            set { m_ShowBackColor = value; }
        }
        #endregion

        #region IBaseItemDB
        private string m_Category = Language.LanguageStrategy.DefaultText;//"默认";
        [Browsable(true), DefaultValue("默认"), Description("该项所处的分类"), Category("属性")]
        public string Category
        {
            get { return m_Category; }
            set { m_Category = value; }
        }
        #endregion

        #region Clone
        public virtual ToolStripItem Clone()
        {
            MaskedTextBoxItem item = new MaskedTextBoxItem();
            item.MaskedTextBoxControl.Name = this.MaskedTextBoxControl.Name;
            item.MaskedTextBoxControl.Text = this.MaskedTextBoxControl.Text;
            item.MaskedTextBoxControl.Tag = this.MaskedTextBoxControl.Tag;
            //
            item.Category = this.Category;
            item.Name = this.Name + "[GUID]" + System.Guid.NewGuid().ToString();
            item.Text = this.Text;
            item.DisplayStyle = this.DisplayStyle;
            item.ImageAlign = this.ImageAlign;
            item.ImageIndex = this.ImageIndex;
            item.ImageKey = this.ImageKey;
            item.ImageScaling = this.ImageScaling;
            item.ImageTransparentColor = this.ImageTransparentColor;
            item.TextImageRelation = this.TextImageRelation;
            if (this.Image != null) item.Image = this.Image.Clone() as Image;
            item.ToolTipText = this.ToolTipText;
            item.Tag = this.Tag;
            item.BackgroundImage = this.BackgroundImage;
            item.BackgroundImageLayout = this.BackgroundImageLayout;
            item.BackColor = this.BackColor;
            item.DoubleClickEnabled = this.DoubleClickEnabled;
            item.MaskInputRejected += new MaskInputRejectedEventHandler(item_MaskInputRejected);
            item.EnabledChanged += new EventHandler(item_EnabledChanged);
            item.VisibleChanged += new EventHandler(item_VisibleChanged);
            item.KeyDown += new KeyEventHandler(item_KeyDown);
            item.KeyPress += new KeyPressEventHandler(item_KeyPress);
            item.KeyUp += new KeyEventHandler(item_KeyUp);
            return item;
        }
        void item_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            this.OnMaskInputRejected(e);
        }
        void item_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }
        void item_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }
        void item_KeyDown(object sender, KeyEventArgs e)
        {
            this.OnKeyDown(e);
        }
        void item_EnabledChanged(object sender, EventArgs e)
        {
            this.OnEnabledChanged(e);
        }
        void item_VisibleChanged(object sender, EventArgs e)
        {
            this.OnVisibleChanged(e);
        }
        #endregion

        //事件
        protected virtual void OnMaskInputRejected(MaskInputRejectedEventArgs e)
        {
            if (this.MaskInputRejected != null) { this.MaskInputRejected(this, e); }
        }

        //
        //
        //

        class ToolStripMaskedTextBoxControl : MaskedTextBox, ITextBoxItemDB
        {
            ToolStripControlHost m_Owner;

            public ToolStripMaskedTextBoxControl()
                : base() { }

            public void SetOwner(ToolStripControlHost owner)
            {
                this.m_Owner = owner;
            }

            #region WFNew.IBaseItem
            WFNew.RenderStyle m_eRenderStyle = WFNew.RenderStyle.eSystem;
            [Browsable(true), DefaultValue(typeof(WFNew.RenderStyle), "eSystem"), Description("渲染类型"), Category("外观")]
            public virtual WFNew.RenderStyle eRenderStyle
            {
                get { return m_eRenderStyle; }
                set { m_eRenderStyle = value; }
            }

            private WFNew.BaseItemState m_eBaseItemState = WFNew.BaseItemState.eNormal;
            protected virtual bool SetBaseItemState(WFNew.BaseItemState baseItemState)
            {
                if (this.m_eBaseItemState == baseItemState) return false;
                this.m_eBaseItemState = baseItemState;
                return true;
            }
            protected virtual bool SetBaseItemStateEx(WFNew.BaseItemState baseItemState)
            {
                if (this.m_eBaseItemState == baseItemState) return false;
                this.m_eBaseItemState = baseItemState;
                this.Refresh();
                return true;
            }
            [Browsable(false), Description("自身所处的状态（激活、按下、不可用、正常）"), Category("状态")]
            public virtual WFNew.BaseItemState eBaseItemState
            {
                get
                {
                    return m_eBaseItemState;
                }
            }
            #endregion

            #region IBaseTextBoxItem
            [Browsable(false), Description("其外框矩形"), Category("布局")]
            public Rectangle FrameRectangle
            {
                get
                {
                    return new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                }
            }
            #endregion

            [Browsable(false)]
            public override bool Multiline
            {
                get
                {
                    return false;
                }
                set
                {
                    base.Multiline = value;
                }
            }

            protected override void OnMouseDown(MouseEventArgs mevent)
            {
                this.SetBaseItemState(this.ContainsFocus ? WFNew.BaseItemState.eHot : GISShare.Controls.WinForm.WFNew.BaseItemState.ePressed);
                base.OnMouseDown(mevent);
            }

            protected override void OnMouseUp(MouseEventArgs mevent)
            {
                if (this.DisplayRectangle.Contains(mevent.Location)) { this.SetBaseItemState(WFNew.BaseItemState.eHot); }
                else { this.SetBaseItemState(WFNew.BaseItemState.eNormal); }
                base.OnMouseUp(mevent);
            }

            protected override void OnMouseLeave(EventArgs e)
            {
                this.SetBaseItemState(WFNew.BaseItemState.eNormal);
                base.OnMouseLeave(e);
            }

            protected override void OnMouseEnter(EventArgs e)
            {
                this.SetBaseItemState(WFNew.BaseItemState.eHot);
                base.OnMouseEnter(e);
            }

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case (int)GISShare.Win32.Msgs.WM_NCPAINT:
                        //System.Diagnostics.Debug.WriteLine(String.Format("{0}----{1}", this.Name, "WM_NCPAINT"));
                        base.WndProc(ref m);
                        if (this.BorderStyle != BorderStyle.None) this.WmNCPaint(ref m);
                        return;

                }
                //
                base.WndProc(ref m);
            }
            private void WmNCPaint(ref Message m)
            {
                IntPtr iHandle = GISShare.Win32.API.GetWindowDC(m.HWnd);
                try
                {
                    Graphics g = Graphics.FromHdc(iHandle);
                    //
                    this.OnNCPaint(new PaintEventArgs(g, this.DisplayRectangle));
                    //
                    g.Dispose();
                }
                catch { }
                finally
                {
                    GISShare.Win32.API.ReleaseDC(m.HWnd, iHandle);
                }
            }

            protected virtual void OnNCPaint(PaintEventArgs e)
            {
                this.OnNCDraw(e);
            }

            protected virtual void OnNCDraw(PaintEventArgs e)
            {
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderTextBoxC
                    (
                    new ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle)
                    );
            }
        }
    }
}
