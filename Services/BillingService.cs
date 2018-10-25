using System;
using System.Data.SqlClient;
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
