using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taro_kyrsovaя
{
    internal class ClientVM : BaseVM
    {
        private Client newclient = new();

        public Client Newclient
        {
            get => newclient;
            set
            {
                newclient = value;
                Signal();
            }
        }

        public CommandMvvm Insertclient { get; set; }
        public ClientVM()
        {
            Insertclient = new CommandMvvm(() =>
            {

                if (newclient.Id == 0)
                {
                    ClientDB.GetDb().Insert(Newclient);
                    close?.Invoke();
                }



            },
                () =>
                !string.IsNullOrEmpty(newclient.Name) &&
                !string.IsNullOrEmpty(newclient.Email) &&
               newclient.Dateregistration <= DateTime.Now &&
                Newclient.Age >= 18);





        }

        public void Setclient(Client selectedClient)
        {
            Newclient = selectedClient;
        }

        Action close;
        internal void SetClose(Action close)
        {
            this.close = close;
        }
    }
}
