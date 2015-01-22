module DiscrimUnions

open System

type Command = 
    | RegisterUser of id : Guid * email : string * password : string
    | ConfirmEmail of token : string
    | SendMessage of senderId : Guid * recipientId : Guid * text : string

let execute = 
    function 
    | RegisterUser(id, email, password) -> printfn "Registering user %s with e-mail %s" email password
    | ConfirmEmail(token) -> printfn "Confirming e-mail with token %s" token
    | SendMessage(senderId, recipientId, text) -> 
        printfn "%A -> %A: %s" senderId recipientId text

let user1 = Guid.NewGuid()
let user2 = Guid.NewGuid()

execute (RegisterUser(user1, "a@b.pl", "p1"))
execute (RegisterUser(user2, "c@d.pl", "p2"))
//
execute (ConfirmEmail("token1"))
execute (ConfirmEmail("token2"))
//
execute (SendMessage(user1, user2, "Hi there!"))
execute (SendMessage(user2, user1, "Hi"))
execute (SendMessage(user1, user2, "Going for beer?"))
