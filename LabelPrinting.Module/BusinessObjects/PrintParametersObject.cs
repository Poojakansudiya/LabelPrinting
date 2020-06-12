using DevExpress.ExpressApp.DC;
using DevExpress.Xpo;
using System;

namespace LabelPrinting.Module.BusinessObjects
{
    [DomainComponent]
    public class PrintParametersObject
    {
        public PrintParametersObject(ProductMaster ProductName)
        {
            Date = DateTime.Now;
            Productname = ProductName.ProductName;
            PartNo = ProductName.PartNo;
            //IncludeStock = true;
        }
       
        public DateTime Date { get; set; }   
        public string InvoiceNo { get; set; }

        public string PartNo { get; set; }
        public string Productname { get; set; }
        public int Qty { get; set; }
    }
}