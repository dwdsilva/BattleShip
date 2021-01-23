using System;
namespace Battleship.Cli
{
    /// <summary>
    /// Emumeravel com as opções do menu inicial do jogo
    /// </summary>
    public enum StartMenuOptions
    {
        NewGame,
        LoadGame,
        Instructions,
        Exit
    }
    /// <summary>
    /// Emumeravel com as opções criar novo jogo
    /// </summary>
    public enum NewGameOptions
    {
        CreatePlayer1,
        CreatePlayer2,
        StartGame,
        Exit
    }

    /// <summary>
    /// Emumeravel com as opções do menu Carregar jogo
    /// </summary>
    public enum LoadGameOptions
    {
        NextPage,
        PreviousPage,
        Exit,
        File
    }
    /// <summary>
    /// Emumeravel com as opções do menu Criar compo do jogo onde se inserem os navios
    /// </summary>
    public enum CreateBoardOptions
    {
        AddShip,
        RemoveShip,
        Exit
    }

    /// <summary>
    /// Emumeravel com as opções do menu de Exit do jogo
    /// </summary>
    public enum ExitGameOptions
    {
        SaveGame,
        Return,
        Exit
    }

    public class Menus
    {

        /// <summary>
        /// Pede a opeção do menu Incial e retorna a opeção em Emun
        /// </summary>
        /// <returns>Retorna o Emun corespondente a opeção selecionada, pode return NULL </returns>
        public static StartMenuOptions? getStartMenuOption()
        {
            string option = Console.ReadLine();
            switch (option)
            {
                case "a":
                case "A":
                    return StartMenuOptions.NewGame;
                case "b":
                case "B":
                    return StartMenuOptions.LoadGame;
                case "c":
                case "C":
                    return StartMenuOptions.Instructions;
                case "q":
                case "Q":
                    return StartMenuOptions.Exit;
                default:
                    return null;
            }
        }


        /// <summary>
        /// Pede a opeção do menu Novo Jogo e retorna a opeção em Emun
        /// </summary>
        /// <returns>Retorna o Emun corespondente a opeção selecionada, pode return NULL </returns>
        public static NewGameOptions? getNewGameOption()
        {
            string option = Console.ReadLine();
            switch (option)
            {
                case "a":
                case "A":
                    return NewGameOptions.CreatePlayer1;
                case "b":
                case "B":
                    return NewGameOptions.CreatePlayer2;
                case "c":
                case "C":
                    return NewGameOptions.StartGame;
                case "q":
                case "Q":
                    return NewGameOptions.Exit;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Pede a opeção do menu  Carregar jogo e retorna a opeção em Emun
        /// </summary>
        /// <returns>Retorna o Emun corespondente a opeção selecionada, pode return NULL </returns>
        public static (LoadGameOptions? option, int file) getLoadGameOption()
        {
            string option = Console.ReadLine();
            var result = Tools.StringToInt(option);
            switch (option)
            {
                case "a":
                case "A":
                    return (LoadGameOptions.PreviousPage, 0);
                case "d":
                case "D":
                    return (LoadGameOptions.NextPage, 0);
                case "q":
                case "Q":
                    return (LoadGameOptions.Exit, 0);
                default:
                    if (result.valid)
                    {
                        return (LoadGameOptions.File, result.number);
                    }
                    return (null, 0);
            }
        }

        /// <summary>
        /// Pede a opeção do menu Criar campo e retorna a opeção em Emun
        /// </summary>
        /// <returns>Retorna o Emun corespondente a opeção selecionada, pode return NULL </returns>
        public static CreateBoardOptions? getCreateBoardOption()
        {
            string option = Console.ReadLine();
            switch (option)
            {
                case "a":
                case "A":
                    return CreateBoardOptions.AddShip;
                case "b":
                case "B":
                    return CreateBoardOptions.RemoveShip;
                case "q":
                case "Q":
                    return CreateBoardOptions.Exit;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Pede a opeção do menu sair do jogo e retorna a opeção em Emun
        /// </summary>
        /// <returns>Retorna o Emun corespondente a opeção selecionada, pode return NULL </returns>
        public static ExitGameOptions? getExitGameOption()
        {
            string option = Console.ReadLine();
            switch (option)
            {
                case "s":
                case "S":
                    return ExitGameOptions.SaveGame;
                case "c":
                case "C":
                    return ExitGameOptions.Return;
                case "q":
                case "Q":
                    return ExitGameOptions.Exit;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Logica do menu inical
        /// </summary>
        public static void StartGameMenu()
        {
            string msg = null;
            Boolean loop = true;
            while (loop)
            {
                GameCli.StartMenu(msg);
                StartMenuOptions? option = getStartMenuOption();
                if (option == null)
                    msg = Settings.MsgApp["Erro_InputInvalid"];
                else
                {
                    switch (option)
                    {
                        case StartMenuOptions.NewGame:
                            Menus.NewGameMenu();
                            break;
                        case StartMenuOptions.LoadGame:
                            String[] files = Tools.ProcessDirectory("Saves");
                            int file = Menus.LoadGameMenu(files, 0, 10);
                            if (file >= 0 && file < files.Length)
                            {
                                Game.LoadSavedFile(files[file]);
                            }
                            break;
                        case StartMenuOptions.Instructions:
                            GameCli.Intrucoes();
                            break;
                        case StartMenuOptions.Exit:
                            loop = false;
                            break;
                    }
                    msg = null;
                }
            }
        }

        /// <summary>
        /// Logica do menu Novo jogo
        /// </summary>
        public static void NewGameMenu()
        {
            Player P1 = null;
            Player P2 = null;
            Game G = null;

            string msg = null;
            Boolean loop = true;
            while (loop)
            {
                GameCli.MenuGame(P1, P2, msg);

                NewGameOptions? option = getNewGameOption();
                if (option == null)
                    msg = Settings.MsgApp["Erro_InputInvalid"];
                else
                {
                    switch (option)
                    {
                        case NewGameOptions.CreatePlayer1:
                            if (P1 == null)
                            {
                                Console.WriteLine("|     Insira o nome do Player 1!                                       |");
                                Console.Write("|     Nome:");
                                string Name = Console.ReadLine();
                                if (Name.Length > 1)
                                {
                                    P1 = new Player(Name);
                                    P1.CreatBoard();
                                    P1.Defend.MenuCreatShips(P1.Ships);
                                }
                                else
                                    msg = Settings.MsgApp["Erro_PlayerSizeName"];

                            }
                            else
                            {
                                P1.Defend.MenuCreatShips(P1.Ships);
                            }

                            break;
                        case NewGameOptions.CreatePlayer2:
                            if (P2 == null)
                            {
                                Console.WriteLine("|     Insira o nome do Player 2!                                       |");
                                Console.Write("|     Nome:");
                                string Name = Console.ReadLine();
                                if (Name.Length > 1)
                                {
                                    P2 = new Player(Name);
                                    P2.CreatBoard();
                                    P2.Defend.MenuCreatShips(P2.Ships);
                                }
                                else
                                    msg = Settings.MsgApp["Erro_PlayerSizeName"];
                            }
                            else
                                P2.Defend.MenuCreatShips(P1.Ships);
                            break;
                        case NewGameOptions.StartGame:
                            if (P1 != null && P2 != null)
                            {
                                G = new Game(P1, P2, 0, 1);
                                G.Gamming(P1, P2);
                            }
                            else
                                msg = Settings.MsgApp["Erro_StartGame"];
                            break;
                        case NewGameOptions.Exit:
                            loop = false;
                            break;

                    }
                }
            }
        }

        /// <summary>
        /// Pogica do menu de carregamento de jogos
        /// Permite selecionar um dos ficheiros de json a carregar
        /// </summary>
        /// <param name="files"> Lista de ficheiros de saves</param>
        /// <param name="pag_start"> Index inical da lista de saves </param>
        /// <param name="n_lines"> Numero de ficheiros apresentados por pagina</param>
        /// <returns></returns>
        public static int LoadGameMenu(string[] files, int pag_start, int n_lines)
        {
            string msg = null;
            Boolean loop = true;

            if (pag_start < 0)
            {
                pag_start = 0;
            }
            while (loop)
            {
                GameCli.LoadGameMenuCli(files, pag_start, n_lines);

                var result = getLoadGameOption();
                if (result.option == null)
                    msg = Settings.MsgApp["Erro_InputInvalid"];
                else
                {
                    switch (result.option)
                    {
                        case LoadGameOptions.PreviousPage:
                            if ((pag_start - n_lines) > 0)
                                pag_start = pag_start - n_lines;
                            else
                                pag_start = 0;
                            break;
                        case LoadGameOptions.NextPage:
                            if ((pag_start + n_lines) < files.Length)
                                pag_start = pag_start + n_lines;
                            break;
                        case LoadGameOptions.File:
                            return result.file - 1;
                        case LoadGameOptions.Exit:
                            loop = false;
                            break;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Apresenta um menu e pergunta ao jogador se quer sair do jogo, ou se quer guardar o jogo.
        /// </summary>
        /// <returns> true - o jogo continua, false- o jogo e encesserado  </returns>
        public static bool ExitGameMenu(Game game)
        {
            string msg = null;
            while (true)
            {
                GameCli.ExitSaveGame(msg);
                ExitGameOptions? option = getExitGameOption();

                if (option == null)
                    msg = Settings.MsgApp["Erro_InputInvalid"];
                else
                {
                    switch (option)
                    {
                        case ExitGameOptions.SaveGame:
                            game.SaveGameJson();
                            msg = Settings.MsgApp["Success_Save"];
                            break;
                        case ExitGameOptions.Return:
                            return true;
                        case ExitGameOptions.Exit:
                            return false;
                    }
                }
            }
        }
    }
}