namespace Kairos.Generator.Randomizer.Tests

open System
open Xunit
open FluentAssertions
open Kairos.Generator.Randomizer

module RandomBooleanTests =

    [<Fact>]
    let ``Getting_Random_True`` () =
        let mutable isContinued = true
        let mutable actualResult = false;
        let mutable attempts = 0
        while isContinued do
            actualResult <- RandomBoolean.get()
            attempts <- attempts + 1
            if (actualResult) then isContinued <- false
            elif (attempts >= 100) then isContinued <- false
        
        let becauseString = String.Format("because it's expected to get 'True' value randomly. It took {0} attempt(s).", attempts)
        actualResult.Should().BeTrue(becauseString)

    [<Fact>]
    let ``Getting_Random_False`` () =
        let mutable isContinued = true
        let mutable actualResult = true;
        let mutable attempts = 0
        while isContinued do
            actualResult <- RandomBoolean.get()
            attempts <- attempts + 1
            if (not actualResult) then isContinued <- false
            elif (attempts >= 100) then isContinued <- false
        
        let becauseString = String.Format("because it's expected to get 'False' value randomly. It took {0} attempt(s).", attempts)
        actualResult.Should().BeFalse(becauseString)