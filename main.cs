using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class Sudoku
{
    public static int sor;
    public static int oszlop;
    public static int szam;
    public static int[,] tomb = new int[10,10];
      
    public static bool kitoltott = (tomb[sor, oszlop] == 0) ? false : true;
      
    public static bool foglalt_sor()
    {
        bool foglalt = false;
        for(int j=1; j<=9; j++)
        {
            if (tomb[sor, j] == szam)
            {
                foglalt = true; 
            }
        }
        return foglalt;
    }

    public static bool foglalt_oszlop()
    {
        bool foglalt = false;
        for(int i=1; i<=9; i++)
        {
            if (tomb[i, oszlop] == szam)
            {
                foglalt = true; 
            }
        }
        return foglalt;
    }
    
    public static bool foglalt_resztabla()
    { 
        int x = sor - (sor-1)%3;
        int y = oszlop - (oszlop-1)%3;
        bool foglalt = false;
        
        for(int i=0; i<3; i++)
        {
            for(int j=0; j<3; j++)
            {
                if (tomb[i+x, j+y] == szam)
                {
                    foglalt = true; 
                }
            }
        }
        return foglalt;
    } 
    public static int resztabla()
    {
        return (sor-1)/3 * 3 + (oszlop-1)/3 + 1;
    }
    
    public static bool megteheto()
    {
        return (!kitoltott && !foglalt_sor() && !foglalt_oszlop() && !foglalt_resztabla()) ? true : false;
    }
}

class Lepes
{
    public int sor; 
    public int oszlop;
    public int szam;
     
    public Lepes(string line)
    {
        var s = line.Split(' ');
        szam   = int.Parse(s[0]);
        sor    = int.Parse(s[1]);
        oszlop = int.Parse(s[2]);
    }
}

class Program 
{
    public static void Main (string[] args) 
    {
// 1. Olvassa be egy fájl nevét, egy sor és egy oszlop sorszámát (1 és 9 közötti számot)!
//   A későbbi feladatokat ezen értékek felhasználásával kell megoldania!
//   (A program megírásakor a felhasználó által megadott adatok helyességét,érvényességét nem kell ellenőriznie, 
//   feltételezheti, hogy a rendelkezésre álló adatok a leírtaknak megfelelnek.)

        Console.WriteLine($"1. feladat");
        Console.Write(    $"Adja meg a bemeneti fájl nevét! ");
        string fajlnev = Console.ReadLine();
        Console.Write($"Adja meg egy sor számát! ");
        int sorszam = int.Parse( Console.ReadLine() );
        Console.Write($"Adja meg egy oszlop számát! ");
        int oszlopszam = int.Parse( Console.ReadLine() );

// 2. Az előző feladatban beolvasott névnek megfelelő fájl tartalmát olvassa be, és tárolja el atáblázat adatait! 
//   Ha ezt nem tudja megtenni, akkor használja forrásként a rendelkezésre álló állományok egyikét!

        var sr=new StreamReader(fajlnev);      
        for(int sor=1; sor<=9; sor++)
        {
            var line = sr.ReadLine().Split(' ');
            for(int oszlop=1; oszlop<=9; oszlop++)
            {
                Console.WriteLine();
                Sudoku.tomb[sor, oszlop] = int.Parse(line[oszlop-1]);
            }
        }
        var lista=new List<Lepes>();
        while(!sr.EndOfStream)
        {
            lista.Add(new Lepes(sr.ReadLine()));
        }     
        sr.Close();

// 3. Írja ki a képernyőre, hogy a beolvasott sor és oszlop értékének megfelelő hely…
//      a. milyen értéket tartalmaz! 
//        Ha az adott helyen a 0 olvasható, akkor az „Az adotthelyet még nem töltötték ki.” szöveget jelenítse meg!
//      b. melyik résztáblázathoz tartozik!      
        Console.WriteLine();
        Console.WriteLine($"3. feladat");
        if (Sudoku.kitoltott)
        {
            Console.WriteLine($"Az adott helyen szereplő szám: {Sudoku.tomb[sorszam,oszlopszam]}");
        }
        else
        {
            Console.WriteLine($"Az adott helyet még nem töltötték ki."); 
        }
        Console.WriteLine($"A hely a(z) {Sudoku.resztabla()} résztáblázathoz tartozik.");

// 4. Határozza meg a táblázat hány százaléka nincs még kitöltve! Az eredményt egy tizedesjegy pontossággal jelenítse meg a képernyőn!

        int szamlalo=0;
        for(int sor=1; sor<=9; sor++)
        {         
            for(int oszlop=1;oszlop<=9;oszlop++)
            {
                if(Sudoku.tomb[sor,oszlop]==0)
                {
                    szamlalo++;
                }   
            }          
        }
                      
        Console.WriteLine();
        Console.WriteLine($"4. feladat"); 
        Console.WriteLine($"Az üres helyek aránya: {(double)szamlalo/81*100:0.#}%");
/*
5. Vizsgálja meg, hogy a fájlban szereplő lépések lehetségesek-e a beolvasott táblázaton!
Tekintse mindegyiket úgy, mintha az lenne az egyetlen lépés az eredeti táblázaton, de ne hajtsa azt végre! 
Állapítsa meg, hogy okoz-e valamilyen ellentmondást a lépés végrehajtása!
Írja ki a lépéshez tartozó három értéket, majd a következő sorba írja az alábbi megállapítások egyikét! 
Ha több megállapítás is igaz, elegendő csak egyet megjelenítenie.
• „A helyet már kitöltötték”
• „Az adott sorban már szerepel a szám”
• „Az adott oszlopban már szerepel a szám”
• „Az adott résztáblázatban már szerepel a szám”
• „A lépés megtehető”
*/
        Console.WriteLine();
        Console.WriteLine($"5. feladat"); 
        foreach ( var lepes in lista )
        {
            Sudoku.sor    = lepes.sor;
            Sudoku.oszlop = lepes.oszlop;
            Sudoku.szam   = lepes.szam;
            Console.WriteLine($"A kiválasztott sor: {lepes.sor} oszlop: {lepes.oszlop} a szám: {lepes.szam}");
            
            if (Sudoku.kitoltott)
            {
                Console.WriteLine($"A helyet már kitöltötték.");
                Console.WriteLine();
            }
            else
            {
                if (Sudoku.megteheto())
                {
                    Console.WriteLine($"A lépés megtehető.");                    
                    Console.WriteLine();
                }

                if (Sudoku.foglalt_sor())
                {
                    Console.WriteLine($"Az adott sorban már szerepel a szám.");
                    Console.WriteLine();
                }

                else if (Sudoku.foglalt_oszlop())
                {
                    Console.WriteLine($"Az adott oszlopban már szerepel a szám.");
                    Console.WriteLine();
                }

                else if (Sudoku.foglalt_resztabla())
                {
                    Console.WriteLine($"Az résztáblázatban már szerepel a szám.");
                    Console.WriteLine();
                }
            }

        }

    }
}