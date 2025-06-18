using kkk1604;
using kkk1604.Model.Db;
using kkk1604.Model;
using kkk1604.ViewModel;
using Spire.Xls;
using System.Collections.ObjectModel;

namespace TestProject2
{
    public class Tests
    {
        public Coffin Coffin1 { get; set; } = new Coffin();
        public DeathPlace DeathPlace1 { get; set; } = new DeathPlace();
        public Flower Flower1 { get; set; } = new Flower();
        public Form Form1 { get; set; } = new Form();
        public Grave Grave1 { get; set; } = new Grave();
        public Material Material1 { get; set; } = new Material();
        public Organization Organization1 { get; set; } = new Organization();
        public Place Place1 { get; set; } = new Place();
        public Size Size1 { get; set; } = new Size();


        [SetUp]
        public void Setup()
        {
            Coffin1.Title = "Test";
            Coffin1.Price = 100;
            Coffin1.MaterialId = 1;
            Coffin1.SizeId = 1;

            DeathPlace1.Title = "Test";
            DeathPlace1.Price = 100;
            DeathPlace1.FlowersId = 1;
            DeathPlace1.CoffinTypeId = 1;
            DeathPlace1.GraveTypeId = 1;
           
            Flower1.Title = "Test";
            Flower1.Price = 100;
            Flower1.Count = 1;

            Form1.Title = "Test";
            Form1.Height = 100;
            Form1.Width = 100;
            Form1.MaterialUsage = 2;

            Grave1.Price = 100;
            Grave1.MaterialId = 1;
            Grave1.Title = "Test";
            Grave1.FormId = 1;
            
            Material1.Title = "Test";
            Material1.Price = 100;
            
            Place1.Price = 100;
            Place1.CemeteryPlotNumber = 1;
            Place1.CemeterySectorNumber = 1;
            Place1.CemetaryAdress = "TEST";
            
            Size1.Height = 100;
            Size1.Width = 100;
            Size1.Title = "Test";
            Size1.Length = 1;
        }

        [Test]
        public void AddingNewCoffinActuallyAddsOne()
        {
            List<Coffin> was = CoffinsDB.GetDb().SelectAll();
            CoffinsDB.GetDb().Insert(Coffin1);
            List<Coffin> now = CoffinsDB.GetDb().SelectAll();

            Assert.Greater(now.Count, was.Count);
        }

        [Test]
        public void RemoveCoffinActuallyRemovingOne()
        {
            CoffinsDB.GetDb().Insert(Coffin1);
            List<Coffin> was = CoffinsDB.GetDb().SelectAll();
            CoffinsDB.GetDb().Remove(Coffin1);
            List<Coffin> now = CoffinsDB.GetDb().SelectAll();

            Assert.Less(now.Count, was.Count);
        }


        [Test]
        public void AddingNewDeathPlaceActuallyAddsOne()
        {
            List<DeathPlace> was = DeathPlacesDB.GetDb().SelectAll();
            DeathPlacesDB.GetDb().Insert(DeathPlace1);
            List<DeathPlace> now = DeathPlacesDB.GetDb().SelectAll();

            Assert.Greater(now.Count, was.Count);
        }

        [Test]
        public void RemoveDeathPlaceActuallyRemovingOne()
        {
            DeathPlacesDB.GetDb().Insert(DeathPlace1);
            List<DeathPlace> was = DeathPlacesDB.GetDb().SelectAll();
            DeathPlacesDB.GetDb().Remove(DeathPlace1);
            List<DeathPlace> now = DeathPlacesDB.GetDb().SelectAll();

            Assert.Less(now.Count, was.Count);
        }


        [Test]
        public void AddingNewFlowerActuallyAddsOne()
        {
            List<Flower> was = FlowersDB.GetDb().SelectAll();
            FlowersDB.GetDb().Insert(Flower1);
            List<Flower> now = FlowersDB.GetDb().SelectAll();

            Assert.Greater(now.Count, was.Count);
        }

        [Test]
        public void RemoveFlowerActuallyRemovingOne()
        {
            FlowersDB.GetDb().Insert(Flower1);
            List<Flower> was = FlowersDB.GetDb().SelectAll();
            FlowersDB.GetDb().Remove(Flower1);
            List<Flower> now = FlowersDB.GetDb().SelectAll();

            Assert.Less(now.Count, was.Count);
        }


        [Test]
        public void AddingNewFormActuallyAddsOne()
        {
            List<Form> was = FormsDB.GetDb().SelectAll();
            FormsDB.GetDb().Insert(Form1);
            List<Form> now = FormsDB.GetDb().SelectAll();

            Assert.Greater(now.Count, was.Count);
        }

        [Test]
        public void RemoveFormActuallyRemovingOne()
        {
            FormsDB.GetDb().Insert(Form1);
            List<Form> was = FormsDB.GetDb().SelectAll();
            FormsDB.GetDb().Remove(Form1);
            List<Form> now = FormsDB.GetDb().SelectAll();

            Assert.Less(now.Count, was.Count);
        }


        [Test]
        public void AddingNewGraveActuallyAddsOne()
        {
            List<Grave> was = GravesDB.GetDb().SelectAll();
            GravesDB.GetDb().Insert(Grave1);
            List<Grave> now = GravesDB.GetDb().SelectAll();

            Assert.Greater(now.Count, was.Count);
        }

        [Test]
        public void RemoveGraveActuallyRemovingOne()
        {
            GravesDB.GetDb().Insert(Grave1);
            List<Grave> was = GravesDB.GetDb().SelectAll();
            GravesDB.GetDb().Remove(Grave1);
            List<Grave> now = GravesDB.GetDb().SelectAll();

            Assert.Less(now.Count, was.Count);
        }


        [Test]
        public void AddingNewMaterialActuallyAddsOne()
        {
            List<Material> was = MaterialsDB.GetDb().SelectAll();
            MaterialsDB.GetDb().Insert(Material1);
            List<Material> now = MaterialsDB.GetDb().SelectAll();

            Assert.Greater(now.Count, was.Count);
        }

        [Test]
        public void RemoveMaterialActuallyRemovingOne()
        {
            MaterialsDB.GetDb().Insert(Material1);
            List<Material> was = MaterialsDB.GetDb().SelectAll();
            MaterialsDB.GetDb().Remove(Material1);
            List<Material> now = MaterialsDB.GetDb().SelectAll();

            Assert.Less(now.Count, was.Count);
        }



        [Test]
        public void AddingNewOrganizationActuallyAddsOne()
        {
            List<Organization> was = OrganizationsDB.GetDb().SelectAll();
            OrganizationsDB.GetDb().Insert(Organization1);
            List<Organization> now = OrganizationsDB.GetDb().SelectAll();

            Assert.Greater(now.Count, was.Count);
        }

        [Test]
        public void RemoveOrganizationActuallyRemovingOne()
        {
            OrganizationsDB.GetDb().Insert(Organization1);
            List<Organization> was = OrganizationsDB.GetDb().SelectAll();
            OrganizationsDB.GetDb().Remove(Organization1);
            List<Organization> now = OrganizationsDB.GetDb().SelectAll();

            Assert.Less(now.Count, was.Count);
        }



        [Test]
        public void AddingNewPlaceActuallyAddsOne()
        {
            List<Place> was = PlacesDB.GetDb().SelectAll();
            PlacesDB.GetDb().Insert(Place1);
            List<Place> now = PlacesDB.GetDb().SelectAll();

            Assert.Greater(now.Count, was.Count);
        }

        [Test]
        public void RemovePlaceActuallyRemovingOne()
        {
            PlacesDB.GetDb().Insert(Place1);
            List<Place> was = PlacesDB.GetDb().SelectAll();
            PlacesDB.GetDb().Remove(Place1);
            List<Place> now = PlacesDB.GetDb().SelectAll();

            Assert.Less(now.Count, was.Count);
        }


        [Test]
        public void AddingNewSizeActuallyAddsOne()
        {
            List<Size> was = SizesDB.GetDb().SelectAll();
            SizesDB.GetDb().Insert(Size1);
            List<Size> now = SizesDB.GetDb().SelectAll();

            Assert.Greater(now.Count, was.Count);
        }

        [Test]
        public void RemoveSizeActuallyRemovingOne()
        {
            SizesDB.GetDb().Insert(Size1);
            List<Size> was = SizesDB.GetDb().SelectAll();
            SizesDB.GetDb().Remove(Size1);
            List<Size> now = SizesDB.GetDb().SelectAll();

            Assert.Less(now.Count, was.Count);
        }


        [Test]
        public void DeathPlacePriceCalculatesRight()
        {
            int act = Grave1.Price + Coffin1.Price + Flower1.Price;

            DeathPlace deathPlace = new DeathPlace("test" ,Grave1, Flower1, Coffin1);
            int exp = deathPlace.Price;

            Assert.AreEqual(act,exp);
        }


        [Test]
        public void DeathPlaceCreatesRight()
        {
            string name = "test";
            DeathPlace create = new DeathPlace(name, Grave1, Flower1, Coffin1);
            DeathPlacesDB.GetDb().Insert(create);
            DeathPlace neww = DeathPlacesDB.GetDb().SelectAll().ToList().Last();
            Assert.AreEqual(neww.Title, name);
            Assert.AreEqual(neww.GraveTypeId, Grave1.Id);
            Assert.AreEqual(neww.FlowersId, Flower1.Id);
            Assert.AreEqual(neww.CoffinTypeId, Coffin1.Id);
        }

        [Test]
        public void CoffinCreatesRight()
        {
            string name = "test";
            Coffin create = new Coffin();
            create.Title = name;
            create.MaterialId = 1;
            create.SizeId = 1;
            create.Price = 100;

            CoffinsDB.GetDb().Insert(create);

            Coffin neww = CoffinsDB.GetDb().SelectAll().ToList().Last();

            int materialId = 1;
            int sizeId = 1;
            int price = 100;


            Assert.AreEqual(neww.Title, name);
            Assert.AreEqual(neww.MaterialId, materialId);
            Assert.AreEqual(neww.SizeId, sizeId);
            Assert.AreEqual(neww.Price, price);
        }


        [Test]
        public void CheckDateWorks()
        {
            string dateCheckResult = " ";
            DateTime checkDate = OrganizationsDB.GetDb().SelectAll().Last().Date; //специально выбирается уже занятая дата для дальнейшей проверки метода
            //тут код из команды проверки даты скопирован
            ObservableCollection<Organization> list = new ObservableCollection<Organization>(OrganizationsDB.GetDb().SelectAll());
            foreach (Organization o in list)
            {
                if (o.Date.Year == checkDate.Year && o.Date.Month == checkDate.Month && o.Date.Day == checkDate.Day)
                {
                    dateCheckResult = "Занято";
                }
            }
            if (dateCheckResult != "Занято")
                dateCheckResult = "Свободно";
            string resEXP = "Занято";

            Assert.AreEqual(dateCheckResult, resEXP);
        }

        [Test]
        public void GraveCreatesRight()
        {
            string name = "test";
            Grave create = new Grave();
            create.Title = name;
            create.MaterialId = 1;
            create.FormId = 1;
            create.Price = 100;

            GravesDB.GetDb().Insert(create);

            Grave neww = GravesDB.GetDb().SelectAll().ToList().Last();

            int materialId = 1;
            int formId = 1;
            int price = 100;


            Assert.AreEqual(neww.Title, name);
            Assert.AreEqual(neww.MaterialId, materialId);
            Assert.AreEqual(neww.FormId, formId);
            Assert.AreEqual(neww.Price, price);
        }

        [Test]
        public void FlowersCreatesRight()
        {
            string name = "test";
            Flower create = new Flower();
            create.Title = name;
            create.Count = 13;
            create.Price = 100;

            FlowersDB.GetDb().Insert(create);

            Flower neww = FlowersDB.GetDb().SelectAll().ToList().Last();

            int count = 13;
            int price = 100;


            Assert.AreEqual(neww.Title, name);
            Assert.AreEqual(neww.Count, count);
            Assert.AreEqual(neww.Price, price);
        }


    }
}