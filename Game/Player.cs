using System.Collections.Generic;
namespace Battleship.Cli
{
    public class Player
    {
        public string Name { get; set; }
        public Board Attack { get; set; }
        public Board Defend { get; set; }
        public List<Ship> Ships { get; set; }

        /// <summary>
        /// Construtor da class Player
        /// </summary>
        /// <param name="name">Nome do jogador</param>
        public Player(string name)
        {
            Name = name;
            Attack = new Board(Settings.BoardSize);
            Defend = new Board(Settings.BoardSize);
            Ships = new List<Ship>();
        }

        /// <summary>
        /// CreatBoard - Reseta a Board Attack e Defesa do Jogador
        /// </summary>
        public void CreatBoard()
        {
            Attack.ResetBoard();
            Defend.ResetBoard();
        }

        /// <summary>
        /// NewPlayer - Cria um novo Jogador 
        /// </summary>
        /// <param name="Name"> Nome do Jogador</param>
        /// <returns>Retorna a istancia do jogador</returns>
        // public static Player NewPlayer(string Name)
        // {
        //     Player player = new Player(Name);
        //     return player;
        // }

        /// <summary>
        /// No caso de um navio ser atingido no tabuleiro vai verificar em qual navio foi atingido.
        /// Verifica se o navio atingido foi afundado
        /// </summary>
        /// <param name="x"> Cordenada X</param>
        /// <param name="y"> Cordenada Y</param>
        /// <param name="Ships"> Lista dos Navios do Jogador</param>
        /// <returns> Retorna "True" no caso de um navio ser complatamente afundado ou "False" caso não seja </returns>
        public static bool FindShip(int x, int y, List<Ship> Ships)
        {
            bool sink = false;
            foreach (var ship in Ships)
            {
                foreach (var cord in ship.Cords)
                {
                    if (cord.X == x && cord.Y == y)
                    {
                        cord.State = CordState.HitOcean;
                        if (ship.IsSink())
                        {
                            sink = true;
                        }

                    }
                }
            }
            Ships.RemoveAll(item => item.Size == item.Shots);
            return sink;
        }
    }
}