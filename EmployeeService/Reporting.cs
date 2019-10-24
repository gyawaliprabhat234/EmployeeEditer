using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService
{
   public static  class Reporting
    {
        public static void SuccessMerging(string sourcefile)
        {
            var backupPath = new EmployeeService().sourcePath + "\\Backup";
            if (!Directory.Exists(backupPath))
            {
                Directory.CreateDirectory(backupPath);
            }
            string createDirectoryForFiles = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")).ToString() + "_" + Guid.NewGuid().ToString();
            Directory.CreateDirectory(Path.Combine(backupPath, createDirectoryForFiles));
            File.Move(sourcefile, Path.Combine(backupPath, createDirectoryForFiles, Path.GetFileName(sourcefile).ToLower()));
            //File.AppendAllText(new EmployeeService().logfilePath, DateTime.Now.ToString("dddd, dd MMMM yyyy") +
            //        "|| Records Successfully Added  " + Environment.NewLine + Environment.NewLine);
            //Console.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy") +
            //        "|| Records Successfully Added  " + Environment.NewLine + Environment.NewLine);
            string info =  DateTime.Now.ToString("dddd, dd MMMM yyyy") +
                                          "|| Records Successfully Added"
                                          + Environment.NewLine + Environment.NewLine;
            File.AppendAllText(new EmployeeService().logfilePath, info);
            Console.WriteLine(info);

        }
        //public static string SuccessCreateBackUp()
        //{

        //}
        public static void ExceptionHandler(string errMsg)
        {
            string info = DateTime.Now.ToString("dddd, dd MMMM yyyy") +
                                          "|| Error Occured  ," + Environment.NewLine + "" +
                                          " Error Message = " + errMsg + Environment.NewLine + Environment.NewLine;
            File.AppendAllText(new EmployeeService().logfilePath, info);
            Console.WriteLine(info);
            Console.ReadKey();
        }

    }
}
