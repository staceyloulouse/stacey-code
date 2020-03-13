using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

namespace CSharpExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "AnimalData.dat";
            string xmlPath = "C:/Users/stace/source/repos/CSharpExample/CSharpExample/bowser.xml";
            string xmlPath2 = "C:/Users/stace/source/repos/CSharpExample/CSharpExample/animals.xml";

            Animal bowser = new Animal("Bowser", 45,25);
            Stream stream = File.Open(path, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, bowser);
            stream.Close();

            bowser = null;
            stream = File.Open(path, FileMode.Open);
            bowser = (Animal)bf.Deserialize(stream);
            stream.Close();
            Console.WriteLine(bowser.ToString());

            bowser.Weight = 50;

            XmlSerializer serializer = new XmlSerializer(typeof(Animal));
            using(TextWriter tw = new StreamWriter(xmlPath))
            {
                serializer.Serialize(tw, bowser);
            }
            bowser = null;
            XmlSerializer deserializer =new XmlSerializer(typeof(Animal));
            TextReader reader = new StreamReader(xmlPath);
            object obj = deserializer.Deserialize(reader);
            bowser = (Animal)obj;
            reader.Close();

            Console.WriteLine(bowser.ToString());
            //save a collection of Animals
            List<Animal> theAnimals = new List<Animal>
            {
                new Animal("Mario",60, 30),
                new Animal("Luigi",55, 24),
                new Animal("Peach",40, 20)
            };

            using (Stream fs = new FileStream(xmlPath2, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer serializer2 = new XmlSerializer(typeof(List<Animal>));
                serializer2.Serialize(fs, theAnimals);
            }
            theAnimals = null;
            XmlSerializer serializer3 = new XmlSerializer(typeof(List<Animal>));

            using (FileStream fs2 = File.OpenRead(@xmlPath2))
            {
                theAnimals = (List<Animal>)serializer3.Deserialize(fs2);
            }

            foreach (Animal a in theAnimals)
            {
                Console.WriteLine(a.ToString());
            }

            Console.ReadLine();
        }
    }
}
