namespace Kairos.Generator.Randomizer

module RandomNegativeInt =

    let get min =
        if (min >= 0)
        then failwith "'min' cannot be more than or equal to '0'"
        else RandomInt.getAnyFromTo min -1