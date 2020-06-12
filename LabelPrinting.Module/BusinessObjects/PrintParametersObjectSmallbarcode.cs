using DevExpress.ExpressApp.DC;

namespace LabelPrinting.Module.BusinessObjects
{
    [DomainComponent]
    public class PrintParametersObjectSmallbarcode
    {
        public PrintParametersObjectSmallbarcode(ProductMaster ProductName)
        {
            Productname = ProductName.ProductName;
            PartNo = ProductName.PartNo;
            //IncludeStock = true;
        }
        public string PartNo { get; set; }
        public string Productname { get; set; }
        public int Qty { get; set; }
    }
}