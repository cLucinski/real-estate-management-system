using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBLL
{
    /// <summary>
    /// A class to contain the information of a person.
    /// </summary>
    [Serializable]
    public class Person
    {
        //Properties
        public string Name { get; set; }
        public Address Address { get; set; }

        //Constructor
        public Person()
        {
            Address = new Address();
        }
        //Constructor
        public Person (string name, Address address)
        {
            Name = name;
            Address = address;
        }

        //Override methods
        public override string ToString()
        {
            return $"{Name}: {Address}";
        }
    }
}
