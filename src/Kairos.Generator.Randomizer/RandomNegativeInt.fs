namespace Kairos.Generator.Randomizer

module RandomNegativeInt =

    let get min =
        if (min >= 0)
        then invalidArg "min" "'min' cannot be more than or equal to '0'"
        else RandomInt.getAnyFromTo min -1