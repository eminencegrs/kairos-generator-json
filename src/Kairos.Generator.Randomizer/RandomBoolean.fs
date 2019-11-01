namespace Kairos.Generator.Randomizer

module RandomBoolean =

    let get () = (System.Random()).Next(0, 2).Equals(1)
