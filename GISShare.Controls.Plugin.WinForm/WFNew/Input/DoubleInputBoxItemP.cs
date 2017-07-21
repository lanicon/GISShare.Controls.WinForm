﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew
{
    public class DoubleInputBoxItemP : IPlugin, IPluginInfo , IEventChain, IPlugin2, ISetEntityObject, IBaseItemP_, ITextBoxItemP, IDoubleInputBoxItemP
    {
        #region 私有属性
        private object m_EntityObject;
        #endregion

        #region 受保护的属性
        protected string _Name = "GISShare.Controls.Plugin.WinForm.WFNew.DoubleInputBoxItemP";
        //
        protected string _Text = "GISShare.Controls.Plugin.WinForm.WFNew.DoubleInputBoxItemP";
        protected bool _Visible = true;
        protected bool _Enabled = true;
        protected System.Windows.Forms.Padding _Padding = new System.Windows.Forms.Padding(0);
        protected System.Drawing.Font _Font = new System.Drawing.Font("宋体", 9f);
        protected System.Drawing.Color _ForeColor = System.Drawing.SystemColors.ControlText;
        protected System.Drawing.Size _Size = new System.Drawing.Size(120, 21);
        protected bool _Checked = false;
        protected bool _LockHeight = false;
        protected bool _LockWith = false;
        protected string _Category = "默认";
        protected System.Drawing.Size _MinimumSize = new System.Drawing.Size(10, 10);
        protected bool _UsingViewOverflow = true;
        //
        protected GISShare.Controls.WinForm.WFNew.BorderStyle _eBorderStyle = Controls.WinForm.WFNew.BorderStyle.eSingle;
        protected char _PasswordChar = '\0';
        protected bool _CanEdit = true;
        //
        protected double _Value = 0;
        protected double _Step = 1;
        protected double _Minimum = int.MinValue;
        protected double _Maximum = int.MaxValue;
        protected int _FloatLength = 6;
        protected int _ShowFloatLength = -1;
        #endregion

        #region IPlugin
        /// <summary>
        /// 唯一编码（必须设置）
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
        }

        /// <summary>
        /// 接口所在的分类（用于标识自身反射对象的分类）
        /// </summary>
        public int CategoryIndex
        {
            get
            {
                return (int)CategoryIndex_1_Style.eDoubleInputBoxItem;
            }
        }
        #endregion

        #region IPluginInfo
        public virtual string GetDescribe()
        {
            return this.Text;
        }
        #endregion

        #region IInteraction
        public virtual object CommandOperation(int iOperationStyle, params object[] objs)
        {
            return null;
        }
        #endregion

        #region IEventChain
        /// <summary>
        /// 事件触发
        /// </summary>
        /// <param name="iEventStyle">事件类型</param>
        /// <param name="e">事件参数</param>
        public virtual void OnTriggerEvent(int iEventStyle, EventArgs e) { }
        #endregion

        #region IPlugin2
        public object EntityObject
        {
            get { return m_EntityObject; }
        }

        public virtual void OnCreate(object hook) { }
        #endregion

        #region ISetEntityObject
        void ISetEntityObject.SetEntityObject(object obj)
        {
            this.m_EntityObject = obj;
        }
        #endregion

        #region IBaseItemP_
        public string Text
        {
            get { return _Text; }
        }

        public bool Visible
        {
            get { return _Visible; }
        }

        public bool Enabled
        {
            get { return _Enabled; }
        }

        public System.Windows.Forms.Padding Padding
        {
            get { return _Padding; }
        }

        public System.Drawing.Font Font
        {
            get { return _Font; }
        }

        public System.Drawing.Color ForeColor
        {
            get { return _ForeColor; }
        }

        public System.Drawing.Size Size
        {
            get { return _Size; }
        }

        public bool Checked
        {
            get { return _Checked; }
        }

        public bool LockHeight
        {
            get { return _LockHeight; }
        }

        public bool LockWith
        {
            get { return _LockWith; }
        }

        public string Category
        {
            get { return _Category; }
        }

        public System.Drawing.Size MinimumSize
        {
            get { return _MinimumSize; }
        }

        public bool UsingViewOverflow
        {
            get { return _UsingViewOverflow; }
        }
        #endregion

        #region ITextBoxItemP
        public GISShare.Controls.WinForm.WFNew.BorderStyle eBorderStyle
        {
            get { return _eBorderStyle; }
        }

        public char PasswordChar
        {
            get { return _PasswordChar; }
        }

        public bool CanEdit
        {
            get { return _CanEdit; }
        }
        #endregion

        #region IDoubleInputBoxItemP
        public double Value
        {
            get { return _Value; }
        }

        public double Step
        {
            get { return _Step; }
        }

        public double Minimum
        {
            get { return _Minimum; }
        }

        public double Maximum
        {
            get { return _Maximum; }
        }

        public int FloatLength
        {
            get { return _FloatLength; }
        }

        public int ShowFloatLength
        {
            get { return _ShowFloatLength; }
        }
        #endregion

        //
        //
        //

        public static GISShare.Controls.WinForm.WFNew.DoubleInputBoxItem CreateDoubleInputBoxItem(IDoubleInputBoxItemP pBaseItemP)
        {
            GISShare.Controls.WinForm.WFNew.DoubleInputBoxItem baseItem = new Controls.WinForm.WFNew.DoubleInputBoxItem();

            //IPlugin
            baseItem.Name = pBaseItemP.Name;
            //ISetEntityObject
            GISShare.Controls.Plugin.ISetEntityObject pSetEntityObject = pBaseItemP as GISShare.Controls.Plugin.ISetEntityObject;
            if (pSetEntityObject != null) pSetEntityObject.SetEntityObject(baseItem);
            //IBaseItemP_
            baseItem.Checked = pBaseItemP.Checked;
            baseItem.Enabled = pBaseItemP.Enabled;
            baseItem.Font = pBaseItemP.Font;
            baseItem.ForeColor = pBaseItemP.ForeColor;
            baseItem.LockHeight = pBaseItemP.LockHeight;
            baseItem.LockWith = pBaseItemP.LockWith;
            baseItem.Padding = pBaseItemP.Padding;
            baseItem.Size = pBaseItemP.Size;
            baseItem.Text = pBaseItemP.Text;
            baseItem.Visible = pBaseItemP.Visible;
            baseItem.Category = pBaseItemP.Category;
            baseItem.MinimumSize = pBaseItemP.MinimumSize;
            baseItem.UsingViewOverflow = pBaseItemP.UsingViewOverflow;
            //ITextBoxItemP
            baseItem.eBorderStyle = pBaseItemP.eBorderStyle;
            //baseItem.PasswordChar = pBaseItemP.PasswordChar;
            baseItem.CanEdit = pBaseItemP.CanEdit;
            //IDoubleInputBoxItemP
            baseItem.Value = pBaseItemP.Value;
            baseItem.Step = pBaseItemP.Step;
            baseItem.Minimum = pBaseItemP.Minimum;
            baseItem.Maximum = pBaseItemP.Maximum;
            baseItem.FloatLength = pBaseItemP.FloatLength;
            baseItem.ShowFloatLength = pBaseItemP.ShowFloatLength;

            return baseItem;
        }
    }
}
