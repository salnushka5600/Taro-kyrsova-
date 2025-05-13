using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

        public CommandMvvm InsertMaster { get; set; }
        public addmasterVM()
        {
            InsertMaster = new CommandMvvm(() =>
            {
               
               
                MasterDB.GetDb().Insert(NewMaster);
                close?.Invoke();


            },
                () =>

                !string.IsNullOrEmpty(newMaster.Name) &&
                !string.IsNullOrEmpty(newMaster.SurName));
                

        }

        public void SetMaster(Master selectedMaster)
        {
            NewMaster = selectedMaster;
        }

        Action close;
        internal void SetClose(Action close)
        {
            this.close = close;
        }


    }
}
