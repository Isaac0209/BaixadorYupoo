using BaixadorYuupo.Classes;
using HtmlAgilityPack;
using System.Net;
class Program
{
    static async Task Main(string[] args)
    {

        while (true) {
            Console.WriteLine("Digite a opção desejada: \n 1° Baixar um album \n 2°Baixar uma categoria");
            var opcao = Console.ReadLine();
            bool eNumero = int.TryParse(opcao, out int n);
            if (eNumero == true) {
               switch(n)
                {
                    case 1:
                        Console.WriteLine("Digite a url do album:");
                        
                        new album(Console.ReadLine());
                        break;
                    case 2:
                        new categoria();
                        break;
                    default:
                        Console.WriteLine("Escolha uma opção válida!");
                        break;
                }
            }else
            {
                Console.WriteLine("Escolha uma opção válida!");
            }
       }
    }
}

