using LanguageExt;
using static LanguageExt.Prelude;

namespace MixedBindingExp;

public class PipelineMighFail
{
    public async Task<Either<string, PipelineMighFailPayload>> RightWhenPositive_ExtractionMightFail(int keyToRetrieveData)
    {
        EitherAsync<string, PipelineMighFailPayload> EitherAsync =
            RetrieveData_MightFail(keyToRetrieveData)
            .BiBind(
                Left: retrieveFailureMessage => PipelineFailure(retrieveFailureMessage),
                Right: ProcessRetrievedData
                    );

        return await EitherAsync;
    }

    private EitherAsync<string, PipelineMighFailPayload> ProcessRetrievedData(double retrievedData)
    {
        EitherAsync<string, PipelineMighFailPayload> eitherAsync = RetrievedDataMeansFailure(retrievedData)
            ? PipelineFailure("NegativeValue")
            : PipelineSuccess(retrievedData * 2);
        return eitherAsync;
    }


    private static EitherAsync<string, double> RetrieveData_MightFail(int keyToRetrieveData)
    {
        var eitherAsync= ExternalServiceCall(keyToRetrieveData).ToAsync();
        return eitherAsync;
    }

    private static async Task<Either<string, double>> ExternalServiceCall(int keyToRetrieveData)
    {
        await Task.CompletedTask;

        Either<string, double> either = keyToRetrieveData > 10
            ? Either<string, double>.Left("ExtractionFailed")
            : Either<string, double>.Right(keyToRetrieveData);

        return either;

    }

    private static bool RetrievedDataMeansFailure(double retrievedData) =>
        retrievedData <0;

    static EitherAsync<string, PipelineMighFailPayload> PipelineSuccess(double value) =>
        EitherAsync<string, PipelineMighFailPayload>.Right(new PipelineMighFailPayload(value));
    static EitherAsync<string, PipelineMighFailPayload> PipelineFailure(string message) =>
        EitherAsync<string, PipelineMighFailPayload>.Left(message);
}