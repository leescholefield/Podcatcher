using System.Collections.Generic;

namespace Podcatcher.Models.Database
{
    public interface IDatabase
    {
        /// <summary>
        /// Inserts the given <paramref name="values"/> into the given table.
        /// </summary>
        void Insert(string table, Dictionary<string, object> values);

        /// <summary>
        /// Searches a database for all columns matching the given values.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="values"></param>
        /// <seealso cref="Search(string, long)"/>
        List<Dictionary<string, object>> Search(string table, Dictionary<string, object> values);

        /// <summary>
        /// Searches the given table for the row matching <paramref name="id"/>
        /// </summary>
        /// <param name="table"></param>
        /// <param name="id"></param>
        /// <returns>A dictionary containing all rows values. Or an empty dictionary if no recrod was found.</returns>
        Dictionary<string, object> Search(string table, long id);
        

        /// <summary>
        /// Deletes an object by id.
        /// </summary>
        /// <param name="id"></param>
        void Delete(string table, long id);

        /// <summary>
        /// Deletes all records where every value in <paramref name="values"/> matches.
        /// </summary>
        void Delete(string table, Dictionary<string, object> values);
    }
}
