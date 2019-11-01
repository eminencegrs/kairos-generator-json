namespace Kairos.Generator.Randomizer.Tests

open System.Linq
open Xunit
open FluentAssertions
open Kairos.Generator.Randomizer

module RandomStringTests =

    [<Theory>]
    [<MemberData("testData")>]
    let ``TestGettingRandomString`` (range : Range, upperCase : UpperCase)=
        let actualResult = RandomString.getAlphabeticString range upperCase

        let actualResultArray = Seq.toArray actualResult

        actualResultArray.Count().Should().BePositive("Because a random generated string must not be empty") |> ignore

        match upperCase with
        | None -> 
            let resultInUpperCase = Seq.toArray (actualResult.ToUpper()) 
            match actualResultArray.Intersect(resultInUpperCase).Count() with
            | 0 -> Assert.True(true)
            | _ -> Assert.False(true)
        | First ->
            let resultInUpperCase = Seq.toArray (actualResult.ToUpper())
            match actualResultArray.Intersect(resultInUpperCase).Count() with
            | 1 -> Assert.True(true)
            | _ -> Assert.False(true)
        | All ->
            let resultInUpperCase = Seq.toArray (actualResult.ToUpper())
            match actualResultArray.Except(resultInUpperCase).Count() with
            | 0 -> Assert.True(true)
            | _ -> Assert.False(true)
        | Random ->
            actualResult.Should().NotBeNullOrWhiteSpace("because it's expected to get some random string.") |> ignore

    let testData : obj array seq = 
        seq { 
            yield [| Range.Default; UpperCase.None |]
            yield [| Range.Between(uint16(5), uint16(10)); UpperCase.None |]
            yield [| Range.Between(uint16(25), uint16(50)); UpperCase.None |]
            yield [| Range.Between(uint16(100), uint16(300)); UpperCase.None |]
            yield [| Range.Default; UpperCase.First |]
            yield [| Range.Between(uint16(5), uint16(10)); UpperCase.First |]
            yield [| Range.Between(uint16(25), uint16(50)); UpperCase.First |]
            yield [| Range.Between(uint16(100), uint16(300)); UpperCase.First |]
            yield [| Range.Default; UpperCase.All |]
            yield [| Range.Between(uint16(5), uint16(10)); UpperCase.All |]
            yield [| Range.Between(uint16(25), uint16(50)); UpperCase.All |]
            yield [| Range.Between(uint16(100), uint16(300)); UpperCase.All |]
            yield [| Range.Default; UpperCase.Random |]
            yield [| Range.Between(uint16(5), uint16(10)); UpperCase.Random |]
            yield [| Range.Between(uint16(25), uint16(50)); UpperCase.Random |]
            yield [| Range.Between(uint16(100), uint16(300)); UpperCase.Random |]
        }
