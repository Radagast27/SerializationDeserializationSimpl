using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SerializationDeserializationSimpl
{
    class Program
    {
        static void Main(string[] args)
        {
            Album album = new Album(){ AlbumId = 1, AlbumName = "James Vincent McMorrow", ArtistName="Post Tropical", CurrentBillboardRank=1045, HighestBillboardRank=102, SoundScanUnits=207000};
            Console.WriteLine("Original object in memory:");
            album.WriteSummary();

            Console.WriteLine("Press Enter to Serialize this object to XML");
            Console.ReadLine();

            //Serialize it to XML file (disk) form
            var serializer = new XmlSerializer(album.GetType());
            using (var writer = XmlWriter.Create("album.xml"))
            {
                serializer.Serialize(writer, album); //serializes to XmlWriter for later deserialization back into Album obj
                var sWriter = new StringWriter();
                serializer.Serialize(sWriter, album); //serializes to StringWriter for display in the Console via WriteLine
                Console.WriteLine(sWriter.ToString());
                Console.WriteLine("------------------------------------");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("------------------------------------");
            }

            Console.WriteLine("Serialization to XML sucessfull!");
            Console.WriteLine("------------------------------------");
            album.WriteSummary();
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Press Enter to Deserialize from the XML");
            Console.ReadLine();

            //Deserialize from that XML back into object (RAM) form
            using (var reader = XmlReader.Create("album.xml"))
            {
                var albumDeserialized = (Album)serializer.Deserialize(reader);
                Console.WriteLine("Deserialization from XML sucessfull!");
                Console.WriteLine("------------------------------------");
                albumDeserialized.WriteSummary();
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }
        }
    }

    public class Album
    {
        public int AlbumId;
        public string AlbumName;
        public string ArtistName;
        public int SoundScanUnits;
        public int CurrentBillboardRank;
        public int HighestBillboardRank;

        public void WriteSummary()
        {
            Console.WriteLine("AlbumId: " + AlbumId);
            Console.WriteLine("Album: " + AlbumName);
            Console.WriteLine("Artist: " + ArtistName);
            Console.WriteLine("SoundScanUnits: " + SoundScanUnits);
            Console.WriteLine("CurrentBillboardRank: " + CurrentBillboardRank);
            Console.WriteLine("HighestBillboardRank: " + HighestBillboardRank);
        }
    }
}
