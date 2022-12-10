using FluentAssertions;
using Xunit;

namespace Day_10.Tests
{
    public class ProcessInstructionsTests
    {
        [Fact]
        public void Test1()
        {
            var expected = 13140;

            var input = File.ReadAllLines("input.test.txt");

            var (actual, _) = new Computer().Compute(input);

            actual.Should().Be(expected);
        }

        [Fact]
        public void TestMain()
        {
            var expected = 14540;

            var input = File.ReadAllLines("input.txt");

            var (actual, _) = new Computer().Compute(input);

            actual.Should().Be(expected);
        }

        [Fact]
        public void Test2()
        {
            var expected = "##..##..##..##..##..##..##..##..##..##..\r\n###...###...###...###...###...###...###.\r\n####....####....####....####....####....\r\n#####.....#####.....#####.....#####.....\r\n######......######......######......####\r\n#######.......#######.......#######.....";

            var input = File.ReadAllLines("input.test.txt");

            var (_, crtOutput) = new Computer().Compute(input);

            crtOutput.Should().Be(expected);
        }

        [Fact]
        public void TestMain2()
        {
            var expected = "####.#..#.####.####.####.#..#..##..####.\r\n#....#..#....#.#.......#.#..#.#..#....#.\r\n###..####...#..###....#..####.#......#..\r\n#....#..#..#...#.....#...#..#.#.....#...\r\n#....#..#.#....#....#....#..#.#..#.#....\r\n####.#..#.####.#....####.#..#..##..####.";

            var input = File.ReadAllLines("input.txt");

            var (_, crtOutput) = new Computer().Compute(input);

            crtOutput.Should().Be(expected);
        }
    }
}