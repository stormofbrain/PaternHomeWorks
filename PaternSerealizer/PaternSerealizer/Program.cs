using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;


namespace PaternSerealizer
{   
    [DataContract]
    public class Employer
    {
        [DataMember]
      public   string name { get; set; }
        [DataMember]
        public string surname { get; set; }
        [DataMember]
        public string position { get; set; }

       

    }




    interface ISerealizer
    {
        string Serealization(object obj);
        
    }

    class XmlSerealizer_class : ISerealizer
    {
        public string Serealization(object obj)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Employer));
            StringWriter sw = new StringWriter();
            ser.Serialize(sw, obj);
            return sw.ToString();
        }
    }
    class JsonSerealization_class : ISerealizer
    {
        public string Serealization(object obj)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Employer));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, obj);
            StreamWriter sw = new StreamWriter(ms);
            sw.Flush();
            ms.Position = 0;
            StreamReader sr=new StreamReader(ms);
            string str = sr.ReadToEnd();

            return str;
 
        }
    }

    public enum serealization { Json, Xml };

    class SerealizationFactory
    {
        public static ISerealizer GetSerealization(serealization s)
    
        {
            switch (s)
            {
                case serealization.Json:
                    {
                        return new JsonSerealization_class();
                    }
                case serealization.Xml:
                    {
                        return new XmlSerealizer_class();
                    }

                default:
                    return new XmlSerealizer_class();
            }
        }
    }



    class Program
    {
        static void Main(string[] args)
        {

            Run();

        }

        public static void Run()
        {
            Employer e = new Employer();
            e.name = "Osama";
            e.surname = "Ben'Laden";
            e.position = "Leader of Al'Kaida";
            ISerealizer s = SerealizationFactory.GetSerealization(serealization.Xml);
           string str= s.Serealization(e);
           Console.WriteLine(str);
        }
    }

  
}
