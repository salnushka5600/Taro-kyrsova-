using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Taro_kyrsovaя
{

    internal class TaroMain : BaseVM
    {
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
                new Добавить_услугу().ShowDialog();

            }, () => true);

            ADDSession = new CommandMvvm(() =>
            {
                new Добавить_Сессию().ShowDialog();

            }, () => true);

            ScanService = new CommandMvvm(() =>
            {
                new Просмотр_услуг().ShowDialog();

            }, () => true);

            ScanSession = new CommandMvvm(() =>
            {
                new Просмотр_Сессий().ShowDialog();

            }, () => true);

            Removespetialization = new CommandMvvm(() =>
            {

                var teamvozvrta = MessageBox.Show("Вы уверены что хотите удалить специализацию ?", "Подтверждение", MessageBoxButton.YesNo);

                if (teamvozvrta == MessageBoxResult.Yes)
                {
                    specializationDB.GetDb().Remove(Selectedspecialization);
                }
                SelectAll();
            }, () => true);

        }
        private void SelectAll()
        {
           
            Masters = new ObservableCollection<Master>(MasterDB.GetDb().SelectAll());
            Specializations = new ObservableCollection<specialization>(specializationDB.GetDb().SelectAll());
        }
    }
}
