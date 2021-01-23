using System;
using System.Collections.Generic;
namespace Battleship.Cli
{
    public class Board
    {

        public int Size { get; set; }
        public List<Cord> Grid { get; set; }

        public Board(int s_board)
        {
            Size = s_board;
            Grid = new List<Cord>();
        }

        /// <summary>
        /// ResetBoard - Controi ou Reinicia uma Board
        /// Atribui 0 a todas as cordenadas da Board 
        /// </summary>
        /// <returns> Retorna a Board </returns>
        public Board ResetBoard()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Grid.Add(new Cord(y, x, 0));
                }
            }
            return this;
        }

        /// <summary>
        /// InsertValue - Insere os navios no tabuleiro
        /// Caso o navio seja bem inserido adiciona o navio a lista de navios do jogador
        /// </summary>
        /// <param name="x">Codenada X inical</param>
        /// <param name="y"> Cordenada Y inicial</param>
        /// <param name="size"> Tamanho do navio</param>
        /// <param name="state"> Estado do navio  </param>
        /// <param name="orientation"> Orientação do navio (Vertical, horizontal)</param>
        /// <param name="Ships"> Lista dos navios do jogador</param>
        /// <returns> Retorna o sucesso da insersção do navio </returns>
        public bool InsertValue(int x, int y, int size, CordState state, Orientation orientation, List<Ship> Ships)
        {
            bool valid = false;
            if (Tools.InIndex(x, 0, Size) && Tools.InIndex(y, 0, Size))
            {
                if (Tools.InIndex(x + (orientation == Orientation.Vertical ? size : 0), 0, Size) && Tools.InIndex(y + (orientation == Orientation.Horizontal ? size : 0), 0, Size))
                {
                    if (Tools.TryInsert(this, x, y, size, orientation))
                    {
                        Ship ship = new Ship(size, orientation);
                        for (var i = 0; i < size; i++)
                        {
                            foreach (Cord cord in Grid)
                            {
                                if (cord.X == x + (orientation == Orientation.Vertical ? i : 0) && cord.Y == y + (orientation == Orientation.Horizontal ? i : 0))
                                {
                                    cord.State = state;
                                    Cord C = new Cord(x + (orientation == Orientation.Vertical ? i : 0), y + (orientation == Orientation.Horizontal ? i : 0), 0);
                                    ship.Cords.Add(C);
                                    valid = true;
                                }
                            }
                        }
                        Ships.Add(ship);
                    }
                }
            }
            return valid;
        }


        /// <summary>
        /// Pede dados ao utilizador da posição inical do navio e da sua orientação
        /// O tamanho e inserido tendo en conta uma lista de tamanho dos navios ja defenida em Settings
        /// </summary>
        /// <param name="ships"> Lista de navios do jogador </param>
        /// <param name="size"> Tamanho do navio que vai ser inserido</param>
        public bool AddByListShips(List<Ship> ships, int size)
        {
            bool loop = true, valid = false;
            dynamic orientation = null;
            do
            {
                var cords = Tools.ConvertToCord(Console.ReadLine());
                if (cords.valid)
                {
                    if (size != 1)
                    {
                        orientation = Ship.getOrientation();
                        if (orientation == null)
                        {
                            Console.WriteLine(Settings.MsgApp["Erro_OrientationInvalid"]);
                            break;
                        }
                    }
                    else
                    {
                        orientation = Orientation.Horizontal;
                    }
                    valid = this.InsertValue(cords.cord[0], cords.cord[1], size, CordState.Ship, orientation, ships);
                    loop = false;
                }
                else
                    Console.WriteLine("Cordenada não é valida! [ex: A3 ou a3]");
            } while (loop);
            return valid;
        }



        /// <summary>
        /// Remove um navio durante a criação do tabuleiro
        /// </summary>
        /// <param name="ships"> Lista de navios do jogador</param>
        /// <returns>retorna um boleano com a sucesso e o valor do tamanho do navio para adiconar novamente a lista de insersão</returns>
        public (bool, int) RemoveByListShips(List<Ship> ships)
        {
            bool loop = true, valid = false;
            int size = -1;
            do
            {
                var cords = Tools.ConvertToCord(Console.ReadLine());

                if (cords.valid)
                {
                    foreach (var ship in ships)
                    {
                        if (ship.Cords[0].X == cords.cord[0] && ship.Cords[0].Y == cords.cord[1])
                        {
                            foreach (var cord_ship in ship.Cords)
                            {
                                foreach (Cord cord in Grid)
                                {
                                    if (cord.X == cord_ship.X && cord.Y == cord_ship.Y)
                                    {
                                        cord.State = 0;
                                        ship.Shots++;
                                        valid = true;
                                    }
                                }
                            }
                            size = ship.Size;

                        }
                    }
                    if (valid)
                    {
                        ships.RemoveAll(ship => ship.Size == ship.Shots);
                        loop = false;
                    }
                }

            } while (loop);

            return (valid, size);

        }


        /// <summary>
        /// Logica do Menu de de inserir navios
        /// apresenta o tabuleiro onde os navios são inseridos e pergunta ao utilizador qual ação quer tomar
        /// </summary>
        /// <param name="ships"> Lista de navios do jogador</param>
        public void MenuCreatShips(List<Ship> ships)
        {
            bool loop = true;
            bool isAdded = false;
            String msg = null;
            List<int> listSizeShips = new List<int>(Settings.ListShipsToInsert);
            do
            {
                GameCli.CreatBoard(this, listSizeShips.Count, msg);
                CreateBoardOptions? option = Menus.getCreateBoardOption();
                if (option == null)
                    msg = Settings.MsgApp["Erro_InputInvalid"];
                else
                {
                    switch (option)
                    {
                        case CreateBoardOptions.AddShip:
                            if (0 < listSizeShips.Count)
                            {
                                Console.WriteLine("Tamanho do navio a inserir:" + listSizeShips[0]);
                                Console.WriteLine("Insira a cordenada do Navio:");
                                isAdded = AddByListShips(ships, listSizeShips[0]);
                                if (isAdded)
                                {
                                    listSizeShips.RemoveAt(0);
                                    msg = Settings.MsgApp["Success_InsertShip"];
                                }
                                else
                                {
                                    msg = Settings.MsgApp["Erro_InsertShip"];
                                }
                            }
                            else
                                Console.WriteLine("Todos os Navios foram inseridos!");
                            break;
                        case CreateBoardOptions.RemoveShip:
                            if (ships.Count > 0)
                            {
                                Console.WriteLine("Insira a cordenada do Navio a Remover:");
                                var IsRemove = RemoveByListShips(ships);
                                if (IsRemove.Item1)
                                {
                                    listSizeShips.Insert(0, IsRemove.Item2);
                                    Console.WriteLine("Navio removido");
                                }
                                else
                                    Console.WriteLine("Navio não removido");
                            }
                            else
                                Console.WriteLine("Não existem navios para remover!");
                            break;
                        case CreateBoardOptions.Exit:
                            loop = false;
                            break;

                    }
                }
            } while (loop);
        }

        /// <summary>
        /// Verefica se nas cordenadas dadas pelo jogador é navio ou mar
        /// Troca o estado da cordenada
        /// Caso um navio seja atingido, chama a função responsavel por vereficar na lista do Navios do jogador
        /// </summary>
        /// <param name="x"> Cordenada X </param>
        /// <param name="y"> Cordenada Y</param>
        /// <param name="B_Attack"> Tabuleiro do jogador atacado</param>
        /// <param name="Ships"> Lista de navios do Jogador atacado</param>
        /// <returns> Retorna um estado do ataque 1 falhou, 2 acertou , 3 afundou navio</returns>
        public int Shot(int x, int y, Board B_Attack, List<Ship> Ships)
        {
            if (Tools.InIndex(x, 0, Size) && Tools.InIndex(y, 0, Size))
            {
                foreach (Cord cord in Grid)
                {
                    if (cord.X == x && cord.Y == y)
                    {
                        if (cord.State == CordState.Ocean)
                        {
                            cord.State = CordState.HitOcean;
                            B_Attack.MarkShot(x, y, CordState.HitOcean);
                            return 1;
                        }
                        else if (cord.State == CordState.Ship)
                        {
                            cord.State = CordState.HitShip;
                            B_Attack.MarkShot(x, y, CordState.HitShip);
                            if (Player.FindShip(x, y, Ships))
                            {
                                return 3;
                            }
                            return 2;
                        }
                        else
                            return 5;
                    }
                }
            }
            return 0;

        }

        /// <summary>
        /// Corre toda o tabuleiro ate encontrar a cordenada especificado, quando encontrar, altera o estado da cordenda
        /// </summary>
        /// <param name="x"> Cordenada X</param>
        /// <param name="y"> Cordenada Y</param>
        /// <param name="st"> Estado da cordenada</param>
        public void MarkShot(int x, int y, CordState st)
        {
            foreach (Cord cord in Grid)
            {
                if (cord.X == x && cord.Y == y)
                {
                    cord.State = st;
                }
            }
        }

    }
}

