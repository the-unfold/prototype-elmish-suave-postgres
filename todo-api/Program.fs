module Program

open System
open System.Threading
open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Chiron
open Todo

let todos =
  [ { text = "read one book"
      isDone = true }
    { text = "read another book"
      isDone = false } ]

let todosWebpart =
  (todos
   |> Json.serialize
   |> Json.format
   |> OK)

let app = choose [ GET >=> choose [ path "/todos" >=> todosWebpart ] ]

[<EntryPoint>]
let main _ =
  let wait = new ManualResetEventSlim()
  let cts = new CancellationTokenSource()

  let shutdownHandler _ =
    printfn "Shutting down"
    cts.Cancel() // завершаем сервер
    wait.Set() // резолвим ожидание события завершения
    printfn "Done"

  AppDomain.CurrentDomain.ProcessExit.Add(shutdownHandler)
  AppDomain.CurrentDomain.DomainUnload.Add(shutdownHandler)
  Console.CancelKeyPress.Add(shutdownHandler)

  let conf =
    { defaultConfig with
        cancellationToken = cts.Token
        bindings =
          [ { scheme = HTTP
              socketBinding = Sockets.SocketBinding.create Net.IPAddress.Any 8080us } ] }

  let listening, server = startWebServerAsync conf app
  Async.Start(server, cts.Token)

  wait.Wait() // начинаем ждать завершение
  0 // возвращаем exit code
