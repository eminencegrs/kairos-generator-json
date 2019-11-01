namespace Kairos.Generator.Json

open System
open System.Linq
open Newtonsoft.Json.Schema
open Newtonsoft.Json.Linq

module NestedItemProcessor =

    let rec ``process`` 
        (schema : JSchema)
        (config : SchemaProcessorConfig)
        (createValue : JSchemaType -> JSchema -> JValue) 
        (createProperty : string -> JSchemaType -> JSchema -> SchemaProcessorConfig -> JProperty) =
            let item = schema.Items.FirstOrDefault() 
            match item.Type.Value with
            | JSchemaType.None -> failwith "Could not process 'JSchemaType.None'"
            | JSchemaType.Object ->
                let mutable generatedItems : JArray = new JArray()
                for i in [1..(int)config.ItemsNumber] do
                    let result = JArray([| for item in schema.Items do NestedObjectProcessor.``process`` item config createProperty |])
                    generatedItems.Merge(result)
                generatedItems
            | JSchemaType.Array ->
                let mutable generatedItems : JArray = new JArray()
                for i in [1..(int)config.ItemsNumber] do
                    let result = JArray([| for item in schema.Items do ``process`` item config createValue createProperty |])
                    generatedItems.Merge(result)
                generatedItems
            | JSchemaType.String
            | JSchemaType.Number
            | JSchemaType.Integer
            | JSchemaType.Boolean ->
                let mutable generatedItems : JArray = new JArray()
                for i in [1..(int)config.ItemsNumber] do
                    let result = JArray([| for item in schema.Items do createValue item.Type.Value item |])
                    generatedItems.Merge(result)
                generatedItems
            | JSchemaType.Null -> failwith "Could not process 'JSchemaType.Null'"
            | _ -> String.Format("Could not recognize '%s' type", item.Type.Value) |> failwith
