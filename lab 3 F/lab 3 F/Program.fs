open System
open System.IO

// Глобальный экземпляр генератора случайных чисел
let rnd = new Random()


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

// Ручной ввод целых чисел (сбор в список, затем преобразование в последовательность)
let rec readIntNumbers acc =
    printf "Введите целое число (или пустую строку для завершения): "
    let input = Console.ReadLine()
    if String.IsNullOrWhiteSpace input then 
        acc |> List.rev |> Seq.ofList
    else 
        match Int32.TryParse input with
        | (true, number) -> readIntNumbers (number :: acc)
        | _ ->
            printfn "Некорректный ввод. Попробуйте снова."
            readIntNumbers acc

// Ручной ввод вещественных чисел (сбор в список, затем преобразование в последовательность)
let rec readFloatNumbers acc =
    printf "Введите вещественное число (или пустую строку для завершения): "
    let input = Console.ReadLine()
    if String.IsNullOrWhiteSpace input then 
        acc |> List.rev |> Seq.ofList
    else 
        match Double.TryParse input with
        | (true, number) -> readFloatNumbers (number :: acc)
        | _ ->
            printfn "Некорректный ввод. Попробуйте снова."
            readFloatNumbers acc

// Генерация последовательности случайных целых чисел с кэшированием
let randomIntSeq count minVal maxVal =
    Seq.init count (fun _ -> rnd.Next(minVal, maxVal + 1))
    |> Seq.cache

// Генерация последовательности случайных вещественных чисел с кэшированием
let randomFloatSeq count minVal maxVal =
    Seq.init count (fun _ -> rnd.NextDouble() * (maxVal - minVal) + minVal)
    |> Seq.cache


// Для целых чисел: вычисление последних цифр чисел
let lastDigitsIntSeq numbers =
    numbers |> Seq.map (fun n -> abs n % 10)

// Для вещественных чисел: вычисляем последнюю цифру целой части числа
let lastDigitsFloatSeq numbers =
    numbers |> Seq.map (fun n -> int (abs n) % 10)

// Для целых чисел: суммирование элементов, оканчивающихся на заданную цифру
let sumByLastDigitIntSeq digit numbers =
    numbers |> Seq.fold (fun acc x -> if x % 10 = digit then acc + x else acc) 0

// Для вещественных чисел: суммирование элементов, у которых последняя цифра целой части равна заданной
let sumByLastDigitFloatSeq digit numbers =
    numbers |> Seq.fold (fun acc x -> if int (abs x) % 10 = digit then acc + x else acc) 0.0

// Меню выбора способа заполнения последовательности для целых чисел.
let menuIntSeq inputMode =
    match inputMode with
    | "ручной" ->
        readIntNumbers []
    | "рандомный" ->
        let count = getIntFromConsole "Введите количество элементов последовательности: "
        let minVal = getIntFromConsole "Введите минимальное значение: "
        let maxVal = getIntFromConsole "Введите максимальное значение: "
        randomIntSeq count minVal maxVal
    | _ -> 
        Seq.empty

// Меню выбора способа заполнения последовательности для вещественных чисел.
let menuFloatSeq inputMode =
    match inputMode with
    | "ручной" ->
        readFloatNumbers []
    | "рандомный" ->
        let count = getIntFromConsole "Введите количество элементов последовательности: "
        let minVal = getFloatFromConsole "Введите минимальное значение: "
        let maxVal = getFloatFromConsole "Введите максимальное значение: "
        randomFloatSeq count minVal maxVal
    | _ ->
        Seq.empty

// Подменю для задачи 1: вывод последовательности последних цифр чисел
let rec menuTask1 () =
    printfn "\n=== Задача 1: Последние цифры чисел ==="
    printfn "Выберите тип последовательности:"
    printfn "1. Целые числа"
    printfn "2. Вещественные числа"
    printfn "3. Назад в главное меню"
    let typeChoice = getIntFromConsole "Ваш выбор: "
    match typeChoice with
    | 1 ->
        // Способ заполнения для целых чисел.
        printfn "\nВыберите способ заполнения последовательности целых чисел:"
        printfn "1. Ручной ввод"
        printfn "2. Рандомное заполнение"
        printfn "3. Назад"
        let methodChoice = getIntFromConsole "Ваш выбор: "
        let numbers =
            match methodChoice with
            | 1 -> menuIntSeq "ручной"
            | 2 -> menuIntSeq "рандомный"
            | _ -> Seq.empty
        if Seq.isEmpty numbers then () 
        else
            let cachedNumbers = numbers |> Seq.cache
            printfn "Полученная последовательность: %A" (cachedNumbers |> Seq.toList)
            printfn "Последние цифры чисел: %A" (lastDigitsIntSeq cachedNumbers |> Seq.toList)
        menuTask1 ()
    | 2 ->
        // Способ заполнения для вещественных чисел.
        printfn "\nВыберите способ заполнения последовательности вещественных чисел:"
        printfn "1. Ручной ввод"
        printfn "2. Рандомное заполнение"
        printfn "3. Назад"
        let methodChoice = getIntFromConsole "Ваш выбор: "
        let numbers =
            match methodChoice with
            | 1 -> menuFloatSeq "ручной"
            | 2 -> menuFloatSeq "рандомный"
            | _ -> Seq.empty
        if Seq.isEmpty numbers then () 
        else
            let cachedNumbers = numbers |> Seq.cache
            printfn "Полученная последовательность: %A" (cachedNumbers |> Seq.toList)
            printfn "Последние цифры чисел (по целой части): %A" (lastDigitsFloatSeq cachedNumbers |> Seq.toList)
        menuTask1 ()
    | 3 ->
        ()
    | _ ->
        printfn "Некорректный выбор."
        menuTask1 ()

// Подменю для задачи 2: сумма элементов последовательности, оканчивающихся на заданную цифру
let rec menuTask2 () =
    printfn "\n--- Задача 2: Сумма элементов последовательности, оканчивающихся на заданную цифру ---"
    printfn "Выберите тип последовательности:"
    printfn "1. Целые числа"
    printfn "2. Вещественные числа"
    printfn "3. Назад в главное меню"
    let typeChoice = getIntFromConsole "Ваш выбор: "
    match typeChoice with
    | 1 ->
        printfn "\nСпособ заполнения последовательности целых чисел:"
        printfn "1. Ручной ввод"
        printfn "2. Рандомное заполнение"
        printfn "3. Назад"
        let methodChoice = getIntFromConsole "Ваш выбор: "
        let numbers =
            match methodChoice with
            | 1 -> menuIntSeq "ручной"
            | 2 -> menuIntSeq "рандомный"
            | _ -> Seq.empty
        if Seq.isEmpty numbers then () 
        else
            let cachedNumbers = numbers |> Seq.cache
            printfn "Полученная последовательность: %A" (cachedNumbers |> Seq.toList)
            let digit = getIntFromConsole "Введите цифру (0-9) для фильтрации: "
            if digit < 0 || digit > 9 then
                printfn "Некорректная цифра."
            else
                let sum = sumByLastDigitIntSeq digit cachedNumbers
                printfn "Сумма элементов, оканчивающихся на %d: %d" digit sum
        menuTask2 ()
    | 2 ->
        printfn "\nСпособ заполнения последовательности вещественных чисел:"
        printfn "1. Ручной ввод"
        printfn "2. Рандомное заполнение"
        printfn "3. Назад"
        let methodChoice = getIntFromConsole "Ваш выбор: "
        let numbers =
            match methodChoice with
            | 1 -> menuFloatSeq "ручной"
            | 2 -> menuFloatSeq "рандомный"
            | _ -> Seq.empty
        if Seq.isEmpty numbers then () 
        else
            let cachedNumbers = numbers |> Seq.cache
            printfn "Полученная последовательность: %A" (cachedNumbers |> Seq.toList)
            let digit = getIntFromConsole "Введите цифру (0-9) для фильтрации: "
            if digit < 0 || digit > 9 then
                printfn "Некорректная цифра."
            else
                let sum = sumByLastDigitFloatSeq digit cachedNumbers
                printfn "Сумма элементов, оканчивающихся на %d: %f" digit sum
        menuTask2 ()
    | 3 ->
        ()
    | _ ->
        printfn "Некорректный выбор."
        menuTask2 ()

//-------------------------------------------------------------------------------------------------------------------
// ФУНКЦИЯ ДЛЯ ОБРАБОТКИ ФАЙЛОВ 

let processNonTextFiles () =
    printf "Введите путь к каталогу: "
    let directoryPath = Console.ReadLine()
    if Directory.Exists directoryPath then
        // Получаем последовательность всех файлов со всеми подкаталогами из указанной директории
        let filesSeq = Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories)
        // Фильтруем файлы: оставляем только те, у которых расширение не равно ".txt"
        let nonTextFiles =
            filesSeq
            |> Seq.filter (fun file -> 
                String.Compare(Path.GetExtension(file), ".txt", true) <> 0)
        printfn "\nНайдены файлы, не являющиеся текстовыми:"
        if Seq.isEmpty nonTextFiles then
            printfn "Таких файлов не найдено."
        else
            nonTextFiles |> Seq.iter (printfn "%s")
    else
        printfn "Указанный каталог не существует."


// ГЛАВНОЕ МЕНЮ ПРОГРАММЫ
let rec mainMenu () =
    printfn "\n=== Главное меню ==="
    printfn "Выберите задачу:"
    printfn "1. Список последних цифр чисел"
    printfn "2. Сумма элементов, оканчивающихся на заданную цифру"
    printfn "3. Вывести пути файлов, не являющихся текстовыми"
    printfn "4. Выход"
    let choice = getIntFromConsole "Ваш выбор: "
    match choice with
    | 1 ->
        menuTask1 ()
        mainMenu ()
    | 2 ->
        menuTask2 ()
        mainMenu ()
    | 3 ->
        processNonTextFiles ()
        mainMenu ()
    | 4 ->
        printfn "Выход из программы"
    | _ ->
        printfn "Некорректный выбор, попробуйте снова."
        mainMenu ()

[<EntryPoint>]
let main argv =
    mainMenu ()
    0