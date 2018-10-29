using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarrierIntegrationCore.Archive
{
    public class ArchiveLabels
    {
        public void Execute(string source, string target)
        {
            ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            logger.Info("Start Archiving");
            DirectoryInfo origDir = new DirectoryInfo(source);

            FileInfo[] origDirFiles = origDir.GetFiles();
            logger.Info(origDirFiles.Length + " files are detected");
            int count = 0;
            if (origDirFiles.Length > 0)
            {
                foreach (FileInfo file in origDirFiles)
                {
                    if ((DateTime.Now - file.CreationTime).TotalDays >= 30)
                    {
                        var targetPath = Path.Combine(target, file.Name);
                        if (!File.Exists(targetPath))
                        {
                            try
                            {
                                file.MoveTo(targetPath);
                                count++;
                            }
                            catch (Exception ex)
                            {
                                logger.Info(targetPath);
                                logger.Error(file.Name + " is not moved. Error: " + ex.Message);
                            }
                        }
                        else
                        {
                            logger.Info(file.Name + " already exists");
                        }
                    }
                }
            }
            logger.Info(count + " files are moved");
            logger.Info("Finish Archiving");
        }
    }
}
