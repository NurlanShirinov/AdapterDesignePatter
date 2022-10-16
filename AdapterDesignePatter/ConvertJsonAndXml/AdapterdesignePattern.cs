using AdapterDesignePatter.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdapterDesignePatter.ConvertJsonAndXml
{

    public interface IAdapter
    {
        void Write(Human human);
        void Read();
    }

    class JsonAdapter : IAdapter
    {
        private WriteJson _jsonWrite;

        public JsonAdapter(WriteJson jsonWrite)
        {
            _jsonWrite = jsonWrite;
        }

        public void Read()
        {
            _jsonWrite.ReadHuman();
        }

        public void Write(Human human)
        {
            _jsonWrite.WriteHuman(human);
        }
    }
    class WriteJson
    {
        public void WriteHuman(Human human)
        {
            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter("human.json"))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    jw.Formatting = Formatting.Indented;
                    serializer.Serialize(jw, human);
                }
            }
        }

        public Human ReadHuman()
        {
            Human human = null;
            var serializer = new JsonSerializer();
            using (var sr = new StreamReader("human.json"))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    human = serializer.Deserialize<Human>(jr);
                }
            }
            return human;
        }
    }

    public class XamlAdapter : IAdapter
    {
        private XamlWriter _Xamlwriter;

    
        public XamlAdapter(XamlWriter xamlWriter)
        {
            _Xamlwriter = xamlWriter;
        }
        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Write(Human human)
        {
            throw new NotImplementedException();
        }
    }

    public class XamlWriter
    {
        public void WriteHuman(Human human)
        {
            var xml = new XmlSerializer(typeof(Human));
            using (var fs = new FileStream("human.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, human);
            }
        }

        public void ReadHuman()
        {
            Human readXml = null;
            var xml2 = new XmlSerializer(typeof(Human));
            using (var fs = new FileStream("human.xml", FileMode.OpenOrCreate))
            {
                readXml = xml2.Deserialize(fs) as Human;
            }

        }
    }

    public class Converter
    {
        private readonly IAdapter _adapter;

        public Converter(IAdapter adapter)
        {
            _adapter = adapter;
        }

        public void Write(Human human)
        {
            _adapter.Write(human);
        }
        public void Read()
        {
            _adapter.Read();
        }
    }






}
