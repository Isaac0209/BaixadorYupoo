using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaixadorYuupo.Classes
{
    public class categoria
    {
        public categoria()
        {

            Console.WriteLine("Digite a url da categoria a ser baixado");
            var urldacategoria = Console.ReadLine();
            Console.WriteLine("Quantidade de albuns dessa categoria a ser baixado");
            var quantidadeEscolhida = Console.ReadLine();
           
            bool eNumero = int.TryParse(quantidadeEscolhida, out var quantidade);
            if (eNumero == true) {
                var web = new HtmlWeb();
                var doc = web.Load(urldacategoria);
                quantidade++;
                var nome_da_categoria = doc.DocumentNode.SelectNodes("//a[@class='yupoo-crumbs-span']")[0].InnerText;
                Console.WriteLine(nome_da_categoria);
                for (int i = 1; i < quantidade; i++)
                {
                    
                    var link = doc.DocumentNode.SelectNodes($"/html/body/div[5]/div[2]/div[2]/div[2]/div[{i + 1}]/a")[0].Attributes["href"].Value;
                    new album($"https://paypalshop.x.yupoo.com{link}", nome_da_categoria);
                    
                }
            }else
            {
                Console.WriteLine("Opção invalida");
            }

        }
    }
}
