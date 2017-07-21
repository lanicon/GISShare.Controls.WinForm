﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.View
{
    public interface IGridNodeViewItemTreeEvent : INodeViewItemTreeEvent
    {
        event MouseEventHandler RowHeaderMouseClick;

        event MouseEventHandler RowHeaderMouseDoubleClick;

        event RowHeaderItemDrawEventHandler RowHeaderItemDrawing;
    }
}
