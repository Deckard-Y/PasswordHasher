namespace TestProject1
{
    public class PasswordHasherTests
    {
        [Theory]
        [InlineData("MyTestPassword")]
        [InlineData("")]
        [InlineData("ThisIsAVeryLongPasswordToEnsureThatTheHashingMechanismCanHandleLargeInputsProperly")]
        [InlineData("特殊文字#%&")]
        public void InputChecker_ValidatesInputsCorrectly(string password)
        {
            // Arrange - ここで必要な状態を設定します。

            // Act - テストを行いたい操作を実行します。
            // この場合、空の文字列や長すぎる文字列で例外が投げられることを確認することが目的です。
            Action act = () => PasswordHasher.InputChecker(password);

            // Assert - 期待される結果をアサートします。
            // 空の文字列の場合は例外が投げられるべきです。
            if (string.IsNullOrWhiteSpace(password))
            {
                Assert.Throws<ArgumentException>(act);
            }
            else if (password.Length > 256)
            {
                Assert.Throws<ArgumentException>(act);
            }
            else
            {
                var exception = Record.Exception(act);
                Assert.Null(exception);
            }
        }

        [Fact]
        public void HashPassword_DifferentPasswords_ProduceDifferentHashes()
        {
            // Arrange
            var passwordOne = "password123";
            var passwordTwo = "password124";

            // Act
            var hashOne = PasswordHasher.HashPassword(passwordOne);
            var hashTwo = PasswordHasher.HashPassword(passwordTwo);

            // Assert
            Assert.NotEqual(hashOne, hashTwo);
        }

        [Fact]
        public void HashPassword_WithNullInput_ThrowsArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => PasswordHasher.HashPassword(null));
            Assert.Equal("password", exception.ParamName); // ArgumentNullExceptionのParamNameが"password"であることを確認
        }
    }
}