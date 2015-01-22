module Records

type Book = { 
    Title:string; 
    Authors:string list 
}

let panTadeusz = { Title = "Pan Tadeusz"; Authors = [ "Adam Mickiewicz" ] }   

let dziady = { panTadeusz with Title = "Dziady" }

printfn "%A" panTadeusz
printfn "%A" dziady