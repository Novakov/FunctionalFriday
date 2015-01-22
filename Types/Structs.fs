module Structs
open System

type Vector = 
    struct
        val DeltaX : float
        val DeltaY : float

        member this.Length 
            with get() = Math.Sqrt(this.DeltaX * this.DeltaX + this.DeltaY + this.DeltaY)

        new(dx : float, dy : float) = 
            { DeltaX = dx
              DeltaY = dy }
    end

type Point = 
    struct
        val X : float
        val Y : float
        new(x : float, y : float) = 
            { X = x
              Y = y }

        member this.Move(by:Vector) = 
            Point(this.X + by.DeltaX, this.Y + by.DeltaY)

        override this.ToString() = 
            sprintf "(%f, %f)" this.X this.Y
    end

let a = Point(1.0, 2.0)

printfn "A%A" a

let vec = Vector(-2.0, 5.0)

printfn "Vector length = %f" vec.Length

let a' = a.Move(vec)

printfn "A'%A" a'