using System;
using System.Collections.Generic;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EmployeeService
{
    public class EmployeeService
    {

        public string sourcePath
        {
            get
            {
                return ConfigurationManager.AppSettings["DirectoryPath"].ToString();
            }
        }
        public string logfilePath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["logfilePath"].ToString());
            }
        }
        public string fileName
        {
            get
            {
                return ConfigurationManager.AppSettings["fileName"].ToString();
            }
        }
        private string conString
        {
            get
            {
                return ConfigurationManager.AppSettings["connectionString"].ToString();
            }
        }
        public bool AddEmployeeData(string sourcefile, string EmployeeDataInXml)
        {
            using (OracleConnection con = new OracleConnection(conString))
            {
                string msg = "";
                con.Open();
                OracleTransaction tran = con.BeginTransaction();
                try
                {
                    GetXmlData(con);
                    OracleCommand cmd = new OracleCommand("SP_ADDUSERS_XML", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    OracleParameter param = new OracleParameter(":P_XMLDATA", OracleDbType.XmlType, EmployeeDataInXml, System.Data.ParameterDirection.Input);
                    cmd.Parameters.Add(param);
                    cmd.Transaction = tran;
                    cmd.ExecuteNonQuery();
                    tran.Commit();
                    con.Close();
                    Reporting.SuccessMerging(sourcefile);
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Reporting.ExceptionHandler(ex.Message);
                    return false;
                }
            }
        }

        public void GetXmlData(OracleConnection con)
        {
            OracleCommand cmd = new OracleCommand("SP_GETDATAINXML");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter param = new OracleParameter(":P_XMLDATA", OracleDbType.Clob, System.Data.ParameterDirection.Output);
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();
             (string)cmd.Parameters["P_XMLDATA"].Value;
        }

    }
}
