using PointLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsApp
{
    public class FoxsFormatter
    {
        public static void Serialize(FileStream file, IEnumerable collection)
        {
            var lines = new List<string>();

            foreach (var item in collection)
            {
                var itemType = item.GetType();
                var itemFields = itemType.GetProperties();

                var fieldValues = itemFields.Select(field => field.GetValue(item)?.ToString() ?? "null");
                lines.Add($"{string.Join(" ", fieldValues)}");
            }

            string data = string.Join(Environment.NewLine, lines);
            byte[] bytes = new UTF8Encoding(true).GetBytes(data);
            file.Write(bytes, 0, bytes.Length);
        }

        public static IEnumerable Deserialize(FileStream file, Type elementType)
        {
            string serializedData = null;
            using (StreamReader reader = new StreamReader(file))
            {
                serializedData = reader.ReadToEnd();
            }
            var lines = serializedData.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var collection = new Point[5];

            for (int i = 0; i < lines.Length; ++i)
            {
                var parts = lines[i].Split(' ');
                if (parts.Length == 3)
                {
                    collection[i] = new Point3D(Int32.Parse(parts[1]), Int32.Parse(parts[2]), Int32.Parse(parts[0]));
                }
                else
                {
                    collection[i] = new Point(Int32.Parse(parts[0]), Int32.Parse(parts[1]));
                }
            }

            return collection;
        }
    }
}
