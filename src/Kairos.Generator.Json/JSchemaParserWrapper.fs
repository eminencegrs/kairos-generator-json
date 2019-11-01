namespace Kairos.Generator.Json

open System
open Newtonsoft.Json.Schema
open Kairos.Generator.Json.Exceptions

module JSchemaParserWrapper =
    let ``process`` (schema : string) =
        try JSchema.Parse(schema)
        with | _ as ex -> raise (ParsingJsonSchemaError (String.Format("Could not properly parse provided JSON schema.\n{0}", ex.Message)))
