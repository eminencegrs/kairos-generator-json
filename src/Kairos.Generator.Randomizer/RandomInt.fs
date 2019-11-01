namespace Kairos.Generator.Randomizer

module RandomInt =

    let getAny () = (System.Random()).Next()

    let getAnyFromTo min max = (System.Random()).Next(min, max)
