using System;
using System.Text;
using System.Globalization;
using System.Collections.Generic;

//
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
	static void Main(string[] args)
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
	    Console.WriteLine('\t' + ordlista[indxValtOrd]);

	    // int kvarvarandeFörsök=9; // om man räknar för den stackars streckgubben
	    int kvarvarandeFörsök=10;
	    StringBuilder felaktigaBokstavsgissningar = new StringBuilder( "", kvarvarandeFörsök);
	    HashSet<char> bokstavsGissningar = new HashSet<char>();
	    // StringBuilder korrektaBokstavsgissningar = new StringBuilder( "", 29); // antal bokstäver i svenskt alfabet
	    // StringBuilder nuvarandeLäge = new StringBuilder( ordlista[indxValtOrd].Length);

	    //
	    // jämförbar med om man inte har några korrekta gissningar - generalisera !
	    //
	    // for (int pos=0 ; pos < ordlista[indxValtOrd].Length ; pos++) {
	    //	nuvarandeLäge.Append("_");
	    // }

	    // if (Iteration( ordlista[indxValtOrd],
	    //		   kvarvarandeFörsök,
	    //		   nuvarandeLäge,
	    //		   korrektaBokstavsgissningar,
	    //		   felaktigaBokstavsgissningar)){
	    //	Console.WriteLine("Grattis, du hittade rätt svar");
	    // } else {
	    //	Console.WriteLine("Nej, du hittade INTE rätt svar. Bättre lycka nästa gång");
	    // }
	    if (Iteration2( ordlista[indxValtOrd],
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

	static bool Iteration2( string hemligtOrd,
				int kvarvarandeFörsök,
				HashSet<char> bokstavsGissningar,
				StringBuilder felaktigaBokstavsgissningar)
	{
	    if (kvarvarandeFörsök > 0) {
		Console.WriteLine( "{0} {1} {2}", String.Format( "{0,3}", kvarvarandeFörsök), nuvarandeLäge(hemligtOrd, bokstavsGissningar), felaktigaBokstavsgissningar);

		string svar = Console.ReadLine();
		while (svar.Length==0) {
		    string svar = Console.ReadLine();
		}

		if (svar.Length==1) { // en enda bokstav, finns den i det hemliga ordet ?
		    if ( hemligtOrd.ToString().IndexOf(svar) == -1 ) { // bokstaven finns inte i det hemliga ordet
			if ( felaktigaBokstavsgissningar. bokstavsGissningar.Contains(svar)) { // svarsförsöket är har redan varit uppe en gång
			    Console.WriteLine("bokstaven {0} är redan med i uppräkningen av felaktiga", svar);
			    return Iteration2( hemligtOrd, kvarvarandeFörsök, bokstavsGissningar, felaktigaBokstavsgissningar);
			} else {
			    felaktigaBokstavsgissningar.Append(svar);
			    return Iteration2( hemligtOrd, kvarvarandeFörsök-1, bokstavsGissningar, felaktigaBokstavsgissningar);
			}
		    } else {
		    }
		} else {
		    return Iteration2( hemligtOrd, kvarvarandeFörsök-1, bokstavsGissningar, felaktigaBokstavsgissningar);
		}
	    } else {
		return false;
	    }
	}

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

	// static bool Iteration( string hemligtOrd,
	//		       int kvarvarandeFörsök,
	//		       StringBuilder nuvarandeLäge,
	//		       StringBuilder korrektaBokstavsgissningar,
	//		       StringBuilder felaktigaBokstavsgissningar)
	// {
	//     if (kvarvarandeFörsök>0) {
	//	Console.WriteLine( "omgång: {0} längd: {1} nuvarande: {2} felaktiga: {3}",
	//			   kvarvarandeFörsök,
	//			   hemligtOrd.Length,
	//			   nuvarandeLäge,
	//			   felaktigaBokstavsgissningar);

	//	string svar = Console.ReadLine();
	//	while (svar.Length == 0) {
	//	    svar = Console.ReadLine();
	//	}

	//	if (svar.Length == 1) { // en enda bokstav, finns den i det hemliga ordet ?
	//	    if ( hemligtOrd.ToString().IndexOf(svar) == -1 ) { // bokstaven finns inte i det hemliga ordet
	//		if (felaktigaBokstavsgissningar.ToString().IndexOf(svar) >= 0) { // svarsförsöket är redan med i felaktigaBokstavsgissningar
	//		    Console.WriteLine("bokstaven {0} är redan med i uppräkningen av felaktiga", svar);
	//		    return Iteration( hemligtOrd,
	//				      kvarvarandeFörsök,
	//				      nuvarandeLäge,
	//				      korrektaBokstavsgissningar,
	//				      felaktigaBokstavsgissningar);
	//		} else {
	//		    felaktigaBokstavsgissningar.Append(svar);
	//		    return Iteration( hemligtOrd,
	//				      kvarvarandeFörsök-1,
	//				      nuvarandeLäge,
	//				      korrektaBokstavsgissningar,
	//				      felaktigaBokstavsgissningar);
	//		}
	//	    } else { // korrekt gissning, bokstaven finns i ordet
	//		korrektaBokstavsgissningar.Append(svar);
	//		//
	//		// modifiera nuvarandeLäge - ersätt ett eller flera '_' med den rätta bokstaven,
	//		// samma bokstav kan finnas i flera exemplar i ordet så ersätt alla samtidigt
	//		//
	//		// Ett hack, bättre att generera om nuvarandeLäge utgående från
	//		// det hemliga ordet och de tidigare godkända bokstäverna
	//		// Ett medlemstest för bokstaven ? Finns bokstaven i en mängd ?
	//		//
	//		// det specifika läget där ingen gissning finns eller ingen gissning redan är godkänd
	//		// är jämförbar med det ställe där nuvarandeLäge initialiseras från början
	//		//
	//		for (int pos=0 ; pos < hemligtOrd.Length ; pos++) {
	//		    if ( korrektaBokstavsgissningar.
	//		}

	//		// for (int pos=0 ; pos < hemligtOrd.Length ; pos++) {
	//		// if ( svar.Equals( hemligtOrd[pos] )) {
	//		// nuvarandeLäge.Replace( '_', char.Parse(svar), pos, 1);
	//		// }
	//		// }
	//		return Iteration( hemligtOrd,
	//				  kvarvarandeFörsök,
	//				  nuvarandeLäge,
	//				  korrektaBokstavsgissningar,
	//				  felaktigaBokstavsgissningar);
	//	    }
	//	} else { // något som är längre än 1 bokstav, ett svar ?
	//	    if (svar.Equals(hemligtOrd, StringComparison.CurrentCultureIgnoreCase )) {
	//		return true;
	//	    } else {
	//		return Iteration( hemligtOrd,
	//				  kvarvarandeFörsök-1,
	//				  nuvarandeLäge,
	//				  korrektaBokstavsgissningar,
	//				  felaktigaBokstavsgissningar);
	//	    }
	//	}
	//     } else {
	//	return false;
	//     }
	// }
    }
}
