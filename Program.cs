using System.Collections;
using System.Xml;
using System.Data;

// Trabalho feito por Rodrigo Duarte, Maurilio Frade e Marcos Filipe Dutra

namespace CadastroDeVeiculos
{
    class Program
    {
        static void Main(string[] args)
        {
            int opc, cont = 1;
            Hashtable hashVeiculos = new Hashtable();

            Console.WriteLine("LISTAGEM DE VEÍCULOS");
            Console.WriteLine("----------------------");
            do
            {
            escolheOpcao:
                Console.WriteLine("Digite a opção desejada:\n");
                Console.WriteLine("1 - Inserir novo veículo");
                Console.WriteLine("2 - Listar veículos");
                Console.WriteLine("3 - Pesquisar veículo");
                Console.WriteLine("4 - Excluir veículo");
                Console.WriteLine("5 - Converter em XML\n");
                Console.WriteLine("6 - Encerrar programa");
                try
                {
                    opc = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Opção inválida! Digite novamente");
                    Console.ReadKey();
                    Console.Clear();
                    goto escolheOpcao;
                }

                switch (opc)
                {
                    case 1:
                        Inserir(hashVeiculos, cont);
                        break;
                    case 2:
                        Listar(hashVeiculos);
                        break;
                    case 3:
                        Pesquisar(hashVeiculos);
                        break;
                    case 4:
                        Excluir(hashVeiculos);
                        break;
                    case 5:
                        ConverterEmXML(hashVeiculos);
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("\n\nO programa será fechado! Aperte qualquer tecla.");
                        Console.ReadKey();
                        break;
                }

            } while (opc != 6);
        }


        static void Inserir(Hashtable hashVeiculos, int cont)
        {
            Veiculo myVeiculo = new Veiculo();
            Console.Clear();
            Console.WriteLine("Você escolheu inserir novo veículo.\n");
            Console.WriteLine("Digite as informações do novo automóvel.");
            Console.Write("Modelo: ");
            myVeiculo.modelo = Console.ReadLine();
            Console.Write("Marca: ");
            myVeiculo.marca = Console.ReadLine();
            Console.Write("Ano de fabricação: ");
            myVeiculo.anoFab = Console.ReadLine();
            Console.Write("Placa alfanumérica: ");
            myVeiculo.placa = Console.ReadLine();

            try
            {
                hashVeiculos.Add(myVeiculo.placa, myVeiculo);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nVeículo incluido no hashtable com sucesso!");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nErro na inserção ou chave inválida, tente novamente.");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadKey();
            Console.Clear();

        }


        static void Listar(Hashtable hashVeiculos)
        {
            Console.Clear();
            int cont = 1;
            Console.WriteLine("Listagem de carros.\n\n");
            foreach (Veiculo v in hashVeiculos.Values)
            {

                Console.WriteLine($"Veículo {cont}");
                Console.WriteLine($"Modelo: {v.modelo}");
                Console.WriteLine($"Marca: {v.marca}");
                Console.WriteLine($"Ano de fabricação: {v.anoFab}");
                Console.WriteLine($"Placa: {v.placa}\n\n");

                cont++;
            }
            Console.WriteLine("\n\n-- Fim --");
            Console.ReadKey();
            Console.Clear();
        }


        static void Pesquisar(Hashtable hashVeiculos)
        {
            Console.Clear();
            string opc;
            Console.WriteLine("Pesquisa de veículos.");
            Console.WriteLine("A pesquisa é feita com base na placa do veiculo, digite-a a abaixo:");
            string p = Console.ReadLine();

            if (hashVeiculos.ContainsKey(p))
            {
                Console.WriteLine("\n\nVeículo encontrado!\n");

                foreach (Veiculo x in hashVeiculos.Values)
                {
                    if (x.placa == p)
                    {
                        Console.WriteLine($"Modelo: {x.modelo}");
                        Console.WriteLine($"Marca: {x.marca}");
                        Console.WriteLine($"Ano de fabricação: {x.anoFab}");
                        Console.WriteLine($"Placa: {x.placa}\n\n");
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Veículo não encontrado seguindo a placa informada!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("Aperte qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();
        }
        static void Excluir(Hashtable hashVeiculos)
        {
            Console.Clear();
            Console.WriteLine("Você escolheu deletar um veículo.\n");
            Console.WriteLine("Digite a placa do veículo indesejável.");
            Console.Write("Placa alfanumérica: ");
            string p = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Red;

            if (hashVeiculos.ContainsKey(p))
            {
                try
                {
                    hashVeiculos.Remove(p);
                    Console.WriteLine("\nVeículo excluido!");
                }
                catch
                {
                    Console.WriteLine("\nErro na inserção ou chave inválida, tente novamente.");
                }
            }
            else
            {
                Console.WriteLine("\nErro na inserção ou chave inválida, tente novamente.");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Aperte qualquer tecla para continuar");
            Console.ReadKey();
            Console.Clear();
        }

        static void ConverterEmXML(Hashtable hashVeiculos)
        {
            Console.Clear();

            try
            {
                XmlDocument xmlDocument = new XmlDocument();

                XmlElement raiz = xmlDocument.CreateElement("ListaDeVeiculos");
                xmlDocument.AppendChild(raiz);

                foreach (DictionaryEntry x in hashVeiculos)
                {
                    XmlElement veiculo = xmlDocument.CreateElement("Veiculo");

                    XmlElement marca = xmlDocument.CreateElement("Marca");
                    XmlElement modelo = xmlDocument.CreateElement("Modelo");
                    XmlElement ano = xmlDocument.CreateElement("AnoFabricação");
                    XmlElement placa = xmlDocument.CreateElement("Placa");


                    marca.InnerText = x.Key.ToString();
                    veiculo.AppendChild(marca);
                    raiz.AppendChild(veiculo);

                    modelo.InnerText = x.Key.ToString();
                    veiculo.AppendChild(modelo);
                    raiz.AppendChild(veiculo);

                    ano.InnerText = x.Key.ToString();
                    veiculo.AppendChild(ano);
                    raiz.AppendChild(veiculo);

                    placa.InnerText = x.Key.ToString();
                    veiculo.AppendChild(placa);
                    raiz.AppendChild(veiculo);
                }
                xmlDocument.Save(@"CadastroVeiculos.xml");
                Console.WriteLine("Arquivo XML gerado com sucesso, ele estará na própria pasta 'bin' do programa!");
            }
            catch
            {
                Console.WriteLine("Erro no salvamento, reveja o caminho do arquivo");
            }

            Console.WriteLine("Aperte uma tecla para prosseguir");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
class Veiculo
{
    public string modelo;
    public string marca;
    public string anoFab;
    public string placa;

    public Veiculo()
    {
        modelo = "";
        marca = "";
        anoFab = "";
        placa = "";
    }
}