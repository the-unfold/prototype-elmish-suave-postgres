open canopy.runner.classic
open canopy.configuration
open canopy.classic
open canopy.types

chromeDir <- System.AppContext.BaseDirectory
let browser = start ChromeHeadless // use Crome instead of ChromeHeadless during local development

context "Todos"

before (fun _ ->
    printf("Starting services...\n")
    System.Diagnostics.Process.Start("docker-compose", "up -d") |> ignore
    sleep(3)
)

after (fun _ ->
    printf("Stopping services...\n")
    System.Diagnostics.Process.Start("docker-compose", "down -v") |> ignore
    sleep(3)
)

let todoParentByText = elementWithText ".todo-text" >> parent

let todoStatus = todoParentByText >> elementWithin ".todo-status" >> read

let todoToggleButton = todoParentByText >> elementWithin ".todo-toggle"

"toggle button changes todo status" &&& fun _ ->
    // Если nginx не поднялся, то в браузере будет ошибка. 
    // Браузер открывает страницу раньше, чем завершится before.
    sleep(3) 
    url "http://localhost:8080/"
    todoStatus "read one book" == "isDone: true"
    click (todoToggleButton "read one book")
    todoStatus "read one book" == "isDone: false"
    click (todoToggleButton "read one book")
    todoStatus "read one book" == "isDone: true"

run()

quit()