using n5.webApi.Models.dto;
using n5.webApi.Services.Interface;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace n5.webApi.Services.Impl
{
    public class ProducerKafka : IProducerKafka
    {
        private readonly IProducer<Null, string> _producer;
        private readonly IConfiguration _configuration;
        public ProducerKafka(IProducer<Null, string> producer, IConfiguration configuration)
        {
            _producer = producer;
            _configuration = configuration;
        }
        public void Publish(EventDto eventDto)
        {
            _producer.Produce(_configuration["TopicKafka"], new Message<Null, string>{
                Value = JsonConvert.SerializeObject(eventDto)
            });
        }
    }
}