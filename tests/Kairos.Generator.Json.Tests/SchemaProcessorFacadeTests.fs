namespace Kairos.Generator.Json.Tests

open AutoFixture
open FluentAssertions
open Newtonsoft.Json.Schema
open Newtonsoft.Json.Linq
open Newtonsoft.Json
open Xunit
open Kairos.Generator.Json
open Kairos.Generator.Json.Exceptions

module SchemaProcessorFacadeTests = 
    
    // TODO: these tests must be improved once SchemaProcessor and SchemaProcessorFacade are refactored.

    [<Fact>]
    let ``Should successfully generate JSON data`` () =
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

        let testSchema = JSchema.Parse(testData);
        
        let fixture = new Fixture()
        let testConfig = fixture.Create<SchemaProcessorConfig>()

        let generatedData = SchemaProcessorFacade.``process`` testSchema testConfig

        let actualResult = JObject.Parse(generatedData)

        actualResult.IsValid(testSchema).Should().BeTrue("because generated JSON data based on valid 'testSchema' ") |> ignore

    [<Fact>]
    let ``Should throw GeneratingJsonDataError when generating JSON data`` () =
        // TODO: it must be written when SchemaProcessor is completely done.
        Assert.True(true)

