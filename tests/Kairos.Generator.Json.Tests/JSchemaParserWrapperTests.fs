namespace Kairos.Generator.Json.Tests

open FluentAssertions
open Newtonsoft.Json.Schema
open Newtonsoft.Json.Linq
open Newtonsoft.Json
open Xunit
open Kairos.Generator.Json
open Kairos.Generator.Json.Exceptions

module JSchemaParserWrapperTests =

    [<Fact>]
    let ``Should successfully parse JSON schema`` () =
        let testData = @"{ 
            'type': 'object',
            'properties': {
                'name': {'type':'string'},
                'numbers': {
                    'type': 'array',
                    'items': { 'type': 'integer' } 
                }
            }
        }"

        let expectedResult = JSchema.Parse(testData);
        let actualResult = JSchemaParserWrapper.``process`` testData
        actualResult.Should().BeEquivalentTo(expectedResult, "because it's expected they should be equivalent")

    [<Fact>]
    let ``Should throw ParsingJsonSchemaError when parsing JSON schema`` () =
        let testData = @"{ 'type': 'object', 'properties': }"
        let action = (fun () -> JSchemaParserWrapper.``process`` testData |> ignore)
        Assert.Throws<ParsingJsonSchemaError>(action)