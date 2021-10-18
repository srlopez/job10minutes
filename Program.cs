// https://dev.to/albinotonnina/how-to-lose-a-it-job-in-10-minutes-35pi
// ["Tokyo", "London", "Rome", "Donlon", "Kyoto", "Paris"]
// Resultado
// [["Tokyo", "Kyoto"], ["London", "Donlon"], ["Rome"], ["Paris"]]
using System;
using System.Linq;
using System.Collections.Generic;
var lista = new List<string>{ "Tokyo", "London", "Rome", "Donlon", "Kyoto", "Paris" };
var dicc = new Dictionary<string, List<string>>();

// Algoritmo 😉 
foreach (var item in lista)
{
    /*
    tokyotokyo   => [ Tokyo, Kyoto ]
    londonlondon => [ London, Donlon ]
    romerome     => [ Rome ]
    parisparis   => [ Paris ]
    */
    var city = item.ToLower();
    var key = dicc.Keys.FirstOrDefault(key => key.Contains(city));
    if(key is not null) dicc[key].Add(item);
    else dicc[city+city] = new List<string>{item};
}

// output
// con lambdas
Func<string, string> toQuota = (string s) => $"\"{s}\"";
Func<List<string>, string> toListQuoted = (List<string> l) => String.Format("[{0}]",String.Join(", ", l.Select(toQuota)));
Console.WriteLine(toListQuoted(lista));
Console.WriteLine("["+String.Join(", ",dicc.Values.Select(toListQuoted))+"]");
// Interpolado en una sóla línea 😉 
// Console.WriteLine($"[{String.Join(", ", lista.Select(c=>'"'+c+'"'))}]");
// Console.WriteLine($"[{String.Join(", ", dicc.Values.Select(l => "["+String.Join(", ", l.Select(c=>'"'+c+'"'))+"]"))}]");

/*
using System;
using System.Collections.Generic;
using System.Linq;
#nullable enable

//  == CRITERIOS DE CLASIFICACION ============================
//  Ciclograma*: Valores que tienen las mismas letras y en el mismo orden *Palabra inventada
//  London == Donlon =/= Dloonn =/= Oldn
//  Ojo: Predicate adminte un uníco parámetro, por lo tanto he definido una tupla de dos items.
Predicate<(string, string)> SonCiclogramas = bitupla => (bitupla.Item1 + bitupla.Item1).ToLower().Contains(bitupla.Item2.ToLower());

//  Anagrama: alores que tiene las mismas letras y en la misma cantidad
//  London == Donlon == Dloonn =/= Oldn
Predicate<(string, string)> SonAnagramas = bitupla =>
{
    return ToAnagrama(bitupla.Item1).Equals(ToAnagrama(bitupla.Item2));
    // método interno
    // https://stackoverflow.com/questions/6441583/is-there-a-simple-way-that-i-can-sort-characters-in-a-string-in-alphabetical-ord/6441603
    static string ToAnagrama(string s) => String.Concat(s.ToLower().OrderBy(c => c));
};

// Valores que contiene las mismas letras por lo menos una vez, excluidas repeticiones.
// London == Donlon == Dloonn == Oldn 
Predicate<(string, string)> MismasLetras = bitupla =>
{
    return ToLetras(bitupla.Item1) == ToLetras(bitupla.Item2);
    static string ToLetras(string s) => String.Concat(s.ToLower().OrderBy(c => c).Distinct());
};

// Valores que contiene las mismas vocales por lo menos una vez, excluidas repeticiones.
// London == Donlon == Dloonn == Oldn = Tokyo = Kyoto
Predicate<(string, string)> MismasVocales = bitupla =>
{
    return ToVocales(bitupla.Item1) == ToVocales(bitupla.Item2);
    // https://stackoverflow.com/questions/18109890/c-sharp-count-vowels
    static string ToVocales(string str) =>
         String.Concat(str.ToLower().OrderBy(c => c).Distinct().Where(c => "aeiou".Contains(c)));
};

// == ALGORITMO DE CLASIFICACION: CICLO =======================
List<List<string>> ClasificaEn<T>(List<string> lista, Predicate<(string, string)> predicate) where T : ICollection<string>, new()
{
    var dicc = new Dictionary<string, T>();
    // HashSet y Vocales
    // London => ["Tokyo", "London", "Donlon", "Kyoto", "Oldn", "Nolodon", "Dloonn"]
    // Rome   => ["Rome", "Londeen"]
    // Paris  => [ Paris ]
    //
    foreach (var item in lista)
    {
        var key = dicc.Keys.FirstOrDefault(key => predicate((item, key)));
        if (key is null) dicc[item] = new T() { item };
        else dicc[key].Add(item);
    }
    return dicc.Values.Select(l => l.ToList()).ToList();
};
// == AUXILIARES PARA PRESENTACION ===========
Func<string, string> toComillas = s => $"\"{s}\"";
Func<List<string>, string> toCorchetes = l => String.Format("[{0}]", String.Join(", ", l.Select(toComillas)));
Action<string, List<List<string>>> Display = (title, lista) => Console.WriteLine(title + " : " + String.Join(", ", lista.Select(toCorchetes)));

// =====================  RUNNING =======================
var lista = new List<string> { 
// Valores originales https://hackernoon.com/how-to-lose-an-it-job-in-10-minutes-3d63213c8370
"Tokyo", "London", "Rome", "Donlon", "Kyoto", "Paris",
// Añadidos para explorar
"Oldn",   // Mismas Letras pero cantidad menor que London(key), Anagrama NO , Ciclograma NO
"Nolodon",// Mismas Letras pero cantidad mayor, Anagrama NO , Ciclograma NO
"Dloonn", // Distinto orden mismas letras, Anagrama SI, Ciclograma NO, Mismas Letras SI
"London", // Duplicado, para verificar el HashSet vs List
"Londeen" // Mismas vocales pero no ciclograma ni anagrama que Rome, 
};

Console.WriteLine(toCorchetes(lista));

Display("Ciclograma ", ClasificaEn<List<string>>(lista, SonCiclogramas));
Display("Anagrama   ", ClasificaEn<List<string>>(lista, SonAnagramas));
Display("Letras     ", ClasificaEn<List<string>>(lista, MismasLetras));
Display("Vocales    ", ClasificaEn<List<string>>(lista, MismasVocales));

Display("CiclograSet", ClasificaEn<HashSet<string>>(lista, SonCiclogramas));
Display("AnagramaSet", ClasificaEn<HashSet<string>>(lista, SonAnagramas));
Display("Letra y Set", ClasificaEn<HashSet<string>>(lista, MismasLetras));
Display("Vocal y Set", ClasificaEn<HashSet<string>>(lista, MismasVocales));
*/