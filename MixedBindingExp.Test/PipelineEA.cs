using LanguageExt;

namespace MixedBindingExp;

public class PipelineEA
{
    public async Task<Either<int, double>> RightWhenPositiveTaskEither(int leftValue)
    {
        var e1 = await RetrieveStartingData(leftValue);

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

    private static async Task<Either<int, Unit>> RetrieveStartingData(int leftValue)
    {
        await Task.CompletedTask;
        return Either<int, Unit>.Left(leftValue);
    }



}