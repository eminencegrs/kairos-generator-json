namespace Kairos.Generator.Randomizer.Tests

open FluentAssertions
open Xunit
open Kairos.Generator.Randomizer

module RandomDoubleTests =

    [<Fact>]
    let ``Getting_Random_Double`` () =
        let actualResult = RandomDouble.getAny()
        actualResult.Should().BeInRange(0.0, 1.0, "because it's expected to get random 'float' value")
