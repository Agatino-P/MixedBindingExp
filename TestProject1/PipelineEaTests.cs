using FluentAssertions;
using LanguageExt;

namespace MixedBindingExp.Tests;
public class PipelineEaTests
{
    private readonly PipelineEA _sut = new();

    [Fact]
    public async Task ShouldReturnRightWhenPositive_WithDoubledValueAsync()
    {
        int value = 6;
        Either<int, double> actual = await _sut.RightWhenPositiveTaskEither(value);
        actual.IsRight.Should().BeTrue();
        actual.Match(
            d => d.Should().Be(value * 2),
            _ => false.Should().BeTrue());
    }

    [Fact]
    public async Task ShouldReturnLeft_WhenNegative_WithValueMinsOneAsync()
    {
        int value = -3;
        Either<int, double> actual = await _sut.RightWhenPositiveTaskEither(value);
        actual.IsLeft.Should().BeTrue();
        actual.Match(
            _ => false.Should().BeTrue(),
            d => d.Should().Be(value - 1)
        );
    }

}