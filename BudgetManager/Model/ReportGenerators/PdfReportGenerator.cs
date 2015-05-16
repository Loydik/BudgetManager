using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Media;
using BudgetManager.Model.Db;
using BudgetManager.Model.Managers;

namespace BudgetManager.Model.ReportGenerators
{
    class PdfReportGenerator : ReportGenerator
    {
        private ReportsManager _reportsManager;

        public PdfReportGenerator()
        {
            _reportsManager = new ReportsManager();
        }

        public override void Generate(TimePeriod timePeriod, List<Account> accounts)
        {
            var doc1 = new Document();
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Model\\ReportGenerators\\pdf";
            //var timePeriod = timePeriods.First();

            try
            {
                PdfWriter.GetInstance(doc1, new FileStream(path + "/Doc1.pdf", FileMode.Create));
                doc1.Open();

                foreach (Account acc in accounts)
                {
                    Paragraph p = new Paragraph();
                    Phrase ph1 = new Phrase("Totals for account " + acc.Name);
                    Phrase ph2 = new Phrase("Total spendings:" + _reportsManager.GetTotalSpendingsOfAccount(acc.Id));

                    p.Add(ph1);
                    p.Add(ph2);
                }


            }
            catch (DocumentException dex)
            {
                throw (dex);
            }
            catch (IOException ioex)
            {
                throw (ioex);
            }
            finally
            {
                doc1.Close();
            }

        }

    }
}
