﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public class ResizeViewItemEx : ResizeViewItem
    {
        public ResizeViewItemEx() { }

        public ResizeViewItemEx(string text)
            : base(text) { }

        public ResizeViewItemEx(string name, string text)
            : base(name, text) { }

        public ResizeViewItemEx(string name, string text, Font font)
            : base(name, text, font) { }

        private object m_Tag;
        [Browsable(true), DefaultValue(""), TypeConverter(typeof(StringConverter)), Description("用来携带附加信息"), Category("数据")]
        public object Tag
        {
            get { return m_Tag; }
            set { m_Tag = value; }
        }
    }
}
