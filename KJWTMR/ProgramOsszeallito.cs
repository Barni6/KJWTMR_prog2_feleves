using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KJWTMR
{
    public delegate void Talalat();
    class ProgramOsszeallito
    {
        public event Talalat talalt;

        protected string[] stilusok;
        protected ITorna[] megfeleloStilusok;
        //protected ITorna[] listaTomb;
        protected ITorna[] optimalis;

        public virtual void StilusValogato(string megadottStilusok, FitnessTeremLista lista)
        { }

        //public abstract bool ft();


        private ITorna[] E;
        private int[] M;
        private ITorna[,] R;
        public ITorna[] VisszaKereses(int bekertIdo)
        {
            bool van = false;
            E = new ITorna[megfeleloStilusok.Length];
            M = new int[megfeleloStilusok.Length];
            for (int i = 0; i < M.Length; i++)
            {
                M[i] = megfeleloStilusok.Length - 1;
            }
            R = new ITorna[E.Length, E.Length];
            for (int i = 0; i < R.GetLength(0); i++)
            {
                for (int j = 0; j < R.GetLength(1); j++)
                {
                    R[i, j] = megfeleloStilusok[j];
                }
            }

            BackTrack(0, ref van, ref E, ref optimalis, bekertIdo);
            if (optimalis == null)
            {
                throw new NincsMegoldasKivetel();
            }
            return optimalis;
        }
        //nem vizsgálja meg az összes lehetséges megoldást, nem lét vissza az első szintig, az optimalis valamiért változik random
        public void BackTrack(int szint, ref bool van, ref ITorna[] E, ref ITorna[] optimalis, int bekertIdo)
        {
            int i = -1;
            int joIdoE = 0;
            int joIdoOPT = 0;
            while (szint<M.Length-1 && i < M[szint])
            {
                i++;
                if (fk(szint, megfeleloStilusok[i], E, bekertIdo))
                {
                    E[i] = megfeleloStilusok[i];
                    joIdoE = JosagIdo(E);
                    if (optimalis != null)
                    {
                        joIdoOPT = JosagIdo(optimalis);
                    }
                    if (!van || (joIdoE > joIdoOPT) || (joIdoE == joIdoOPT && JosagAr(E) < JosagAr(optimalis)))
                    {
                        optimalis = E;
                        talalt?.Invoke();
                        van = true;
                    }
                }
                else
                {
                    BackTrack(szint + 1, ref van, ref E, ref optimalis, bekertIdo);
                }
            }
        }
        static bool fk(int szint, ITorna aktulais, ITorna[] E, int bekertIdo)
        {
            bool ok = false;
            int osszido = 0;
            int db = Db(E);
            if (!E.Contains(aktulais))
            {
                for (int i = 0; i < db; i++)
                {
                    osszido += E[i].Idotartam;
                }
                if ((osszido + aktulais.Idotartam) <= bekertIdo)
                {
                    ok = true;
                }
            }
            return ok;
        }
        public double JosagAr(ITorna[] segedTart)
        {
            double osszAr = 0;
            int db = Db(segedTart);
            if (db == 0)
            {
                return osszAr;
            }
            else
            {
                for (int i = 0; i < db; i++)
                {
                    osszAr += (((double)segedTart[i].Idotartam / 60) * segedTart[i].OraBer);
                }
            }

            return osszAr;
        }
        public int JosagIdo(ITorna[] segedTart)
        {
            int osszIdo = 0;
            int db = Db(segedTart);

            for (int i = 0; i < db; i++)
            {
                osszIdo += segedTart[i].Idotartam;
            }

            return osszIdo;
        }
        static public int Db(ITorna[] E)
        {
            int db = 0;
            if (E == null)
            {
                return db;
            }
            else
            {
                for (int i = 0; i < E.Length; i++)
                {
                    if (E[i] != null)
                    {
                        db++;
                    }
                }
            }
            return db;
        }
        public int SegedOsszeg(int szint, ITorna[] E)
        {
            int segedOsszeg = 0;
            for (int i = 0; i < szint + 1; i++)
            {
                segedOsszeg += E[i].Idotartam;
            }
            return segedOsszeg;
        }
    }
}
