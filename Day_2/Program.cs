var input = File.ReadAllText("input.txt");

var lines = input.Split("\r\n");
var rawCombinations = lines.Select(line => line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
    .Select(x => new { Opponent = LeftShape(x[0]), My = RightShape(x[1]), Outcome = RightOutcome(LeftShape(x[0]), x[1]) });

var combinationsPart1 = rawCombinations
    .Select(x => OutcomeScore(x.Opponent, x.My) + ShapeScore(x.My));

var combinationsPart2 = rawCombinations
    .Select(x => OutcomePart2Score(x.Outcome) + ShapePart2Score(x.Opponent, x.Outcome));

Console.WriteLine(combinationsPart1.Sum());
Console.WriteLine(combinationsPart2.Sum());


static int OutcomeScore(Shape opponentShape, Shape myShape)
{
    return (opponentShape, myShape) switch
    {
        (Shape.Rock, Shape.Scissors) or (Shape.Paper, Shape.Rock) or (Shape.Scissors, Shape.Paper) => 0,
        (Shape.Rock, Shape.Rock) or (Shape.Paper, Shape.Paper) or (Shape.Scissors, Shape.Scissors) => 3,
        (Shape.Scissors, Shape.Rock) or (Shape.Rock, Shape.Paper) or (Shape.Paper, Shape.Scissors) => 6,
        _ => throw new NotImplementedException(nameof(OutcomeScore))
    };
}

static int OutcomePart2Score(Outcome outcome)
{
    return outcome switch
    {
        Outcome.Loose => 0,
        Outcome.Draw => 3,
        Outcome.Win => 6,
        _ => throw new NotImplementedException(nameof(OutcomeScore))
    };
}

static int ShapeScore(Shape myShape)
{
    return myShape switch
    {
        Shape.Rock => 1,
        Shape.Paper => 2,
        Shape.Scissors => 3,
        _ => throw new NotImplementedException(nameof(ShapeScore)),
    };
}

static int ShapePart2Score(Shape opponentShape, Outcome outcome)
{
    return (opponentShape, outcome) switch
    {
        (Shape.Rock, Outcome.Draw) or (Shape.Paper, Outcome.Loose) or (Shape.Scissors, Outcome.Win) => 1,
        (Shape.Rock, Outcome.Win) or (Shape.Paper, Outcome.Draw) or (Shape.Scissors, Outcome.Loose) => 2,
        (Shape.Scissors, Outcome.Draw) or (Shape.Rock, Outcome.Loose) or (Shape.Paper, Outcome.Win) => 3,
        _ => throw new NotImplementedException(nameof(OutcomeScore))
    };
}

static Shape LeftShape(string obj)
{
    return obj switch
    {
        "A" => Shape.Rock,
        "B" => Shape.Paper,
        "C" => Shape.Scissors,
        _ => throw new NotImplementedException()
    };
}

static Shape RightShape(string obj)
{
    return obj switch
    {
        "X" => Shape.Rock,
        "Y" => Shape.Paper,
        "Z" => Shape.Scissors,
        _ => throw new NotImplementedException()
    };
}

static Outcome RightOutcome(Shape opponentShape, string obj)
{
    return obj switch
    {
        "X" => Outcome.Loose,
        "Y" => Outcome.Draw,
        "Z" => Outcome.Win,
        _ => throw new NotImplementedException()
    };
}


public enum Shape
{
    Rock,
    Paper,
    Scissors
}

public enum Outcome
{
    Win,
    Loose, 
    Draw
}