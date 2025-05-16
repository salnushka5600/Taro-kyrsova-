using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Taro_kyrsovaя
{
    internal class addmasterVM : BaseVM
    {
        private Master newMaster = new();

        public Master NewMaster
        {
            get => newMaster;
            set
            {
                newMaster = value;
                Signal();
            }
        }

        public List<specialization> Specializations { get; set; }

        public CommandMvvm InsertMaster { get; set; }
        public addmasterVM()
        {
            Specializations = specializationDB.GetDb().SelectAll();
            InsertMaster = new CommandMvvm(() =>
            {
                bool insert = NewMaster.Id == 0;

                if (insert)
                    MasterDB.GetDb().Insert(NewMaster);
                else
                    MasterDB.GetDb().Update(NewMaster);
                MasterspecializationDB.GetDb().RemoveByMaster(NewMaster.Id);

                foreach (specialization s in listSpec.SelectedItems)
                {
                    MasterspecializationDB.GetDb().Insert(new Masterspecialization { IdMaster = NewMaster.Id, Idspecialization = s.Id });
                }

                close?.Invoke();


            },
                () =>

                !string.IsNullOrEmpty(newMaster.Name) &&
                !string.IsNullOrEmpty(newMaster.SurName));


        }

        public void SetMaster(Master selectedMaster)
        {
            NewMaster = selectedMaster;
            if (newMaster.Id != 0)
            {
                var specs = MasterspecializationDB.GetDb().SelectByMaster(newMaster.Id).Select(s => s.Idspecialization);


                foreach (var spec in Specializations)
                {
                    if (specs.Contains(spec.Id))
                    {
                        listSpec.SelectedItems.Add(spec);
                    }
                }

            }
        }

            Action close;
        internal void SetClose(Action close)
        {
            this.close = close;
        }
        ListBox listSpec;
        internal void SetListSpec(ListBox listSpec)
        {
            this.listSpec = listSpec;
        }

    }
}
