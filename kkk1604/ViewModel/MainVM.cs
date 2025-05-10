using kkk1604.Model;
using kkk1604.Model.Db;
using kkk1604.View;
using Microsoft.Win32;
using Spire.Xls;
using Spire.Xls.AI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kkk1604.ViewModel
{
    class MainVM : BaseVM
    {
        private string dateCheckResult;
        private DateTime checkDate = DateTime.Now;

        public CommandVM OpenPriceTest { get; set; }
        public CommandVM OpenEventMenu { get; set; }
        public CommandVM OpenRepertoire { get; set; }
        public CommandVM OpenDeathPlace { get; set; }
        public CommandVM DateCheck { get; set; }
        public CommandVM CreateReport {  get; set; }

        public string DateCheckResult { get => dateCheckResult; set { dateCheckResult = value; Signal(); } }
        public DateTime CheckDate { get => checkDate; set { checkDate = value; Signal();} }
        public DateTime Reportdate {  get; set; } = DateTime.Now;


        public MainVM()
        {

            DateCheck = new CommandVM(() =>
            {
                DateCheckResult = " ";
                ObservableCollection<Organization> list = new ObservableCollection<Organization>(OrganizationsDB.GetDb().SelectAll());
                foreach (Organization o in list)
                {
                    if (o.Date.Year == CheckDate.Year && o.Date.Month == CheckDate.Month && o.Date.Day == CheckDate.Day)
                    {
                        DateCheckResult = "Занято";
                    }
                }
                if (DateCheckResult != "Занято")
                    DateCheckResult = "Свободно";

            }, () => true);

            OpenPriceTest = new CommandVM(() =>
        {
            PriceTest priceTest = new PriceTest();
            priceTest.ShowDialog();

        }, () => true);

            OpenEventMenu = new CommandVM(() =>
            {
                EventMenu eventMenu = new EventMenu();
                eventMenu.ShowDialog();

            }, () => true);

            OpenRepertoire = new CommandVM(() =>
            {
                Repertoire repertoire = new Repertoire();
                repertoire.ShowDialog();

            }, () => true);

            OpenDeathPlace = new CommandVM(() =>
            {
                AddDeathplace addDeathplace = new AddDeathplace();
                addDeathplace.ShowDialog();

            }, () => true);

            CreateReport = new CommandVM(() =>
            {
                Workbook workbook = new Workbook();
                //1лист
                Worksheet sheet = workbook.Worksheets[0];
                sheet.Name = "События";
                Worksheet sheetTwo = workbook.Worksheets[1];
                sheetTwo.Name = "Статистика продаж";
                Worksheet sheetThree = workbook.Worksheets[2];
                sheetThree.Remove();

                sheet.Range["A1"].Text = "Количество гостей";
                sheet.Range["A1"].ColumnWidth = 18;
                sheet.Range["A1"].BorderAround();

                sheet.Range["B1"].Text = "Дата";
                sheet.Range["B1"].ColumnWidth = 15;
                sheet.Range["B1"].BorderAround();

                sheet.Range["C1"].Text = "Адрес";
                sheet.Range["C1"].ColumnWidth = 15;
                sheet.Range["C1"].BorderAround();

                sheet.Range["D1"].Text = "Статус";
                sheet.Range["D1"].ColumnWidth = 15;
                sheet.Range["D1"].BorderAround();

                sheet.Range["E1"].Text = "Общая цена";
                sheet.Range["E1"].ColumnWidth = 15;
                sheet.Range["E1"].BorderAround();

                sheet.Range["F1"].Text = "Тип памятника";
                sheet.Range["F1"].ColumnWidth = 15;
                sheet.Range["F1"].BorderAround();

                sheet.Range["G1"].Text = "Тип гроба";
                sheet.Range["G1"].ColumnWidth = 15;
                sheet.Range["G1"].BorderAround();

                sheet.Range["H1"].Text = "Тип цветов";
                sheet.Range["H1"].ColumnWidth = 15;
                sheet.Range["H1"].BorderAround();

                sheet.Range["K1"].Text = "Количество проведенных событий";
                sheet.Range["K1"].ColumnWidth = 25;
                sheet.Range["K1"].IsWrapText = true;
                sheet.Range["K1"].BorderAround();

                sheet.Range["L1"].Text = "Заработок за все проведенные события";
                sheet.Range["L1"].ColumnWidth = 25;
                sheet.Range["L1"].IsWrapText = true;
                sheet.Range["L1"].BorderAround();

                int organizationsForReportCount = 0;
                int organizationsForReportPrice = 0;

                ObservableCollection<Organization> allorganization = new ObservableCollection<Organization>(OrganizationsDB.GetDb().SelectAll());
                ObservableCollection<Organization> organizationsForReport = new ObservableCollection<Organization>();
                foreach (Organization org in allorganization)
                {
                    if (org.Date.Year == Reportdate.Year && org.Date.Month == Reportdate.Month && org.Status == true)
                    {
                        organizationsForReport.Add(org);
                    }
                }
                for (int i = 0; i < organizationsForReport.Count; i++)
                {
                    if (organizationsForReport[i].Date.Year == Reportdate.Year && organizationsForReport[i].Date.Month == Reportdate.Month)
                    {

                        sheet.Range[$"A{i + 2}"].Text = organizationsForReport[i].GuestCount.ToString();
                        sheet.Range[$"B{i + 2}"].Text = organizationsForReport[i].Date.ToString();
                        sheet.Range[$"C{i + 2}"].Text = organizationsForReport[i].Place.CemetaryAdress.ToString();
                        sheet.Range[$"D{i + 2}"].Text = organizationsForReport[i].Status.ToString();
                        sheet.Range[$"E{i + 2}"].Text = organizationsForReport[i].Price.ToString();
                        sheet.Range[$"F{i + 2}"].Text = organizationsForReport[i].DeathPlace.Grave.Title.ToString();
                        sheet.Range[$"G{i + 2}"].Text = organizationsForReport[i].DeathPlace.Coffin.Title.ToString();
                        sheet.Range[$"H{i + 2}"].Text = organizationsForReport[i].DeathPlace.Flower.Title.ToString();

                        sheet.Range[$"A{i + 2}"].BorderAround();
                        sheet.Range[$"B{i + 2}"].BorderAround();
                        sheet.Range[$"C{i + 2}"].BorderAround();
                        sheet.Range[$"D{i + 2}"].BorderAround();
                        sheet.Range[$"E{i + 2}"].BorderAround();
                        sheet.Range[$"F{i + 2}"].BorderAround();
                        sheet.Range[$"G{i + 2}"].BorderAround();
                        sheet.Range[$"H{i + 2}"].BorderAround();

                        if (organizationsForReport[i].Status == true)
                        {
                            organizationsForReportCount++;
                            organizationsForReportPrice += organizationsForReport[i].Price;
                        }
                    }
                }

                sheet.Range["K2"].Text = organizationsForReportCount.ToString();
                sheet.Range["K2"].ColumnWidth = 25;
                sheet.Range["K2"].IsWrapText = true;
                sheet.Range["K2"].BorderAround();

                sheet.Range["L2"].Text = organizationsForReportPrice.ToString();
                sheet.Range["L2"].ColumnWidth = 25;
                sheet.Range["L2"].IsWrapText = true;
                sheet.Range["L2"].BorderAround();

                sheetTwo.Range["A1"].Text = "Гробы";
                sheetTwo.Range["A1:G1"].Merge();
                sheetTwo.Range["A1:G1"].HorizontalAlignment = HorizontalAlignType.Center;
                sheetTwo.Range["A1:G1"].BorderAround();

                sheetTwo.Range[$"A1"].ColumnWidth = 13;
                sheetTwo.Range[$"B2"].ColumnWidth = 13;
                sheetTwo.Range[$"C3"].ColumnWidth = 13;
                sheetTwo.Range[$"D4"].ColumnWidth = 13;

                sheetTwo.Range["A2"].Text = "Название";
                sheetTwo.Range["A2"].BorderAround();
                sheetTwo.Range["B2"].Text = "Материал";
                sheetTwo.Range["B2"].BorderAround();
                sheetTwo.Range["C2"].Text = "Размер";
                sheetTwo.Range["C2"].BorderAround();
                sheetTwo.Range["D2"].Text = "Цена";
                sheetTwo.Range["D2"].BorderAround();
                sheetTwo.Range["F2"].Text = "Количество продано";
                sheetTwo.Range["F2"].BorderAround();
                sheetTwo.Range["F2"].ColumnWidth = 10;
                sheetTwo.Range["G2"].Text = "Общая выручка";
                sheetTwo.Range["G2"].BorderAround();
                sheetTwo.Range["G2"].ColumnWidth = 14;

                int coffincount = 0;
                int coffinprice = 0;
                for (int i = 0; i < organizationsForReport.Count; i++)
                {
                    List<Coffin> coffins = CoffinsDB.GetDb().SelectAll();
                    foreach(Coffin coffin in coffins)
                    {
                        if(coffin.Id == organizationsForReport[i].DeathPlace.Coffin.Id)
                        {
                            sheetTwo.Range[$"A{i + 3}"].Text = coffin.Title;
                            sheetTwo.Range[$"B{i + 3}"].Text = coffin.Material.Title;
                            sheetTwo.Range[$"C{i + 3}"].Text = coffin.Size.Title;
                            sheetTwo.Range[$"D{i + 3}"].Text = coffin.Price.ToString();

                            sheetTwo.Range[$"A{i + 3}"].BorderAround();
                            sheetTwo.Range[$"B{i + 3}"].BorderAround();
                            sheetTwo.Range[$"C{i + 3}"].BorderAround();
                            sheetTwo.Range[$"D{i + 3}"].BorderAround();

                           

                            coffincount++;
                            coffinprice += coffin.Price;
                        }
                    }
                }
                sheetTwo.Range["F3"].Text = coffincount.ToString();
                sheetTwo.Range["G3"].Text = coffinprice.ToString();
                sheetTwo.Range["F3"].BorderAround();
                sheetTwo.Range["G3"].BorderAround();


                 sheetTwo.Range["J1"].Text = "Памятники";
                sheetTwo.Range["J1:P1"].Merge();
                sheetTwo.Range["J1:P1"].HorizontalAlignment = HorizontalAlignType.Center;
                sheetTwo.Range["J1:P1"].BorderAround();

                sheetTwo.Range[$"J1"].ColumnWidth = 13;
                sheetTwo.Range[$"K2"].ColumnWidth = 13;
                sheetTwo.Range[$"L3"].ColumnWidth = 13;
                sheetTwo.Range[$"M4"].ColumnWidth = 13;

                sheetTwo.Range["J2"].Text = "Название";
                sheetTwo.Range["J2"].BorderAround();
                sheetTwo.Range["K2"].Text = "Материал";
                sheetTwo.Range["K2"].BorderAround();
                sheetTwo.Range["L2"].Text = "Форма";
                sheetTwo.Range["L2"].BorderAround();
                sheetTwo.Range["M2"].Text = "Цена";
                sheetTwo.Range["M2"].BorderAround();
                sheetTwo.Range["O2"].Text = "Количество продано";
                sheetTwo.Range["O2"].BorderAround();
                sheetTwo.Range["O2"].ColumnWidth = 10;
                sheetTwo.Range["P2"].Text = "Общая выручка";
                sheetTwo.Range["P2"].BorderAround();
                sheetTwo.Range["P2"].ColumnWidth = 14;

                int gravecount = 0;
                int gravenprice = 0;
                for (int i = 0; i < organizationsForReport.Count; i++)
                {
                    List<Grave> graves = GravesDB.GetDb().SelectAll();
                    foreach(Grave grave in graves)
                    {
                        if(grave.Id == organizationsForReport[i].DeathPlace.Grave.Id)
                        {
                            sheetTwo.Range[$"J{i + 3}"].Text = grave.Title;
                            sheetTwo.Range[$"K{i + 3}"].Text = grave.Material.Title;
                            sheetTwo.Range[$"L{i + 3}"].Text = grave.Form.Title;
                            sheetTwo.Range[$"M{i + 3}"].Text = grave.Price.ToString();

                            sheetTwo.Range[$"J{i + 3}"].BorderAround();
                            sheetTwo.Range[$"k{i + 3}"].BorderAround();
                            sheetTwo.Range[$"L{i + 3}"].BorderAround();
                            sheetTwo.Range[$"M{i + 3}"].BorderAround();



                            gravecount++;
                            gravenprice += grave.Price;
                        }
                    }
                }
                sheetTwo.Range["O3"].Text = gravecount.ToString();
                sheetTwo.Range["P3"].Text = gravenprice.ToString();
                sheetTwo.Range["O3"].BorderAround();
                sheetTwo.Range["P3"].BorderAround();

                sheetTwo.Range["S1"].Text = "Цвета";
                sheetTwo.Range["S1:X1"].Merge();
                sheetTwo.Range["S1:X1"].HorizontalAlignment = HorizontalAlignType.Center;
                sheetTwo.Range["S1:X1"].BorderAround();

                sheetTwo.Range[$"S1"].ColumnWidth = 13;
                sheetTwo.Range[$"T2"].ColumnWidth = 13;
                sheetTwo.Range[$"U3"].ColumnWidth = 13;
                sheetTwo.Range[$"V4"].ColumnWidth = 13;

                sheetTwo.Range["S2"].Text = "Название";
                sheetTwo.Range["S2"].BorderAround();
                sheetTwo.Range["T2"].Text = "Количество в буете";
                sheetTwo.Range["T2"].BorderAround();
                sheetTwo.Range["U2"].Text = "Цена";
                sheetTwo.Range["U2"].BorderAround();
                sheetTwo.Range["W2"].Text = "Количество продано";
                sheetTwo.Range["W2"].BorderAround();
                sheetTwo.Range["W2"].ColumnWidth = 13;
                sheetTwo.Range["W2"].IsWrapText = true;
                sheetTwo.Range["X2"].Text = "Общая выручка";
                sheetTwo.Range["X2"].BorderAround();
                sheetTwo.Range["X2"].ColumnWidth = 14;

                int flowercount = 0;
                int flowerprice = 0;
                for (int i = 0; i < organizationsForReport.Count; i++)
                {
                    List<Flower> flowers = FlowersDB.GetDb().SelectAll();
                    foreach (Flower flower in flowers)
                    {
                        if (flower.Id == organizationsForReport[i].DeathPlace.Grave.Id)
                        {
                            sheetTwo.Range[$"S{i + 3}"].Text = flower.Title;
                            sheetTwo.Range[$"T{i + 3}"].Text = flower.Count.ToString();
                            sheetTwo.Range[$"U{i + 3}"].Text = flower.Price.ToString();

                            sheetTwo.Range[$"S{i + 3}"].BorderAround();
                            sheetTwo.Range[$"T{i + 3}"].BorderAround();
                            sheetTwo.Range[$"U{i + 3}"].BorderAround();



                            flowercount++;
                            flowerprice += flower.Price;
                        }
                    }
                }
                sheetTwo.Range["W3"].Text = flowercount.ToString();
                sheetTwo.Range["X3"].Text = flowerprice.ToString();
                sheetTwo.Range["W3"].BorderAround();
                sheetTwo.Range["X3"].BorderAround();






                //делать норм создание очтета летсгоу
                //основана инфа о соыбтия 1первый лист +
                //количсевто провыеденных событий 1первый лист +
                //общий заработок 1первый лист +
                //кратка инфа о  гробов,памятников,цветов + количество проданных того или иног овида плюс сколько заработали 2втопрой лист +


                try
                {
                    var path = String.Format("Reports\\NewReport.xls", AppDomain.CurrentDomain);

                    workbook.SaveToFile(path);
                    Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                }
                catch 
                {
                    MessageBox.Show("Закройте excel и попробуйте еще раз");
                }


            }, () => true);

        }

    }
}
