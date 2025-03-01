open System

// Функция для ввода целого числа 
let rec getIntFromConsole z =
    printf "%s" z
    let s = Console.ReadLine()
    match Int32.TryParse s with
    | (true, value) -> value
    | (false, _) ->
        Console.ForegroundColor <- ConsoleColor.Red
        printfn "Ошибка: введите корректное целое число"
        Console.ResetColor()
        getIntFromConsole z

// Функция для ввода вещественного числа 
let rec getFloatFromConsole z =
    printf "%s" z
    let s = Console.ReadLine()
    match Double.TryParse s with
    | (true, value) -> value
    | (false, _) ->
        Console.ForegroundColor <- ConsoleColor.Red
        printfn "Ошибка: введите корректное вещественное число"
        Console.ResetColor()
        getFloatFromConsole z

// Ручной ввод целых чисел
let rec readIntNumbers spisok =
    printf "Введите целое число (или пустую строку для завершения): "
    let input = Console.ReadLine()
    if String.IsNullOrWhiteSpace(input) then 
        List.rev spisok
    else 
        match Int32.TryParse input with
        | (true, number) -> readIntNumbers (number :: spisok)
        | _ ->
            printfn "Некорректный ввод. Попробуйте снова."
            readIntNumbers spisok

// Ручной ввод вещественных чисел
let rec readFloatNumbers spisok =
    printf "Введите вещественное число (или пустую строку для завершения): "
    let input = Console.ReadLine()
    if String.IsNullOrWhiteSpace(input) then 
        List.rev spisok
    else 
        match Double.TryParse input with
        | (true, number) -> readFloatNumbers (number :: spisok)
        | _ ->
            printfn "Некорректный ввод. Попробуйте снова."
            readFloatNumbers spisok

// Генерация списка случайных целых чисел
let randomIntList count minVal maxVal =
    let rnd = Random()
    List.init count (fun _ -> rnd.Next(minVal, maxVal + 1))

// Генерация списка случайных вещественных чисел
let randomFloatList count minVal maxVal =
    let rnd = Random()
    List.init count (fun _ -> rnd.NextDouble() * (maxVal - minVal) + minVal)

// Для целых чисел: функция, возвращающая список последних цифр чисел
let lastDigitsInt numbers =
    numbers |> List.map (fun n -> abs(n) % 10)

// Для вещественных чисел: вычисляем последнюю цифру целой части
let lastDigitsFloat numbers =
    numbers |> List.map (fun n -> int (abs n) % 10)

// Для целых чисел: суммирует те элементы, которые оканчиваются на заданную цифру
let sumByLastDigitInt digit numbers =
    numbers |> List.fold (fun spisok x -> if x % 10 = digit then spisok + x else spisok) 0

// Для вещественных чисел: суммирует элементы, у которых последняя цифра целой части равна заданной
let sumByLastDigitFloat digit numbers =
    numbers |> List.fold (fun spisok x -> if int (abs x) % 10 = digit then spisok + x else spisok) 0.0

// Меню выбора способа заполнения списка для целых чисел.
let rec menuIntList inputMode =
    match inputMode with
    | "ручной" ->
        let numbers = readIntNumbers []
        numbers
    | "рандомный" ->
        let count = getIntFromConsole "Введите количество элементов списка: "
        let minVal = getIntFromConsole "Введите минимальное значение: "
        let maxVal = getIntFromConsole "Введите максимальное значение: "
        randomIntList count minVal maxVal
    | _ -> 
        []  

// Меню выбора способа заполнения списка для вещественных чисел
let rec menuFloatList inputMode =
    match inputMode with
    | "ручной" ->
        let numbers = readFloatNumbers []
        numbers
    | "рандомный" ->
        let count = getIntFromConsole "Введите количество элементов списка: "
        let minVal = getFloatFromConsole "Введите минимальное значение: "
        let maxVal = getFloatFromConsole "Введите максимальное значение: "
        randomFloatList count minVal maxVal
    | _ ->
        []

// Подменю выбора типа списка для задачи 1 (Список последних цифр чисел)
let rec menuTask1 () =
    printfn "\n=== Задача 1: Список последних цифр чисел ==="
    printfn "Выберите тип списка:"
    printfn "1. Целые числа"
    printfn "2. Вещественные числа"
    printfn "3. Назад в главное меню"
    let typeChoice = getIntFromConsole "Ваш выбор: "
    match typeChoice with
    | 1 ->
        // Выбор способа заполнения для целых чисел.
        printfn "\nВыберите способ заполнения списка целых чисел:"
        printfn "1. Ручной ввод"
        printfn "2. Рандомное заполнение"
        printfn "3. Назад"
        let methodChoice = getIntFromConsole "Ваш выбор: "
        let numbers =
            match methodChoice with
            | 1 -> menuIntList "ручной"
            | 2 -> menuIntList "рандомный"
            | _ -> []
        if numbers = [] then () 
        else
            printfn "Полученный список: %A" numbers
            printfn "Последние цифры чисел: %A" (lastDigitsInt numbers)
        menuTask1 ()
    | 2 ->
        // Выбор способа заполнения для вещественных чисел
        printfn "\nВыберите способ заполнения списка вещественных чисел:"
        printfn "1. Ручной ввод"
        printfn "2. Рандомное заполнение"
        printfn "3. Назад"
        let methodChoice = getIntFromConsole "Ваш выбор: "
        let numbers =
            match methodChoice with
            | 1 -> menuFloatList "ручной"
            | 2 -> menuFloatList "рандомный"
            | _ -> []
        if numbers = [] then () 
        else
            printfn "Полученный список: %A" numbers
            printfn "Последние цифры чисел (по целой части): %A" (lastDigitsFloat numbers)
        menuTask1 ()
    | 3 ->
        ()  
    | _ ->
        printfn "Некорректный выбор."
        menuTask1 ()

// Подменю выбора типа списка для задачи 2 
let rec menuTask2 () =
    printfn "\n--- Задача 2: Сумма элементов списка, оканчивающихся на заданную цифру ---"
    printfn "Выберите тип списка:"
    printfn "1. Целые числа"
    printfn "2. Вещественные числа"
    printfn "3. Назад в главное меню"
    let typeChoice = getIntFromConsole "Ваш выбор: "
    match typeChoice with
    | 1 ->
        printfn "\nСпособ заполнения списка целых чисел:"
        printfn "1. Ручной ввод"
        printfn "2. Рандомное заполнение"
        printfn "3. Назад"
        let methodChoice = getIntFromConsole "Ваш выбор: "
        let numbers =
            match methodChoice with
            | 1 -> menuIntList "ручной"
            | 2 -> menuIntList "рандомный"
            | _ -> []
        if numbers = [] then () 
        else
            printfn "Полученный список: %A" numbers
            let digit = getIntFromConsole "Введите цифру (0-9) для фильтрации: "
            if digit < 0 || digit > 9 then
                printfn "Некорректная цифра."
            else
                let sum = sumByLastDigitInt digit numbers
                printfn "Сумма элементов, оканчивающихся на %d: %d" digit sum
        menuTask2 ()
    | 2 ->
        printfn "\nСпособ заполнения списка вещественных чисел:"
        printfn "1. Ручной ввод"
        printfn "2. Рандомное заполнение"
        printfn "3. Назад"
        let methodChoice = getIntFromConsole "Ваш выбор: "
        let numbers =
            match methodChoice with
            | 1 -> menuFloatList "ручной"
            | 2 -> menuFloatList "рандомный"
            | _ -> []
        if numbers = [] then () 
        else
            printfn "Полученный список: %A" numbers
            let digit = getIntFromConsole "Введите цифру (0-9) для фильтрации: "
            if digit < 0 || digit > 9 then
                printfn "Некорректная цифра."
            else
                let sum = sumByLastDigitFloat digit numbers
                printfn "Сумма элементов, оканчивающихся на %d: %f" digit sum
        menuTask2 ()
    | 3 ->
        ()  
    | _ ->
        printfn "Некорректный выбор."
        menuTask2 ()

// Главное меню программы.
let rec mainMenu () =
    printfn "\n=== Главное меню ==="
    printfn "Выберите задачу:"
    printfn "1. Список последних цифр чисел"
    printfn "2. Сумма элементов, оканчивающихся на заданную цифру"
    printfn "3. Выход"
    let choice = getIntFromConsole "Ваш выбор: "
    match choice with
    | 1 ->
        menuTask1 ()
        mainMenu ()
    | 2 ->
        menuTask2 ()
        mainMenu ()
    | 3 ->
        printfn "Выход из программы"
        ()
    | _ ->
        printfn "Некорректный выбор, попробуйте снова."
        mainMenu ()

[<EntryPoint>]
let main argv =
    mainMenu ()
    0