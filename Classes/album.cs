using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaixadorYuupo.Classes
{
    public class album
    {
        private string url;
        private string nome;
        public album(string url)
        {
            this.url = url;
            Calbum(false);
             
            
        }
        public album(string url, string nome)
        {
            this.url = url;
            this.nome = nome;
            Calbum(true);

        }
        private void Calbum(bool category)
        {

            //Ler a url do album
            var web = new HtmlWeb();
            var doc = web.Load(url);

            //Pegar o titulo do album
            var nome = doc.DocumentNode.SelectNodes("//span[@class='showalbumheader__gallerytitle']")[0].InnerText;
            //Pegar a pasta documentos do usuarios
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string diretorio = "";
            if (category == true) {
                 diretorio = $"{documentsPath}/BaixadorYuupo/{this.nome}/{nome}";
            }
            else
            {
                diretorio = $"{documentsPath}/BaixadorYuupo/{nome}";

            }
            //Criar o diretorio na pasta documentos
            Directory.CreateDirectory(diretorio);
            var quantidade = 0;
            foreach (var node in doc.DocumentNode.SelectNodes("/html/body/main/div/div/div[1]/img"))
            {
                quantidade++;
                new baixar(node.Attributes["src"].Value, quantidade, diretorio).baixarF();

            }
            Console.WriteLine("Download feito");
            quantidade = 0;
        }
    }
}
