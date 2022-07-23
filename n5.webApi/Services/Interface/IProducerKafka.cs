using n5.webApi.Models.dto;

namespace n5.webApi.Services.Interface
{
    public interface IProducerKafka 
    {
        public void Publish(EventDto  eventDto);
    }
}