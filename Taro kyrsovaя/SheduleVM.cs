using System;
using System.Collections.Generic;
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

        public CommandMvvm Insertshedule { get; set; }
        public SheduleVM()
        {
            Insertshedule = new CommandMvvm(() =>
            {

                if (newshedule.Id == 0)
                {
                    SheduleDB.GetDb().Insert(NewShedule);
                    close?.Invoke();
                }



            },
                () =>
                !string.IsNullOrEmpty(newshedule.IDClients) &&
                !string.IsNullOrEmpty(newshedule.IDService) &&
                !string.IsNullOrEmpty(newshedule.IDMaster) &&
                !string.IsNullOrEmpty(newshedule.Date) &&
                !string.IsNullOrEmpty(newshedule.Servicestatus));





        }

        public void Setshedule(Shedule selectedshedule)
        {
            NewShedule = selectedshedule;
        }

        Action close;
        internal void SetClose(Action close)
        {
            this.close = close;
        }
    }
}
