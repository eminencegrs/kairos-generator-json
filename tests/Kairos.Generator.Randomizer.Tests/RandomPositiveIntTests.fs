namespace Kairos.Generator.Randomizer.Tests

open Xunit
open FluentAssertions
open Kairos.Generator.Randomizer

module RandomPositiveIntTests =
    open System

    [<Fact>]
    let ``Getting_Random_Positive_Int_Beetween_0_and_100`` () =
        let maxLimit = 1000
        let actualResult = RandomPositiveInt.get maxLimit
        actualResult.Should().BeOfType(typeof<int>, "because it's expected to be 'int'") |> ignore
        actualResult.Should().BeInRange(0, 1000, "because it's expected to be beetween '0' and '1000'") |> ignore

    [<Fact>]
    let ``Getting_Random_Positive_Int_Equals_0`` () =
        let actualResult = RandomPositiveInt.get 0
        actualResult.Should().BeOfType(typeof<int>, "because it's expected to be 'int'") |> ignore
        actualResult.Should().Be(0, "because it's expected to be '0'") |> ignore

    [<Fact>]
    let ``Should_throw_ArgumentException_when_max_is_negative`` () =
        let sut = (fun () -> RandomPositiveInt.get -1 |> ignore)
        Assert.Throws<ArgumentException>(sut)