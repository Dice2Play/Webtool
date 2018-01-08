using System.Diagnostics;

namespace FileHelperTool.Path
{

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;


    public static class Path_Help
    {
        public enum FileNameMode
        {
            Increment,  // When there are more files with the same name are expected
            Overwrite,  // If there already exists a file with the same name it will be overwritten
            Default,    // Throws error when there already exists a file with the same name

        }

        #region Public methods

        // Returns an unique file name 
        public static string ReturnUniqueFileName(string directory, string fileName, FileNameMode mode)
        {
            switch (mode)
            {
                case FileNameMode.Default:
                    if (ExistsFileWithSameName(fileName, directory))
                    {
                        throw new Exception("There already exists a file with the same name.");
                    }

                    return fileName;

                case FileNameMode.Increment:
                    return ReturnNewIncrementFileName(fileName, directory);


                case FileNameMode.Overwrite:
                    return fileName;
                    

                default: return "";
            }
        }

        public static string ReturnProjectDirectoryPath()
        {
            return (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }
        
        #endregion
         

        #region Private helpers
        // Checks whether filename already exists
        private static bool ExistsFileWithSameName(string fileName, string directory)
        {
            return Directory.GetFiles(@directory).Any(x => x.Contains(fileName)); 
        }

        // Returns an unique incremented filename
        private static string ReturnNewIncrementFileName(string fileName, string directory)
        {
            string fileExtension = Path.GetExtension(fileName);
            string fileWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            for (int i = 0; ; i++)
            {

                if (!ExistsFileWithSameName(
                    $"{fileWithoutExtension}_{i}{fileExtension}",
                    directory))
                    {
                        #if DEBUG
                        Debug.WriteLine($"Generated the following file name: {fileWithoutExtension}_{i}{fileExtension}");
                            
                        #endif
                    return Path.Combine(directory,$"{fileWithoutExtension}_{i}{fileExtension}");
                    }
            }

        }


        #endregion

    }
}
