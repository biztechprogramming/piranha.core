using Piranha.Data;
using Piranha.Extend.Blocks;
using Piranha.Extend;
using System.ComponentModel;
using System.Xml.Linq;

namespace RazorWeb.Models
{
    [BlockType(Name = "Html", Category = "Content", Icon = "fab fa-html5", Component = "rawhtml-block")]
    public class RawHtmlBlock : TextBlock
    {
    }
}
