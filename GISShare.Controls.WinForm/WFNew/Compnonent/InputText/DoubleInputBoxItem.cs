using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    [DefaultEvent("ValueChanged")]
    public class DoubleInputBoxItem : BaseItem, IDoubleInputBoxItem, IDoubleInputBoxItemEvent, IUpDownButtonObjectHelper, IInputObject, IInputObject2, IInputObjectHelper
    {
        private const int CTR_BORDERSPASE = 3;
        private const int CTR_SPASE_LR = 1;
        //
        private const int CTR_UPDOWNBUTTONWIDTH = 17;
        private const int CTR_UPDOWNBUTTONHEIGHT = 7;
        private const int CTR_ARROWWIDTH = 6;
        private const int CTR_ARROWHEIGHT = 6;

        private UpButtonItem m_UpButtonItem;
        private DownButtonItem m_DownButtonItem;
        private IDoubleInputRegion m_InputRegion;

        #region 构造函数
        public DoubleInputBoxItem()
            : base()
        {
            this.m_UpButtonItem = new UpButtonItem();
            ((ISetOwnerHelper)this.m_UpButtonItem).SetOwner(this);
            this.m_DownButtonItem = new DownButtonItem();
            ((ISetOwnerHelper)this.m_DownButtonItem).SetOwner(this);
            //
            this.m_InputRegion = new DoubleInputRegion(this);
            this.m_InputRegion.PopupClosed += new EventHandler(InputRegion_PopupClosed);
            //
            if (this.Text == null || this.Text.Length <= 0)
            {
                if (this.Minimum > 0) { this.Text = this.Minimum.ToString(); }
                else { this.Text = "0"; }
            }
            //
            //
            //
            this.Size = new Size(120, 21);
        }
        void InputRegion_PopupClosed(object sender, EventArgs e)
        {
            this.Refresh();
        }

        //public DoubleInputBoxItem(GISShare.Controls.Plugin.WFNew.IDoubleInputBoxItemP pBaseItemP)
        //    : this()
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
        //    //ITextBoxItemP
        //    this.eBorderStyle = pBaseItemP.eBorderStyle;
        //    //this.PasswordChar = pBaseItemP.PasswordChar;
        //    this.CanEdit = pBaseItemP.CanEdit;
        //    //IDoubleInputBoxItemP
        //    this.Value = pBaseItemP.Value;
        //    this.Step = pBaseItemP.Step;
        //    this.Minimum = pBaseItemP.Minimum;
        //    this.Maximum = pBaseItemP.Maximum;
        //    this.FloatLength = pBaseItemP.FloatLength;
        //    this.ShowFloatLength = pBaseItemP.ShowFloatLength;
        //}
        #endregion

        protected override EventStateStyle GetEventStateSupplement(string strEventName)
        {
            switch (strEventName)
            {
                case "ValueChanged":
                    return this.ValueChanged != null ? EventStateStyle.eUsed : EventStateStyle.eUnused;
                default:
                    break;
            }
            //
            return base.GetEventStateSupplement(strEventName);
        }

        protected override bool RelationEventSupplement(string strEventName, EventArgs e)
        {
            switch (strEventName)
            {
                case "ValueChanged":
                    if (this.ValueChanged != null) { this.ValueChanged(this, e as DoubleValueChangedEventArgs); }
                    return true;
                default:
                    break;
            }
            //
            return base.RelationEventSupplement(strEventName, e);
        }

        #region IInputObject
        string m_InputText = "0";
        string IInputObject.InputText
        {
            get { return this.m_InputText; }
            set
            {
                if (this.m_InputText == value) return;
                //
                double dOld = this.Value;
                this.m_InputText = value;
                this.OnValueChanged(new DoubleValueChangedEventArgs(dOld, this.Value));
            }
        }

        Font IInputObject.InputFont
        {
            get { return base.Font; }
        }

        Color IInputObject.InputForeColor
        {
            get { return base.ForeColor; }
        }

        [Browsable(false), Description("输入区矩形框（屏幕坐标）"), Category("布局")]
        Rectangle IInputObject.InputRegionRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                int iH = this.m_InputRegion.GetInputRegionSize().Height;
                switch (this.eBorderStyle)
                {
                    case BorderStyle.eNone:
                        return new Rectangle
                            (
                            this.PointToScreen(new Point(rectangle.Location.X, (rectangle.Top + rectangle.Bottom - iH) / 2)),
                            new Size(rectangle.Width - CTR_UPDOWNBUTTONWIDTH, iH)
                            );
                    case BorderStyle.eSingle:
                    default:
                        return new Rectangle
                            (
                            this.PointToScreen(new Point(rectangle.Location.X + CTR_BORDERSPASE, (rectangle.Top + rectangle.Bottom - iH) / 2)),
                            new Size(rectangle.Width - 2 * CTR_BORDERSPASE - CTR_UPDOWNBUTTONWIDTH, iH)
                            );
                }
            }
        }

        [Browsable(false), Description("是否正在输入"), Category("状态")]
        public bool IsInputing
        {
            get { return this.m_InputRegion.IsOpened; }
        }
        #endregion

        #region IInputObject2
        [Browsable(false), DefaultValue(false), Description("输入时就开始过滤"), Category("属性")]
        public bool InputingFilterText
        {
            get { return this.m_InputRegion.InputingFilterText; }
            set { this.m_InputRegion.InputingFilterText = value; }
        }
        #endregion

        #region IInputObjectHelper
        IInputRegion IInputObjectHelper.GetInputRegion()
        {
            return this.m_InputRegion;
        }
        #endregion

        #region ITextBoxItem
        bool m_CanEdit = true;
        [Browsable(true), DefaultValue(true), Description("是否可以编辑"), Category("状态")]
        public virtual bool CanEdit
        {
            get { return m_CanEdit; }
            set { m_CanEdit = value; }
        }

        GISShare.Controls.WinForm.WFNew.BorderStyle m_eBorderStyle = GISShare.Controls.WinForm.WFNew.BorderStyle.eSingle;
        [Browsable(true), DefaultValue(typeof(GISShare.Controls.WinForm.WFNew.BorderStyle), "eSingle"), Description("外框类型"), Category("外观")]
        public GISShare.Controls.WinForm.WFNew.BorderStyle eBorderStyle
        {
            get { return m_eBorderStyle; }
            set
            {
                if (m_eBorderStyle == value) return;
                m_eBorderStyle = value;
                this.Refresh();
            }
        }

        [Browsable(false), Description("其外框矩形"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            }
        }

        [Browsable(false), Description("文本绘制矩形框"), Category("布局")]
        public virtual Rectangle TextRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                int iH = this.m_InputRegion.GetInputRegionSize().Height;
                switch (this.eBorderStyle)
                {
                    case BorderStyle.eNone:
                        return new Rectangle
                            (
                            rectangle.X,
                            (rectangle.Top + rectangle.Bottom - iH) / 2,
                            rectangle.Width - CTR_UPDOWNBUTTONWIDTH,
                            iH
                            );
                    case BorderStyle.eSingle:
                    default:
                        return new Rectangle
                            (
                            rectangle.X + CTR_SPASE_LR,
                            (rectangle.Top + rectangle.Bottom - iH) / 2,
                            rectangle.Width - 2 * CTR_SPASE_LR - CTR_UPDOWNBUTTONWIDTH,
                            iH
                            );
                }
            }
        }
        #endregion

        #region IDoubleInputBoxItemEvent
        public event DoubleValueChangedHandler ValueChanged;
        #endregion

        #region IDoubleInputBoxItem
        double m_Step = 1;
        [Browsable(true), DefaultValue(1), Description("增量"), Category("属性")]
        public double Step
        {
            get { return m_Step; }
            set { m_Step = value; }
        }

        [Browsable(true), Description("获取当前值"), Category("属性")]
        public double Value
        {
            get
            {
                double dValue;
                if (double.TryParse(this.m_InputText, out dValue))
                {
                    return dValue;
                }
                else
                {
                    return this.Minimum > 0 ? this.Minimum : 0;
                }
            }
            set
            {
                string strValue = value.ToString();
                if (GISShare.Controls.WinForm.Util.UtilCompare.CompareNumEx(strValue, this.Minimum.ToString(".")) >= 0 &&
                    GISShare.Controls.WinForm.Util.UtilCompare.CompareNumEx(strValue, this.Maximum.ToString(".")) <= 0)
                {
                    ((IInputObject)this).InputText = strValue;
                }
            }
        }

        [Browsable(true), Description("最小值"), Category("属性")]
        public double Minimum
        {
            get { return this.m_InputRegion.Minimum; }
            set { this.m_InputRegion.Minimum = value; }
        }

        [Browsable(true), Description("最大值"), Category("属性")]
        public double Maximum
        {
            get { return this.m_InputRegion.Maximum; }
            set { this.m_InputRegion.Maximum = value; }
        }

        [Browsable(true), DefaultValue(6), Description("浮点长度（最大值100）"), Category("属性")]
        public int FloatLength
        {
            get { return this.m_InputRegion.FloatLength; }
            set { this.m_InputRegion.FloatLength = value; }
        }

        private int m_ShowFloatLength = -1;
        [Browsable(true), DefaultValue(-1), Description("显示的浮点长度（与值本身无关）"), Category("属性")]
        public int ShowFloatLength
        {
            get { return m_ShowFloatLength; }
            set { m_ShowFloatLength = value; }
        }
        #endregion

        #region IUpDownButtonObjectHelper
        bool IUpDownButtonObjectHelper.UpButtonEnabled
        {
            get { return this.CanEdit && this.Value < this.Maximum; }
        }

        bool IUpDownButtonObjectHelper.DownButtonEnabled
        {
            get { return this.CanEdit && this.Value > this.Minimum; }
        }

        bool IUpDownButtonObjectHelper.UpButtonVisible
        {
            get { return (this.Width > CTR_UPDOWNBUTTONWIDTH + 2) && (this.m_UpDownButtonHeight >= CTR_UPDOWNBUTTONHEIGHT); }
        }

        bool IUpDownButtonObjectHelper.DownButtonVisible
        {
            get { return (this.Width > CTR_UPDOWNBUTTONWIDTH + 2) && (this.m_UpDownButtonHeight >= CTR_UPDOWNBUTTONHEIGHT); }
        }

        int m_UpDownButtonHeight = CTR_UPDOWNBUTTONHEIGHT;
        Rectangle IUpDownButtonObjectHelper.UpButtonRectangle
        {
            get 
            {
                Rectangle rectangle = this.DisplayRectangle;
                switch (this.eBorderStyle) 
                {
                    case BorderStyle.eNone:
                        this.m_UpDownButtonHeight = rectangle.Height / 2;
                        return new Rectangle(rectangle.Right - CTR_UPDOWNBUTTONWIDTH, rectangle.Top, CTR_UPDOWNBUTTONWIDTH, this.m_UpDownButtonHeight);
                    case BorderStyle.eSingle:
                        this.m_UpDownButtonHeight = (rectangle.Height - 4) / 2;
                        return new Rectangle(rectangle.Right - CTR_UPDOWNBUTTONWIDTH, rectangle.Top + 2, CTR_UPDOWNBUTTONWIDTH - 2, this.m_UpDownButtonHeight);
                    default:
                        return Rectangle.Empty;
                }
            }
        }

        Rectangle IUpDownButtonObjectHelper.DownButtonRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                switch (this.eBorderStyle)
                {
                    case BorderStyle.eNone:
                        //this.m_UpDownButtonHeight = rectangle.Height / 2;
                        return new Rectangle(rectangle.Right - CTR_UPDOWNBUTTONWIDTH, rectangle.Bottom - this.m_UpDownButtonHeight, CTR_UPDOWNBUTTONWIDTH, this.m_UpDownButtonHeight);
                    case BorderStyle.eSingle:
                        //this.m_UpDownButtonHeight = (rectangle.Height - 2) / 2;
                        return new Rectangle(rectangle.Right - CTR_UPDOWNBUTTONWIDTH, rectangle.Bottom - this.m_UpDownButtonHeight - 2, CTR_UPDOWNBUTTONWIDTH - 2, this.m_UpDownButtonHeight);
                    default:
                        return Rectangle.Empty;
                }
            }
        }

        void IUpDownButtonObjectHelper.UpButtonOperation()
        {
            this.Value += this.Step;
        }

        void IUpDownButtonObjectHelper.DownButtonOperation()
        {
            this.Value -= this.Step;
        }
        #endregion

        protected override bool RefreshBaseItemState
        {
            get
            {
                return true;
            }
        }

        public override BaseItemState eBaseItemState
        {
            get
            {
                if (this.IsInputing) return BaseItemState.eHot;
                return base.eBaseItemState;
            }
        }

        public override bool LockHeight
        {
            get { return true; }
        }

        public override bool LockWith
        {
            get { return false; }
        }

        #region Clone
        public override object Clone()
        {
            DoubleInputBoxItem baseItem = new DoubleInputBoxItem();
            baseItem.Checked = this.Checked;
            baseItem.Enabled = this.Enabled;
            baseItem.Font = this.Font;
            baseItem.ForeColor = this.ForeColor;
            baseItem.Name = this.Name;
            baseItem.Site = this.Site;
            baseItem.Size = this.Size;
            baseItem.Tag = this.Tag;
            baseItem.Text = this.Text;
            baseItem.Visible = this.Visible;
            //
            baseItem.Minimum = this.Minimum;
            baseItem.Maximum = this.Maximum;
            baseItem.Value = this.Value;
            baseItem.FloatLength = this.FloatLength;
            baseItem.CanEdit = this.CanEdit;
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
            if (this.GetEventState("TextChanged") == EventStateStyle.eUsed) baseItem.TextChanged += new EventHandler(baseItem_TextChanged);
            if (this.GetEventState("KeyDown") == EventStateStyle.eUsed) baseItem.KeyDown += new KeyEventHandler(baseItem_KeyDown);
            if (this.GetEventState("KeyPress") == EventStateStyle.eUsed) baseItem.KeyPress += new KeyPressEventHandler(baseItem_KeyPress);
            if (this.GetEventState("KeyUp") == EventStateStyle.eUsed) baseItem.KeyUp += new KeyEventHandler(baseItem_KeyUp);
            if (this.GetEventState("ValueChanged") == EventStateStyle.eUsed) baseItem.ValueChanged += new DoubleValueChangedHandler(baseItem_ValueChanged);
            return baseItem;
        }
        void baseItem_ValueChanged(object sender, DoubleValueChangedEventArgs e)
        {
            this.RelationEvent("ValueChanged", e);
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
        void baseItem_KeyUp(object sender, KeyEventArgs e)
        {
            this.RelationEvent("KeyUp", e);
        }
        void baseItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.RelationEvent("KeyPress", e);
        }
        void baseItem_KeyDown(object sender, KeyEventArgs e)
        {
            this.RelationEvent("KeyDown", e);
        }
        void baseItem_TextChanged(object sender, EventArgs e)
        {
            this.RelationEvent("TextChanged", e);
        }
        #endregion

        public override Size MeasureSize(Graphics g)//有待完善
        {
            return DisplayRectangle.Size;
        }

        /// <summary>
        /// 用来向下传递消息
        /// </summary>
        /// <param name="messageInfo"></param>
        protected override void MessageMonitor(MessageInfo messageInfo)
        {
            base.MessageMonitor(messageInfo);
            //
            ((IMessageChain)this.m_UpButtonItem).SendMessage(messageInfo);
            ((IMessageChain)this.m_DownButtonItem).SendMessage(messageInfo);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int iH = this.m_InputRegion.GetInputRegionSize().Height;
            switch (this.eBorderStyle)
            {
                case BorderStyle.eNone:
                    if (this.Height != iH) this.Size = new Size(this.Width, iH);
                    break;
                case BorderStyle.eSingle:
                default:
                    iH += 2 * CTR_BORDERSPASE;
                    if (this.Height != iH) this.Size = new Size(this.Width, iH);
                    break;
            }
            //
            base.OnPaint(e);
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderTextBox(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
            if (!this.IsInputing)
            {
                string strText = this.m_InputText;
                if (this.ShowFloatLength == 0)
                {
                    int iIndex = strText.IndexOf('.');
                    if (iIndex > 0) strText = strText.Substring(0, iIndex);
                }
                else if (this.ShowFloatLength > 0)
                {
                    int iIndex = strText.IndexOf('.');
                    if (iIndex >= 0)
                    {
                        int iLen = strText.Length - (strText.IndexOf('.') + 1);
                        if (strText.Length >= iLen + 2)
                        {
                            int iNum = this.ShowFloatLength - iLen;
                            if (iNum >= 0)
                            {
                                for (int i = 0; i < iNum; i++)
                                {
                                    strText += "0";
                                }
                            }
                            else
                            {
                                strText = strText.Substring(0, strText.Length + iNum);
                            }
                        }
                    }
                    else
                    {
                        strText += ".";
                        for (int i = 0; i < this.ShowFloatLength; i++)
                        {
                            strText += "0";
                        }
                    }
                }
                GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderTextBoxText(
                    new TextRenderEventArgs(e.Graphics, this, this.Enabled, strText, this.ForeColor, this.Font, this.TextRectangle));
            }
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            //
            if (this.Enabled && this.TextRectangle.Contains(mevent.Location)) this.m_InputRegion.Show();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            //
            this.Invalidate(this.TextRectangle);
        }

        //
        protected virtual void OnValueChanged(DoubleValueChangedEventArgs e)
        {
            if (this.ValueChanged != null) { this.ValueChanged(this, e); }
        }
    }
}
