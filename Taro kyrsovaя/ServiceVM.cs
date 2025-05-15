using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taro_kyrsovaя
{
    internal class ServiceVM : BaseVM
    {
        private Service newservice = new();

        public Service NewService
        {
            get => newservice;
            set
            {
                newservice = value;
                Signal();

            }
        }

        public CommandMvvm Insertservice { get; set; }
        public ServiceVM()
        {
            Insertservice = new CommandMvvm(() =>
            {

                if (newservice.Id == 0)
                {
                    ServiceDB.GetDb().Insert(NewService);
                    close?.Invoke();
                }



            },
                () =>
                !string.IsNullOrEmpty(newservice.Title) &&
                !string.IsNullOrEmpty(newservice.Description) &&
                NewService.Price >0 &&
               NewService.Sessionduration >30);





        }

        public void Setservice(Service selectedservice)
        {
            NewService = selectedservice;
        }

        Action close;
        internal void SetClose(Action close)
        {
            this.close = close;
        }
    }
}
