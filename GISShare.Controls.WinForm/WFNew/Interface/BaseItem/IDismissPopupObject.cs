﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    public interface IDismissPopupObject
    {
        DismissPopupStyle eDismissPopupStyle { get; }

        Rectangle DismissTriggerRectangle { get; }
    }
}
