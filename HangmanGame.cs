using System;

public class HangmanGame : WordGameBase
{
    private string _targetWord;


    private char[] progress = new char[20];
    private int _targetLength;

    public HangmanGame(string word, int maxAttempts)
    {
        _targetWord = word.ToLower();
        AttemptsLeft = maxAttempts;
        _targetLength = _targetWord.Length;

        for (int i = 0; i < 20; i++)
        {
            if (i < _targetLength)
                progress[i] = '_';
            else
                progress[i] = '\0';
        }
    }

    public override bool MakeGuess(char letter)
    {
        letter = char.ToLower(letter);
        bool hit = false;

        for (int i = 0; i < _targetLength; i++)
        {
            if (_targetWord[i] == letter && progress[i] == '_')
            {
                progress[i] = letter;
                hit = true;
            }
        }

        if (!hit)
        {
            AttemptsLeft--;
        }

        return hit;
    }

    public string GetProgressString()
    {
        string result = "";
        for (int i = 0; i < _targetLength; i++)
        {
            result += progress[i];
        }
        return result;
    }

    public bool IsWon()
    {
        for (int i = 0; i < _targetLength; i++)
        {
            if (progress[i] == '_') return false;
        }
        return true;
    }

    public override bool IsGameOver()
    {
        return AttemptsLeft <= 0 || IsWon();
    }

    public string GetSecretWord() => _targetWord;
}