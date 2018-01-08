using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelperTool.Model.Enums
{

    public enum HtmlDocumentFilterContentType
    {
        Body,
        Header
    }

    public enum HtmlDocumentFilterOptions
    {
        All,
        WithoutScript,
        WithoutStyle,
        WithoutScriptAndStyle
    }

    
}
