using System;
using System.Collections.Generic;
using System.IO;
namespace Battleship.Cli
{
    public class Tools
    {
        /// <summary>
        /// Converte o input do jogador de string para as cordenadas do jogo
        /// Convert A1 -> 0,0
        /// </summary>
        /// <param name="input"> Cordenada em string</param>
        /// <returns> bool IsValid(true-cordenada valida, false- não é valida) , int[] intCord  - Cordenada </returns>
        public static (bool valid, int[] cord) ConvertToCord(string input)
        {
            int[] intCord = new int[2];
            bool isValid = false;
            if (input.Length == 2)
            {
                char[] inputArr = input.ToUpper().ToCharArray();
                int X = Array.IndexOf(Settings.Alpha, inputArr[0]);
                if (int.TryParse(inputArr[1].ToString(), out int Y) && X != -1)
                {
                    if (ValidIndex(X, Y - 1))
                    {
                        intCord[0] = X;
                        intCord[1] = Y - 1;
                        isValid = true;
                    }
                }
            }
            return (isValid, intCord);
        }

        /// <summary>
        /// Verefica se a cordenada dada esta entre o valor 0 e o valor do tamanho do tabuleiro
        /// </summary>
        /// <param name="cX"> Cordenada X</param>
        /// <param name="cY"> Cordenada Y</param>
        /// <returns> true - valido , false- não valido </returns>
        public static bool ValidIndex(int cX, int cY)
        {
            if (cX < Settings.BoardSize && cY < Settings.BoardSize && cX >= 0 && cY >= 0)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Verefica se o valor esta entre 1
        /// </summary>
        /// <param name="x"> valor </param>
        /// <param name="min"> minimo</param>
        /// <param name="max"> maximo</param>
        /// <returns>true- esta no intervalo , false- esta fora do intervalo</returns>
        public static bool InIndex(int x, int min, int max)
        {
            if (x >= min && x <= max)
                return true;
            return false;
        }

        /// <summary>
        /// Verefica cada cordenada a volta da posição dada a ver se existe algum navio nessa posição
        /// </summary>
        /// <param name="board"> Tabuleiro do jogador</param>
        /// <param name="x"> Cordenada X</param>
        /// <param name="y"> Cordenada Y</param>
        /// <returns> True-  Se for possivel adicionar navio naquela posição, False- não e possivel adicionar naquela posição </returns>
        public static bool CheckInsertShip(Board board, int x, int y)
        {
            List<bool> valid = new List<bool>();
            foreach (var cord in board.Grid)
            {
                if (cord.X == x && cord.Y == y)
                    valid.Add(cord.State != 0 ? false : true);
                if (cord.X == x && cord.Y == y + 1)
                    valid.Add(cord.State != 0 ? false : true);
                if (cord.X == x && cord.Y == y - 1)
                    valid.Add(cord.State != 0 ? false : true);
                if (cord.X == x + 1 && cord.Y == y)
                    valid.Add(cord.State != 0 ? false : true);
                if (cord.X == x + 1 && cord.Y == y + 1)
                    valid.Add(cord.State != 0 ? false : true);
                if (cord.X == x + 1 && cord.Y == y - 1)
                    valid.Add(cord.State != 0 ? false : true);
                if (cord.X == x - 1 && cord.Y == y)
                    valid.Add(cord.State != 0 ? false : true);
                if (cord.X == x - 1 && cord.Y == y + 1)
                    valid.Add(cord.State != 0 ? false : true);
                if (cord.X == x - 1 && cord.Y == y - 1)
                    valid.Add(cord.State != 0 ? false : true);

            }
            return CheckValid(valid.ToArray());
        }


        /// <summary>
        /// Verefica um array boleano e retorna falso se achar algum valor falso
        /// </summary>
        /// <param name="arr"> array de boleanos</param>
        /// <returns>True- se dos  os valores do array sejam true, false- se algum valor do array é falso</returns>
        public static bool CheckValid(bool[] arr)
        {
            bool check = true;
            for (var i = 0; i < arr.Length; i++)
            {
                if (arr[i] == false)
                    check = false;
            }
            return check;

        }

        /// <summary>
        /// Verefica se e possivel inserir um navio
        /// Testa cada cordenada de um navio com tamanho variavel, dependendo da orientação do navio
        /// Testa cada posição cria um array boleano e no fim verifica se existe alguma cordenada que não seja possivel inserir ou nao
        /// </summary>
        /// <param name="board">Tabuleiro</param>
        /// <param name="x">Cordenada X de origem </param>
        /// <param name="y"> Cordenada Y de origem</param>
        /// <param name="state"> Tamanho do navio</param>
        /// <param name="orientation"> Orientação do navio</param>
        /// <returns> true - possivel inserir , false - não possivel inserir </returns>
        public static bool TryInsert(Board board, int x, int y, int state, Orientation orientation)
        {
            bool[] valid = new bool[state];
            if (x + (orientation == Orientation.Vertical ? state : 0) <= board.Size && y + (orientation == Orientation.Horizontal ? state : 0) <= board.Size)
            {
                for (var i = 0; i < state; i++)
                {
                    valid[i] = CheckInsertShip(board, x + (orientation == Orientation.Vertical ? i : 0), y + (orientation == Orientation.Horizontal ? i : 0));
                }
            }
            return CheckValid(valid);
        }

        /// <summary>
        /// Verefica se o input da orientação do navio é valida
        /// </summary>
        /// <returns> retorna a o char H - horizontal , V - vertical</returns>
        public static char ValidDirection()
        {
            string console;
            char direction = ' ';
            bool notValid = true;
            do
            {
                Console.WriteLine("Insira a orientação do Navio [ H - Horizontal, V - Vertical ] :");
                console = Console.ReadLine();
                if (console.Length == 1)
                {
                    console = console.ToUpper();
                    if (Char.TryParse(console, out direction))
                    {
                        if (direction == 'H' || direction == 'V')
                        {
                            notValid = false;
                            return direction;
                        }
                    }
                }
                else Console.WriteLine("Caretares a mais!");
            } while (notValid);
            return direction;
        }

        /// <summary>
        /// Verefica se o tamanho do navio é valido
        /// </summary>
        /// <param name="min">Tamanho minimo</param>
        /// <param name="max">Tamanho maximo</param>
        /// <returns>retorna o tamanho do navio ou -1 não valido</returns>
        public static int ValidSize(int min, int max)
        {
            int size = -1;
            bool notValid = true;
            do
            {
                Console.WriteLine($"Insira o tamanho do Navio [ min:{min} , max:{max} ]");
                if (int.TryParse(Console.ReadLine(), out size))
                {
                    if (size <= max && size >= min)
                    {
                        notValid = false;
                        return size;
                    }
                }
            } while (notValid);
            return size;
        }

        /// <summary>
        /// Retorna uma lista de ficheiros presentes num diretorio
        /// </summary>
        /// <param name="targetDirectory">Diretorio</param>
        /// <returns> array com o nome dos ficheiros</returns>
        public static string[] ProcessDirectory(string targetDirectory)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            return fileEntries;
        }

        /// <summary>
        /// Tenta converter uma string para int
        /// </summary>
        /// <param name="input">input do jogador</param>
        /// <returns>devovle boleano com o sucesso da conversão e o valor convertido </returns>
        public static (bool valid, int number) StringToInt(string input)
        {
            return (int.TryParse(input.ToString(), out int Y), Y);
        }
    }


}
