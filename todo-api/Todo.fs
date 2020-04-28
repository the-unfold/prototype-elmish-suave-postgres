module Todo

open Chiron

type Todo =
  { text: string
    isDone: bool }
  static member ToJson(x: Todo) =
    json {
      do! Json.write "text" x.text
      do! Json.write "isDone" x.isDone
    }
