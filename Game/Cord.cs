namespace Battleship.Cli
{
    /// <summary>
    /// Emumeravel com o estado de um posição
    /// </summary>
    public enum CordState
    {
        Ocean = 0,
        HitOcean = 1,
        Ship = 2,
        HitShip = 3
    }
    public class Cord
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public CordState State { get; set; }

        /// <summary>
        /// Contrutor da class Cord
        /// </summary>
        /// <param name="x"> Cordernada X</param>
        /// <param name="y"> Cordenada Y</param>
        /// <param name="st"> Estado da cordenada</param>
        public Cord(int x, int y, CordState st)
        {
            X = x;
            Y = y;
            State = st;
        }

        /// <summary>
        /// A função retorna um valor numero convertendo o emun
        /// </summary>
        /// <param name="state">Opção emun </param>
        /// <returns>Valor do enum</returns>
        public static int getCordState(CordState state)
        {
            switch (state)
            {
                case CordState.Ocean: return 0;
                case CordState.HitOcean: return 1;
                case CordState.Ship: return 2;
                case CordState.HitShip: return 3;
                default: return -1;
            }

        }

    }
}