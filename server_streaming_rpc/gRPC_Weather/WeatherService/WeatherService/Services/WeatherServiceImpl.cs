using Grpc.Core;
using WeatherService.Protos;

namespace WeatherService.Services
{
    public class WeatherServiceImpl : Weather.WeatherBase //Bu sınıf, bizim proto dosyasındaki Weather servisini hayata geçiriyor.
    {
        public override async Task StreamWeather(WeatherRequest request, IServerStreamWriter<WeatherReply> responseStream, ServerCallContext context)
        /*
         Client’tan city bilgisi gelir.
        responseStream.WriteAsync(...) ile server, sürekli mesaj yollar.
        Task.Delay(2000) → 2 saniyede bir veri gönderilir.
         */
        {
            var random = new Random();

            for (int i = 0; i < 10; i++) // 10 kere veri gönder
            {
                if (context.CancellationToken.IsCancellationRequested)
                    break;

                var reply = new WeatherReply
                {
                    City = request.City,
                    Temperature = $"{random.Next(20, 35)} °C",
                    Humidity = $"{random.Next(40, 70)} %",
                    Timestamp = DateTime.Now.ToString("HH:mm:ss")
                };

                await responseStream.WriteAsync(reply);
                await Task.Delay(2000); // 2 saniye bekle
            }
        }
    }
}
