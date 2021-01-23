using System;
namespace Battleship.Cli
{
    public class GameCli
    {
        /// <summary>
        /// Menu Cli com opeções quando o jogador sai de um jogo
        /// </summary>
        public static void ExitSaveGame(string msg)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                               Sair jogo                              ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("║              Tem Certeza que deseja sair do jogo?                    ║");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("║            Guarde o jogo para não perder o progresso!                ║");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("╠═══════════════════════╦═══════════════════════╦══════════════════════╣");
            Console.WriteLine("║   S - Guardar Jogo    ║      C - Voltar       ║      Q - Sair        ║");
            Console.WriteLine("╚═══════════════════════╩═══════════════════════╩══════════════════════╝");
            if (msg != null)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(msg);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write("Opção: ");

        }

        /// <summary>
        /// Menu Cli que apresenta os ficheiros com paginação
        /// </summary>
        /// <param name="files"> Lista de saves em json</param>
        /// <param name="pag_start"> Index incial da lista de saves</param>
        /// <param name="n_lines"> Numero de ficheiros por pagina</param>
        public static void LoadGameMenuCli(string[] files, int pag_start, int n_lines)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                           Lista de Saves                             ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                                      ║");
            if (files.Length > 0)
            {
                for (int i = pag_start; i < (pag_start + n_lines <= files.Length ? pag_start + n_lines : files.Length); i++)
                {
                    int size = files[i].Length;
                    Console.WriteLine($"║          {i + 1} - {files[i].Substring(6, (size - 11))}                                            ");
                }
            }
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine($"║                                {(pag_start + n_lines) / n_lines}/{Math.Ceiling((double)files.Length / n_lines)}");
            Console.WriteLine("╠════════════════════════╦═══════════════════════╦═════════════════════╣");
            Console.WriteLine("║  " + (pag_start > 0 ? "A - Pagina Anterior   " : "                      ") + "║" + ((pag_start + n_lines) < files.Length ? "  D - Pagina Seguinte  " : "                       ") + "║      Q - Sair       ║");
            Console.WriteLine("╚════════════════════════╩═══════════════════════╩═════════════════════╝");
        }

        /// <summary>
        /// Apresenta uma mensagem de Winner com o nome do jogador vencedor
        /// </summary>
        /// <param name="Name">Nome do jogador vencedor</param>
        /// <returns></returns>
        public static string Won(string Name)
        {
            string won = "║                                                                      ║\n";
            won += "║     ▓        ▓   ▓▓▓▓▓   ▓▓    ▓  ▓▓    ▓  ▓▓▓▓▓▓  ▓▓▓▓▓      ▓▓     ║\n";
            won += "║     ▓   ▓▓   ▓     ▓     ▓ ▓   ▓  ▓ ▓   ▓  ▓       ▓    ▓▓    ▓▓     ║\n";
            won += "║     ▓  ▓  ▓  ▓     ▓     ▓  ▓  ▓  ▓  ▓  ▓  ▓▓▓▓    ▓▓▓▓▓      ▓▓     ║\n";
            won += "║     ▓ ▓    ▓ ▓     ▓     ▓   ▓ ▓  ▓   ▓ ▓  ▓       ▓    ▓            ║\n";
            won += "║     ▓▓      ▓▓   ▓▓▓▓▓   ▓    ▓▓  ▓    ▓▓  ▓▓▓▓▓▓  ▓     ▓    ▓▓     ║\n";
            won += "║                                " + Name;
            return won;
        }

        /// <summary>
        /// Apresenta uma pagina de espera para o proximo turno
        /// Inpede que os jogadores vejam o tabuleiro um do outro
        /// </summary>
        /// <param name="Name"> Nome do poximo jogador</param>
        public static void NextTurn(string Name)
        {
            Console.Clear();
            string next = "╔══════════════════════════════════════════════════════════════════════╗\n";
            next += "║                                                                      ║\n";
            next += "║                  ▓▓    ▓  ▓▓▓▓▓▓▓  ▓    ▓  ▓▓▓▓▓▓▓▓▓                 ║\n";
            next += "║                  ▓ ▓   ▓  ▓         ▓  ▓       ▓                     ║\n";
            next += "║                  ▓  ▓  ▓  ▓▓▓▓▓      ▓▓        ▓                     ║\n";
            next += "║                  ▓   ▓ ▓  ▓         ▓  ▓       ▓                     ║\n";
            next += "║                  ▓    ▓▓  ▓▓▓▓▓▓▓  ▓    ▓      ▓                     ║\n";
            next += "║                                                                      ║\n";
            next += "║         ▓▓▓▓▓▓   ▓           ▓▓     ▓      ▓  ▓▓▓▓▓▓▓  ▓▓▓▓▓▓        ║\n";
            next += "║         ▓     ▓  ▓          ▓  ▓     ▓    ▓   ▓        ▓     ▓       ║\n";
            next += "║         ▓▓▓▓▓▓   ▓         ▓    ▓      ▓▓▓    ▓▓▓▓     ▓▓▓▓▓▓        ║\n";
            next += "║         ▓        ▓        ▓▓▓▓▓▓▓▓      ▓     ▓        ▓    ▓        ║\n";
            next += "║         ▓        ▓▓▓▓▓▓  ▓        ▓    ▓      ▓▓▓▓▓▓▓  ▓     ▓       ║\n";
            next += "║                                                                      ║\n";
            next += "║                              " + Name + "\n";
            next += "╠══════════════════════════════════════════════════════════════════════╣\n";
            next += "║                      Clique em qualquer tecla                        ║\n";
            next += "╚══════════════════════════════════════════════════════════════════════╝\n";
            Console.WriteLine(next);
            Console.ReadLine();

        }

        /// <summary>
        /// Menu inical para cli
        /// </summary>
        public static void StartMenu(string msg)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                     _~     Batalha Naval!      _~                    ║");
            Console.WriteLine("║                 _~ )_)_~                   _~ )_)_~                  ║");
            Console.WriteLine("║        ~       )_))_))_)                  )_))_))_)            ~     ║");
            Console.WriteLine("║  ~     ~~~     _!__!__!_    ~     ~       _!__!__!_    ~~~           ║");
            Console.WriteLine("║    ~~~~~~      \\______t/       ~~~        \\______t/      ~~~~~~      ║");
            Console.WriteLine("║      ~~~~~~~~~~~~~~~~~~~~~~~~~~   ~~~~~~~~~~~~~~~~~~~~~~~~~~~~       ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("║                      A - Novo Jogo                                   ║");
            Console.WriteLine("║                      B - Continuar Jogo                              ║");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("║                      C - Intruções                                   ║");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║               Q - Sair                                               ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
            if (msg != null)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(msg);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write("Opção: ");
        }

        /// <summary>
        /// Menu de criação de novo jogo
        /// </summary>
        public static void MenuGame(Player player1, Player player2, string msg)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                              Criar jogo                              ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("║                                                                      ║");
            if (player1 == null)
                Console.WriteLine("║                    A - Criar  Player 1.                              ║");
            else
                Console.WriteLine($"║                    A - Editar Player 1 [{player1.Name}]                              ║");
            if (player2 == null)
                Console.WriteLine("║                    B - Criar  Player 2.                              ║");
            else
                Console.WriteLine($"║                    B - Editar Player 2 [{player2.Name}]                              ║");
            if (player1 != null && player2 != null)
            {
                Console.Write("║                    ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("C - Iniciar Jogo.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                                 ║\n");

            }
            else
            {
                Console.Write("║                    ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("C - Iniciar Jogo.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("                                 ║\n");
            }
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║               Q - Sair                                               ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
            if (msg != null)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(msg);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write("Opção: ");

        }

        /// <summary>
        /// Instruções do jogo
        /// </summary>
        public static void Intrucoes()
        {
            string op;
            do
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                              Intruções                               ║");
                Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
                Console.WriteLine("║            #  Colocar os seguintes navios na grelha de Defesa,       ║");
                Console.WriteLine("║               inserindo na grelha com um espaço de distancia.        ║");
                Console.WriteLine("║            #  Navios:                                                ║");
                Console.WriteLine("║                  NNNN  |   NNN  NNN   | NN  NN  NN  |  N  N  N  N    ║");
                Console.WriteLine("║                                                                      ║");
                Console.WriteLine("║            #  Insira a cordenada para atacar o ponente tendo em,     ║");
                Console.WriteLine("║               conta a letra e o numero Ex( D3 ).                     ║");
                Console.WriteLine($"║            #  O mar é representado por: {Settings.Gui[0]}                           ║");
                Console.WriteLine($"║            #  Os navios  é representado por: {Settings.Gui[2]}                      ║");
                Console.WriteLine($"║            #  Os disparos falados  é representado por: {Settings.Gui[1]}            ║");
                Console.WriteLine($"║            #  Os disparos com acerto  é representado por: {Settings.Gui[3]}         ║");
                Console.WriteLine("║                                                                      ║");
                Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
                Console.WriteLine("║               Q - Sair                                               ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                op = Console.ReadLine();
            } while (op != "Q" && op != "q");
        }

        /// <summary>
        /// Apresenta o tabuleiro de defesa do jogador
        /// Menu de insersão de navios
        /// </summary>
        /// <param name="board"> Tabuleiro de Defesa do jogador </param>
        public static void CreatBoard(Board board, int ListShipsSize, string msg)
        {
            string line = null;
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   Criar tabuleiro de jogo                            ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                                      ║");
            Console.Write("║                             ");
            for (var i = 0; i < board.Size; i++)
            {
                Console.Write(i + 1 + " ");
            }
            Console.Write("\n");
            for (var i = 0; i < Settings.BoardSize; i++)
            {
                line = null;
                line += "║                          " + Settings.Alpha[i] + " ";
                for (var d = 0; d < Settings.BoardSize; d++)
                {

                    line += Settings.Gui[Cord.getCordState(board.Grid[d + (Settings.BoardSize * i)].State)];
                }
                Console.WriteLine(line + "                          ║");
            }
            Console.WriteLine("║                                                                      ║");
            Console.WriteLine("╠════════════════════════╦═════════════════════════╦═══════════════════╣");
            Console.WriteLine("║   A - Inserir Navio.   ║   B - Remover Navio.    ║     Q - Sair      ║");
            Console.WriteLine("╠════════════════════════╩═════════════════════════╩═══════════════════╣");
            Console.WriteLine($"║      Navios a inserir: {ListShipsSize}");
            Console.WriteLine("║      Orientação:   V - ⇩    H - ⇨");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
            if (msg != null)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(msg);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write("Opção: ");
        }

        /// <summary>
        /// Mostra o jogo
        /// Apresenta o tabuleiro de ataque e de defesa
        /// </summary>
        /// <param name="Attack"> Tabuleiro de ataque do jogador</param>
        /// <param name="Defend"> Tabuleiro de defesa do jogador</param>
        /// <param name="name"> Nome do jogador</param>
        /// <param name="N_ships_pa"> Numero de navios do atual jogador </param>
        /// <param name="N_ships_pd"> Numero de navios do jogador rival</param>
        /// <param name="turn"> Numero de turnos do jogo</param>
        public static void GameCliGame(Board Attack, Board Defend, string name, int N_ships_pa, int N_ships_pd, int turn)
        {
            string line = null;
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  Turno: {turn}                   ║    É a sua vez:{name}                             ");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║      " + Settings.Gui[0] + " - Mar       " + Settings.Gui[1] + " - Falhou     " + Settings.Gui[2] + " - Navio    " + Settings.Gui[3] + " - Acertou       ║");
            Console.WriteLine("╠═════════════════════════════════╦════════════════════════════════════╣");
            Console.WriteLine("║          Grelha de Defesa       ║          Grelha de Ataque          ║");
            Console.WriteLine("╠═════════════════════════════════╬════════════════════════════════════╣");
            Console.WriteLine("║                                 ║                                    ║");
            Console.WriteLine("║            1 2 3 4 5 6 7 8      ║            1 2 3 4 5 6 7 8         ║");
            for (var i = 0; i < Settings.BoardSize; i++)
            {
                line = null;
                line += "║         " + Settings.Alpha[i] + " ";

                for (var d = 0; d < Settings.BoardSize; d++)
                {
                    line += Settings.Gui[Cord.getCordState(Defend.Grid[d + (Settings.BoardSize * i)].State)];

                }
                line += "      ║         " + Settings.Alpha[i] + " ";
                for (var a = 0; a < Settings.BoardSize; a++)
                {
                    line += Settings.Gui[Cord.getCordState(Attack.Grid[a + (Settings.BoardSize * i)].State)];
                }
                Console.WriteLine(line + "         ║");
            }
            Console.WriteLine("║                                 ║                                    ║");
            Console.WriteLine("╠═════════════════════════════════╬════════════════════════════════════╣");
            Console.WriteLine($"║  Meus navios: {N_ships_pa}                 ║  Navios inimigos: {N_ships_pd}                ║");
        }

        /// <summary>
        /// Apresenta as ações que o jogador pode tomar durante o jogo
        /// </summary>
        /// <param name="attakOn">se o jogador pode atacar ou não</param>
        /// <param name="shot"> estado do ataque 0-não atacou    1-falhou 2-acertou 3-afundou navio 4-venceu jogo </param>
        /// <param name="name"> Nome do vencedor</param>
        public static void GamingActions(bool attakOn, int shot, string name, string msg)
        {
            string info = null;
            if (attakOn)
            {
                Console.WriteLine("╠══════════════╦══════════════════╬════════════════════╦═══════════════╣");
                Console.WriteLine("║  A - Atacar  ║ B - Passar turno ║    S - Guardar     ║   Q - Sair    ║");
                Console.WriteLine("╚══════════════╩══════════════════╩════════════════════╩═══════════════╝");
            }
            else
            {
                switch (shot)
                {
                    case 1:
                        info = "║       " + Settings.MsgApp["Info_ShipHitFail"];
                        break;
                    case 2:
                        info = "║       " + Settings.MsgApp["Info_ShipHitSuccess"];
                        break;
                    case 3:
                        info = "║       " + Settings.MsgApp["Info_ShipSink"];
                        break;
                    case 4:
                        info = GameCli.Won(name);
                        break;
                    case 5:
                        info = "║       " + Settings.MsgApp["Info_AttackInvalid"];
                        break;
                    case 6:
                        info = "║       " + Settings.MsgApp["Info_Welcome"];
                        break;
                    case 0:
                        info = "║       " + Settings.MsgApp["Erro_InputInvalid"];
                        break;
                    default:
                        info = "║       Não é valido";
                        break;
                }
                Console.WriteLine("╠═════════════════════════════════╩════════════════════════════════════╣");
                Console.WriteLine(info);
                if (shot != 4)
                {
                    Console.WriteLine("╠════════════════════════╦═════════════════════════╦═══════════════════╣");
                    Console.WriteLine("║    B - Passar turno    ║    S - Guardar Jogo     ║     Q - Sair      ║");
                    Console.WriteLine("╚════════════════════════╩═════════════════════════╩═══════════════════╝");
                }
                else
                {
                    Console.WriteLine("╠══════════════════════════════════════════════════════════════════════╣");
                    Console.WriteLine("║                       Q - Sair do jogo                               ║");
                    Console.WriteLine("╚══════════════════════════════════════════════════════════════════════╝");
                }
            }
            Console.WriteLine(msg);
        }
    }
}