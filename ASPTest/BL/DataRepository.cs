using ASPTest.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPTest.BL
{
    public class DataRepository : IData
    {
        public void SaveItems(List<ItemWithNextPosition> Items)
        {
            try
            {
                using (Model context = new Model())
                {
                    var now = DateTime.Now;
                    foreach (var item in Items)
                    {
                        SelectedItems i = new SelectedItems();
                        i.Number = item.eventId;
                        i.ShowDate = now;
                        context.SelectedItems.Add(i);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception exp)
            {
            }
        }

        ////Acts as a sort of cache since there isn't one and there is precious little time.
        //public List<ItemWithNextPosition> GetItems()
        //{
        //    try
        //    {
        //        using (Model context = new Model())
        //        {
        //            List<ItemWithNextPosition> results = new List<ItemWithNextPosition>();
        //            var dbRes = context.SelectedItems;
        //            foreach (var item in dbRes)
        //            {
        //                results.Add(new ItemWithNextPosition { eventId= item.Number, eventNextPosition= item.NextPosition });
        //            }
        //            return results;
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// Return each  /// Will return each number and the count of times it is in the DB
        /// Gets Date to show from specific date or number
        /// 2,100 - the number 2 appears 100 times. 
        /// </summary>
        /// <param name="Date"></param>
        /// <returns>List of results with a number from 1-100 and the count of recurring values in the list later than the specified day.</returns>
        public List<Tuple<int, int>> GetItemsAccroding(DateTime Date)
        {
            return GetItemsWithCondition(Date, true);
        }

        /// <summary>
        /// Will return each number and the count of times it is in the DB
        /// 2,100 - the number 2 appears 100 times.
        /// </summary>
        /// <returns></returns>
        public List<Tuple<int, int>> GetItemsAndCount()
        {
            return GetItemsWithCondition(null);
        }

        private List<Tuple<int, int>> GetItemsWithCondition(DateTime? Date,  bool getDataFromDate = false)
        {
            try
            {
                List<Tuple<int, int>> ItemResult = new List<Tuple<int, int>>(100);

                using (Model context = new Model())
                {
                    var result = context.SelectedItems;
                    int numberOfAppearances = 0;
                    for (int i = 0; i < ItemResult.Count; i++)
                    {
                        //Check coinciding date.
                        IQueryable<SelectedItems> dbResult;
                        if(Date != null && getDataFromDate)
                        {
                            dbResult = context.SelectedItems.Where(x => x.Number == i && x.ShowDate > Date);
                        }
                        else
                        {
                            dbResult = context.SelectedItems.Where(x => x.Number == i);
                        }
                        if (dbResult != null)
                            numberOfAppearances = dbResult.Count();
                        else
                        {
                            numberOfAppearances = 0;
                        }
                        ItemResult.Add(new Tuple<int, int>(i, numberOfAppearances));
                    }
                }
                return ItemResult;
            }
            catch (NullReferenceException)
            {
                throw;
                //Special handling if DB data is null
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    interface IData
    {
        /// <summary>
        /// Saves Item in DB
        /// </summary>
        /// <param name="Items"></param>
        void SaveItems(List<ItemWithNextPosition> Items);

        /// <summary>
        /// Returns previous results without additional parameters.
        /// </summary>
        /// <returns>List of items with next position</returns>
        //List<ItemWithNextPosition> GetItems();

        List<Tuple<int, int>> GetItemsAccroding(DateTime Date);

        List<Tuple<int, int>> GetItemsAndCount();

    }
}
