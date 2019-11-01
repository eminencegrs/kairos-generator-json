namespace Kairos.Generator.Json

open System
open Newtonsoft.Json.Schema
open Kairos.Generator.Json.Exceptions

module SchemaProcessorFacade =

    let ``process`` (schema : JSchema) (config : SchemaProcessorConfig) =
        try SchemaProcessor.createRoot schema config
        with
        | InvalidJsonSchemaTypeError msg -> reraise()
        | _ as ex -> raise (GeneratingJsonDataError (String.Format("Could not properly process JSON schema to generate data.\n{0}", ex.Message)))
