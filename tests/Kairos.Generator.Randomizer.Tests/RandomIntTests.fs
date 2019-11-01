namespace Kairos.Generator.Randomizer.Tests

open Xunit
open Kairos.Generator.Randomizer

module RandomIntTests =

    [<Fact>]
    let ``Getting_Random_Int_Beetween_minus10_and_10`` () =
        let actualResult = RandomInt.getAnyFromTo -10 10
        Assert.NotNull(actualResult)
        Assert.IsAssignableFrom<int>(actualResult) |> ignore
        Assert.InRange<int>(actualResult, -10, 10)

    [<Fact>]
    let ``Getting_Random_Positive_Int_Beetween_0_and_10`` () =
        let actualResult = RandomInt.getAnyFromTo 0 10
        Assert.NotNull(actualResult)
        Assert.IsAssignableFrom<int>(actualResult) |> ignore
        Assert.InRange<int>(actualResult, 0, 10)

    [<Fact>]
    let ``Getting_Random_Negative_Int_Beetween_minus100_and_minus1`` () =
        let actualResult = RandomInt.getAnyFromTo -100 -1
        Assert.NotNull(actualResult)
        Assert.IsAssignableFrom<int>(actualResult) |> ignore
        Assert.InRange<int>(actualResult, -100, -1)
