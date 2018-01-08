using System.Diagnostics;

namespace FileHelperTool.CRUD
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Policy;
    using System.Text;
    using System.Threading.Tasks;
    using FileHelperTool.Path;
    using HtmlAgilityPack;

    public static class CREATE_Help
    {


        public static void SaveHTMLFile(HtmlDocument htmlDoc, string fileLocation, string fileName)
        {
            try
            {
                using (FileStream sw = new FileStream($"{Path_Help.ReturnUniqueFileName(fileLocation, fileName, Path_Help.FileNameMode.Increment)}", FileMode.Create))
                {
                    htmlDoc.Save(sw);
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }


    }

    
}
