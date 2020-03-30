using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace footgolf
{
    class Footgolf
    {
        public string Nev { get; set; }
        public string Kategoria { get; set; }
        public string Egyesulet { get; set; }
        public int[] Pontok { get; set; }

        public Footgolf(String s)
        {
            string[] tmp = s.Split(';');
            Nev = tmp[0];
            Kategoria = tmp[1];
            Egyesulet = tmp[2];
            Pontok = new int[8];
            for (int i = 3; i <= Pontok.Length; i++)
            {
                Pontok[i - 3] = Convert.ToInt32(tmp[i]);
            }

        }

        public int OsszPontszam()
        {
            int pontok = 0;
            Array.Sort(Pontok);
            for (int i = 2; i < Pontok.Length; i++)
            {
                pontok += Pontok[i];
                if (Pontok[0] > 0) pontok += 10;
                if (Pontok[1] > 0) pontok += 10;
            }
            return pontok;
        }
    }
    class Program
    {
        static List<Footgolf> versenyzok;
        static void Main(string[] args)
        {
            F02();
            F03();
            F04();
            F06();
            F07();
            F08();
            Console.ReadKey();
        }
        private static void F08()
        {
            Console.WriteLine("8. feladat: ");
            var s = versenyzok.GroupBy(x => x.Egyesulet).Select(group => Tuple.Create(group.Key, group.Count())).ToList();
                s.ForEach(t => Console.WriteLine($"\t{t.Item1} - {t.Item2} fő"));
        }
        private static void F07()
        {
            var sw = new StreamWriter(@"..\..\Res\osszPontFF.txt");
            versenyzok.ForEach(k => sw.WriteLine(k.Nev + ";" + k.OsszPontszam()));
            sw.Close();

        }
        private static void F06()
        {
            var v = versenyzok.Where(x => x.Kategoria == "Noi").OrderByDescending(x => x.OsszPontszam()).FirstOrDefault();
            Console.WriteLine($"6. feladat: A bajnok női versenyző\n" +
                $"Név: {v.Nev}\n" +
                $"Egyesület: {v.Egyesulet}\n" +
                $"Összpont: {v.OsszPontszam()}");
        }
        private static void F04()
        {
            var db = versenyzok.Count(a => a.Kategoria == "Noi");
            double eredmeny = Math.Round((double)db / versenyzok.Count, 4);
            Console.WriteLine($"4. feladat: A női versenyzől aránya: {eredmeny * 100}%");

        }
        private static void F03()
        {
            Console.WriteLine($"3. feladat: A versenyzők száma: {versenyzok.Count}");

        }
        private static void F02()
        {
            versenyzok = new List<Footgolf>();
            var sr = new StreamReader(@"..\..\Res\fob2016.txt", Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                versenyzok.Add(new Footgolf(sr.ReadLine()));
            }
            sr.Close();
        }
    }
}
