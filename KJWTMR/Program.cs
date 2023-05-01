using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KJWTMR
{
        
    class Program
    {
        static public void Talalt()
        {
            Console.WriteLine("A keresés talált egy optimális megoldást!");
        }
        static void Main(string[] args)
        {
            CsoportosEdzes joga = new Joga(500,30,Stilus.Kimelo);
            CsoportosEdzes joga2 = new Joga(600, 30, Stilus.Kimelo);
            CsoportosEdzes kettlebell = new Kettlebell(400,20,Stilus.Eronleti);
            CsoportosEdzes norbiTorna = new NorbiTorna(900, 50, Stilus.Kardio);
            //CsoportosEdzes norbiTorna2 = new NorbiTorna(800, 50, Stilus.Kardio);
            SzemelyiEdzo szalkasitas = new Szalkasitas(500,90,Stilus.Kardio);
            //SzemelyiEdzo szalkasitas2 = new Szalkasitas(600, 90, Stilus.Kardio);
            SzemelyiEdzo tomegnoveles = new Tomegnoveles(500,90,Stilus.Eronleti);
            //SzemelyiEdzo tomegnoveles1 = new Tomegnoveles(500, 90, Stilus.Eronleti);

            FitnessTeremLista lista = new FitnessTeremLista();
            lista.talalt += Talalt;
            ITorna[,] refTabla = new ITorna[1,3];            

            Console.WriteLine("Kérem adja meg a keresni kívánt stílust, ha több stílust szeretne keresni akkor ','-vel elválasztva adja meg őket!\nVálaszható stílusok:Kardio,Kimelo,Eronleti");
            string bekertStilus = Console.ReadLine();
            Console.WriteLine("Kére adja meg percben, hogy milyen hosszú műsort keres!");
            int bekertIdo = int.Parse(Console.ReadLine());
            try
            {
                lista.RendezettBeszuras(joga, (int)joga.Stilus);
                lista.RendezettBeszuras(kettlebell, (int)kettlebell.Stilus);
                lista.RendezettBeszuras(norbiTorna, (int)norbiTorna.Stilus);
                //lista.RendezettBeszuras(norbiTorna2, (int)norbiTorna2.Stilus);
                lista.RendezettBeszuras(szalkasitas, (int)szalkasitas.Stilus);
                //lista.RendezettBeszuras(szalkasitas2, (int)szalkasitas2.Stilus);
                lista.RendezettBeszuras(tomegnoveles, (int)tomegnoveles.Stilus);
                //lista.RendezettBeszuras(joga2, (int)joga2.Stilus);
                //lista.RendezettBeszuras(joga, (int)joga.Stilus);
                //lista.Torles(tomegnoveles1);

                lista.ReferenciaTablaFeltoltes(refTabla);


                lista.StilusValogato(bekertStilus, lista);
                lista.VisszaKereses(bekertIdo);
            } 
            catch (MarTartalmazza excpt)
            {
                Console.WriteLine(excpt.Message);
            }
            catch(NemTartalmazzaException excpt)
            {
                Console.WriteLine(excpt.Message);
            }
            catch (NincsValamilyenStilisuElem excpt)
            {
                Console.WriteLine(excpt.Message);
            }
            catch(NincsMegoldasKivetel excpt)
            {
                Console.WriteLine(excpt.Message);
            }          
            //Console.WriteLine(lista.Kereses(joga)); 
            //Console.WriteLine(lista.Kereses(joga2)); 
            ;
        }    
    }

    //Kivételkezelés   
    class MarTartalmazza : Exception
    {
        public MarTartalmazza(int szam) : base($"A(z) {szam}. óra már szerepel a programban!")
        {

        }
    }
    class NemTartalmazzaException : Exception
    {
        public NemTartalmazzaException() : base("Nem tartalmazza a törölni kívánt elemet!")
        {
        }
    }
    class NincsValamilyenStilisuElem : Exception
    {
        public NincsValamilyenStilisuElem() : base("A mai programban nem szerepel valamelyik stílusú mozgás!")
        {
        }
    }
    class NincsMegoldasKivetel : Exception
    {
        public NincsMegoldasKivetel() : base("Nincs megoldása a feladatnak!")
        {
        }
    }
}
