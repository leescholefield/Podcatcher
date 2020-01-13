using System.Collections.Generic;

namespace Podcatcher.Models.Database
{
    /// <summary>
    /// Interface for a generic model-orientated Database
    /// </summary>
    /// <typeparam name="T">Class that this database can save / retrieve.</typeparam>
    public interface IDatabase
    {
        /// <summary>
        /// Inserts the given <paramref name="values"/> into the given table.
        /// </summary>
        /// <param name="table">Table to insert into. If no table is found with the given name this will throw an Exception. </param>
        void Insert(string table, ContentValues values);

        /// <summary>
        /// Deletes an object by id.
        /// </summary>
        /// <param name="id"></param>
        void Delete(long id);

        List<ContentValues> AllRecords();
    }
}
