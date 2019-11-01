namespace Kairos.Generator.Randomizer

module TryParser =

    let tryParseWith (tryParseFunc: string -> bool * _) = tryParseFunc >> function
        | true, v    -> Some v
        | false, _   -> None

    let parseDate   = tryParseWith System.DateTime.TryParse

    let parseInt    = tryParseWith System.Int32.TryParse

    let parseSingle = tryParseWith System.Single.TryParse

    let parseDouble = tryParseWith System.Double.TryParse

    let parseByte = tryParseWith System.Byte.TryParse

    let (|Date|_|)   = parseDate

    let (|Int|_|)    = parseInt

    let (|Single|_|) = parseSingle

    let (|Double|_|) = parseDouble

    let (|Byte|_|) = parseByte
