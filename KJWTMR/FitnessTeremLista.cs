using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KJWTMR
{

    class ListaElem
    {
        public ITorna Tartalom { get; set; }
        public ListaElem Kovetkezo { get; set; }
    }
    class FitnessTeremLista : ProgramOsszeallito
    {
        ListaElem fej;
        ListaElem fejKimelo;
        ListaElem fejEronleti;
        ListaElem fejKardio;
        public void Tartalmazzae(ListaElem p, ListaElem uj)
        {
            int i = 0;
            while (p != null)
            {
                i++;
                if (p.Tartalom == uj.Tartalom)
                {
                    throw new MarTartalmazza(i);
                }
                p = p.Kovetkezo;
            }
        }
        public void RendezettBeszuras(ITorna tartalom, int kulcs)
        {
            ListaElem uj = new ListaElem();
            uj.Tartalom = tartalom;
            uj.Kovetkezo = null;

            ListaElem p = fej;
            ListaElem e = null;

            Tartalmazzae(p, uj);

            if (fej == null)
            {
                fej = uj;
            }
            else
            {
                while (!((p == null) || ((int)p.Tartalom.Stilus < kulcs)))
                {
                    e = p;
                    p = p.Kovetkezo;
                }
                if (e != null)
                {
                    e.Kovetkezo = uj;
                }
                else
                {
                    fej = uj;
                }
                uj.Kovetkezo = p;
            }
        }
        public void Torles(ITorna tartalom)
        {
            ListaElem e = null;
            ListaElem p = fej;
            while (p != null && p.Tartalom != tartalom)
            {
                e = p;
                p = p.Kovetkezo;
            }
            if (p != null)
            {
                if (e == null)
                {
                    fej = p.Kovetkezo;
                }
                else
                {
                    e.Kovetkezo = p.Kovetkezo;
                }
            }
            else
            {
                throw new NemTartalmazzaException();
            }
        }
        public void ReferenciaTablaFeltoltes(ITorna[,] refTabla)
        {
            ListaElem p = fej;

            for (int i = 0; i < refTabla.GetLength(1); i++)
            {
                if (p != null)
                {
                    if (refTabla[0, i] == null && (int)p.Tartalom.Stilus == 0)
                    {
                        refTabla[0, i] = p.Tartalom;
                    }
                    else if (refTabla[0, i] == null && (int)p.Tartalom.Stilus == 1)
                    {
                        refTabla[0, i] = p.Tartalom;
                    }
                    else if (refTabla[0, i] == null && (int)p.Tartalom.Stilus == 2)
                    {
                        refTabla[0, i] = p.Tartalom;
                    }
                }

                while (p != null && (int)p.Tartalom.Stilus == (int)refTabla[0, i].Stilus)
                {
                    p = p.Kovetkezo;
                }
            }

            for (int i = 0; i < refTabla.GetLength(1); i++)
            {
                if (refTabla[0, i] == null)
                {
                    throw new NincsValamilyenStilisuElem();
                }
            }

            FejReferenciak();
        }
        public void FejReferenciak()
        {
            ListaElem p = fej;
            fejKimelo = new ListaElem();
            fejEronleti = new ListaElem();
            fejKardio = new ListaElem();
            while (p != null)
            {
                if (fejKardio.Tartalom == null && (int)p.Tartalom.Stilus == 1)
                {
                    fejKardio.Tartalom = p.Tartalom;
                                     
                }
                else if (fejEronleti.Tartalom == null && (int)p.Tartalom.Stilus == 0)
                {
                    fejEronleti.Tartalom = p.Tartalom;                    
                }
                else if (fejKimelo.Tartalom == null && (int)p.Tartalom.Stilus == 2)
                {
                    fejKimelo.Tartalom = p.Tartalom;                  
                }
                p = p.Kovetkezo;
            }            
        }
        public string Kereses(ITorna keres)
        {
            ListaElem p = fej;
            ListaElem k = null;
            if ((int)keres.Stilus == 2)
            {
                k = fejKimelo;
            }
            else if ((int)keres.Stilus == 1)
            {
                k = fejEronleti;
            }
            else
            {
                k = fejKardio;
            }
            ITorna talalt = null;          
            while (p != null)
            {
                if (p.Tartalom == keres)
                {
                    talalt = p.Tartalom;
                }
                p = p.Kovetkezo;
            }
            if (talalt == null)
            {
                return "A keresett elem nincs benne a listában!";
            }
            else
            {
                return "A keresett elem benne van a listában!";
            }
        }

        public override void StilusValogato(string megadottStilusok, FitnessTeremLista lista)
        {
            if (megadottStilusok.Contains(","))
            {
                stilusok = megadottStilusok.Split(',');
            }
            else
            {
                stilusok = new string[1];
                stilusok[0] = megadottStilusok;
            }
            ;
            int megfeleloStilusokDB = 0;          
            ListaElem p;
            for (int i = 0; i < stilusok.Length; i++)
            {
                p = fej;
                while (p != null)
                {
                    if (p.Tartalom.Stilus.ToString() == stilusok[i])
                    {
                        megfeleloStilusokDB++;
                    }
                    p = p.Kovetkezo;
                }           
            }
            
            megfeleloStilusok = new ITorna[megfeleloStilusokDB];
            int n = 0;
            for (int i = 0; i < stilusok.Length; i++)
            {
                p = fej;
                while (p != null)
                {
                    if (p.Tartalom.Stilus.ToString() == stilusok[i])
                    {
                        megfeleloStilusok[n] = p.Tartalom;
                        n++;
                    }
                    p = p.Kovetkezo;
                }
            }         
        }
       
        /*
        public int Count()
        {
            int count = 0;
            ListaElem p = fej;
            while (p!= null)
            {
                count++;
                p = p.Kovetkezo;
            }
            return count;
        }
        public void ListaToTomb(FitnessTeremLista lista)
        {
            ListaElem p = fej;
            listaTomb = new ITorna[lista.Count()];
            int i = 0;
            while (p!= null)
            {
                listaTomb[i] = p.Tartalom;
                i++;
                p = p.Kovetkezo;
            }
        }
        */
    }
}
