namespace Kairos.Generator.Json

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
type SchemaProcessorConfig(depth : byte, itemsNumber : byte) =
    member this.Depth = depth
    member this.ItemsNumber = itemsNumber
