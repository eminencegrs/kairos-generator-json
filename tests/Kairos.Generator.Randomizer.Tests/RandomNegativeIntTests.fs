namespace Kairos.Generator.Randomizer.Tests

open System
open Xunit
open FluentAssertions
open Kairos.Generator.Randomizer

module RandomNegativeIntTests =

    [<Fact>]
    let ``Getting_Random_Negative_Int_Beetween_minus1000_and_minus1`` () =
        let minLimit = -1000
        let actualResult = RandomNegativeInt.get minLimit
        actualResult.Should().BeOfType(typeof<int>, "because it's expected to be 'int'") |> ignore
        actualResult.Should().BeInRange(-1000, -1, "because it's expected to be beetween '-1000' and '-1'") |> ignore

    [<Fact>]
    let ``Getting_Random_Negative_Int_Equals_minus1`` () =
        let actualResult = RandomNegativeInt.get -1
        actualResult.Should().BeOfType(typeof<int>, "because it's expected to be 'int'") |> ignore
        actualResult.Should().Be(-1, "because it's expected to be '-1'") |> ignore

    [<Fact>]
    let ``Should_throw_ArgumentException_when_min_is_positive`` () =
        let sut = (fun () -> RandomNegativeInt.get 0 |> ignore)
        Assert.Throws<ArgumentException>(sut)
