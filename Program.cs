using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RisLab5
{
    class Program
    {
        static void Main(string[] args)
        {
            bookBus();
        }

        static void bookBus()
        {
            Console.WriteLine("!Select a destination from the list below: ");

            XElement root = XElement.Load("buses.xml");
            IEnumerable<XElement> buses = from el in root.Elements("Destination")
                                          select el;
            int i = 1;
            foreach (XElement el in buses)
            {
                Console.WriteLine(i++ + ") " + el.Element("City").Value);
            }

            XElement picked = buses.ElementAt(int.Parse(Console.ReadLine()) - 1);
            string dest = picked.Element("City").Value;

            Console.WriteLine("Select a time from the list below: ");

            IEnumerable<XElement> times = (from el in root.Elements("Destination")
                                           where (string)el.Element("City") == dest
                                           select el).Elements("Buses").First().Elements("Bus");

            i = 1;
            foreach (XElement el in times)
            {
                Console.WriteLine(i++ + ") " + el.Element("Date").Value);
            }

            XElement pickedBus = times.ElementAt(int.Parse(Console.ReadLine()) - 1);
            string date = pickedBus.Element("Date").Value;

            Console.WriteLine("1) Register.\n 2) Unregister.");
            switch(int.Parse(Console.ReadLine()))
            {
                case 1:                    
                    registerPerson(dest, date);
                    decreasePlaces(pickedBus);
                    break;
                case 2:
                    unregisterPerson(dest, date);
                    increasePlaces(pickedBus);
                    break;                
            }

        }

        private static void registerPerson(string dest, string date)
        {
            Console.WriteLine("Your Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Your Surname: ");
            string surname = Console.ReadLine();
            Console.WriteLine("Your Phone Number: ");
            string phone = Console.ReadLine();

            Console.WriteLine("Your Pass Serie: ");
            string serie = Console.ReadLine();
            Console.WriteLine("Your Pass Number: ");
            string num = Console.ReadLine();

            XElement rootBook = XElement.Load("bookings.xml");
            rootBook.Add(new XElement("Booking",
                new XElement("City", dest),
                new XElement("Date", date),
                new XElement("Name", name),
                new XElement("Surname", surname),
                new XElement("Phone", phone),
                new XElement("Serie", serie),
                new XElement("Number", num)));
            rootBook.Save("bookings.xml");
        }

        private static void unregisterPerson(string dest, string date)
        {
            Console.WriteLine("Your Pass Serie: ");
            string serie = Console.ReadLine();
            Console.WriteLine("Your Pass Number: ");
            string num = Console.ReadLine();

            XElement root = XElement.Load("bookings.xml");
            (from el in root.Elements("Booking")
                                           where (string)el.Element("City") == dest &&
                                           el.Element("Date").Value == date &&
                                           el.Element("Serie").Value == serie &&
                                           el.Element("Number").Value == num
                                           select el).Remove();
            root.Save("bookings.xml");
        }

        private static void increasePlaces(XElement picked)
        {
            int free = int.Parse(picked.Element("FreePlaces").Value) + 1;
            picked.Element("FreePlaces").ReplaceWith(new XElement("FreePlaces", free));
            picked.Parent.Parent.Parent.Save("buses.xml");
        }

        private static void decreasePlaces(XElement picked)
        {
            int free = int.Parse(picked.Element("FreePlaces").Value) - 1;
            if (free == 0)
            {
                picked.Parent.Remove();
            }
            picked.Element("FreePlaces").ReplaceWith(new XElement("FreePlaces", free));
            picked.Parent.Parent.Parent.Save("buses.xml");
        }
    }
}
