module Tests

// more examples: https://github.com/Zaid-Ajaj/Fable.Mocha/blob/master/tests/Tests.fs

#if FABLE_COMPILER
open Fable.Mocha
#else
open Expecto
#endif

let demoTests =
    testList "Demo tests" [
        testCase "addition works" <| fun () ->
            Expect.equal (1 + 1) 2 "plus"
    ]

let allTests = testList "All" [
    demoTests
    App.Test.tests
]

[<EntryPoint>]
let main args =
#if FABLE_COMPILER
    Mocha.runTests allTests
#else
    runTestsWithArgs defaultConfig args allTests
#endif