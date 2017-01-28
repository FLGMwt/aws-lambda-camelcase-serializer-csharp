using Amazon.Lambda.Core;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AwsLambdaCamelCaseSerializer
{
    public class AwsLambdaCamelCaseSerializer : ILambdaSerializer
    {
        private JsonSerializer serializer;

        public AwsLambdaCamelCaseSerializer()
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            serializer = JsonSerializer.Create(serializerSettings);
        }

        public T Deserialize<T>(Stream requestStream)
        {
            var streamReader = new StreamReader(requestStream);
            var jsonTextReader = new JsonTextReader(streamReader);
            return serializer.Deserialize<T>(jsonTextReader);
        }

        public void Serialize<T>(T response, Stream responseStream)
        {
            var streamWriter = new StreamWriter(responseStream);
            serializer.Serialize(streamWriter, response);
            streamWriter.Flush();
        }
    }
}
