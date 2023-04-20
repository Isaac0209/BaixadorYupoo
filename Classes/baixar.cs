using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BaixadorYuupo.Classes
{
    internal class baixar
    {
        private string urlImagem;
        private int quantidade;
        private string diretorio;
        public baixar(string url, int quantidade, string diretorio)
        {
            this.urlImagem = url;
            this.quantidade = quantidade;
            this.diretorio = diretorio;
        }
        public async void baixarF()
        {
            var url = "https:" + urlImagem;
            quantidade++;
            Console.WriteLine("Baixando a " + quantidade + " imagem");

            // Define o cabeçalho da requisição
            var headers = new Dictionary<string, string>()
        {
            { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.54 Safari/537.36" },
            { "referer", "https://yupoo.com/" }
        };

            // Define o timeout para a conexão e leitura
            TimeSpan timeout = TimeSpan.FromSeconds(30);

            // Configura o handler da requisição com o timeout e cabeçalhos definidos acima
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                AllowAutoRedirect = true,
                UseCookies = true,
                UseProxy = true,
                CookieContainer = new CookieContainer(),
                ClientCertificateOptions = ClientCertificateOption.Manual,
                SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls11 | System.Security.Authentication.SslProtocols.Tls,
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using (var client = new HttpClient(handler))
            {
                client.Timeout = timeout;

                // Adiciona os cabeçalhos à requisição
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                }

                // Faz a requisição GET à URL especificada
                HttpResponseMessage response = await client.GetAsync(url.ToString());

                // Verifica se a resposta foi bem-sucedida
                response.EnsureSuccessStatusCode();

                // Lê o conteúdo da resposta como um stream
                Stream contentStream = await response.Content.ReadAsStreamAsync();

                // Salva o conteúdo do stream em um arquivo
                string caminhoDoArquivo = $"{diretorio}/image{quantidade}.jpg";
                using (var fileStream = new FileStream(caminhoDoArquivo, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
                {
                    await contentStream.CopyToAsync(fileStream);
                }


            }
        }
    }

    }
