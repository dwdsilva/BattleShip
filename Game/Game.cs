using System;
using Newtonsoft.Json;
using System.IO;
namespace Battleship.Cli
{

    /// <summary>
    /// Emuneravel com as opeções do menu
    /// </summary>
    public enum GamingOptions
    {
        Attack,
        NextTurn,
        Save,
        Exit
    }

    /// <summary>
    /// Emuneravel com as opeções do Estado do Jogo
    /// </summary>
    public enum GameState
    {
        P1_Turn_Attack = 0,
        P1_Only_View = 1,
        P2_Turn_Attack = 2,
        P2_Only_View = 3,
        Victory = 4,
        TryAgain = 5

    }
    public class Game
    {
        public Player P1 { get; set; }
        public Player P2 { get; set; }
        public GameState State { get; set; }
        public int Turn { get; set; }
        public Game(Player player1, Player player2, GameState state, int turn)
        {
            P1 = player1;
            P2 = player2;
            State = state;
            Turn = turn;
        }


        /// <summary>
        /// Esta função seleciona e retorna uma opeção de um Emun
        /// </summary>
        /// <returns>Retorna a opção do emun selecionada</returns>
        public static GamingOptions? getGamingOptions()
        {
            string option = Console.ReadLine();
            switch (option)
            {
                case "a":
                case "A":
                    return GamingOptions.Attack;
                case "b":
                case "B":
                    return GamingOptions.NextTurn;
                case "s":
                case "S":
                    return GamingOptions.Save;
                case "q":
                case "Q":
                    return GamingOptions.Exit;
                default:
                    return null;
            }
        }
        /// <summary>
        /// Apresenta o tabuleiro de ataque e defesa do jogador e algumas ações
        /// O jogador pode atacar ou passar e sair
        /// Esta função tambem troca o turno do jogador
        /// </summary>
        /// <param name="PA">Player que ataca</param>
        /// <param name="PD">Player que defende</param>
        public void Gamming(Player PA, Player PD)
        {
            bool loop = true;
            int shot = 6;
            string msg = null;
            while (loop)
            {
                bool attakOn = true;
                if (this.State == GameState.P1_Only_View || this.State == GameState.P2_Only_View)
                {
                    attakOn = false;
                }
                GameCli.GameCliGame(PA.Attack, PA.Defend, PA.Name, PA.Ships.Count, PD.Ships.Count, Turn);
                GameCli.GamingActions(attakOn, shot, PA.Name, msg);

                var option = getGamingOptions();
                if (option == null)
                    msg = Settings.MsgApp["Erro_InputInvalid"];
                else
                {
                    switch (option)
                    {
                        case GamingOptions.Attack:
                            if (attakOn)
                            {
                                Console.WriteLine(" Insira a cordenada [ex: A3]:");
                                var cords = Tools.ConvertToCord(Console.ReadLine());
                                if (cords.valid)
                                {
                                    shot = PD.Defend.Shot(cords.cord[0], cords.cord[1], PA.Attack, PD.Ships);
                                    var fw = FindWinner();
                                    if (fw.Item1)
                                    {
                                        shot = 4;
                                    };
                                }
                            }
                            if (shot != 5)
                            {
                                attakOn = false;
                                if (State == GameState.P1_Turn_Attack)
                                    State = GameState.P1_Only_View;
                                else if (State == GameState.P2_Turn_Attack)
                                    State = GameState.P2_Only_View;
                            }
                            break;
                        case GamingOptions.NextTurn:
                            if (shot != 4)
                            {
                                if (State == GameState.P1_Turn_Attack || State == GameState.P1_Only_View)
                                {
                                    State = GameState.P2_Turn_Attack;
                                }
                                else
                                {
                                    State = GameState.P1_Turn_Attack;
                                    Turn++;
                                }
                                GameCli.NextTurn(PD.Name);
                                var Aux = PD;
                                PD = PA;
                                PA = Aux;
                            }
                            break;
                        case GamingOptions.Save:
                            this.SaveGameJson();
                            msg = Settings.MsgApp["Success_Save"];
                            break;
                        case GamingOptions.Exit:
                            loop = Menus.ExitGameMenu(this);
                            break;
                    }
                }
            }
        }



        /// <summary>
        /// Verefica se algum dos jogadores ganhou
        /// Testa se alguma das listas de navios dos jogadores esta vazia
        /// </summary>
        /// <returns> bool - (true - existe vencedor, false - não existe vencedor) string - retorna o nome do vencedor</returns>
        public (bool, string) FindWinner()
        {
            bool winner = false;
            string name = null;
            if (P1.Ships.Count == 0)
            {
                winner = true;
                name = P2.Name;
            }
            if (P2.Ships.Count == 0)
            {
                winner = true;
                name = P1.Name;
            }
            return (winner, name);
        }



        /// <summary>
        /// Guarda o jogo em Json
        /// </summary>
        /// <param name="game"> Jogo a ser guardado</param>
        public void SaveGameJson()
        {
            DateTime datasave = DateTime.Now;
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(@"Saves/Save_" + this.P1.Name + "_vs_" + this.P2.Name + "_" + datasave.ToString("dd-MM-yyyy_HH-mm-ss") + ".json", json);
        }

        /// <summary>
        /// Carrega o jogo de ficheiro Json
        /// </summary>
        /// <param name="file">Caminho ou fecheiro de json</param>
        /// <returns> retorna o jogo</returns>
        public static Game LoadGameJson(string file)
        {
            var json = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<Game>(json);
        }

        /// <summary>
        /// Carrega um jogo guardado em json.
        /// </summary>
        /// <param name="file"> Caminho do ficheiro</param>
        public static void LoadSavedFile(string file)
        {
            if (file != null)
            {
                Game G = Game.LoadGameJson(file);
                if (G.State == GameState.P1_Turn_Attack || G.State == GameState.P1_Only_View)
                {
                    G.Gamming(G.P1, G.P2);
                }
                else if (G.State == GameState.P2_Turn_Attack || G.State == GameState.P2_Only_View)
                {
                    G.Gamming(G.P2, G.P1);
                }
            }
        }
    }

}