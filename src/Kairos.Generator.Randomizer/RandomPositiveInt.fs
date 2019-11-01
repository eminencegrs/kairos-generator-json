namespace Kairos.Generator.Randomizer

module RandomPositiveInt =

    let get max = 
        if (max < 0)
        then failwith "'max' cannot be less than '0'"
        else RandomInt.getAnyFromTo 0 max
