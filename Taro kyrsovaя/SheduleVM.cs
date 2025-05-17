using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taro_kyrsovaя
{
   internal class SheduleVM : BaseVM
    {
        private Shedule newshedule = new();

        public Shedule NewShedule
        {
            get => newshedule;
            set
            {
                newshedule = value;
                Signal();
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


        public CommandMvvm Insertshedule { get; set; }
        public SheduleVM()
        {
            Insertshedule = new CommandMvvm(() =>
            {

                NewShedule.IDClients = SelectedClient.Id;
                NewShedule.IDMaster = SelectedMaster.Id;
                NewShedule.IDService = SelectedService.Id;

                SheduleDB.GetDb().Insert(NewShedule);
                    close?.Invoke();
                



            },
                () => true);
               





        }

        public void SetShedule(Shedule selectedshedule)
        {
            NewShedule = selectedshedule;
            SelectAll();
        }
        private void SelectAll()
        {
            Masters = new ObservableCollection<Master>(MasterDB.GetDb().SelectAll());
            Clients = new ObservableCollection<Client>(ClientDB.GetDb().SelectAll());
            Services = new ObservableCollection<Service>(ServiceDB.GetDb().SelectAll());

        }

        Action close;
        internal void SetClose(Action close)
        {
            this.close = close;
        }
    }
}
