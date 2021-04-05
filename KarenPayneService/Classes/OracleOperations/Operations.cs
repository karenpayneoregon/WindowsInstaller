using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace KarenPayneService.Classes.OracleOperations
{
    
    /// <summary>
    /// mocked up for calling Operations.InsertRecord.
    /// </summary>
    public class InsertRecordExample
    {
        /// <summary>
        /// Demo on a call to insert new record
        /// </summary>
        public InsertRecordExample()
        {
            CbrAddress address = new CbrAddress() {PostalCode = "eeeee"};
            
            int newIdentifier = -1;
            
            /*
             * See comment in the `else` statement below
             */
            var exception = Operations.InsertRecord(address,ref newIdentifier);
            
            if (newIdentifier > -1)
            {
                // record inserted
            }
            else
            {
                // record failed to insert, inspect exception var returned from `Operations.InsertRecord`
                
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Operations
    {
        private static string connectionString = "";
        /// <summary>
        /// Mockup for inserting a new record and returning the new primary key
        /// </summary>
        /// <param name="address"><see cref="CbrAddress"/></param>
        /// <param name="newIdentifier">On a successful insert will be the new primary key</param>
        public static Exception InsertRecord(CbrAddress address, ref int newIdentifier)
        {
            
            using (var cn = new OracleConnection(connectionString))
            {
                using (var cmd = new OracleCommand() {Connection = cn, CommandText = InsertStatement()})
                {
                    cmd.Parameters.Add(new OracleParameter(":POSTAL_CODE", address.PostalCode));

                    var idParameter = new OracleParameter("pIdentifier", OracleDbType.Decimal)
                    {
                        Direction = ParameterDirection.Output
                    };

                    try
                    {
                        
                        cn.Open();
                        
                        var result = cmd.ExecuteNonQuery();
                        if (result == -1)
                        {
                            // insert failed
                            return new Exception("Insert failed without raising a runtime exception");
                        }
                        else
                        {
                            var tempIdentifier = idParameter.Value.ToString();

                            if (int.TryParse(tempIdentifier, out var pKey))
                            {
                                newIdentifier = pKey;
                            }

                            return null;
                        }
                        

                    }
                    catch (Exception exception)
                    {
                        newIdentifier = -1;
                        return exception;
                    }
                }
            }
            
        }

        /// <summary>
        /// Insert statement 
        /// </summary>
        /// <returns>Insert statement</returns>
        /// <remarks>
        /// You need to change OCS.OCSMSG_SEQ.nextval to the name of the sequence for CBR_ADDRESSES
        /// </remarks>
        private static string InsertStatement()
        {
            return @"
            INSERT INTO CBR_ADDRESSES 
            (   
                POSTAL_CODE
            ) 
            VALUES 
            (
                 OCS.OCSMSG_SEQ.nextval,
                :POSTAL_CODE
            )  returning id INTO :pIdentifier";
        }
        
    }
}
