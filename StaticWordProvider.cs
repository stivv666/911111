using System;

public class StaticWordProvider : IWordProvider
{

    private string[] words = new string[100];


    private string[] hints = new string[100];

    private int _wordCount = 0;
    private int _currentIndex = 0;
    private Random _random = new Random();

    public StaticWordProvider()
    {

        words[0] = "алгоритм"; hints[0] = "Набір інструкцій для вирішення задачі";
        words[1] = "інтерфейс"; hints[1] = "Спільна межа між двома системами або класами";
        words[2] = "процесор"; hints[2] = "Головний мозок комп'ютера";
        words[3] = "компілятор"; hints[3] = "Перекладає код у машинну мову";
        words[4] = "програміст"; hints[4] = "Людина, яка перетворює каву на код";
        words[5] = "спадкування"; hints[5] = "Принцип ООП (передача властивостей від батьківського класу)";

        _wordCount = 6;
    }

    public string GetWord()
    {

        _currentIndex = _random.Next(0, _wordCount);
        return words[_currentIndex];
    }

    public string GetHint()
    {

        return hints[_currentIndex];
    }
}