using HtmlAgilityPack;
using System.Net;
class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Digite a url do album a ser baixado");
            //Ler a url do album
            var urldoalbum = Console.ReadLine();
            var web = new HtmlWeb();
            var doc = web.Load(urldoalbum);
            //Pegar o titulo do album
            var nome = doc.DocumentNode.SelectNodes("/html/body/div[6]/div[2]/h2/span[1]")[0].InnerText;
            //Pegar a pasta documentos do usuarios
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            
            string diretorio = $"{documentsPath}/BaixadorYuupo/{nome}";
            //Criar o diretorio na pasta documentos
            Directory.CreateDirectory(diretorio);
            var quantidade = 0;
            foreach (var node in doc.DocumentNode.SelectNodes("/html/body/main/div/div/div[1]/img"))
            {

                //pegar as urls das imagens
                var url = "https:" + node.Attributes["src"].Value;
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
            Console.WriteLine("Download feito");
            quantidade = 0;
        }
    }
}

