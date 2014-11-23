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
    }
}
