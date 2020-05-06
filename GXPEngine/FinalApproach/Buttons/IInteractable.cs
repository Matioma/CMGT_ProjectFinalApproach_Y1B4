using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
interface IInteractable
{
    void OnClick();
    void OnHover();
    void OnHoverEnd();
    void OnClickRelease();
    void OnClickPressed();
}

