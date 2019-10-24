using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EmployeeService
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverPath = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(serverPath, "Reports/logfile.txt");
            string backPath = Path.Combine(serverPath, "Backup");
            string sourcePath = new EmployeeService().sourcePath;
            string fileName = new EmployeeService().fileName;
            if (Directory.Exists(sourcePath))
            {
                while (true)
                {
                    try
                    {
                        List<string> sourcefiles = new DirectoryInfo(sourcePath).GetFiles().OrderBy(f => f.LastWriteTime)
                            .Where(x => Path.GetExtension(x.FullName) == ".xml")
                            .Select(x => x.FullName.ToString()).ToList();


                        foreach (string sourcefile in sourcefiles)
                        {
                            string getFileName = Path.GetFileName(sourcefile).ToLower();
                            if (getFileName == fileName)
                            {
                                XmlTextReader xmlreader = new XmlTextReader(sourcefile);
                                DataSet ds = new DataSet();
                                ds.ReadXml(xmlreader);
                                xmlreader.Close();
                                // string employeeXml = File.ReadAllText(sourcefile);
                                new EmployeeService().AddEmployeeData(sourcefile , ds.GetXml()); 


                             
                            }
                        }




                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message + Environment.NewLine);
                    }
                }

            }
            else
            {

                File.AppendAllText(fullPath, "Folder Doesnot Exists || "
                    + DateTime.Now.ToString("dddd, dd MMMM yyyy") + "" + Environment.NewLine + Environment.NewLine);

            }
        }
    }
}
