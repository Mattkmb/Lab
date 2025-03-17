open System

// Глобальный генератор случайных чисел
let rnd = System.Random()

// Вспомогательная функция для проверки ввода положительного целого числа
let rec readInt z =
    printf "%s" z
    match System.Int32.TryParse(System.Console.ReadLine()) with
    | (true, n) when n > 0 -> n
    | _ ->
        printfn "Ошибка: введите положительное целое число!"
        readInt z

// ================================
// Задача 1: Работа с деревом строк
// ================================

// Определение типа дерева для строк
type Tree =
    | Leaf of string
    | Node of string * Tree list

// Функция для добавления символа в начало строки
let rec addSymbolToTree symbol tree =
    match tree with
    | Leaf value -> Leaf (symbol + value)
    | Node (value, children) -> Node (symbol + value, List.map (addSymbolToTree symbol) children)

// Функция для вывода дерева (для строк)
let rec printTree level tree =
    let indent = String.replicate (level * 2) " "
    match tree with
    | Leaf value ->
        printfn "%s└── %s" indent value
    | Node (value, children) ->
        printfn "%s├── %s" indent value
        children |> List.iter (printTree (level + 1))

// Функция ввода символа с проверкой (должен быть один символ)
let rec getSymbol () =
    printf "Введите символ для добавления в начало строк: "
    let input = System.Console.ReadLine()
    match input with
    | "" ->
         printfn "Ошибка: символ не введен!"
         getSymbol()
    | s when s.Length > 1 ->
         printfn "Ошибка: введите только один символ!"
         getSymbol()
    | s -> s

// Функция для создания дерева строк вручную
let rec createTree () =
    printf "Введите значение для узла (строку): "
    let value = System.Console.ReadLine()
    if value = "" then
         printfn "Ошибка: значение не может быть пустым!"
         createTree()
    else
         printf "Добавить дочерние узлы? (Y/N): "
         match System.Console.ReadLine() with
         | "y" ->
             printf "Сколько дочерних узлов добавить? "
             match System.Int32.TryParse(System.Console.ReadLine()) with
             | (true, num) when num > 0 ->
                 let children = [ for _ in 1..num -> createTree() ]
                 Node(value, children)
             | _ ->
                 printfn "Ошибка: введите положительное число!"
                 createTree()
         | "n" -> Leaf value
         | _ ->
             printfn "Ошибка: введите 'Y' или 'N'!"
             createTree()

// Функция для создания случайного дерева строк (из прикрепленного файла)
let rec createRandomTree (maxDepth: int) (maxChildren: int) =
    let value = string (rnd.Next(100)) 
    if maxDepth <= 1 then
         Leaf value
    else
         // Случайное число дочерних узлов от 0 до maxChildren
         let childrenCount = rnd.Next(maxChildren + 1)
         if childrenCount = 0 then
             Leaf value
         else
             let children = [ for _ in 1..childrenCount -> createRandomTree (maxDepth - 1) maxChildren ]
             Node(value, children)

// =================================================================
// Задача 2: Сравнение суммы четных и нечетных чисел в дереве (fold)
// =================================================================

// Определение типа дерева для целых чисел
type IntTree =
    | ILeaf of int
    | INode of int * IntTree list

// Функция для создания дерева целых чисел вручную
let rec createIntTree () =
    printf "Введите значение для узла (целое число): "
    let input = System.Console.ReadLine()
    match System.Int32.TryParse(input) with
    | (true, number) ->
         printf "Добавить дочерние узлы? (Y/N): "
         match System.Console.ReadLine() with
         | "y" ->
             printf "Сколько дочерних узлов добавить? "
             match System.Int32.TryParse(System.Console.ReadLine()) with
             | (true, num) when num > 0 ->
                 let children = [ for _ in 1..num -> createIntTree() ]
                 INode(number, children)
             | _ ->
                 printfn "Ошибка: введите положительное целое число!"
                 createIntTree()
         | "n" -> ILeaf number
         | _ ->
             printfn "Ошибка: введите 'Y' или 'N'!"
             createIntTree()
    | _ ->
         printfn "Ошибка: введите корректное целое число!"
         createIntTree()

// Функция для создания случайного дерева целых чисел (из прикрепленного файла)
let rec createRandomIntTree (maxDepth: int) (maxChildren: int) =
    let value = rnd.Next(100)
    if maxDepth <= 1 then
         ILeaf value
    else
         let childrenCount = rnd.Next(maxChildren + 1)
         if childrenCount = 0 then
             ILeaf value
         else
             let children = [ for _ in 1..childrenCount -> createRandomIntTree (maxDepth - 1) maxChildren ]
             INode(value, children)

// Функция для вывода дерева целых чисел
let rec printIntTree level tree =
    let indent = String.replicate (level * 2) " "
    match tree with
    | ILeaf value ->
         printfn "%s└── %d" indent value
    | INode (value, children) ->
         printfn "%s├── %d" indent value
         children |> List.iter (printIntTree (level + 1))

// Функция fold для дерева целых чисел
let rec foldIntTree f acc tree =
    match tree with
    | ILeaf v -> f acc v
    | INode (v, children) ->
         let acc' = f acc v
         List.fold (fun a child -> foldIntTree f a child) acc' children

// Функция для подсчёта суммы четных и нечетных чисел в дереве
let sumEvenOdd tree =
    foldIntTree (fun (evenSum, oddSum) n ->
         if n % 2 = 0 then (evenSum + n, oddSum)
         else (evenSum, oddSum + n)
         ) (0, 0) tree

// ============================
// Меню и выбор задачи
// ============================

// Главное меню
let rec mainLoop () =
    printfn "\n=== Главное меню ==="
    printfn "1. Добавление символа в начало строк в дереве"
    printfn "2. Сравнение суммы четных и нечетных чисел в дереве"
    printfn "E. Выход"
    printf "Введите номер задачи (или 'E' для выхода): "
    let input = System.Console.ReadLine()
    match input with
    | "1" -> runTask1()
    | "2" -> runTask2()
    | "e" -> ()   
    | _ ->
        printfn "Ошибка: выберите корректный вариант."
        mainLoop()

// Задача 1: Добавление символа в начало строк
and runTask1 () =
    printfn "\n=== Задача: Добавление символа в начало строк ==="
    printfn "Выберите способ заполнения дерева строк:"
    printfn "1. Вручную"
    printfn "2. Рандомно"
    printfn "B. Назад"
    printfn "E. Выход"
    printf "Ваш выбор: "
    let methodChoice = System.Console.ReadLine()
    match methodChoice with
    | "1" ->
        let originalTree = createTree()
        let symbol = getSymbol()
        let modifiedTree = addSymbolToTree symbol originalTree
        printfn "\nИсходное дерево:"
        printTree 0 originalTree
        printfn "\nМодифицированное дерево:"
        printTree 0 modifiedTree
        mainLoop()
    | "2" ->
        let depth = readInt "Введите максимальную глубину дерева: "
        let maxChildren = readInt "Введите максимальное количество дочерних узлов: "
        let originalTree = createRandomTree depth maxChildren
        let symbol = getSymbol()
        let modifiedTree = addSymbolToTree symbol originalTree
        printfn "\nИсходное дерево:"
        printTree 0 originalTree
        printfn "\nМодифицированное дерево:"
        printTree 0 modifiedTree
        mainLoop()
    | "b" -> mainLoop()
    | "e" -> ()
    | _ ->
        printfn "Неверный выбор."
        runTask1()

// Задача 2: Сравнение суммы четных и нечетных чисел в дереве
and runTask2 () =
    printfn "\n=== Задача: Сравнение суммы четных и нечетных чисел в дереве ==="
    printfn "Выберите способ заполнения дерева чисел:"
    printfn "1. Вручную"
    printfn "2. Рандомно"
    printfn "B. Назад"
    printfn "E. Выход"
    printf "Ваш выбор: "
    let methodChoice = System.Console.ReadLine().Trim().ToLower()
    match methodChoice with
    | "1" ->
        let intTree = createIntTree()
        printfn "\nДерево чисел:"
        printIntTree 0 intTree
        let (evenSum, oddSum) = sumEvenOdd intTree
        printfn "\nСумма четных чисел: %d" evenSum
        printfn "Сумма нечетных чисел: %d" oddSum
        if evenSum > oddSum then
            printfn "\nСумма четных чисел больше суммы нечетных чисел."
        elif oddSum > evenSum then
            printfn "\nСумма нечетных чисел больше суммы четных чисел."
        else
            printfn "\nСуммы четных и нечетных чисел равны."
        mainLoop()
    | "2" ->
        let depth = readInt "Введите максимальную глубину дерева: "
        let maxChildren = readInt "Введите максимальное количество дочерних узлов: "
        let intTree = createRandomIntTree depth maxChildren
        printfn "\nДерево чисел:"
        printIntTree 0 intTree
        let (evenSum, oddSum) = sumEvenOdd intTree
        printfn "\nСумма четных чисел: %d" evenSum
        printfn "Сумма нечетных чисел: %d" oddSum
        if evenSum > oddSum then
            printfn "\nСумма четных чисел больше суммы нечетных чисел."
        elif oddSum > evenSum then
            printfn "\nСумма нечетных чисел больше суммы четных чисел."
        else
            printfn "\nСуммы четных и нечетных чисел равны."
        mainLoop()
    | "b" -> mainLoop()
    | "e" -> ()
    | _ ->
        printfn "Неверный выбор."
        runTask2()

// Точка входа программы
[<EntryPoint>]
let main argv =
    mainLoop()
    0