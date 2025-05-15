using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taro_kyrsovaя
{
    internal class MasterspecializationVM : BaseVM
    {
        private Masterspecialization newmasterspecialization = new();

        public Masterspecialization NewMasterspecialization
        {
            get => newmasterspecialization;
            set
            {
                newmasterspecialization = value;
                Signal();
            }
        }

        public CommandMvvm Insertmasterspecialization { get; set; }
        public MasterspecializationVM()
        {
            Insertmasterspecialization = new CommandMvvm(() =>
            {

                if (newmasterspecialization.Id == 0)
                {
                    MasterspecializationDB.GetDb().Insert(NewMasterspecialization);
                    close?.Invoke();
                }



            },
                () => true);






        }

        public void Setmasterspecialization(Masterspecialization selectedmasterspecialization)
        {
            NewMasterspecialization = selectedmasterspecialization;
        }

        Action close;
        internal void SetClose(Action close)
        {
            this.close = close;
        }
    }
}
