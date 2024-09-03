using FluentAssertions;
using LanguageExt;

namespace MixedBindingExp.Tests;
public class PipelineTestsExtractMightFail
{
    private readonly PipelineMighFail _sut = new();

    [Fact]
    public async Task WhenLeftIsPositiveUpToTen_ShouldReturn_Right_WithDoubledValue()
    {
        int value = 6;
        Either<string, PipelineMighFailPayload> actual = await _sut.RightWhenPositive_ExtractionMightFail(value);
        actual.IsRight.Should().BeTrue();
        actual.Match(
            d => d.Should().BeEquivalentTo(new PipelineMighFailPayload(value * 2)),
            _ => false.Should().BeTrue());
    }

    [Fact]
    public async Task WhenLeftIsGreaterThanTen_ShouldReturn_Left_With_ExtractionFailed()
    {
        int value = 16;
        Either<string, PipelineMighFailPayload> actual = await _sut.RightWhenPositive_ExtractionMightFail(value);
        actual.IsLeft.Should().BeTrue();
        actual.Match(
            Right: _ => false.Should().BeTrue(),
            Left: d => d.Should().Be("ExtractionFailed")
            );
    }

    [Fact]
    public async Task ShouldReturnLeft_WhenNegative_With_ErrorMessage_NegativeValue()
    {
        int value = -3;
        Either<string, PipelineMighFailPayload> actual = await _sut.RightWhenPositive_ExtractionMightFail(value);
        actual.IsLeft.Should().BeTrue();
        actual.Match(
            Right: _ => false.Should().BeTrue(),
            Left: d => d.Should().Be("NegativeValue")
        );
    }

}