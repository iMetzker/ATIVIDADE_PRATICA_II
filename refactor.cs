using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaTreino
{
    // Classe para agrupar as propriedades do exercício
    class Exercicio
    {
        public string GrupoMuscular { get; set; }
        public double Carga { get; set; }
        public int Repeticoes { get; set; }

        public Exercicio(string grupo, double carga, int reps)
        {
            GrupoMuscular = grupo;
            Carga = carga;
            Repeticoes = reps;
        }
    }

    class Program
    {
        // Dictionary central: Chave é o Nome do exercício, Valor é o objeto Exercicio
        // StringComparer.OrdinalIgnoreCase faz com que "Supino" e "supino" sejam tratados como iguais
        static Dictionary<string, Exercicio> treinos = new Dictionary<string, Exercicio>(StringComparer.OrdinalIgnoreCase);

        static void Main()
        {
            int opcao = -1;
            while (opcao != 0)
            {
                Console.Clear();
                Console.WriteLine("--- SISTEMA DE TREINO ACADEMIA (DICTIONARY MODE) ---");
                Console.WriteLine("1 - Adicionar/Atualizar exercício");
                Console.WriteLine("2 - Listar exercícios");
                Console.WriteLine("3 - Busca por nome");
                Console.WriteLine("4 - Filtro por grupo muscular");
                Console.WriteLine("5 - Calcular carga total do treino");
                Console.WriteLine("6 - Exibir exercício mais pesado");
                Console.WriteLine("7 - Remover exercício");
                Console.WriteLine("0 - Sair");
                Console.Write("\nEscolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) opcao = -1;

                switch (opcao)
                {
                    case 1: AdicionarExercicio(); break;
                    case 2: ListarExercicios(); break;
                    case 3: BuscarNome(); break;
                    case 4: FiltrarGrupo(); break;
                    case 5: CalcularCargaTotal(); break;
                    case 6: ExibirMaisPesado(); break;
                    case 7: RemoverExercicio(); break;
                    case 0: Console.WriteLine("Sais..."); break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void AdicionarExercicio()
        {
            Console.Write("Nome do exercício: ");
            string nome = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nome)) return;

            Console.Write("Grupo muscular: ");
            string grupo = Console.ReadLine();

            double carga;
            Console.Write("Carga (kg): ");
            while (!double.TryParse(Console.ReadLine(), out carga) || carga < 0)
                Console.Write("Carga inválida! Tente de novo: ");

            int reps;
            Console.Write("Repetições: ");
            while (!int.TryParse(Console.ReadLine(), out reps) || reps < 1)
                Console.Write("Repetições inválidas! Tente de novo: ");

            // No Dictionary, se a chave existe, ele atualiza. Se não, ele adiciona.
            treinos[nome] = new Exercicio(grupo, carga, reps);
            Console.WriteLine("Exercício salvo com sucesso!");
        }

        static void ListarExercicios()
        {
            if (treinos.Count == 0)
            {
                Console.WriteLine("Nenhum exercício cadastrado.");
                return;
            }

            foreach (var item in treinos)
            {
                Console.WriteLine($"{item.Key.PadRight(15)} | {item.Value.GrupoMuscular.PadRight(12)} | {item.Value.Carga}kg | {item.Value.Repeticoes} reps");
            }
        }

        static void BuscarNome()
        {
            Console.Write("Digite o nome do exercício: ");
            string busca = Console.ReadLine();

            // TryGetValue é a forma mais eficiente de buscar em um Dictionary
            if (treinos.TryGetValue(busca, out Exercicio ex))
                Console.WriteLine($"Encontrado: {busca} | Grupo: {ex.GrupoMuscular} | Carga: {ex.Carga}kg");
            else
                Console.WriteLine("Exercício não encontrado.");
        }

        static void FiltrarGrupo()
        {
            Console.Write("Grupo muscular para filtro: ");
            string grupoBusca = Console.ReadLine();

            var filtrados = treinos.Where(x => x.Value.GrupoMuscular.Equals(grupoBusca, StringComparison.OrdinalIgnoreCase)).ToList();

            if (filtrados.Any())
                foreach (var item in filtrados) Console.WriteLine($"- {item.Key}");
            else
                Console.WriteLine("Nenhum exercício encontrado para este grupo.");
        }

        static void CalcularCargaTotal()
        {
            // Somamos apenas a propriedade Carga de todos os Values do dicionário
            double total = treinos.Values.Sum(ex => ex.Carga);
            Console.WriteLine($"A carga total do treino é: {total} kg");
        }

        static void ExibirMaisPesado()
        {
            if (treinos.Count == 0) return;

            // Ordenamos pelos valores e pegamos o primeiro
            var maisPesado = treinos.OrderByDescending(x => x.Value.Carga).First();
            Console.WriteLine($"Mais pesado: {maisPesado.Key} com {maisPesado.Value.Carga}kg");
        }

        static void RemoverExercicio()
        {
            Console.Write("Nome do exercício para remover: ");
            string nome = Console.ReadLine();

            if (treinos.Remove(nome))
                Console.WriteLine("Exercício removido com sucesso!");
            else
                Console.WriteLine("Exercício não encontrado.");
        }
    }
}
