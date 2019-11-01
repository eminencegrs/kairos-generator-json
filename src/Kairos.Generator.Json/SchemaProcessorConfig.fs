namespace Kairos.Generator.Json

type SchemaProcessorConfig(depth : byte, itemsNumber : byte) =
    member this.Depth = depth
    member this.ItemsNumber = itemsNumber
