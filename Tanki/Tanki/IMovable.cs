using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanki
{
    interface IMovable
    {
        bool Move(Direction direction, int speed);
    }
}
