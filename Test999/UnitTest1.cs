// Подключаем библиотеку NUnit, которая предоставляет атрибуты (такие как [Test]) 
// и методы для проверок (например, Assert.AreEqual) для юнит-тестирования.
using NUnit.Framework;

using lab_1_toliik_nooolik;

namespace TestHangman
{
    // Класс, содержащий набор тестов для игры "Виселица" (Hangman)
    public class Tests
    {
        // Атрибут [SetUp] указывает, что этот метод будет запускаться автоматически 
        // ПЕРЕД каждым отдельным тестом. Здесь обычно подготавливают общие данные,
        // но в данном случае он пуст.
        [SetUp]
        public void Setup()
        {
            // Место для инициализации общих данных перед каждым тестом
        }

        // --- Тест 1: Проверка правильности инициализации игры в конструкторе ---
        [Test] // Атрибут, указывающий NUnit, что этот метод является тест-кейсом
        public void Constructor_ShouldInitializeCorrectly()
        {
            // Arrange (Подготовка): Задаем исходные данные для теста
            string word = "Test"; // Загаданное слово
            int attempts = 5;     // Количество попыток

            // Act (Действие): Создаем новый экземпляр игры с нашими данными
            var game = new HangmanGame(word, attempts);

            // Assert (Проверка): Убеждаемся, что игра создалась с правильными параметрами
            // Проверяем, что количество оставшихся попыток равно заданному (5)
            Assert.AreEqual(attempts, game.AttemptsLeft);
            // Проверяем, что слово из 4 букв зашифровано 4-мя подчеркиваниями
            Assert.AreEqual("____", game.GetProgressString()); 
        }

        // --- Тест 2: Проверка логики при угадывании ПРАВИЛЬНОЙ буквы ---
        [Test]
        public void MakeGuess_ValidLetter_ShouldRevealLetter()
        {
            // Arrange: Создаем игру со словом "Cat" и 5 попытками
            var game = new HangmanGame("Cat", 5);

            // Act: Пытаемся угадать правильную букву 'a'
            bool result = game.MakeGuess('a');

            // Assert: Проверяем ожидаемый результат
            // Проверяем, что метод вернул true (буква угадана)
            Assert.IsTrue(result);
            // Проверяем, что буква 'a' открылась, а остальные остались скрыты ("_a_")
            Assert.AreEqual("_a_", game.GetProgressString());
            // Проверяем, что количество попыток НЕ уменьшилось, так как буква верная
            Assert.AreEqual(5, game.AttemptsLeft); 
        }

        // --- Тест 3: Проверка логики при угадывании НЕПРАВИЛЬНОЙ буквы ---
        [Test]
        public void MakeGuess_InvalidLetter_ShouldDecreaseAttempts()
        {
            // Arrange: Создаем игру со словом "Cat" и 5 попытками
            var game = new HangmanGame("Cat", 5);

            // Act: Пытаемся угадать букву 'z', которой нет в слове
            bool result = game.MakeGuess('z');

            // Assert: Проверяем реакцию игры на ошибку
            // Проверяем, что метод вернул false (буква не угадана)
            Assert.IsFalse(result);
            // Проверяем, что количество попыток уменьшилось на 1 (стало 4)
            Assert.AreEqual(4, game.AttemptsLeft); 
        }

        // --- Тест 4: Проверка нечувствительности к регистру (большие/маленькие буквы) ---
        [Test]
        public void MakeGuess_UpperCaseLetter_ShouldWorkAsLowerCase()
        {
            // Arrange: Создаем игру со словом "Cat"
            var game = new HangmanGame("Cat", 5);

            // Act: Вводим заглавную 'C'
            bool result = game.MakeGuess('C'); 

            // Assert: Проверяем, что игра правильно обработала регистр
            // Проверяем, что буква засчитана как угаданная
            Assert.IsTrue(result);
            // Проверяем, что открылась первая буква, причем в нижнем регистре (зависит от логики игры)
            Assert.AreEqual("c__", game.GetProgressString()); 
        }

        // --- Тест 5: Проверка условия победы в игре ---
        [Test]
        public void IsWon_WhenAllLettersGuessed_ShouldReturnTrue()
        {
            // Arrange: Создаем короткое слово "Hi" с 3 попытками
            var game = new HangmanGame("Hi", 3);

            // Act: Угадываем все буквы по очереди
            game.MakeGuess('h');
            game.MakeGuess('i');

            // Assert: Проверяем статус игры
            // Метод IsWon() должен вернуть true, так как слово отгадано
            Assert.IsTrue(game.IsWon());
            // Метод IsGameOver() также должен вернуть true (игра окончена)
            Assert.IsTrue(game.IsGameOver());
        }

        // --- Тест 6: Проверка условия проигрыша (окончание попыток) ---
        [Test]
        public void IsGameOver_WhenAttemptsZero_ShouldReturnTrue()
        {
            // Arrange: Создаем игру со словом "Hi" и всего 1 попыткой на ошибку
            var game = new HangmanGame("Hi", 1);

            // Act: Делаем одну неверную попытку
            game.MakeGuess('z'); 

            // Assert: Проверяем завершение игры из-за исчерпания попыток
            // Попыток должно стать 0
            Assert.AreEqual(0, game.AttemptsLeft);
            // Игра должна считаться оконченной
            Assert.IsTrue(game.IsGameOver());
        }

        // --- Тест 7: Комплексный сценарий (несколько ходов, победы/проигрыша еще нет) ---
        [Test]
        public void GameScenario_PlaySeveralRounds_CheckState()
        {
            // Arrange: Создаем игру со словом "Banana" и 3 попытками
            var game = new HangmanGame("Banana", 3);

            // Act: Делаем верный и неверный ходы
            game.MakeGuess('a'); // Верно: откроются сразу три буквы 'a'
            game.MakeGuess('x'); // Неверно: отнимется одна попытка

            // Assert: Проверяем промежуточное состояние игры
            // Строка прогресса должна показать открытые 'a'
            Assert.AreEqual("_a_a_a", game.GetProgressString());
            // Должно остаться 2 попытки (3 - 1 неверная)
            Assert.AreEqual(2, game.AttemptsLeft);
            // Игра еще НЕ окончена
            Assert.IsFalse(game.IsGameOver());
        }

        // --- Тест 8: Комплексный сценарий (проигрыш) ---
        [Test]
        public void GameScenario_LoseGame_CheckZeroAttempts()
        {
            // Arrange: Создаем игру "Dog" с 2 попытками
            var game = new HangmanGame("Dog", 2);

            // Act: Специально делаем две ошибки
            game.MakeGuess('z'); // Остается 1 попытка
            game.MakeGuess('x'); // Остается 0 попыток

            // Assert: Проверяем состояние проигрыша
            // Попытки должны быть на нуле
            Assert.AreEqual(0, game.AttemptsLeft);
            // Игра должна быть завершена
            Assert.IsTrue(game.IsGameOver());
            // Игрок НЕ должен считаться победителем
            Assert.IsFalse(game.IsWon());
        }
    }
}