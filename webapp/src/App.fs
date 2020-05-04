module App

// MODEL

type Todo = { text: string; isDone: bool }

let todos =
  [ { text = "read one book"
      isDone = true }
    { text = "read another book"
      isDone = false } ]

type Model =
  | FetchSuccess of Todo list
  | FetchError of string
  | Loading

type Msg = ToggleDone of Todo

let init (): Model = FetchSuccess todos

// UPDATE

let update (msg: Msg) (model: Model) =
  match msg, model with
  | ToggleDone t, FetchSuccess todos ->
      todos
      |> List.map (fun x -> if x.text = t.text then { x with isDone = not x.isDone } else x)
      |> FetchSuccess
  | _ -> model

open Fable.React
open Fable.React.Props

// VIEW

let viewTodo dispatch (t: Todo): ReactElement =
  div
    [ Class "todo-item"
      Style [ if t.isDone then Color "silver" else Color "black" ] ]
    [ p [ Class "todo-text" ] [ str t.text ]
      p [ Class "todo-status" ] [ str ("isDone: " + if t.isDone then "true" else "false") ]
      button
        [ Class "todo-toggle"
          OnClick(fun _ -> dispatch <| ToggleDone t) ] [ str "toggle" ] ]

let view model dispatch =
  match model with
  | FetchSuccess todos -> div [] [ div [] (List.map (viewTodo dispatch) todos) ]
  | FetchError e -> str e
  | Loading -> str "still loading"
