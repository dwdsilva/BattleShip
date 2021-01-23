using System.Collections.Generic;
using System;
namespace Battleship.Cli
{
    /// <summary>
    /// Enumeravel com as opeções de Orientação dos navioss
    /// </summary>
    public enum Orientation
    {
        Vertical,
        Horizontal
    }
    public class Ship
    {
        public int Size { get; set; }
        public Orientation Orientation { get; set; }
        public int Shots { get; set; }
        public List<Cord> Cords { get; set; }

        public Ship(int size, Orientation orientation)
        {
            Size = size;
            Orientation = orientation;
            Shots = 0;
            Cords = new List<Cord>();
        }

        /// <summary>
        /// Verefica se o navio afundou
        /// </summary>
        /// <returns> true - afundou, false- não afundou</returns>
        public bool IsSink()
        {
            Shots++;
            if (Shots == Size)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Retorna a o valor de um Emun, tentdo em conta a entrada de um char "v" /"V" ou "h"/"H"
        /// </summary>
        /// <returns> Opeção Enum</returns>
        public static Orientation? getOrientation()
        {
            Console.Write("Orientação:");
            string option = Console.ReadLine();
            switch (option)
            {
                case "v":
                case "V":
                    return Orientation.Vertical;
                case "h":
                case "H":
                    return Orientation.Horizontal;
                default:
                    return null;
            }
        }

    }
}