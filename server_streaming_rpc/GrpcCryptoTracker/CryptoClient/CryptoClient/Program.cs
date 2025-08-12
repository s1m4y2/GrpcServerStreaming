using Grpc.Net.Client;
using CryptoService.Protos;
using Grpc.Core;

var httpHandler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
};

using var channel = GrpcChannel.ForAddress("https://localhost:7295", new GrpcChannelOptions //Server’a bağlantı kurulur.
{
    HttpHandler = httpHandler
});

var client = new Crypto.CryptoClient(channel); //gRPC istemci nesnesi oluşturulur.

Console.Write("Kripto para sembolü girin (örn: BTC, ETH): ");
var symbol = Console.ReadLine();

Console.WriteLine($"\n{symbol} fiyat takibi başladı:\n");

var request = new CryptoRequest { Symbol = symbol };

using var call = client.StreamPrices(request);

await foreach (var update in call.ResponseStream.ReadAllAsync())
{
    Console.WriteLine($"[{update.Timestamp}] {update.Symbol} - ${update.Price} | %{update.ChangePercent} | Hacim: {update.Volume}");
}
/*
 Sunucudan gelen veriler tek tek okunur.
Konsola yazılır.
 */
