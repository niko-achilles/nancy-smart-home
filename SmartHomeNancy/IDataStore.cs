// ****************************************************************************
// <copyright file="IDataStore.cs" author="Nikolaos Kokkinos">
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
    public interface IDataStore
    {
        IEnumerable<Datapoint> GetAll();
        bool TryAdd(Datapoint datapoint);
        bool TryRemove(string name);
        bool TryUpdate(Datapoint datapoint);
    }
}
