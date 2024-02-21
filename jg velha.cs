using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jogo_da_velha
{
    using System;


    namespace JogoDaVelha
    {
        class Program
        {
            static char[,] tabuleiro = new char[3, 3];
            static bool jogadorX = true; // true para jogador X e false para jogador O
            static bool jogoEmAndamento = true;

            static void Main(string[] args)
            {
                Console.WriteLine("Bem-vindo ao Jogo da Velha!\n");

                while (true)
                {
                    IniciarJogo();

                    Console.WriteLine("\nDeseja jogar novamente? (S/N)");
                    string resposta = Console.ReadLine();
                    if (resposta.ToLower() != "s")
                        break;
                }

                Console.WriteLine("Obrigado por jogar! Até mais.");
            }

            static void IniciarJogo()
            {
                InicializarTabuleiro();
                jogoEmAndamento = true;

                while (jogoEmAndamento)
                {
                    Console.Clear();
                    ExibirTabuleiro();

                    int linha, coluna;
                    do
                    {
                        Console.WriteLine($"\nJogador {(jogadorX ? 'X' : 'O')}, digite a linha (0, 1, 2): ");
                        linha = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Jogador {(jogadorX ? 'X' : 'O')}, digite a coluna (0, 1, 2): ");
                        coluna = int.Parse(Console.ReadLine());
                    } while (!MovimentoValido(linha, coluna));

                    tabuleiro[linha, coluna] = jogadorX ? 'X' : 'O';

                    if (VerificarVitoria())
                    {
                        Console.Clear();
                        ExibirTabuleiro();
                        Console.WriteLine($"\nParabéns! Jogador {(jogadorX ? 'X' : 'O')} venceu!");
                        jogoEmAndamento = false;
                    }
                    else if (VerificarEmpate())
                    {
                        Console.Clear();
                        ExibirTabuleiro();
                        Console.WriteLine("\nO jogo terminou em empate!");
                        jogoEmAndamento = false;
                    }

                    jogadorX = !jogadorX; // Alternar jogadores
                }
            }

            static void InicializarTabuleiro()
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tabuleiro[i, j] = ' ';
                    }
                }
            }

            static void ExibirTabuleiro()
            {
                Console.WriteLine("  0    1    2");
                for (int i = 0; i < 3; i++)
                {
                    Console.Write(i + " ");
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write($" {tabuleiro[i, j]} ");
                        if (j < 2) Console.Write("|");
                    }
                    Console.WriteLine();
                    if (i < 2) Console.WriteLine("  -----------");
                }
            }

            static bool MovimentoValido(int linha, int coluna)
            {
                if (linha < 0 || linha > 2 || coluna < 0 || coluna > 2)
                {
                    Console.WriteLine("Movimento inválido. Linha e coluna devem estar entre 0 e 2.");
                    return false;
                }
                else if (tabuleiro[linha, coluna] != ' ')
                {
                    Console.WriteLine("Movimento inválido. Esta posição já está ocupada.");
                    return false;
                }
                return true;
            }

            static bool VerificarVitoria()
            {
                // Verificar linhas, colunas e diagonais
                for (int i = 0; i < 3; i++)
                {
                    if (tabuleiro[i, 0] != ' ' && tabuleiro[i, 0] == tabuleiro[i, 1] && tabuleiro[i, 1] == tabuleiro[i, 2])
                        return true;
                    if (tabuleiro[0, i] != ' ' && tabuleiro[0, i] == tabuleiro[1, i] && tabuleiro[1, i] == tabuleiro[2, i])
                        return true;
                }
                if (tabuleiro[0, 0] != ' ' && tabuleiro[0, 0] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[2, 2])
                    return true;
                if (tabuleiro[0, 2] != ' ' && tabuleiro[0, 2] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[2, 0])
                    return true;

                return false;
            }

            static bool VerificarEmpate()
            {
                // Se não há mais espaços vazios, é um empate
                foreach (char c in tabuleiro)
                {
                    if (c == ' ')
                        return false;
                }
                return true;
            }
        }
    }
}