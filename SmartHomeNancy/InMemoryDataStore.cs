// ****************************************************************************
// <copyright file="InMemoryDataStore.cs" author="Nikolaos Kokkinos">
// Copyright © Nikolaos Kokkinos
// </copyright>
// ****************************************************************************
// <author>Nikolaos Kokkinos</author>
// <email>nik.kokkinos@windowslive.com</email>
// <date>28.01.2015</date>
// <project>SmartHomeNancy</project>
// <web>http://nikolaoskokkinos.wordpress.com/</web>
// ****************************************************************************

using SmartHomeNancy.Model;
using System.Collections.Generic;

namespace SmartHomeNancy
{
    public class InMemoryDataStore:IDataStore
    {
        private Dictionary<string, Datapoint> store = new Dictionary<string, Datapoint>();
        public Dictionary<string, Datapoint> Store 
        { 
            get
            {
                return this.store;
            }
        }

        
        public IEnumerable<Datapoint> GetAll()
        {
            return this.store.Values;
        }


        public long Count
        {
            get { return this.store.Count; }
        }

        public bool TryAdd(Datapoint datapoint)
        {
            
            if (this.store.ContainsKey(datapoint.Name))
            {
                return false;
            }

            this.store.Add(datapoint.Name, datapoint);
            return true;
        }

        public bool TryRemove(string name)
        {
            if (this.store.ContainsKey(name))
            {
                this.store.Remove(name);
                return true;
            }
            return false;
        }

        public bool TryUpdate(Datapoint datapoint)
        {
            if (!this.store.ContainsKey(datapoint.Name))
            {
                return false;
            }
            this.store[datapoint.Name] = datapoint;
            return true;
        }
    }
}
