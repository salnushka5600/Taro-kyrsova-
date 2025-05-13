using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taro_kyrsovaя
{
    internal class specializationVM : BaseVM
    {
        private specialization newspecialization = new();

        public specialization Newspecialization
        {
            get => newspecialization;
            set
            {
                newspecialization = value;
                Signal();
            }
        }

        public CommandMvvm Insertspecialization { get; set; }
        public specializationVM()
        {
            Insertspecialization = new CommandMvvm(() =>
            {


                specializationDB.GetDb().Insert(Newspecialization);
                close?.Invoke();


            },
                () =>

                !string.IsNullOrEmpty(newspecialization.Title) &&
                !string.IsNullOrEmpty(newspecialization.Description));


        }

        public void Setspecialization(specialization selectedspecialization)
        {
            newspecialization = selectedspecialization;
        }

        Action close;
        internal void SetClose(Action close)
        {
            this.close = close;
        }


    }
}
