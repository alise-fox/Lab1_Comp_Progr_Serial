using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using PointLib;

namespace FormsApp
{
    public class PointConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Point[]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var points = new List<Point>();
            var array = JArray.Load(reader);

            foreach (var item in array)
            {
                if (item["Z"] != null)
                {
                    var point3D = item.ToObject<Point3D>();
                    points.Add(point3D);
                }
                else
                {
                    var point = item.ToObject<Point>();
                    points.Add(point);
                }
            }

            return points.ToArray();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
