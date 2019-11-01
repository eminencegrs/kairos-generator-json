namespace Kairos.Generator.Randomizer.Tests

open Xunit
open FluentAssertions
open Kairos.Generator.Randomizer

module RandomIntTests =

    [<Fact>]
    let ``Getting_Random_Non-negative_Int`` () =
        let actualResult = RandomInt.getAny()
        actualResult.Should().BeOfType(typeof<int>, "because it's expected to be 'int'") |> ignore
        actualResult.Should().BeGreaterOrEqualTo(0, "because it's expected to be greater or equal to '0'") |> ignore

    [<Fact>]
    let ``Getting_Random_Int_Beetween_minus10_and_10`` () =
        let actualResult = RandomInt.getAnyFromTo -10 10
        actualResult.Should().BeOfType(typeof<int>, "because it's expected to be 'int'") |> ignore
        actualResult.Should().BeInRange(-10, 10, "because it's expected to be beetween '-10' and '10'") |> ignore

    [<Fact>]
    let ``Getting_Random_Positive_Int_Beetween_0_and_10`` () =
        let actualResult = RandomInt.getAnyFromTo 0 10
        actualResult.Should().BeOfType(typeof<int>, "because it's expected to be 'int'") |> ignore
        actualResult.Should().BeInRange(0, 10, "because it's expected to be beetween '0' and '10'") |> ignore

    [<Fact>]
    let ``Getting_Random_Negative_Int_Beetween_minus100_and_minus1`` () =
        let actualResult = RandomInt.getAnyFromTo -100 -1
        actualResult.Should().BeOfType(typeof<int>, "because it's expected to be 'int'") |> ignore
        actualResult.Should().BeInRange(-100, -1, "because it's expected to be beetween '-100' and '-1'") |> ignore

