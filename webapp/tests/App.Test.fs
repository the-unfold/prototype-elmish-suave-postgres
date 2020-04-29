module App.Test

#if FABLE_COMPILER
open Fable.Mocha
#else
open Expecto
#endif

let tests =
    testList "App" [
        testCase "init" <| fun () ->
            Expect.equal (App.init()) 0 "init value"
    ]
