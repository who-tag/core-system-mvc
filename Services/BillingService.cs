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

        public BillsItems GetBillItem(int idnt)
        {
            BillsItems item = null;

            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT bi_idnt, bi_cost, bi_amount, bs_idnt, bs_name, bl_idnt, bl_date, bl_time, bl_flag, bl_cost, bl_amount, bl_notes, pt_idnt, ps_idnt, ps_names, ps_gender, ps_dob FROM BillsItems INNER JOIN BillableServices ON bi_item=bs_idnt INNER JOIN Bills ON bi_bill=bl_idnt INNER JOIN Patient ON bl_patient=pt_idnt INNER JOIN Person ON pt_person=ps_idnt WHERE bi_idnt=" + idnt);
            if (dr.Read())
            {
                item = new BillsItems
                {
                    Id = Convert.ToInt16(dr[0]),
                    Cost = Convert.ToDouble(dr[1]),
                    Amount = Convert.ToDouble(dr[2])
                };

                item.Item.Id = Convert.ToInt16(dr[3]);
                item.Item.Name = dr[4].ToString();
                
                item.Bill.Id = Convert.ToInt16(dr[5]);
                item.Bill.Date = Convert.ToDateTime(dr[6]);
                item.Bill.Time = TimeSpan.Parse(dr[7].ToString());
                item.Bill.Flag = Convert.ToInt16(dr[8]);
                item.Bill.Cost = Convert.ToDouble(dr[9]);
                item.Bill.Amount = Convert.ToDouble(dr[10]);
                item.Bill.Notes = dr[11].ToString();
                
                item.Bill.Patient.Id = Convert.ToInt16(dr[12]);
                item.Bill.Patient.Person.Id = Convert.ToInt16(dr[13]);
                item.Bill.Patient.Person.Name = dr[14].ToString();
                item.Bill.Patient.Person.Gender = dr[15].ToString();
                item.Bill.Patient.Person.DoB = Convert.ToDateTime(dr[16]);
            }

            return item;
        }


        public List<BillsItems> GetBillItems(Bills bill) {
            List<BillsItems> items = new List<BillsItems>();

            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT bi_idnt, bi_cost, bi_amount, bs_idnt, bs_name, bl_idnt, bl_date, bl_time, bl_flag, bl_cost, bl_amount, bl_notes, pt_idnt, ps_idnt, ps_names, ps_gender, ps_dob FROM BillsItems INNER JOIN BillableServices ON bi_item=bs_idnt INNER JOIN Bills ON bi_bill=bl_idnt INNER JOIN Patient ON bl_patient=pt_idnt INNER JOIN Person ON pt_person=ps_idnt WHERE bi_bill=" + bill.Id);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    BillsItems item = new BillsItems
                    {
                        Id = Convert.ToInt16(dr[0]),
                        Cost = Convert.ToDouble(dr[1]),
                        Amount = Convert.ToDouble(dr[2])
                    };

                    item.Item.Id = Convert.ToInt16(dr[3]);
                    item.Item.Name = dr[4].ToString();

                    item.Bill.Id = Convert.ToInt16(dr[5]);
                    item.Bill.Date = Convert.ToDateTime(dr[6]);
                    item.Bill.Time = TimeSpan.Parse(dr[7].ToString());
                    item.Bill.Flag = Convert.ToInt16(dr[8]);
                    item.Bill.Cost = Convert.ToDouble(dr[9]);
                    item.Bill.Amount = Convert.ToDouble(dr[10]);
                    item.Bill.Notes = dr[11].ToString();

                    item.Bill.Patient.Id = Convert.ToInt16(dr[12]);
                    item.Bill.Patient.Person.Id = Convert.ToInt16(dr[13]);
                    item.Bill.Patient.Person.Name = dr[14].ToString();
                    item.Bill.Patient.Person.Gender = dr[15].ToString();
                    item.Bill.Patient.Person.DoB = Convert.ToDateTime(dr[16]);

                    items.Add(item);
                }
            }

            return items;
        }

        public List<Assets> GetAssets(Boolean IncludeVoid = false)
        {
            List<Assets> assets = new List<Assets>();
            string additionalQuery = "WHERE at_void=0";
            if (IncludeVoid){
                additionalQuery = "";
            }

            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT at_idnt, at_void, at_code, at_description, at_purchase_date, at_purchase_amount, at_purchase_currency, at_manufacture_company, at_manufacure_date, at_supplier_supplier, at_supplier_delivery_number, at_allocation, at_notes, ac_idnt, ac_name, ac_description, as_idnt, as_name, as_description FROM Assets INNER JOIN AssetsCategory ON at_category=ac_idnt INNER JOIN AssetsStatus ON at_status=as_idnt " + additionalQuery + " ORDER BY at_code");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Assets asset = new Assets
                    {
                        Id = Convert.ToInt16(dr[0]),
                        Void = Convert.ToInt16(dr[1]),
                        Code = dr[2].ToString(),
                        Description = dr[3].ToString(),
                        PurchaseDate = Convert.ToDateTime(dr[4]),
                        PurchaseAmount = Convert.ToDouble(dr[5]),
                        PurchaseCurrency = dr[6].ToString(),
                        ManufactureCompany = dr[7].ToString(),
                        ManufactureDate = Convert.ToDateTime(dr[8]),
                        Supplier = dr[9].ToString(),
                        DeliveryNumber = dr[10].ToString(),
                        AllocatedTo = dr[11].ToString(),
                        Notes = dr[12].ToString()
                    };

                    asset.Category.Id = Convert.ToInt16(dr[13]);
                    asset.Category.Name = dr[14].ToString();
                    asset.Category.Description = dr[15].ToString();

                    asset.Status.Id = Convert.ToInt16(dr[16]);
                    asset.Status.Name = dr[17].ToString();
                    asset.Status.Description = dr[18].ToString();

                    assets.Add(asset);
                }
            }

            return assets;
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
