namespace Kairos.Generator.Randomizer

open System

module RandomString = 

    let getGuidString () = Guid.NewGuid().ToString()

    let getAlphabeticString (range : Range) (upperCase : UpperCase) =
        let length =
            match range with
            | Default -> (System.Random()).Next(10, 20)
            | Between (min, max) -> (System.Random()).Next(int(min), int(max))

        let chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        let random = new Random()
        match upperCase with
        | None -> 
            seq { for i in {0..length} do yield chars.[random.Next(chars.Length)] }
            |> Seq.toArray
            |> (fun x -> new String(x))
            |> (fun str -> str.ToLower())
        | First -> 
            seq { for i in {0..length} do yield chars.[random.Next(chars.Length)] } 
            |> Seq.mapi (fun i c -> match i with | 0 -> Char.ToUpper(c) | _ -> Char.ToLower(c))
            |> Seq.toArray 
            |> (fun x -> new String(x))
        | All -> 
            seq { for i in {0..length} do yield chars.[random.Next(chars.Length)] }
            |> Seq.toArray
            |> (fun x -> new String(x))
        | Random -> 
            seq { for i in {0..length} do yield chars.[random.Next(chars.Length)] }
            |> Seq.map (fun c -> match RandomBoolean.get() with | true -> Char.ToUpper(c) | false -> Char.ToLower(c))
            |> Seq.toArray
            |> (fun x -> new String(x))

    let getAlphabeticStringInUpperCase (range : Range) =
        getAlphabeticString range UpperCase.All

    let getAlphabeticStringInLowerCase (range : Range) =
        getAlphabeticString range UpperCase.None