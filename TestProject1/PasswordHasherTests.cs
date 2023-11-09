namespace TestProject1
{
    public class PasswordHasherTests
    {
        [Theory]
        [InlineData("MyTestPassword")]
        [InlineData("")]
        [InlineData("ThisIsAVeryLongPasswordToEnsureThatTheHashingMechanismCanHandleLargeInputsProperly")]
        [InlineData("���ꕶ��#%&")]
        public void InputChecker_ValidatesInputsCorrectly(string password)
        {
            // Arrange - �����ŕK�v�ȏ�Ԃ�ݒ肵�܂��B

            // Act - �e�X�g���s��������������s���܂��B
            // ���̏ꍇ�A��̕�����Ⓑ�����镶����ŗ�O���������邱�Ƃ��m�F���邱�Ƃ��ړI�ł��B
            Action act = () => PasswordHasher.InputChecker(password);

            // Assert - ���҂���錋�ʂ��A�T�[�g���܂��B
            // ��̕�����̏ꍇ�͗�O����������ׂ��ł��B
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
            Assert.Equal("password", exception.ParamName); // ArgumentNullException��ParamName��"password"�ł��邱�Ƃ��m�F
        }

        [Fact]
        public void HashPassword_WithExtremelyLongInput_DoesNotThrowException()
        {
            // Arrange
            var longPassword = new string('a', 10000); // ���ɒ���������

            // Act & Assert
            var exceptionRecorded = Record.Exception(() => PasswordHasher.HashPassword(longPassword));
            Assert.Null(exceptionRecorded); // ��O���������Ȃ����Ƃ��m�F
        }
    }
}