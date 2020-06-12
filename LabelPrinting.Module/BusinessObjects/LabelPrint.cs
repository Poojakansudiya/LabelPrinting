using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.XtraReports.UI;
using DevExpress.ExpressApp.ReportsV2;

namespace LabelPrinting.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class LabelPrint : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public LabelPrint(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        ProductMaster partNo;
        private DateTime _Date;
        public DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                SetPropertyValue("Date", ref _Date, value);

            }
        }

        private string invoiceNo;
        [Size(100)]
        [RuleUniqueValue]
        public string InvoiceNo
        {
            get => invoiceNo;
            set => SetPropertyValue(nameof(InvoiceNo), ref invoiceNo, value);
        }


        private int _Qty;

        public int Qty
        {

            get => _Qty;
            set => SetPropertyValue(nameof(Qty), ref _Qty, value);
        }

        [Persistent("UseQty")]
        private int _useQty;
        [PersistentAlias("_useQty")]
        public int UseQty
        {
            get
            {
                _useQty = _Qty * 2;
                return _useQty;
            }
        }

        [Association("ProductMaster-LabelPrints")]
        public ProductMaster PartNo
        {
            get => partNo;
            set => SetPropertyValue(nameof(PartNo), ref partNo, value);
        }
        [Persistent("ProductName")]
        private string _ProductName;
        [PersistentAlias("_ProductName")]
        public string ProductName
        {
            get
            {
                if (PartNo != null && !IsLoading)
                {
                    _ProductName = PartNo.ProductName;

                }
                return _ProductName;
            }
        }
       

    }
}