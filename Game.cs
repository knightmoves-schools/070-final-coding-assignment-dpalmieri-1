using System.Text;
using System.Linq;
using System;

class Game : knightmoves.Cheatable
{
    private readonly string phrase;
    
    private const string MAGIC_WORD = "CHEAT";
    private const int DIVISIBLE_BY_VALUE = 2;

    public Game(string Phrase)
    {
        this.phrase = Phrase;
    }

    /// <summary>
    /// Returns the initial masked version of the phrase
    /// </summary>
    public string DisplayBlanks()
    {
        return new string(phrase.Select(c => char.IsLetter(c) ? '_' : c).ToArray());
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
            return string.Join(" ", phrase.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(word => string.Concat(word.Select(c => char.IsLetter(c) ? c : c))));
        }

        // Build normal reveal
        string result = new string(phrase.Select(c =>
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
        if (timeCheatActive == true || timeCheatActive == false)
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
        string[] originalWords = phrase.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return wordIndex < originalWords.Length ? originalWords[wordIndex] : "";																						;
	}
	
    // =================================================================
    // Interface Implementation: Cheatable
    // =================================================================

    public bool ApplyTimeCheat(DateTime now, int divisibleByValue)
    {
        if (divisibleByValue <= 0)
            return false;

        return now.Second % divisibleByValue == 0;
    }

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
		
		foreach(char c in guessedLetters){
			if(!Uniqueness.Add(c)){
				return false;
			}
		}
        return guessedLetters.Length is <= 100 and >= 1 &&
               guessedLetters.All(char.IsLetter);
    }
}



/*
class Game{
    // Make your changes in this file
    private string phrase;
    private const string MAGIC_WORD = "CHEAT";
    private const int DIVISIBLE_BY_VALUE = 2;

    public Game(string phrase)
    {
        this.phrase = phrase;
    }

    public string DisplayBlanks()
    {
        return new string(phrase.Select(c => char.IsLetter(c) ? '_' : c).ToArray());
    }

    public string Play(char[] guessedLetters){
        string guessedLettersStr = new string(guessedLetters);
        
        // Check for Easter Egg cheat (magic word)
        if (ApplyEasterEggCheat(guessedLettersStr))
        {
            return string.Join(" ", phrase.Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries).Select(word => string.Concat(word.Select(c => char.IsLetter(c) ? c : c))));
        }
        
        // Check for time cheat
        bool timeCheatActive = ApplyTimeCheat();
        
        string result = new string(phrase.Select(c => char.IsLetter(c) && guessedLetters.Contains(char.ToLower(c)) ? c : (char.IsWhiteSpace(c) ? c : '_')).ToArray());
        
        // Check if all letters have been guessed
        if (!result.Contains('_'))
        {
            Console.Clear();
            return "Congratulations";
        }
        
        if (timeCheatActive)
        {
            // Reveal every other word starting at the second word
            string[] words = result.Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < words.Length; i += 2)
            {
                words[i] = new string(words[i].Select(c => char.IsLetter(phrase.FirstOrDefault(pc => char.ToLower(pc) == char.ToLower(c))) ? phrase.FirstOrDefault(pc => char.ToLower(pc) == char.ToLower(c)) : c).ToArray());
            }
            return string.Join(" ", words);
        }
        
        return string.Join(" ", result.Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries).Select(word => string.Concat(word.Select(c => c == '_' ? '_' : c))));
    }
    
    public bool ApplyTimeCheat()
    {
        int currentSecond = System.DateTime.Now.Second;
        return currentSecond % DIVISIBLE_BY_VALUE == 0;
    }
    
    public bool ApplyEasterEggCheat(string guessedLetters)
    {
        return guessedLetters.ToUpper().Contains(MAGIC_WORD);
    }
        
    public bool IsValid(string guessedLetters){
        if(guessedLetters.Length > 10 || guessedLetters.Length < 10 || !guessedLetters.All(char.IsLetter)){
            return false;
        }
        return true;
    }
}
*/
