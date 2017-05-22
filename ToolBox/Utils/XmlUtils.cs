using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ToolBox.Utils
{
    public static class XmlUtils
    {
        public static string GetRootNode(string xml)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(new StringReader(xml));
                return doc.DocumentElement.Name;
            }
            catch { return null; }
        }
        public static object Deserialize(TextReader reader, Type type, string rootAttribute)
        {
            Contract.Requires(reader != null);
            Contract.Requires(type != null);

            XmlReaderSettings xSet = new XmlReaderSettings();
            xSet.CheckCharacters = false;
            xSet.CloseInput = true;
            
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = rootAttribute;
            xRoot.IsNullable = true;

            var serializer = new XmlSerializer(type, xRoot);

            using (var xmlReader = XmlReader.Create(reader, xSet))
            {
                return serializer.Deserialize(xmlReader);
            }
        }

        public static object Deserialize(TextReader reader, Type type)
        {
            Contract.Requires(reader != null);
            Contract.Requires(type != null);

            XmlReaderSettings xSet = new XmlReaderSettings();
            xSet.CheckCharacters = false;
            xSet.CloseInput = true;

            var serializer = new XmlSerializer(type);

            using (var xmlReader = XmlReader.Create(reader, xSet))
            {
                return serializer.Deserialize(xmlReader);
            }
        }

        public static object Deserialize(TextReader reader, string typeName)
        {
            Contract.Requires(reader != null);
            Contract.Requires(typeName != null);

            return Deserialize(reader, Type.GetType(typeName));
        }

        public static T Deserialize<T>(TextReader reader)
        {
            Contract.Requires(reader != null);

            return (T)Deserialize(reader, typeof(T));
        }

        public static object Deserialize(string serializedObject, Type type)
        {
            Contract.Requires(serializedObject != null);
            Contract.Requires(type != null);

            using (var reader = new StringReader(serializedObject))
            {
                return Deserialize(reader, type);
            }
        }

        public static object Deserialize(string serializedObject, string typeName)
        {
            Contract.Requires(serializedObject != null);
            Contract.Requires(typeName != null);

            return Deserialize(serializedObject, Type.GetType(typeName));
        }

        public static T Deserialize<T>(string serializedObject)
        {
            Contract.Requires(serializedObject != null);

            return (T)Deserialize(serializedObject, typeof(T));
        }

        public static T Deserialize<T>(string serializedObject, string rootAttribute)
        {
            Contract.Requires(serializedObject != null);

            using (var reader = new StringReader(serializedObject))
            {
                return (T)Deserialize(reader, typeof(T), rootAttribute);
            }
        }

        public static void Serialize(TextWriter writer, Type type, object value)
        {
            Contract.Requires(writer != null);
            Contract.Requires(type != null);
            Contract.Requires(value != null);

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var serializer = new XmlSerializer(type);

            var writerSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "  ",
                Encoding = Encoding.UTF8,
                NewLineOnAttributes = false
            };


            using (var xmlWriter = XmlWriter.Create(writer, writerSettings))
            {
                serializer.Serialize(writer, value, ns);

                xmlWriter.Flush();
            }
        }

        public static void Serialize(TextWriter writer, string typeName, object value)
        {
            Contract.Requires(writer != null);
            Contract.Requires(typeName != null);
            Contract.Requires(value != null);

            Serialize(writer, Type.GetType(typeName), value);
        }

        public static void Serialize<T>(TextWriter writer, T value)
        {
            Contract.Requires(writer != null);
            Contract.Requires(value != null);

            Serialize(writer, typeof(T), value);
        }

        public static string Serialize(Type type, object value)
        {
            Contract.Requires(type != null);
            Contract.Requires(value != null);

            using (var writer = new Utf8StringWriter())
            {
                Serialize(writer, type, value);
                return writer.ToString();
            }
        }
        public static string Serialize(string typeName, object value)
        {
            Contract.Requires(typeName != null);
            Contract.Requires(value != null);

            return Serialize(Type.GetType(typeName), value);
        }

        public static string Serialize<T>(T value)
        {
            Contract.Requires(value != null);

            return Serialize(typeof(T), value);
        }
    }

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }
}
