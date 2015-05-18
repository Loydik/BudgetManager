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
    internal class PdfReportGenerator : ReportGenerator
    {
        private ReportsManager _reportsManager;

        public PdfReportGenerator()
        {
            _reportsManager = new ReportsManager();
        }

        public override void Generate(TimePeriod timePeriod, List<Account> accounts, String filename)
        {
            var doc1 = new Document();

            try
            {
                PdfWriter.GetInstance(doc1, new FileStream(filename, FileMode.Create));
                doc1.Open();

                foreach (Account acc in accounts)
                {
                    var totalIncome = _reportsManager.GetTotalIncomeOfAccount(acc.Id, timePeriod);
                    var totalSpendings = _reportsManager.GetTotalSpendingsOfAccount(acc.Id, timePeriod);
                    string startDate = timePeriod.StartDate.Date.ToString("d");
                    string endDate = timePeriod.EndDate.Date.ToString("d");
                    var startingBalance = _reportsManager.GetInitialAccountBalanceAtDate(acc.Id, timePeriod.StartDate);
                    var finalBalance = _reportsManager.GetFinalAccountBalanceAtDate(acc.Id, timePeriod.EndDate);
                    string currencySymbol = " " + acc.Curency.Symbol;

                    List<Category> categories = _reportsManager.GetUsedCategories(acc.Id, timePeriod);

                    Paragraph p = new Paragraph();
                    Phrase ph1 = new Phrase();

                    Chunk ch1 = new Chunk("Totals for account " + acc.Name + "\n");
                    Chunk ch2 = new Chunk("Time period from " + startDate + " to " + endDate + "\n");
                    Chunk ch3 = new Chunk("Total spendings: " + totalSpendings + currencySymbol + "\n");
                    Chunk ch4 = new Chunk("Total income: " + totalIncome + currencySymbol + "\n");
                    Chunk ch5 = new Chunk("Starting balance at " + startDate + " : " + startingBalance + currencySymbol + "\n");
                    Chunk ch6 = new Chunk("Final balance at " + endDate + " : " + finalBalance + currencySymbol + "\n \n \n");

                    ph1.Add(ch1);
                    ph1.Add(ch2);
                    ph1.Add(ch3);
                    ph1.Add(ch4);
                    ph1.Add(ch5);
                    ph1.Add(ch6);

                    p.Add(ph1);

                    Phrase ph2 = new Phrase();
                    Chunk ch7 = new Chunk("Totals by Categories: \n");
                    ph2.Add(ch7);

                    foreach (var category in categories)
                    {
                        Chunk ch8 = new Chunk("Totals for: " + category.Name + "\n");
                        Chunk ch9 = new Chunk(" Income: " + _reportsManager.GetTotalIncomeOfAccount(acc.Id, timePeriod, category) + currencySymbol + "\n");
                        Chunk ch10 = new Chunk(" Spendings: " + _reportsManager.GetTotalSpendingsOfAccount(acc.Id, timePeriod, category) + currencySymbol + "\n\n");

                        ph2.Add(ch8);
                        ph2.Add(ch9);
                        ph2.Add(ch10);
                    }

                    p.Add(ph2);

                    doc1.Add(p);
                }


            }
            catch (DocumentException dex)
            {
                //to do later 
            }
            catch (IOException ioex)
            {
               //to do later 
            }
            finally
            {
                doc1.Close();
            }

        }
    }
}
