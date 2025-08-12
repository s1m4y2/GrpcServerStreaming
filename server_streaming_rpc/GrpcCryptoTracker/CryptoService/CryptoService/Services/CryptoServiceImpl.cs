using Grpc.Core;
using CryptoService.Protos;

namespace CryptoService.Services
{
    public class CryptoServiceImpl : Crypto.CryptoBase //CryptoBase sýnýfý, proto dosyasýndaki Crypto servisinin C# karþýlýðý.

    {
        public override async Task StreamPrices(CryptoRequest request, IServerStreamWriter<CryptoReply> responseStream, ServerCallContext context)
        /*
         Client, sembol (örn. BTC) gönderir.
        Server, bu sembol için fiyatlarý responseStream.WriteAsync() ile sürekli gönderir.
         */
        {
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                if (context.CancellationToken.IsCancellationRequested)
                    break;

                var reply = new CryptoReply
                {
                    Symbol = request.Symbol.ToUpper(),
                    Price = Math.Round(random.NextDouble() * 50000 + 1000, 2),
                    ChangePercent = Math.Round(random.NextDouble() * 10 - 5, 2), // -5% to +5%
                    Volume = Math.Round(random.NextDouble() * 1000, 2),
                    Timestamp = DateTime.Now.ToString("HH:mm:ss")
                };

                await responseStream.WriteAsync(reply);
                await Task.Delay(2000);
            }
        }
    }
}
