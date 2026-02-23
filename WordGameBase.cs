using System;


public abstract class WordGameBase
{
    public int AttemptsLeft { get; protected set; }

    public abstract bool MakeGuess(char letter);
    public abstract bool IsGameOver();
}