using System;
using System.Collections.Generic;
using System.Linq;

class Exercicio
{
    public string Grupo { get; set; }
    public double Carga { get; set; }
    public int Repeticoes { get; set; }
}

class Program
{
    static Dictionary<string, Exercicio> exercicios = new Dictionary<string, Exercicio>();

    static void Main()
    {
        int opcao;
        bool firstTime = true;
        do
        {
            if (!firstTime)
            {
                if (!Console.IsInputRedirected)
                {
                    Console.ReadKey();
                }
            }
            firstTime = false;
            ExibirMenu();
            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida!\n");
                opcao = -1; 
                continue;
            }

            switch (opcao)
            {
                case 1: AdicionarExercicio();
                break;
                case 2: ListarExercicios(); 
                break;
                case 3: BuscarPorNome(); 
                break;
                case 4: FiltrarPorGrupo(); 
                break;
                case 5: CalcularCargaTotal(); 
                break;
                case 6: ExibirMaisPesado(); 
                break;
                case 7: RemoverExercicio(); 
                break;
                case 0: Console.WriteLine("Saindo..."); 
                break;
                default: Console.WriteLine("Opção inválida.\n"); 
                break;
            }

        } while (opcao != 0);
    }

    static void ExibirMenu()
    {
        Console.WriteLine("### CADASTRO DE TREINOS ###");
        Console.WriteLine("1 - Adicionar exercício");
        Console.WriteLine("2 - Listar exercícios");
        Console.WriteLine("3 - Buscar por nome");
        Console.WriteLine("4 - Filtrar por grupo");
        Console.WriteLine("5 - Carga total");
        Console.WriteLine("6 - Exercício mais pesado");
        Console.WriteLine("7 - Remover exercício");
        Console.WriteLine("0 - Sair");
        Console.Write("Escolha uma opção: ");
    }

    static void AdicionarExercicio()
    {
        Console.Write("Nome do exercício: ");
        string nome = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Inválido.");
            return;
        }

        if (exercicios.ContainsKey(nome))
        {
            Console.WriteLine("Esse exercício já existe!\n");
            return;
        }

        Console.Write("Grupo muscular: ");
        string grupo = Console.ReadLine();

        Console.Write("Carga (kg): ");
        double carga;
        while (!double.TryParse(Console.ReadLine(), out carga) || carga < 0)
        {
            Console.Write("Carga inválida: ");
        }

        Console.Write("Repetições: ");
        int reps;
        while (!int.TryParse(Console.ReadLine(), out reps) || reps < 1)
        {
            Console.Write("Repetições inválidas: ");
        }

        exercicios[nome] = new Exercicio
        {
            Grupo = grupo,
            Carga = carga,
            Repeticoes = reps
        };

        Console.WriteLine("Exercício adicionado!\n");
    }

    static void ListarExercicios()
    {
        if (exercicios.Count == 0)
        {
            Console.WriteLine("Nenhum exercício cadastrado.\n");
            return;
        }

        foreach (var item in exercicios)
        {
            Console.WriteLine($"{item.Key} - {item.Value.Grupo} - {item.Value.Carga}kg - {item.Value.Repeticoes} reps");
        }
    }

    static void BuscarPorNome()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        if (exercicios.TryGetValue(nome, out Exercicio ex))
        {
            Console.WriteLine($"{nome} | Grupo: {ex.Grupo} | Carga: {ex.Carga}kg");
        }
        else
        {
            Console.WriteLine("Não encontrado.\n");
        }
    }

    static void FiltrarPorGrupo()
    {
        Console.Write("Grupo: ");
        string grupo = Console.ReadLine();

        var filtrados = exercicios
            .Where(e => e.Value.Grupo.Equals(grupo, StringComparison.OrdinalIgnoreCase));

        if (filtrados.Any())
        {
            foreach (var item in filtrados)
            {
                Console.WriteLine(item.Key);
            }
        }
        else
        {
            Console.WriteLine("Nenhum encontrado.\n");
        }
    }

    static void CalcularCargaTotal()
    {
        double total = exercicios.Sum(e => e.Value.Carga);
        Console.WriteLine($"Total: {total} kg");
    }

    static void ExibirMaisPesado()
    {
        if (exercicios.Count == 0) return;

        var maisPesado = exercicios.OrderByDescending(e => e.Value.Carga).First();

        Console.WriteLine($"Mais pesado: {maisPesado.Key} com {maisPesado.Value.Carga} kg");
    }

    static void RemoverExercicio()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        if (exercicios.Remove(nome))
        {
            Console.WriteLine("Removido com sucesso!\n");
        }
        else
        {
            Console.WriteLine("Não encontrado.");
        }
    }
}
