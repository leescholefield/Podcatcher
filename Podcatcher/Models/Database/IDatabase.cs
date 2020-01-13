using System.Collections.Generic;

namespace Podcatcher.Models.Database
{
    /// <summary>
    /// Interface for a generic model-orientated Database
    /// </summary>
    /// <typeparam name="T">Class that this database can save / retrieve.</typeparam>
    public interface IDatabase<T>
    {

        void Insert(T toInsert);

        /// <summary>
        /// Deletes the passed object from the database.
        /// </summary>
        void Delete(T toDelete);

        /// <summary>
        /// Deletes an object by id.
        /// </summary>
        /// <param name="id"></param>
        void Delete(long id);

        List<T> AllRecords();
    }
}
