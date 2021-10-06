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
