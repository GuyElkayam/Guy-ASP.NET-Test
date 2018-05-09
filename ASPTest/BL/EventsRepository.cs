using ASPTest.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPTest
{
    public class EventsRepository : IEvents
    {
        IData _DataRepository = new DataRepository();
        const int EVENT_REPOSITORY_MAX_VALUE = 100;
        List<ItemWithNextPosition> Items = null;
        Random _rand = new Random();
        public EventsRepository()
        {
            int value = 0;

            //Best to cache results and not re-load them from the DB each call but we are on a time limit on this assignment.
            //Items = _DataRepository.GetItems();
            if (Items == null)
            {
                Items = new List<ItemWithNextPosition>();
                //Build Items if no previously saved results.
                //Array length can also be configured or const.
                //***** Important to note: return position won't be visually correct since the data generated
                // is re-generated in the server and the resulting position is for a new data set not shown. *****
                for (int i = 0; i < 100; i++)
                {
                    value = _rand.Next(0, EVENT_REPOSITORY_MAX_VALUE);
                    //Items are not null but if they are the system will show empty array instead of crashing.

                    int lastIndex = Items.FindLastIndex(a => a.eventId == value);
                    if (lastIndex != -1)
                    {
                        Items[lastIndex].eventNextPosition = i;
                    }
                    Items.Add(new ItemWithNextPosition { eventId = value, eventNextPosition = -1 });
                }
                _DataRepository.SaveItems(Items);
            }

        }

        public int Next(int Postion)
        {
            //Array count as seen here is 100..not 10k so limit checks as according.
            if (Items.Count == 0 || Postion < 0 || Postion > (Items.Count - 1))
                //Illegal values or empty array. (Can be different results or specify an alert to the user regarding the usage of the form.)
                return -1;

            //Work has been done already when we created the data structure - which was although costly; saved us every other iteration complexity.
            return Items[Postion].eventNextPosition;
        }

        public List<ItemWithNextPosition> GetAllItems()
        {
            return Items;
        }
    }

    //public for use in Data Repository
    //Used since Tuple is read only - will not allow changes once set, which this scenario requires.
    public class ItemWithNextPosition
    {
        public int eventId { get; set; }
        public int eventNextPosition { get; set; }
    }

    interface IEvents
    {
        int Next(int Postion);
        List<ItemWithNextPosition> GetAllItems();

    }
}
