using System.Text;
using System.Linq;
using System;

class Game : knightmoves.Cheatable
{
    private readonly string _phrase;
    
    private const string MAGIC_WORD = "CHEAT";
    private const int DIVISIBLE_BY_VALUE = 2;

    public Game(string Phrase)
    {
        _phrase = Phrase;
    }

    /// <summary>
    /// Returns the initial masked version of the phrase
    /// </summary>
    public string DisplayBlanks()
    {
        return new string(_phrase.Select(c => c == ' ' ? ' ' : '_').ToArray());
    }

    /// <summary>
    /// Main game logic
    /// </summary>
    public string Play(char[] guessedLetters)
    {
        // === TIME CHEAT ===
        bool timeCheatActive = ApplyTimeCheat(DateTime.Now, DIVISIBLE_BY_VALUE);

        string guessedLettersStr = new string(guessedLetters);

        if(ApplyEasterEggCheat(guessedLettersStr))
        {
            Console.WriteLine();
            return string.Join(" ", _phrase.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(word => string.Concat(word.Select(c => char.IsLetter(c) ? c : c))));
        }

        // Build normal reveal
        string result = new string(_phrase.Select(c =>
        {
            if (char.IsLetter(c) && guessedLetters.Any(g => char.ToLower(g) == char.ToLower(c)))
                return c;
            return char.IsWhiteSpace(c) ? c : '_';
        }).ToArray());

 		// Win detection
        if (!result.Contains('_'))
        {
			Console.WriteLine();
            Console.WriteLine("Congratulations!");
			Console.WriteLine();
        }

        // Apply time cheat if active
        if (timeCheatActive == true)
        {
            return ApplyTimeCheatReveal(result);
        }
	
        return result;
    }

    /// <summary>
    /// Time cheat: Reveals every other word starting from the second word
    /// </summary>
    private string ApplyTimeCheatReveal(string currentResult)
    {
        //char[] letters = currentResult.ToCharArray();
		string[] words = currentResult.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 1; i < words.Length; i += 2)
        {
            words[i] = GetOriginalWordAtPosition(i);
        }
		Console.WriteLine();
        Console.WriteLine("Time cheat active! Revealing every other word...");
        Console.WriteLine();

        return string.Join(' ', words);
    }

    private string GetOriginalWordAtPosition(int wordIndex)
    {
        string[] originalWords = _phrase.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return wordIndex < originalWords.Length ? originalWords[wordIndex] : "";																						;
	}
	
    // =================================================================
    // Interface Implementation: Cheatable
    // =================================================================

    public bool ApplyTimeCheat(DateTime now, int divisibleByValue)
    {
        /*if (divisibleByValue <= 0)
            return false;*/
            Console.WriteLine(DateTime.Now);
        if(divisibleByValue <= 0)
        {
            return now.Second % divisibleByValue == 0;
        } else if (divisibleByValue > 0)
        {
            return false;
        } else
        {
            return false;
        }
        
    }
    //change if return to if else
    public bool ApplyEasterEggCheat(string magicWord)
    {
        return magicWord.ToUpperInvariant().Contains(MAGIC_WORD);
    }

    /// <summary>
    /// Validates player input. Cheats are always valid.
    /// </summary>
    public bool IsValid(string guessedLetters)
    {
        if (ApplyEasterEggCheat(guessedLetters))
            return true;
		
		HashSet<char> Uniqueness = new HashSet<char>();
		
		/*foreach(char c in guessedLetters){
			if(!Uniqueness.Add(c)){
				return false;
			}
		}*/
        for (int i = 0; i < guessedLetters.Length; i++)
        {
            char c = guessedLetters[i];
            if (!Uniqueness.Add(c))
                return false;
        }
        return guessedLetters.Length is <= 10 and >= 10 &&
               guessedLetters.All(char.IsLetter);
    }
}