using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;

namespace Battleship.Cli
{
    public class Settings
    {
        public static int BoardSize { get; set; } = 8;
        public static int MinSizeShip { get; set; } = 1;
        public static int MaxSizeShip { get; set; } = 4;
        public static List<int> ListShipsToInsert { get; set; } = new List<int>();
        public static string[] Gui { get; set; } = { "🟦", "🌀", "⛵", "💥" };
        public static char[] Alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        public static Dictionary<string, string> MsgApp
                 = new Dictionary<string, string>
                 {
                    {"Erro_InputInvalid","Opção não é Valida."},
                    {"Erro_PlayerSizeName", "Nome tem de ter pelo menos 2 caracteres"},
                    {"Erro_StartGame","É necessario dois jogadores!"},
                    {"Erro_InsertShip", "Navio não foi Inserido! \nAVISO:Não é possivel adicionar navios subrepostos ou com menos de 1 espaço entre eles!"},
                    {"Erro_OrientationInvalid","Opeção invalida. Orientação: V-Vertical ou H-Horizontal"},
                    {"Success_Save","Jogo guardado com sucesso!"},
                    {"Success_InsertShip","Navio Inserido!"},
                    {"Info_ShipHitFail","Não Acertou em nenhum Navio!"},
                    {"Info_ShipHitSuccess","Acertou em um navio"},
                    {"Info_ShipSink","O Navio acertado afundou!"},
                    {"Info_GameWin","Ganhou o jogo!"},
                    {"Info_AttackInvalid","Não pode atacar onde ja atacou! Ataque de novo!"},
                    {"Info_Welcome","Bem vindo!"},
                 };


        /// <summary>
        /// Cria a lista com os tamanhos do navio a ser inseridos
        /// </summary>
        public static void ListAddShips()
        {
            ListShipsToInsert.Add(4);
            ListShipsToInsert.Add(3);
            ListShipsToInsert.Add(3);
            ListShipsToInsert.Add(2);
            ListShipsToInsert.Add(2);
            ListShipsToInsert.Add(1);
            ListShipsToInsert.Add(1);
            ListShipsToInsert.Add(1);
        }

        // public static string[] gui = { "🌊", "🌀", "⛵", "💥" };
        // public static string[] gui = { "🟦", "🟥", "⬜", "🟩" };
        // public static string[] gui = { "🟦", "🔴", "⚪", "🟢" };
        // 🚩❌⭕🟥🟦🟩✅⬜📍🌀🌊🚢🛳️🛥️🚤⛴️⛵🔴🟠🟡🟢🔵🟣🟤⚫⚪🟥🟧🟨🟩🟦🟪🟫⬛⬜


        /// <summary>
        /// Guarda as configurações em Json
        /// </summary>
        public static void SaveSettingsJson()
        {
            List<object> cfg = new List<object>();
            cfg.Add(BoardSize);
            cfg.Add(MinSizeShip);
            cfg.Add(MaxSizeShip);
            cfg.Add(ListShipsToInsert);
            cfg.Add(Gui);
            cfg.Add(MsgApp);
            string json = JsonConvert.SerializeObject(cfg);
            File.WriteAllText(@"Config/Settings.json", json);
        }

        /// <summary>
        /// Carrega as configurações de ficheiro json
        /// </summary>
        /// <param name="file"> Caminho ou ficheiro json</param>
        public static void LoadSettingsJson(string file)
        {
            var json = File.ReadAllText(file);
            List<object> configs = JsonConvert.DeserializeObject<List<object>>(json);
            BoardSize = Convert.ToInt32(configs[0]);
            MinSizeShip = (int)(long)configs[1];
            MaxSizeShip = Convert.ToInt32(configs[2]);
            ListShipsToInsert = JsonConvert.DeserializeObject<List<int>>(configs[3].ToString());
            Gui = JsonConvert.DeserializeObject<List<string>>(configs[4].ToString()).ToArray();
            MsgApp = JsonConvert.DeserializeObject<Dictionary<string, string>>(configs[5].ToString());
        }
    }
}