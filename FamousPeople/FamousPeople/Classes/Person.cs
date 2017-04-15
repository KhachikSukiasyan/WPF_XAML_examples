using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FamousPeople.Classes
{
    class Person
    {


        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageId { get; set; }
        

        public Person() { }
        public Person(string firstName, string lastName, string description, string imageId)
        {
            Name = firstName + lastName;
            Description = description;
            ImageId = imageId;
        }
    }
}
