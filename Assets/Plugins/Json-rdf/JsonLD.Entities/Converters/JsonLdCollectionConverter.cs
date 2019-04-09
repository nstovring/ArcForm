﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NullGuard;

namespace JsonLD.Entities.Converters
{
    /// <summary>
    /// Converter for JSON-LD collections
    /// </summary>
    /// <typeparam name="T">collection element type</typeparam>
    public abstract class JsonLdCollectionConverter<T> : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        public override void WriteJson(JsonWriter writer, [AllowNull] object value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            foreach (var element in (IEnumerable<T>)value)
            {
                serializer.Serialize(writer, element);
            }

            writer.WriteEndArray();
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, [AllowNull] object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                return this.CreateReturnedContainer(GetElementsFromArray(reader, serializer));
            }

            var resultObject = serializer.Deserialize<T>(reader);
            return this.CreateReturnedContainer(new[] { resultObject });
        }

        /// <summary>
        /// Wraps element in collection object.
        /// </summary>
        protected abstract object CreateReturnedContainer(IEnumerable<T> elements);

        private static IEnumerable<T> GetElementsFromArray(JsonReader reader, JsonSerializer serializer)
        {
            reader.Read();
            while (reader.TokenType != JsonToken.EndArray)
            {
                yield return serializer.Deserialize<T>(reader);
                reader.Read();
            }
        }
    }
}
