using FluentAssertions;

namespace MixedBindingExp.Tests;
public class PipelineTests
{
    private readonly Pipeline _sut = new Pipeline();

    [Fact]
    public void ShouldReturnRightWhenPositive_WithDoubledValue()
    {
        int value = 6;
        LanguageExt.Either<int, double> actual = _sut.RightWhenPositive(value);
        actual.IsRight.Should().BeTrue();
        actual.Match(
            d => d.Should().Be(value * 2),
            _ => false.Should().BeTrue());
    }

    [Fact]
    public void ShouldReturnLeft_WhenNegative_WithValueMinsOne()
    {
        int value = -3;
        LanguageExt.Either<int, double> actual = _sut.RightWhenPositive(value);
        actual.IsLeft.Should().BeTrue();
        actual.Match(
            _ => false.Should().BeTrue(),
            d => d.Should().Be(value - 1)
        );
    }

}