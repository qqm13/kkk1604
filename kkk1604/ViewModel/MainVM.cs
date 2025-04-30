using kkk1604.Model;
using kkk1604.Model.Db;
using kkk1604.View;
using Microsoft.Win32;
using Spire.Xls;
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
                if(o.Date.Year == CheckDate.Year && o.Date.Month == CheckDate.Month && o.Date.Day == CheckDate.Day)
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

                sheet.Range["F1"].Text = "Тип организации захоронения";
                sheet.Range["F1"].ColumnWidth = 15;
                sheet.Range["F1"].BorderAround();

                sheet.Range["G1"].Text = "Тип памятника";
                sheet.Range["G1"].ColumnWidth = 15;
                sheet.Range["G1"].BorderAround();

                sheet.Range["H1"].Text = "Тип гроба";
                sheet.Range["H1"].ColumnWidth = 15;
                sheet.Range["H1"].BorderAround();

                sheet.Range["I1"].Text = "Тип цветов";
                sheet.Range["I1"].ColumnWidth = 15;
                sheet.Range["I1"].BorderAround();

                sheet.Range["L1"].Text = "Количество проведенных событий";
                sheet.Range["L1"].ColumnWidth = 25;
                sheet.Range["L1"].IsWrapText = true;
                sheet.Range["L1"].BorderAround();

                sheet.Range["M1"].Text = "Заработок за все проведенные события"; 
                sheet.Range["M1"].ColumnWidth = 25;
                sheet.Range["M1"].IsWrapText = true;
                sheet.Range["M1"].BorderAround();

                int organizationsForReportCount = 0;
                int organizationsForReportPrice = 0;

                ObservableCollection<Organization> allorganization =  new ObservableCollection<Organization>(OrganizationsDB.GetDb().SelectAll());
                ObservableCollection<Organization> organizationsForReport = new ObservableCollection<Organization>();
                foreach (Organization org in allorganization)
                {
                    if (org.Date.Year == Reportdate.Year && org.Date.Month == Reportdate.Month)
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
                        sheet.Range[$"F{i + 2}"].Text = organizationsForReport[i].DeathPlace.Title.ToString();
                        sheet.Range[$"G{i + 2}"].Text = organizationsForReport[i].DeathPlace.Grave.Title.ToString();
                        sheet.Range[$"H{i + 2}"].Text = organizationsForReport[i].DeathPlace.Coffin.Title.ToString();
                        sheet.Range[$"I{i + 2}"].Text = organizationsForReport[i].DeathPlace.Flower.Title.ToString();

                        sheet.Range[$"A{i + 2}"].BorderAround();
                        sheet.Range[$"B{i + 2}"].BorderAround();
                        sheet.Range[$"C{i + 2}"].BorderAround();
                        sheet.Range[$"D{i + 2}"].BorderAround();
                        sheet.Range[$"E{i + 2}"].BorderAround();
                        sheet.Range[$"F{i + 2}"].BorderAround();
                        sheet.Range[$"G{i + 2}"].BorderAround();
                        sheet.Range[$"H{i + 2}"].BorderAround();
                        sheet.Range[$"I{i + 2}"].BorderAround();

                        if (organizationsForReport[i].Status == true)
                        {
                            organizationsForReportCount++;
                            organizationsForReportPrice += organizationsForReport[i].Price;
                        }
                    }
                }

                sheet.Range["L2"].Text = organizationsForReportCount.ToString();
                sheet.Range["L2"].ColumnWidth = 25;
                sheet.Range["L2"].IsWrapText = true;
                sheet.Range["L2"].BorderAround();

                sheet.Range["M2"].Text = organizationsForReportPrice.ToString();
                sheet.Range["M2"].ColumnWidth = 25;
                sheet.Range["M2"].IsWrapText = true;
                sheet.Range["M2"].BorderAround();

                var path = String.Format("Reports\\NewReport.xls", AppDomain.CurrentDomain);

                workbook.SaveToFile(path);

                try
                {
                    //    System.Diagnostics.Process.Start(path);
                    Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                }
                catch { }


                //делать норм создание очтета летсгоу
                //основана инфа о соыбтия 1первый лист +
                //количсевто провыеденных событий 1первый лист +
                //общий заработок 1первый лист +
                //кратка инфа о  гробов,памятников,цветов + количество проданных того или иног овида плюс сколько заработали 2втопрой лист

            }, () => true);

        }

    }
}
