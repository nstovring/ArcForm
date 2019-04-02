﻿using System.Linq;
using JsonLD.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NullGuard;

namespace JsonLD.Entities
{
    /// <summary>
    /// Entity serializer
    /// </summary>
    public class EntitySerializer : IEntitySerializer
    {
        private readonly ContextResolver contextResolver;
        private readonly IFrameProvider frameProvider;
        private readonly JsonSerializer jsonSerializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitySerializer"/> class.
        /// </summary>
        public EntitySerializer()
            : this(new NullContextProvider(), new NullFrameProvider())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitySerializer"/> class.
        /// </summary>
        /// <param name="contextProvider">The JSON-LD @context provider.</param>
        public EntitySerializer(IContextProvider contextProvider)
            : this(contextProvider, new NullFrameProvider())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitySerializer"/> class.
        /// </summary>
        /// <param name="contextProvider">The JSON-LD @context provider.</param>
        /// <param name="frameProvider">The JSON-LD frame provider.</param>
        public EntitySerializer(IContextProvider contextProvider, IFrameProvider frameProvider)
            : this(new ContextResolver(contextProvider))
        {
            this.frameProvider = frameProvider;
        }

        private EntitySerializer(ContextResolver contextResolver)
        {
            this.contextResolver = contextResolver;
            this.jsonSerializer = new JsonLdSerializer();
        }

        /// <summary>
        /// Deserializes the NQuads into a typed model
        /// </summary>
        /// <typeparam name="T">destination entity model type</typeparam>
        /// <param name="nQuads">RDF data in NQuads.</param>
        public T Deserialize<T>(string nQuads)
        {
            var jsonLd = JsonLdProcessor.FromRDF(nQuads);
            var context = this.contextResolver.GetContext(typeof(T));
            var frame = this.frameProvider.GetFrame(typeof(T));
            if (context == null)
            {
                throw new ContextNotFoundException(typeof(T));
            }

            return this.Deserialize<T>(jsonLd, context, frame);
        }

        /// <summary>
        /// Deserializes the JSON-LD object into a typed model.
        /// </summary>
        /// <typeparam name="T">destination entity model</typeparam>
        /// <param name="jsonLd">a JSON-LD object</param>
        public T Deserialize<T>(JToken jsonLd)
        {
            var jsonLdContext = this.contextResolver.GetContext(typeof(T));
            var frame = this.frameProvider.GetFrame(typeof(T));

            return this.Deserialize<T>(jsonLd, jsonLdContext, frame);
        }

        /// <summary>
        /// Serializes the specified entity as JSON-LD.
        /// </summary>
        /// <returns>
        /// A compacted JSON-LD object
        /// </returns>
        public JObject Serialize(object entity, [AllowNull] SerializationOptions options = null)
        {
            options = options ?? new SerializationOptions();
            var jsonLd = JObject.FromObject(entity, this.jsonSerializer);

            var context = this.contextResolver.GetContext(entity);
            if (context != null && IsNotEmpty(context))
            {
                jsonLd.AddFirst(new JProperty("@context", context));

                if (options.SerializeCompacted || entity.GetType().IsMarkedForCompaction())
                {
                    jsonLd = JsonLdProcessor.Compact(jsonLd, context, new JsonLdOptions());
                }
            }

            return jsonLd;
        }

        private static bool IsNotEmpty(JToken context)
        {
            if (context is JObject)
            {
                return ((JObject)context).Count > 0;
            }

            var array = context as JArray;
            if (array != null)
            {
                return array.Any(IsNotEmpty);
            }

            return true;
        }

        private T Deserialize<T>(JToken jsonLd, JToken context, JObject frame)
        {
            if (context == null)
            {
                return jsonLd.ToObject<T>(this.jsonSerializer);
            }

            if (frame == null)
            {
                return JsonLdProcessor.Compact(jsonLd, context, new JsonLdOptions()).ToObject<T>(this.jsonSerializer);
            }

            frame["@context"] = context;
            var framed = JsonLdProcessor.Frame(jsonLd, frame, new JsonLdOptions());
            return framed["@graph"].Single().ToObject<T>(this.jsonSerializer);
        }
    }
}
