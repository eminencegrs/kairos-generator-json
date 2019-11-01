namespace Kairos.Generator.Json

open Newtonsoft.Json.Schema
open Newtonsoft.Json.Linq

module NestedObjectProcessor =

    let ``process``
        (schema : JSchema)
        (config : SchemaProcessorConfig)
        (createProperty : string -> JSchemaType -> JSchema -> SchemaProcessorConfig -> JProperty) =
        let result = JObject()
        for property in schema.Properties do
            createProperty property.Key property.Value.Type.Value property.Value config |> 
                fun property -> result.Add(property.Name, property.Value)
        result
