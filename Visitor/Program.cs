using System;
using System.Collections.Generic;

namespace Visitor
{
    public class KampaniaMarketingowa
    {
        public long kwotaWydana;
        public long iloscKlientow;

        public KampaniaMarketingowa(long kwotaWydana, long iloscKlientow)
        {
            this.kwotaWydana = kwotaWydana;
            this.iloscKlientow = iloscKlientow;
        }
        public virtual void Accept(IVisitor visitor) { }
    }
    public class Influencer : KampaniaMarketingowa
    {
        public string nazwaKonta;
        public Influencer(long kwotaWydana, long iloscKlientow, string nazwaKonta):base(kwotaWydana,iloscKlientow)
        {
            this.nazwaKonta = nazwaKonta;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.MetodaDlaInfluencera(this);
        }
        public string InfoOgolne()
        {
            return $"Informacje o kampanii Influencer ({nazwaKonta}):";
        }
    }
    public class Native : KampaniaMarketingowa
    {
        public long iloscBilboardow;
        public Native(long kwotaWydana, long iloscKlientow, long iloscBilboardow) : base(kwotaWydana, iloscKlientow)
        {
            this.iloscBilboardow = iloscBilboardow;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.MetodaDlaNative(this);
        }
        public string InfoOgolne()
        {
            return $"Informacje o kampanii Native:";
        }
    }
    public class Internet : KampaniaMarketingowa
    {
        public string adresUrl;
        public long iloscReklam;
        public Internet(long kwotaWydana, long iloscKlientow,string adresUrl, long iloscReklam) : base(kwotaWydana, iloscKlientow)
        {
            this.adresUrl = adresUrl;
            this.iloscReklam=iloscReklam;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.MetodaDlaInternetu(this);
        }
        public string InfoOgolne()
        {
            return $"Informacje o kampanii Internet ({adresUrl}):";
        }
    }

    public interface IVisitor
    {
        void MetodaDlaInfluencera(Influencer element);
        void MetodaDlaNative(Native element);
        void MetodaDlaInternetu(Internet element);
    }

    class SprawdzenieDanych : IVisitor
    {
        public void MetodaDlaInfluencera(Influencer element)
        {
            if(element.nazwaKonta!="" && element.kwotaWydana!=0 && element.iloscKlientow!=0)
                Console.WriteLine("Dane o kampanii są poprawne.");
            else
                Console.WriteLine("Uwaga! Dane kampanii są niepoprawne!");
        }

        public void MetodaDlaNative(Native element)
        {
            if (element.iloscBilboardow != 0 && element.kwotaWydana != 0 && element.iloscKlientow != 0)
                Console.WriteLine("Dane o kampanii są poprawne.");
            else
                Console.WriteLine("Uwaga! Dane kampanii są niepoprawne!");
        }

        public void MetodaDlaInternetu(Internet element)
        {
            if (element.adresUrl!="" && element.iloscReklam != 0 && element.kwotaWydana != 0 && element.iloscKlientow != 0)
                Console.WriteLine("Dane o kampanii są poprawne.");
            else
                Console.WriteLine("Uwaga! Dane kampanii są niepoprawne!");
        }
    }

    class GenerowanieRaportu : IVisitor
    {
        public void MetodaDlaInfluencera(Influencer element)
        {
            Console.WriteLine(element.InfoOgolne() + $"\n\tWydano:{element.kwotaWydana}PLN;\tPozyskano:{element.iloscKlientow} klientów.");
        }

        public void MetodaDlaNative(Native element)
        {
            Console.WriteLine(element.InfoOgolne() + $"\n\tWydano:{element.kwotaWydana} PLN;\tPozyskano:{element.iloscKlientow} klientów;\tIlość wykupionych bilboardów:{element.iloscBilboardow}.");
        }
        public void MetodaDlaInternetu(Internet element)
        {
            Console.WriteLine(element.InfoOgolne() + $"\n\tWydano::{element.kwotaWydana} PLN;\tPozyskano:{element.iloscKlientow} klientów;\tIlość reklam:{element.iloscReklam}.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var visitor1 = new SprawdzenieDanych();
            var visitor2 = new GenerowanieRaportu();
            string wybór = "";
            Console.WriteLine("Aby zakończyć działąnie aplikacji wpisz exit:");
            while(wybór!="exit")
            {
                try
                {
                    Console.WriteLine("\nPodaj rodzaj kampanii: (Influencer/Native/Internet)");
                    wybór = Console.ReadLine();
                    if (wybór == "Influencer" || wybór == "Native" || wybór == "Internet")
                    {

                        Console.Write("Podaj kwotę obecnie wydaną:");
                        long kwota = Convert.ToInt64(Console.ReadLine());

                        Console.Write("Podaj ilość pozyskanych klientów:");
                        long iloscKlientow = Convert.ToInt64(Console.ReadLine());
                        KampaniaMarketingowa kampania = new KampaniaMarketingowa(kwota, iloscKlientow);
                        if (wybór == "Influencer")
                        {
                            Console.Write("Podaj nazwę konta/bloga:");
                            string nazwa = Console.ReadLine();
                            kampania = new Influencer(kwota, iloscKlientow, nazwa);
                        }
                        else if (wybór == "Native")
                        {
                            Console.Write("Podaj ilość wykupionych bilboardów:");
                            long iloscBilboardów = Convert.ToInt64(Console.ReadLine());
                            kampania = new Native(kwota, iloscKlientow, iloscBilboardów);
                        }
                        else if (wybór == "Internet")
                        {
                            Console.Write("Podaj ilość wykupionych reklam:");
                            long iloscReklam = Convert.ToInt64(Console.ReadLine());

                            Console.Write("Podaj adres URL strony:");
                            string adresUrl = Console.ReadLine();
                            kampania = new Internet(kwota, iloscKlientow, adresUrl, iloscReklam);
                        }
                        kampania.Accept(visitor1);
                        kampania.Accept(visitor2);
                    }
                    else if (wybór != "exit")
                    {
                        Console.WriteLine("Błędna opcja.");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Uzupełnij pole. Dane liczbowe muszą zostać wprowadzone, w przypadku braku wpisz 0.");
                }
            }
            

        }
        
    }
}