using System;
using System.Collections;
using System.Drawing.Printing;
using DevExpress.Xpo;

namespace LabelPrinting.Module
{
    public static class GlobalVar
    {
       
        public static string LabelPrinterName = "";
        public static string DCPrinterName = "";
        public static ArrayList iList = new ArrayList();
        public static ArrayList PrinterList()
        {
            for (int j = 0; j <= PrinterSettings.InstalledPrinters.Count - 1; j++)
            {
                dynamic Printers = PrinterSettings.InstalledPrinters[j];
                iList.Add(Printers);
            }
            return iList;
        }

    }
}