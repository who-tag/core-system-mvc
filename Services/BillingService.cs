using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Core.Extensions;
using Core.Models;

namespace Core.Services
{
    public class BillingService
    {
        public BillableServices GetBillableServices(int idnt)
        {
            BillableServices billable = null;

            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT bs_idnt, bs_code, bs_name, bs_amount, bs_description FROM BillableServices WHERE bs_idnt=" + idnt);
            if (dr.Read())
            {
                billable = new BillableServices
                {
                    Id = Convert.ToInt16(dr[0]),
                    Code = dr[1].ToString(),
                    Name = dr[2].ToString(),
                    Amount = Convert.ToDouble(dr[3]),
                    Description = dr[4].ToString()
                };
            }

            return billable;
        }

        public BillableServices GetBillableServices(string code)
        {

            BillableServices billable = null;

            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT bs_idnt, bs_code, bs_name, bs_amount, bs_description FROM BillableServices WHERE bs_code='" + code + "'");
            if (dr.Read())
            {
                billable = new BillableServices
                {
                    Id = Convert.ToInt16(dr[0]),
                    Code = dr[1].ToString(),
                    Name = dr[2].ToString(),
                    Amount = Convert.ToDouble(dr[3]),
                    Description = dr[4].ToString()
                };
            }

            return billable;
        }

        public Bills GetBill(int idnt)
        {
            Bills bill = null;

            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT bl_idnt, bl_date, bl_time, bl_flag, bl_void, bl_cost, bl_amount, bl_notes, pt_idnt, ps_idnt, ps_names, ps_gender, ps_dob FROM Bills INNER JOIN Patient ON bl_patient=pt_idnt INNER JOIN Person ON pt_person=ps_idnt WHERE bl_idnt=" + idnt);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    bill = new Bills
                    {
                        Id = Convert.ToInt16(dr[0]),
                        Date = Convert.ToDateTime(dr[1]),
                        Time = TimeSpan.Parse(dr[2].ToString()),
                        Flag = Convert.ToInt16(dr[3]),
                        Void = Convert.ToInt16(dr[4]),
                        Cost = Convert.ToDouble(dr[5]),
                        Amount = Convert.ToDouble(dr[6]),
                        Notes = dr[7].ToString()
                    };

                    bill.Patient.Id = Convert.ToInt16(dr[8]);
                    bill.Patient.Person.Id = Convert.ToInt16(dr[9]);
                    bill.Patient.Person.Name = dr[10].ToString();
                    bill.Patient.Person.Gender = dr[11].ToString();
                    bill.Patient.Person.DoB = Convert.ToDateTime(dr[12]);
                }
            }

            return bill;
        }

        public List<Bills> GetBills(List<int> flags, Patient patient, Boolean includeVoid = false)
        {
            List<Bills> bills = new List<Bills>();
            string additionalQuery = "WHERE bl_flag IN (" + string.Join(",", flags.Select(n => n.ToString()).ToArray()) + ")";

            if (patient != null) {
                additionalQuery += "  AND bl_patient=" + patient.Id;
            }

            if (!includeVoid) {
                additionalQuery += "  AND bl_void=0";
            }

            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT bl_idnt, bl_date, bl_time, bl_flag, bl_void, bl_cost, bl_amount, bl_notes, pt_idnt, ps_idnt, ps_names, ps_gender, ps_dob FROM Bills INNER JOIN Patient ON bl_patient=pt_idnt INNER JOIN Person ON pt_person=ps_idnt " + additionalQuery + " ORDER BY bl_date, bl_time, bl_idnt");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Bills bill = new Bills
                    {
                        Id = Convert.ToInt16(dr[0]),
                        Date = Convert.ToDateTime(dr[1]),
                        Time = TimeSpan.Parse(dr[2].ToString()),
                        Flag = Convert.ToInt16(dr[3]),
                        Void = Convert.ToInt16(dr[4]),
                        Cost = Convert.ToDouble(dr[5]),
                        Amount = Convert.ToDouble(dr[6]),
                        Notes = dr[7].ToString()
                    };

                    bill.Patient.Id = Convert.ToInt16(dr[8]);
                    bill.Patient.Person.Id = Convert.ToInt16(dr[9]);
                    bill.Patient.Person.Name = dr[10].ToString();
                    bill.Patient.Person.Gender = dr[11].ToString();
                    bill.Patient.Person.DoB = Convert.ToDateTime(dr[12]);

                    bills.Add(bill);
                }
            }

            return bills;
        }

        /*Data Writer*/
        public Bills SaveBills(Bills bill){
            SqlServerConnection conn = new SqlServerConnection();
            bill.Id = conn.SqlServerUpdate("IF NOT EXISTS (SELECT bl_idnt FROM Bills WHERE bl_idnt=" + bill.Id + ") BEGIN INSERT INTO Bills(bl_flag, bl_patient, bl_cost, bl_amount, bl_notes) OUTPUT INSERTED.bl_idnt VALUES (" + bill.Flag + ", " + bill.Patient.Id + ", " + bill.Cost + ", " + bill.Amount + ", '" + bill.Notes + "') END ELSE BEGIN UPDATE Bills SET bl_flag=" + bill.Flag + ", bl_patient=" + bill.Patient.Id + ", bl_cost=" + bill.Cost + ", bl_amount=" + bill.Amount + ", bl_notes='" + bill.Notes + "' OUTPUT INSERTED.bl_idnt WHERE bl_idnt=" + bill.Id + " END");

            return bill;
        }

        public Bills UpdateBillsFlag(Bills bill){
            SqlServerConnection conn = new SqlServerConnection();
            bill.Id = conn.SqlServerUpdate("UPDATE Bills SET bl_flag=" + bill.Flag + " OUTPUT INSERTED.bl_idnt WHERE bl_idnt=" + bill.Id);

            return bill;
        }


        public BillsItems SaveBillsItems(BillsItems item){
            SqlServerConnection conn = new SqlServerConnection();
            item.Id = conn.SqlServerUpdate("IF NOT EXISTS (SELECT bi_idnt FROM BillsItems WHERE bi_idnt=" + item.Id + ") BEGIN INSERT INTO BillsItems(bi_bill, bi_item, bi_cost, bi_amount, bi_description) OUTPUT INSERTED.bi_idnt VALUES (" + item.Bill.Id + ", " + item.Item.Id + ", " + item.Cost + ", " + item.Amount + ", '" + item.Description + "') END ELSE BEGIN UPDATE BillsItems SET bi_cost=" + item.Cost + ", bi_amount=" + item.Amount + ", bi_description='" + item.Description + "' OUTPUT INSERTED.bi_idnt WHERE bi_idnt=" + item.Id + " END");

            return item;
        }
    }
}
