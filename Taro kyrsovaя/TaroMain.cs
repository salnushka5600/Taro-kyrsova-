using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Reflection.Metadata.BlobBuilder;

namespace Taro_kyrsovaя
{

    internal class TaroMain : BaseVM
    {
        private string searchs;

        public string Searchs
        {

            get => searchs;
            set
            {
                searchs = value;
                Search(searchs);
                SearchSpecialization(searchs);
                SearchMasters(searchs);
                SearchServices(searchs);
                SearchShedules(searchs);
            }
        }







        private Master selectedmaster;
        private ObservableCollection<Master> masters = new();


        public ObservableCollection<Master> Masters
        {
            get => masters;
            set
            {
                masters = value;
                Signal();
            }
        }
        public Master SelectedMaster
        {
            get => selectedmaster;
            set
            {
                selectedmaster = value;
                Signal();
            }
        }



        private Shedule selectedshedule;
        private ObservableCollection<Shedule> shedules = new();


        public ObservableCollection<Shedule> Shedules
        {
            get => shedules;
            set
            {
                shedules = value;
                Signal();
            }
        }
        public Shedule Selectedshedule 
        {
            get => selectedshedule;
            set
            {
                selectedshedule = value;
                Signal();
            }
        }



        private specialization selectedspecialization;
        private ObservableCollection<specialization> specializations = new();


        public ObservableCollection<specialization> Specializations
        {
            get => specializations;
            set
            {
                specializations = value;
                Signal();
            }
        }
        public specialization Selectedspecialization
        {
            get => selectedspecialization;
            set
            {
                selectedspecialization = value;
                Signal();
            }
        }



        private Masterspecialization selectedmasterspecialization;
        private ObservableCollection<Masterspecialization> masterspecializations = new();


        public ObservableCollection<Masterspecialization> Masterspecializations
        {
            get => masterspecializations;
            set
            {
                masterspecializations = value;
                Signal();
            }
        }
        public Masterspecialization Selectedmasterspecialization
        {
            get => selectedmasterspecialization;
            set
            {
                selectedmasterspecialization = value;
                Signal();
            }
        }






        private Service selectedservice;
        private ObservableCollection<Service> services = new();


        public ObservableCollection<Service> Services
        {
            get => services;
            set
            {
                services = value;
                Signal();
            }
        }
        public Service SelectedService
        {
            get => selectedservice;
            set
            {
                selectedservice = value;
                Signal();
            }
        }



        private Client selectedClient;
        private ObservableCollection<Client> clients = new();


        public ObservableCollection<Client> Clients
        {
            get => clients;
            set
            {
                clients = value;
                Signal();
            }
        }
        public Client SelectedClient
        {
            get => selectedClient;
            set
            {
                selectedClient = value;
                Signal();
            }
        }
        public CommandMvvm ADDClient { get; set; }
        public CommandMvvm ADDSpecialization { get; set; }
        public CommandMvvm ScanClients { get; set; }
        public CommandMvvm ScanSpecialization { get; set; }
        public CommandMvvm ADDMaster { get; set; }
        public CommandMvvm ScanMaster { get; set; }
        public CommandMvvm ADDService { get; set; }
        public CommandMvvm ADDSession { get; set; }
        public CommandMvvm ScanService { get; set; }
        public CommandMvvm ScanSession { get; set; }
        public CommandMvvm Removespetialization { get; set; }
        public CommandMvvm RemovesClient { get; set; }
        public CommandMvvm RemovesMaster { get; set; }
        public CommandMvvm RemovesService { get; set; }
        public CommandMvvm RemovesShedule { get; set; }



        public TaroMain()
        {
            SelectAll();

            ADDClient = new CommandMvvm(() =>
            {
                new ДобавитьКлиента(new Client()).ShowDialog();
               
            }, () => true);

            ADDSpecialization = new CommandMvvm(() =>
            {
                new Добавить_специализацию(new specialization()).ShowDialog();
               
            }, () => true);

            ScanClients = new CommandMvvm(() =>
            {
                new Посмотреть_клиента().ShowDialog();
                SelectAll();
               
            }, () => true);

            ScanSpecialization = new CommandMvvm(() =>
            {
                new Просмотр_специализаций().ShowDialog();
                SelectAll();
               
            }, () => true);

            ADDMaster = new CommandMvvm(() =>
            {
                new Добавить_мастера(new Master()).ShowDialog();
               
            }, () => true);

            ScanMaster = new CommandMvvm(() =>
            {
                new Просмотр_мастеров().ShowDialog();
              

            }, () => true);

            ADDService = new CommandMvvm(() =>
            {
                new Добавить_услугу(new Service()).ShowDialog();

            }, () => true);

            ADDSession = new CommandMvvm(() =>
            {
                new Добавить_расписание_услуг(new Shedule ()).ShowDialog();

            }, () => true);

            ScanService = new CommandMvvm(() =>
            {
                new Просмотр_услуг().ShowDialog();

            }, () => true);

            ScanSession = new CommandMvvm(() =>
            {
                new Просмотр_расписания_услуг().ShowDialog();
                SelectAll();

            }, () => true);

            Removespetialization = new CommandMvvm(() =>
            {

                var speci = MessageBox.Show("Вы уверены что хотите удалить специализацию ?", "Подтверждение", MessageBoxButton.YesNo);

                if (speci == MessageBoxResult.Yes)
                {
                    specializationDB.GetDb().Remove(Selectedspecialization);
                }
                SelectAll();
            }, () => true);





            RemovesClient = new CommandMvvm(() =>
            {

                var clien = MessageBox.Show("Вы уверены что хотите удалить клиента ?", "Подтверждение", MessageBoxButton.YesNo);

                if (clien == MessageBoxResult.Yes)
                {
                    ClientDB.GetDb().Remove(SelectedClient);
                }
                SelectAll();
            
            }, () => true);



            RemovesMaster = new CommandMvvm(() =>
            {

                var mast = MessageBox.Show("Вы уверены что хотите удалить мастера ?", "Подтверждение", MessageBoxButton.YesNo);

                if (mast == MessageBoxResult.Yes)
                {
                    MasterDB.GetDb().Remove(SelectedMaster);
                }
                SelectAll();

            }, () => SelectedMaster != null);

            RemovesService = new CommandMvvm(() =>
            {

                var servi = MessageBox.Show("Вы уверены что хотите удалить услугу ?", "Подтверждение", MessageBoxButton.YesNo);

                if (servi == MessageBoxResult.Yes)
                {
                    ServiceDB.GetDb().Remove(SelectedService);
                }
                SelectAll();

            }, () => true);

            RemovesShedule = new CommandMvvm(() =>
            {

                var shedu = MessageBox.Show("Вы уверены что хотите удалить расписание услуги ?", "Подтверждение", MessageBoxButton.YesNo);

                if (shedu == MessageBoxResult.Yes)
                {
                    SheduleDB.GetDb().Remove(Selectedshedule);
                }
                SelectAll();

            }, () => Selectedshedule != null &&
             Selectedshedule.IDMaster != 0 
            );

           

        }
        //обновление
        private void SelectAll()
        {
            Clients = new ObservableCollection<Client>(ClientDB.GetDb().SelectAll());
            Masters = new ObservableCollection<Master>(MasterDB.GetDb().SelectAllWithSpec());
            Specializations = new ObservableCollection<specialization>(specializationDB.GetDb().SelectAll());
            Services = new ObservableCollection<Service>(ServiceDB.GetDb().SelectAll());
            Shedules = new ObservableCollection<Shedule>(SheduleDB.GetDb().SelectAll());


           
        }
        //поисковики
        private void Search(string search)
        {

            Clients = new ObservableCollection<Client>(Serch.GetTable().Search(search));
        }

        private void SearchSpecialization(string search)
        {

            Specializations = new ObservableCollection<specialization>(Searchspecialization.GetTable().SearchSpecialization(search));
        }

        private void SearchMasters(string search)
        {

            Masters = new ObservableCollection<Master>(SearchMaster.GetTable().SearchMasters(search));
        }

        private void SearchServices(string search)
        {

            Services = new ObservableCollection<Service>(SearchService.GetTable().SearchServices(search));
        }

        public void SearchShedules(string search)
        {

            Shedules = new ObservableCollection<Shedule>(SearchShedule.GetTable().SearchShedules(search));
        }
    }
}
