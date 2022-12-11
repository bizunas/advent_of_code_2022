using FluentAssertions;

namespace Day_11.Tests
{
    public class KeepAwayGameTests
    {
        [Fact]
        public void TestExample1()
        {
            var expected = 10605;

            var input = File.ReadAllText("input.test.txt");

            var actual = new KeepAwayGame(input, x => x / 3).Play(20);

            actual.Should().Be(expected);
        }

        [Fact]
        public void TestMain1()
        {
            var expected = 57348;

            var input = File.ReadAllText("input.txt");

            var actual = new KeepAwayGame(input, x => x / 3).Play(20);

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(1, 2, 4, 3, 6)]
        [InlineData(20, 99, 97, 8, 103)]
        [InlineData(1000, 5204, 4792, 199, 5192)]
        [InlineData(2000, 10419, 9577, 392, 10391)]
        [InlineData(3000, 15638, 14358, 587, 15593)]
        [InlineData(4000, 20858, 19138, 780, 20797)]
        [InlineData(5000, 26075, 23921, 974, 26000)]
        [InlineData(6000, 31294, 28702, 1165, 31204)]
        [InlineData(7000, 36508, 33488, 1360, 36400)]
        [InlineData(8000, 41728, 38268, 1553, 41606)]
        [InlineData(9000, 46945, 43051, 1746, 46807)]
        [InlineData(10000, 52166, 47830, 1938, 52013)]
        public void TestExample2_After1Round(int rounds, int firstExpected, int secondExpected, int thirdExpected, int fourthExpected)
        {
            var expected = new List<long> { firstExpected, secondExpected, thirdExpected, fourthExpected };

            var input = File.ReadAllText("input.test.txt");

            var actual = new KeepAwayGame(input, x => x).PlayRaw(rounds);

            actual.Should().Equal(expected);
        }

        [Fact]
        public void TestMain2_After10000Rounds_Raw()
        {
            var expected = new List<long> { 116421, 23734, 112859, 106904, 57020, 109357, 121166, 22573 };

            var input = File.ReadAllText("input.txt");

            var actual = new KeepAwayGame(input, x => x).PlayRaw(10000);

            actual.Should().Equal(expected);
        }

        [Fact]
        public void TestMain2_After100000Round_Answer()
        {
            long expected = 14106266886;

            var input = File.ReadAllText("input.txt");

            var actual = new KeepAwayGame(input, x => x).Play(10000);

            actual.Should().Be(expected);
        }
    }
}