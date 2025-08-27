using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    class Bebida
    {
        public string Nome { get; set; }
        public double Preco { get; set; }

        public Bebida(string nome, double preco)
        {
            Nome = nome;
            Preco = preco;
        }
    }

    class ItemPedido
    {
        public Bebida Bebida { get; set; }
        public int Quantidade { get; set; }
        public double Subtotal => Bebida.Preco * Quantidade;
    }

    static void Main(string[] args)
    {
        List<Bebida> cardapio = new List<Bebida>
        {
            new Bebida("Coca-Cola", 5.00),
            new Bebida("Suco de Laranja", 6.00),
            new Bebida("Água", 3.00),
            new Bebida("Café", 4.00)
        };

        List<ItemPedido> pedido = new List<ItemPedido>();
        int opcao;

        do
        {
            Console.Clear();
            ExibirPedidoAtual(pedido);
            Console.WriteLine("===== CARDÁPIO DE BEBIDAS =====");
            for (int i = 0; i < cardapio.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {cardapio[i].Nome} (R$ {cardapio[i].Preco:F2})");
            }
            Console.WriteLine($"{cardapio.Count + 1} - Remover item do pedido");
            Console.WriteLine($"{cardapio.Count + 2} - Finalizar pedido");
            Console.WriteLine("================================");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida! Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                continue;
            }

            if (opcao >= 1 && opcao <= cardapio.Count)
            {
                Bebida bebidaEscolhida = cardapio[opcao - 1];
                Console.Write($"Quantas unidades de {bebidaEscolhida.Nome} deseja adicionar? ");
                if (int.TryParse(Console.ReadLine(), out int quantidade) && quantidade > 0)
                {
                    var itemExistente = pedido.FirstOrDefault(p => p.Bebida.Nome == bebidaEscolhida.Nome);
                    if (itemExistente != null)
                    {
                        itemExistente.Quantidade += quantidade;
                    }
                    else
                    {
                        pedido.Add(new ItemPedido { Bebida = bebidaEscolhida, Quantidade = quantidade });
                    }
                    Console.WriteLine($"{quantidade}x {bebidaEscolhida.Nome} adicionados ao pedido.");
                }
                else
                {
                    Console.WriteLine("Quantidade inválida!");
                }
            }
            else if (opcao == cardapio.Count + 1)
            {
                RemoverItemPedido(pedido);
            }
            else if (opcao == cardapio.Count + 2)
            {
                Console.WriteLine("\nTem certeza que deseja finalizar o pedido? (S/N)");
                var confirm = Console.ReadLine();
                if (confirm?.Trim().ToUpper() == "S")
                    break;
            }
            else
            {
                Console.WriteLine("Opção inválida!");
            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();

        } while (true);

        ExibirResumoPedido(pedido);
    }

    static void ExibirPedidoAtual(List<ItemPedido> pedido)
    {
        if (pedido.Count == 0)
        {
            Console.WriteLine("Nenhum item adicionado ao pedido ainda.\n");
            return;
        }
        Console.WriteLine("===== PEDIDO ATUAL =====");
        foreach (var item in pedido)
        {
            Console.WriteLine($"{item.Quantidade}x {item.Bebida.Nome} - R$ {item.Subtotal:F2}");
        }
        Console.WriteLine("========================\n");
    }

    static void RemoverItemPedido(List<ItemPedido> pedido)
    {
        if (pedido.Count == 0)
        {
            Console.WriteLine("Não há itens para remover.");
            return;
        }
        Console.WriteLine("Itens do pedido:");
        for (int i = 0; i < pedido.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {pedido[i].Quantidade}x {pedido[i].Bebida.Nome}");
        }
        Console.Write("Informe o número do item que deseja remover: ");
        if (int.TryParse(Console.ReadLine(), out int indice) && indice >= 1 && indice <= pedido.Count)
        {
            pedido.RemoveAt(indice - 1);
            Console.WriteLine("Item removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Índice inválido!");
        }
    }

    static void ExibirResumoPedido(List<ItemPedido> pedido)
    {
        Console.Clear();
        Console.WriteLine("===== RESUMO DO PEDIDO =====");
        double total = 0;
        foreach (var item in pedido)
        {
            Console.WriteLine($"{item.Quantidade}x {item.Bebida.Nome} - R$ {item.Subtotal:F2}");
            total += item.Subtotal;
        }
        Console.WriteLine("============================");
        Console.WriteLine($"Valor total do pedido: R$ {total:F2}");
        Console.WriteLine("Obrigado pela preferência!");
    }
}
