using System.Text;


class Game : Cheatable
{
    // Make your changes in this file
    private string phrase;

    public Game(string phrase) : Cheatable
    {
        this.phrase = phrase;
        if(ApplyTimeCheat(System.DateTime.Now, 2)){
            Console.WriteLine("Time cheat activated!");
        }
        if(ApplyEasterEggCheat("CHEAT")){
            Console.WriteLine("Easter Egg cheat activated!");
        }
    }

    public string DisplayBlanks()
    {
        var display = new StringBuilder();
        foreach(char c in phrase)        {
            display.Append(char.IsLetter(c) ? "_ " : c + "");
        }
        return display.ToString();
    }

    public string Play(char[] guessedLetters){
        HashSet<char> guessedSet = new HashSet<char>(guessedLetters.Select(char.ToLower));

        string result = "";

        foreach(char c in phrase)        {
            if(char.IsLetter(c)){
                if(guessedSet.Contains(char.ToLower(c))){
                    result += c + " ";
                } else {
                    result += "_ ";
                }
            } else {
                result += c + " ";
            }
        }
        return result;
    }

    public bool IsValid(string guessedLetters){
        if(guessedLetters.Length > 10 || guessedLetters.Length < 10 || !guessedLetters.All(char.IsLetter)){
            return false;
        }
        return true;
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
