using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog
{
    public enum FieldTypes: int
    {
        Text = 0,
        Image = 1,
        Path = 2,
        DateTime = 3,
        Hyperlink = 4,
        Notification = 5, // remember, what is this?
        LinkToItem = 6,

    }
}
