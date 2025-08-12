using Grpc.Core;
using Grpc.Net.Client;
using WeatherService.Protos;

Console.Write("Şehir girin: ");
var city = Console.ReadLine();

using var channel = GrpcChannel.ForAddress("https://localhost:7141");
var client = new Weather.WeatherClient(channel);

var request = new WeatherRequest { City = city };
using var call = client.StreamWeather(request);

Console.WriteLine($"\n{city} için hava durumu akışı başladı:\n");

await foreach (var reply in call.ResponseStream.ReadAllAsync())
{
    Console.WriteLine($"[{reply.Timestamp}] {reply.City}: {reply.Temperature}, Nem: {reply.Humidity}");
}
/*
 Server’dan gelen her cevabı foreach ile sırayla alıyoruz.
Ekrana yazdırıyoruz.
 */

