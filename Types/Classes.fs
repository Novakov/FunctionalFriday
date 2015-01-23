module Classes
open System

type Person(firstName: string, lastName: string, birthday: DateTime) = 
    let mutable  hasDriverLicense = false // private

    member this.FirstName = firstName // public
    member this.LastName = lastName
    member this.FullName = firstName + " " + lastName        
    member this.HasDriverLicense with get() = hasDriverLicense        

    member this.Age(asof: DateTime) = 
        (asof - birthday).TotalDays / 365.0

    member this.GrantDriverLicense() =
        hasDriverLicense <- true        
        ()
        
    member this.print() =
        printfn "%s is %f years old. Driver license: %s" this.FullName  (this.Age(DateTime.Now)) (if this.HasDriverLicense then "yes" else "no")

let janKowalski = Person("Jan", "Kowalski", DateTime(1980, 1, 1))

janKowalski.print()
janKowalski.GrantDriverLicense()
janKowalski.print()

