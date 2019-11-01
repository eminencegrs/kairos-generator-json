namespace Kairos.Generator.Json

open System
open System.Linq
open Newtonsoft.Json.Schema
open Newtonsoft.Json.Linq
open Newtonsoft.Json
open Kairos.Generator.Randomizer
open Kairos.Generator.Json.Exceptions

// TODO: consider refactoring
module SchemaProcessor =

    let createStringValue () = JValue(RandomString.getGuidString())
    
    let createNumberValue () = JValue(RandomInt.getAny())
    
    let createIntegerValue () = JValue(RandomInt.getAny())
    
    let createBooleanValue () = JValue(RandomBoolean.get())

    let createValue (typeName : JSchemaType) (schema : JSchema) =
        match typeName with
        | JSchemaType.None -> failwith "Could not process 'JSchemaType.None'"
        | JSchemaType.String -> createStringValue()
        | JSchemaType.Number -> createNumberValue()
        | JSchemaType.Integer -> createIntegerValue()
        | JSchemaType.Boolean -> createBooleanValue()
        | JSchemaType.Object -> failwith "This operation is not supported for 'JSchemaType.Object'"
        | JSchemaType.Array -> failwith "This operation is not supported for 'JSchemaType.Array'" // TODO: should it be implemented?
        | JSchemaType.Null -> failwith "Could not process 'JSchemaType.Null'"
        | _ -> String.Format("Could not recognize '%s' type", typeName) |> failwith

    let rec createObject (key : string) (schema : JSchema) =
        createNode key schema.Type.Value schema

    and createStringProperty (key : string) (schema : JSchema) =
        JProperty(key, JValue(RandomString.getGuidString()))

    and createNumberProperty (key : string) (schema : JSchema) =
        JProperty(key, JValue(RandomInt.getAny()))

    and createIntegerProperty (key : string) (schema : JSchema) =
        JProperty(key, JValue(RandomInt.getAny()))

    and createBooleanProperty (key : string) (schema : JSchema) =
        JProperty(key, JValue(RandomBoolean.get()))

    and createNode
        (key : string)
        (typeName : JSchemaType)
        (schema : JSchema)
        (config : SchemaProcessorConfig) =
        match typeName with
        | JSchemaType.None -> failwith "Could not process 'JSchemaType.None'"
        | JSchemaType.String -> createStringProperty key schema
        | JSchemaType.Number -> createNumberProperty key schema
        | JSchemaType.Integer -> createIntegerProperty key schema
        | JSchemaType.Boolean -> createBooleanProperty key schema
        | JSchemaType.Object -> 
            let arr = [| for property in schema.Properties do createNode property.Key property.Value.Type.Value property.Value config |]
            let newObj = JObject(arr)
            JProperty(key, newObj)
        | JSchemaType.Array ->            
            let item = schema.Items.FirstOrDefault()
            match item.Type.Value with
            | JSchemaType.Object ->
                let mutable generatedItems : JArray = new JArray()
                for i in [1..(int)config.ItemsNumber] do
                    let result = JArray([| for item in schema.Items do NestedObjectProcessor.``process`` item config createNode |])
                    generatedItems.Merge(result)
                JProperty(key, generatedItems)
            | JSchemaType.Array ->
                let mutable generatedItems : JArray = new JArray()
                for i in [1..(int)config.ItemsNumber] do
                    let result = JArray([| for item in schema.Items do NestedItemProcessor.``process`` item config createValue createNode |])
                    generatedItems.Merge(result)
                JProperty(key, generatedItems)
            | JSchemaType.String 
            | JSchemaType.Number
            | JSchemaType.Integer
            | JSchemaType.Boolean ->
                let mutable generatedItems : JArray = new JArray()
                for i in [1..(int)config.ItemsNumber] do
                    let result = JArray([| for item in schema.Items do createValue item.Type.Value item |])
                    generatedItems.Merge(result)
                JProperty(key, generatedItems)
            | _ -> String.Format("Could not recognize '%s' type", item.Type.Value) |> failwith
        | JSchemaType.Null -> failwith "Could not process 'JSchemaType.Null'"
        | _ -> String.Format("Could not recognize '%s' type", typeName) |> failwith

    let createRoot (schema : JSchema) (config : SchemaProcessorConfig) =
        if (not schema.Type.HasValue) then
            raise (InvalidJsonSchemaTypeError "Invalid JSON schema provided. Schema type is null.")

        let typeName = schema.Type.Value
        match typeName with
        | JSchemaType.None -> failwith "Could not process 'JSchemaType.None'"

        | JSchemaType.String -> createStringValue() |> fun x -> JsonConvert.SerializeObject(x, Formatting.Indented)

        | JSchemaType.Number -> createNumberValue() |> fun x -> JsonConvert.SerializeObject(x, Formatting.Indented)

        | JSchemaType.Integer -> createIntegerValue() |> fun x -> JsonConvert.SerializeObject(x, Formatting.Indented)

        | JSchemaType.Boolean -> createBooleanValue() |> fun x -> JsonConvert.SerializeObject(x, Formatting.Indented)

        | JSchemaType.Object -> 
            let arr = [| for property in schema.Properties do createNode property.Key property.Value.Type.Value property.Value config |]
            let newObj = JObject(arr)
            JsonConvert.SerializeObject(newObj, Formatting.Indented)

        | JSchemaType.Array ->
            let mutable resultItems : JArray = new JArray()
            for item in schema.Items do
                let itemType = item.Type.Value
                match itemType with
                | JSchemaType.Object ->
                    let mutable generatedItems : JArray = new JArray()
                    for i in [1..(int)config.ItemsNumber] do
                        let createdItem = NestedObjectProcessor.``process`` item config createNode
                        generatedItems.Add(createdItem)
                    resultItems.Merge(generatedItems)
                | JSchemaType.Array ->
                    let mutable generatedItems : JArray = new JArray()
                    for i in [1..(int)config.ItemsNumber] do
                        let createdItem = NestedItemProcessor.``process`` item config createValue createNode
                        generatedItems.Add(createdItem)
                    resultItems.Merge(generatedItems)
                | JSchemaType.String ->
                    let mutable generatedItems : JArray = new JArray()
                    for i in [1..(int)config.ItemsNumber] do
                        let createdItem = createStringValue()
                        generatedItems.Add(createdItem)
                    resultItems.Merge(generatedItems)
                | JSchemaType.Number
                | JSchemaType.Integer
                | JSchemaType.Boolean
                | JSchemaType.Null -> failwith "Could not process 'JSchemaType.Null'"
                | _ -> String.Format("Could not recognize '%s' type", typeName) |> failwith
            JsonConvert.SerializeObject(resultItems, Formatting.Indented)
        | JSchemaType.Null -> failwith "Could not process 'JSchemaType.Null'"

        | _ -> String.Format("Could not recognize '%s' type", typeName) |> failwith
