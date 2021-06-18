using System;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

//
// Time-stamp: <2021-06-18 21:53:26 stefan>
//     _______
//     |     |
//     o     |
//    /|\    |
//     |     |
//    / \    |
//
//
namespace hangmanspel
{
    class Program
    {
	static void Main( string[] args)
	{
	    string[] ordlista = { "xylofon",
		"säckpipa",
		"harpa",
		"vibrafon",
		"puka",
		"marimba",
		"bastrumma",
		"flöjt",
		"tvärflöjt",
		"oboe",
		"fagott",
		"klarinett",
		"tenorsaxofon",
		"saxofon",
		"barytonsaxofon",
		"valthorn",
		"trumpet",
		"trombon",
		"tuba",
		"hardangerfela",
		"nyckelharpa",
		"träskofiol",
		"viola",
		"altfiol",
		"violin",
		"cello",
		"basfiol",
		"kontrabas"};

	    Random slumptal = new Random();

	    int indxValtOrd = slumptal.Next( ordlista.Length );
	    //Console.WriteLine('\t' + ordlista[indxValtOrd]);

	    // int kvarvarandeFörsök=9; // om man räknar för den stackars streckgubben
	    int kvarvarandeFörsök=10;
	    StringBuilder felaktigaBokstavsgissningar = new StringBuilder( "", kvarvarandeFörsök);
	    HashSet<char> bokstavsGissningar = new HashSet<char>();

	    if (Iteration( ordlista[indxValtOrd].ToLower(),  // garantera att ordet enbart innehåller gemener
			   kvarvarandeFörsök,
			   bokstavsGissningar,
			   felaktigaBokstavsgissningar)){
		Console.WriteLine("Grattis, du hittade rätt svar");
	    } else {
		Console.WriteLine("Nej, du hittade INTE rätt svar. Bättre lycka nästa gång");
	    }

	    // debug:utskrift - berätta vad som var korrekt
	    Console.WriteLine('\t' + ordlista[indxValtOrd]);
	}

	/// <summary>
	/// rekursiv version av spellogiken
	/// </summary>
	static bool Iteration( string hemligtOrd,
			       int kvarvarandeFörsök,
			       HashSet<char> bokstavsGissningar,
			       StringBuilder felaktigaBokstavsgissningar)
	{
	    if (kvarvarandeFörsök > 0) {
		Console.WriteLine( "{0} {1} {2}", String.Format( "{0,3}", kvarvarandeFörsök), nuvarandeLäge(hemligtOrd, bokstavsGissningar), felaktigaBokstavsgissningar);

		string svar = Console.ReadLine();
		while (svar.Length==0) {
		    svar = Console.ReadLine();
		}
		svar=svar.ToLower(); // gör om alltid om bokstaven till gemen

		if (svar.Length==1) { // en enda bokstav, finns den i det hemliga ordet ?
		    if ( hemligtOrd.ToString().IndexOf(svar) == -1 ) { // bokstaven finns inte i det hemliga ordet
			if ( felaktigaBokstavsgissningar.ToString().IndexOf(svar) >= 0) { // gissningen har redan varit uppe en gång
			    Console.WriteLine( "bokstaven {0} är redan med i uppräkningen av felaktiga", svar);
			    return Iteration( hemligtOrd, kvarvarandeFörsök, bokstavsGissningar, felaktigaBokstavsgissningar);
			} else {
			    felaktigaBokstavsgissningar.Append( svar);
			    return Iteration( hemligtOrd, kvarvarandeFörsök-1, bokstavsGissningar, felaktigaBokstavsgissningar);
			}
		    } else {  // grattis, bokstaven finns i det hemliga ordet
			bokstavsGissningar.Add( char.Parse(svar));
			return Iteration( hemligtOrd, kvarvarandeFörsök, bokstavsGissningar, felaktigaBokstavsgissningar);
		    }
		} else {   // något som är längre än 1 bokstav, ett svar ?
		    if (svar.Equals(hemligtOrd)) { // grattis ! Rätt ord
			return true;
		    } else {  // nää, ordert var felaktigt
			return Iteration( hemligtOrd, kvarvarandeFörsök-1, bokstavsGissningar, felaktigaBokstavsgissningar);
		    }
		}
	    } else {
		return false;
	    }
	}

	///<summary>
	///i utskriften ska för läsbarheten _ få ett mellanslag framför sig
	///</summary>
	static StringBuilder nuvarandeLäge( string hemligtOrd,
					    HashSet<char> bokstavsGissningar)
	{
	    StringBuilder result = new StringBuilder( "", hemligtOrd.Length);
	    foreach (char bokstav in hemligtOrd) {
		result.Append( " ");
		if ( bokstavsGissningar.Contains(bokstav)) {
		    result.Append( bokstav);
		} else {
		    result.Append( "_");
		}
	    }
	    return result;
	}
    }
}
