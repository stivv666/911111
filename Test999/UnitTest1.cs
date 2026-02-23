using NUnit.Framework;
using lab_1_toliik_nooolik;

namespace TestHangman
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            // Метод підготовки перед кожним тестом
        }

        // 1. ТЕСТ: Перевірка правильної ініціалізації гри
        [Test]
        public void Constructor_ShouldInitializeCorrectly()
        {
            // Arrange
            string word = "Test";
            int attempts = 5;

            // Act
            var game = new HangmanGame(word, attempts);

            // Assert
            Assert.AreEqual(attempts, game.AttemptsLeft);
            Assert.AreEqual("____", game.GetProgressString()); // 4 букви = 4 підкреслення
        }

        // 2. ТЕСТ: Логіка - Правильна літера
        [Test]
        public void MakeGuess_ValidLetter_ShouldRevealLetter()
        {
            // Arrange
            var game = new HangmanGame("Cat", 5);

            // Act
            bool result = game.MakeGuess('a');

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("_a_", game.GetProgressString());
            Assert.AreEqual(5, game.AttemptsLeft); // Спроби не знялись
        }

        // 3. ТЕСТ: Логіка - Неправильна літера (Edge-case/Помилка)
        [Test]
        public void MakeGuess_InvalidLetter_ShouldDecreaseAttempts()
        {
            // Arrange
            var game = new HangmanGame("Cat", 5);

            // Act
            bool result = game.MakeGuess('z');

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(4, game.AttemptsLeft); // Знялась 1 спроба
        }

        // 4. ТЕСТ: Edge-Case - Регістр літер (Великі/Малі)
        [Test]
        public void MakeGuess_UpperCaseLetter_ShouldWorkAsLowerCase()
        {
            // Arrange
            var game = new HangmanGame("Cat", 5);

            // Act
            bool result = game.MakeGuess('C'); // Вводимо велику 'C'

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("c__", game.GetProgressString()); // Відкрилась маленька 'c'
        }

        // 5. ТЕСТ: Стан гри - Перемога
        [Test]
        public void IsWon_WhenAllLettersGuessed_ShouldReturnTrue()
        {
            // Arrange
            var game = new HangmanGame("Hi", 3);

            // Act
            game.MakeGuess('h');
            game.MakeGuess('i');

            // Assert
            Assert.IsTrue(game.IsWon());
            Assert.IsTrue(game.IsGameOver());
        }

        // 6. ТЕСТ: Стан гри - Поразка (закінчилися спроби)
        [Test]
        public void IsGameOver_WhenAttemptsZero_ShouldReturnTrue()
        {
            // Arrange
            var game = new HangmanGame("Hi", 1);

            // Act
            game.MakeGuess('z'); // Робимо помилку

            // Assert
            Assert.AreEqual(0, game.AttemptsLeft);
            Assert.IsTrue(game.IsGameOver());
        }

        // 7. ІНТЕГРАЦІЙНИЙ ТЕСТ 1: Сценарій кількох ходів
        [Test]
        public void GameScenario_PlaySeveralRounds_CheckState()
        {
            // Arrange (Створив)
            var game = new HangmanGame("Banana", 3);

            // Act (Кілька дій)
            game.MakeGuess('a'); // Вгадали (відкриє одразу 3 букви 'a')
            game.MakeGuess('x'); // Помилились

            // Assert (Отримав підсумок)
            Assert.AreEqual("_a_a_a", game.GetProgressString());
            Assert.AreEqual(2, game.AttemptsLeft);
            Assert.IsFalse(game.IsGameOver());
        }

        // 8. ІНТЕГРАЦІЙНИЙ ТЕСТ 2: Сценарій повної поразки
        [Test]
        public void GameScenario_LoseGame_CheckZeroAttempts()
        {
            // Arrange (Створив)
            var game = new HangmanGame("Dog", 2);

            // Act (Дії до кінця)
            game.MakeGuess('z'); // Залишилась 1 спроба
            game.MakeGuess('x'); // Залишилось 0 спроб

            // Assert (Отримав підсумок)
            Assert.AreEqual(0, game.AttemptsLeft);
            Assert.IsTrue(game.IsGameOver());
            Assert.IsFalse(game.IsWon());
        }
    }
}