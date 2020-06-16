using System;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.XtraReports.UI;

namespace LabelPrinting.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class ProductMaster : BaseObject
    {
        public ProductMaster(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
    
        private string productName;
        [Size(100)]
        [RuleUniqueValue]
        public string ProductName
        {
            get => productName;
            set => SetPropertyValue(nameof(ProductName), ref productName, value);
        }

        private string description;
        [Size(100)]
    //    [RuleUniqueValue]
        public string Description
        {
            get => description;
            set => SetPropertyValue(nameof(Description), ref description, value);
        }

        private string partNo;
        [Size(100)]

        public string PartNo
        {
            get => partNo;
            set => SetPropertyValue(nameof(PartNo), ref partNo, value);
        }

        private int packing;

        public int Packing
        {

            get => packing;
            set => SetPropertyValue(nameof(Packing), ref packing, value);
        }


        [Association("ProductMaster-LabelPrints")]
        public XPCollection<LabelPrint> LabelPrints
        {
            get
            {
                return GetCollection<LabelPrint>("LabelPrints");
            }
        }
        [Aggregated]
        public IList<PrintParametersObject> parameterobject { get; }

        //[Aggregated]
        //public XPCollection<PrintParametersObject> parameterobject
        //{
        //    get
        //    {
        //        return GetCollection<PrintParametersObject>("parameterobject");
        //    }
        //}
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }


        [Action(Caption = "PRINT BIGBARCODE", ImageName = "Action_Printing_Print", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void Print(PrintParametersObject parameters)
        {

            // int qty =  * 2;
            if (parameters.Qty > 0)
            {
                
                LabelPrint srid = new LabelPrint(Session)
                {
                    Date=parameters.Date,
                    InvoiceNo= parameters.InvoiceNo,                   
                    PartNo = this,                     
                    Qty = parameters.Qty,
                };
                bool PrintFlag = false;
                int doubleqty = parameters.Qty * 2;
                XtraReport lbl = new XtraReport();
               // XPQuery<ProductMaster > BPT = Session.Query<ProductMaster>();
               // List<ProductMaster> BPT = new List<ProductMaster>();
                List<LabelPrint> BPT = new List<LabelPrint>();

                for (int i = 1; i <= doubleqty; i++)
                {
                    BPT.Add(srid);
                }
                //var BPT = (from apt in pmst where apt.Oid == this.Oid select apt);
                //   ProductMaster PM = Session.FindObject<ProductMaster>(new BinaryOperator("Oid", this.Oid));
                ReportDataV2 rv2 = Session.FindObject<ReportDataV2>(new BinaryOperator("DisplayName", "100X150"));
                lbl = ReportDataProvider.ReportsStorage.LoadReport(rv2);
                lbl.DataSource = BPT;



                foreach (string Printer in GlobalVar.iList)
                {
                    if (Printer.Contains(GlobalVar.LabelPrinterName))
                    {
                        //for (int i = 1; i <= ; i++)
                        //{
                        //lbl.ShowPreview();
                        ReportPrintTool rpt = new ReportPrintTool(lbl);

                        rpt.Print(Printer);
                        PrintFlag = true;
                        // TODO: might not be correct. Was : Exit For
                        //}
                    }
                }
                if (!PrintFlag)
                {
                    lbl.Print();
                    PrintFlag = true;
                }

               
                // Session.Save(parameters);
            }



        }

        [Action(Caption = "PRINT SMALL BARCODE", ImageName = "Action_Printing_Print", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void PrintSMALL(PrintParametersObjectSmallbarcode parameters)
        {

            // int qty =  * 2;
            if (parameters.Qty > 0)
            {

              
                bool PrintFlag = false;
                int doubleqty = parameters.Qty * 2;
                XtraReport lbl = new XtraReport();
                // XPQuery<ProductMaster > BPT = Session.Query<ProductMaster>();
                List<ProductMaster> BPT = new List<ProductMaster>();
                for (int i = 1; i <= doubleqty; i++)
                {
                    BPT.Add((ProductMaster)MemberwiseClone());
                }
                //var BPT = (from apt in pmst where apt.Oid == this.Oid select apt);
                //   ProductMaster PM = Session.FindObject<ProductMaster>(new BinaryOperator("Oid", this.Oid));
                ReportDataV2 rv2 = Session.FindObject<ReportDataV2>(new BinaryOperator("DisplayName", "70X30"));
                lbl = ReportDataProvider.ReportsStorage.LoadReport(rv2);
                lbl.DataSource = BPT;

              

                foreach (string Printer in GlobalVar.iList)
                {
                    if (Printer.Contains(GlobalVar.DCPrinterName))
                    {
                        //for (int i = 1; i <= ; i++)
                        //{
                        //lbl.ShowPreview();
                        ReportPrintTool rpt = new ReportPrintTool(lbl);

                        rpt.Print(Printer);
                        PrintFlag = true;
                        // TODO: might not be correct. Was : Exit For
                        //}
                    }
                }
                if (!PrintFlag)
                {
                    lbl.Print();
                    PrintFlag = true;
                }

                // Session.Save(parameters);
            }



        }

    }
    
}