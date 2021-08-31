using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBC_OOP_Queue
{
    public class Person
    {
        private string _name;
        private int _age;

        private string[] RandomNames { get; set; }

        private ConsoleColor color;

        public ConsoleColor Color
        {
            get { return color; }
            set { color = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Person(string name)
        {
            _name = name;

            Random rand = new Random();

            color = (ConsoleColor)rand.Next(0, 16);

            _age = rand.Next(18, 45);
        }

        



    }
}
