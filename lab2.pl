:- encoding(utf8).

% Проверяет, оканчивается ли число на заданную цифру.
ends_with(Number, Digit) :-
    Abs is abs(Number),
    Last is Abs mod 10,
    Last =:= Digit.

% Рекурсивно суммирует элементы списка, оканчивающиеся на заданную цифру.
sum_ending([], _, 0).
sum_ending([H|T], Digit, Sum) :-
    sum_ending(T, Digit, SumTail),
    ( ends_with(H, Digit) ->
         Sum is SumTail + H
    ;
         Sum is SumTail
    ).

% Интерфейс
main :-
    nl,
    write('Здравствуйте! Программа суммирует элементы списка, которые оканчиваются на заданную цифру.'), nl,
    write('Введите цифру от 0 до 9, на которую должен заканчиваться элемент: '),
    read(Digit),
    ( integer(Digit), Digit >= 0, Digit =< 9 ->
         true
    ;
         write('Ошибка: введите корректную цифру (0-9).'), nl,
         main
    ),
    write('Введите список чисел (например, [12, 25, 37, 40]): '),
    read(List),
    ( is_list(List) ->
         sum_ending(List, Digit, Sum),
         format('Сумма элементов, оканчивающихся на ~w, равна ~w.~n', [Digit, Sum])
    ;
         write('Ошибка: необходимо ввести список чисел в квадратных скобках.'), nl,
         main
    ),
    write('Хотите выполнить повторный расчёт? (да/нет): '),
    read(Answer),
    ( (Answer == да ; Answer == 'да') ->
         main
    ;
         write('До свидания!'), nl, halt
    ).

:- initialization(main).