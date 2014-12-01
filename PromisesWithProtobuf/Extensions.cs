using System;
using System.IO;
using ProtoBuf;
using Termine.Promises.WithProtobuf.Interfaces;

// ReSharper disable once CheckNamespace
namespace Termine.Promises
{
    public static class Extensions
    {
        public static void Serialize<TT>(this TT model, Stream destinationStream) where TT : class, ISupportProtobuf
        {
            Serializer.Serialize(destinationStream, model);
        }

        public static TT Deserialize<TT>(this Stream inputStream) where TT : class, ISupportProtobuf
        {
            return Serializer.Deserialize<TT>(inputStream);
        }

        public static Byte[] ToByteArray<TT>(this TT model) where TT : class, ISupportProtobuf
        {
            var memoryStream = new MemoryStream();
            
            Serializer.Serialize(memoryStream, model);

            memoryStream.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream.ToArray();
        }

        public static TT FromByteArray<TT>(this byte[] modelBytes) where TT : class, ISupportProtobuf
        {
            using (var memoryStream = new MemoryStream(modelBytes))
            {
                return Serializer.Deserialize<TT>(memoryStream);
            }
        }
    }
}
