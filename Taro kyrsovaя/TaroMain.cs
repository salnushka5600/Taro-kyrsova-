using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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


        public TaroMain()
        {
            SelectAll();

            ADDClient = new CommandMvvm(() =>
            {
                new ДобавитьКлиента().ShowDialog();
               
            }, () => true);

            ADDSpecialization = new CommandMvvm(() =>
            {
                new Добавить_специализацию(new specialization()).ShowDialog();
               
            }, () => true);

            ScanClients = new CommandMvvm(() =>
            {
                new Посмотреть_клиента().ShowDialog();
               
            }, () => true);

            ScanSpecialization = new CommandMvvm(() =>
            {
                new Просмотр_специализаций().ShowDialog();
               ;
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

        }
        private void SelectAll()
        {
           
            Masters = new ObservableCollection<Master>(MasterDB.GetDb().SelectAll());
        }
    }
}
