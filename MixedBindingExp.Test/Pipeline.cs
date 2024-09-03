using LanguageExt;

namespace MixedBindingExp;

public class Pipeline
{
    public Either<int, double> RightWhenPositive(int leftValue)
    {
        var e1 = Either<int, Unit>.Left(leftValue);
        Either<int, double> e2 =
            e1.Match(
                Right: _ => Either<int, double>.Right(0),
                Left: EvaluateLeft
                    );

        return e2;
    }



    private static Either<int, double> EvaluateLeft(int i)
    {
        return i > 0
            ? Either<int, double>.Right(i * 2)
            : Either<int, double>.Left(i - 1);
    }


}

