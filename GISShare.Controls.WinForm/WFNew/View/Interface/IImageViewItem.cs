using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IImageViewItem : ITextViewItem
    {
        Image Image { get; set; }

        Rectangle ImageRectangle { get; }
    }
}
