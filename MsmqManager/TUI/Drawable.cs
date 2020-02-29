using MsmqManager.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsmqManager.TUI
{
    public interface Drawable
    {
        Coords Coords { get; set; }
        void Display();
        void Clean();

    }
}
