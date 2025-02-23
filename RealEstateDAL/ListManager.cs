using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RealEstateDAL
{
    public class ListManager<T> : IListManager<T>
    {
        private List<T> m_list;

        public ListManager()
        {
            m_list = new List<T>();
        }

        #region All method definitions
        /// <summary>
        /// Return the number of items in the collection m_list
        /// </summary>
        public int Count
        {
            get { return m_list.Count(); }   // get method
        }

        /// <summary>
        /// Add an object to the collection m_list.
        /// </summary>
        /// <param name="aType">A type.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Add(T aType)
        {
            if (aType == null)
            {
                return false;
            }

            m_list.Add(aType);
            return true;
        }

        /// <summary>
        /// Remove an object from the collection m_list at
        /// a given position.
        /// </summary>
        /// <param name="anIndex">Index to object that is to be removed.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool DeleteAt(int anIndex)
        {
            if (CheckIndex(anIndex))
            {
                m_list.RemoveAt(anIndex);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Replace an object from the collection at a given index by a new object.
        /// </summary>
        /// <param name="aType">Object to be replaced.</param>
        /// <param name="anIndex">index to element to be replaced by a new object.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public bool ChangeAt(T aType, int anIndex)
        {
            if (aType != null && CheckIndex(anIndex))
            {
                m_list.RemoveAt(anIndex);
                m_list.Insert(anIndex, aType);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Return an object at a given position from the collection m_list.
        /// </summary>
        /// <param name="anIndex">.</param>
        /// <returns></returns>
        public T GetAt(int anIndex)
        {
            if (CheckIndex(anIndex))
            {
                return m_list[anIndex];
            }
            return default;
        }

        /// <summary>
        /// Control that a given index is >= 0 and less than the number of items in 
        /// the collection.
        /// </summary>
        /// <returns>True if successful, false otherwise.</returns>
        public bool CheckIndex(int index)
        {
            if (index >= 0 && index < Count)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes all object of the collection and set the collection to null.
        /// </summary>
        public void DeleteAll()
        {
            m_list.Clear();
            m_list = null;
            return;
        }

        /// <summary>
        /// Deletes the current list and replaces it with a fresh one.
        /// </summary>
        public void CreateNewList()
        {
            DeleteAll();
            m_list = new List<T>();
        }

        /// <summary>
        /// Prepare a list of strings where each string represents info
        /// about an object in the collection. The info can typically come
        /// from the object's ToString method.
        /// </summary>
        /// <returns>The collection containing strings representing an object in
        /// the collection.</returns>
        public List<string> ToStringList()
        {
            List<string> list = new List<string>();

            foreach(T aType in m_list)
            {
                list.Add(aType.ToString());
            }

            return list;
        }

        /// <summary>
        /// Same as as ToStringList but returning a array of strings.
        /// </summary>
        /// <returns></returns>
        public string[] ToStringArray()
        {
            string[] strOut = new string[Count];
            int i = 0;

            foreach (T aType in m_list)
            {
                strOut[i] += aType.ToString();
                i += 1;
            }
            return strOut;
        }

        /// <summary>
        /// Get a copy of the List.
        /// </summary>
        /// <returns></returns>
        public List<T> GetFullList()
        {
            return m_list;
        }

        #endregion

        #region Serialization 

        /// <summary>
        /// Method to serialize m_list.
        /// </summary>
        /// <param name="fileName">File path including the name of the file to be serialized.</param>
        /// <returns></returns>
        public bool BinarySerialize(string fileName)
        {
            using (Stream stream = File.Open(fileName, FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, m_list);
            }
            return true;
        }

        /// <summary>
        /// Method to deserialize a file and create an m_list.
        /// </summary>
        /// <param name="fileName">File path including the name of the file to be deserialized.</param>
        /// <returns></returns>
        public bool BinaryDeSerialize(string fileName)
        {
            using (Stream stream = File.Open(fileName, FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                DeleteAll(); //remove all objects
                m_list = (List<T>)bin.Deserialize(stream);
            }
            return true;
        }

        //Not implemented due to time constraints
        public bool XMLSerialize(string fileName)
        {
            //TODO
            return false;
        }

        #endregion
    }
}
